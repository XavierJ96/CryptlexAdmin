using CryplexAdmin.Models;
using CryplexAdmin.Services;
using Cryptlex;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CryplexAdmin.Pages
{
    /// <summary>
    /// Interaction logic for ActivatiionForm.xaml
    /// </summary>
    public partial class ActivationForm : Page
    {

        private readonly ProductService _productService;
        private readonly LicenseService _licenseService;
        private readonly LicenseTemplateService _licenseTemplateService;

        public ActivationForm()
        {
            InitializeComponent();
            _productService = new ProductService(new HttpClientService());
            _licenseService = new LicenseService(new HttpClientService());
            _licenseTemplateService = new LicenseTemplateService(new HttpClientService());
            _ = GetLicenceInfoAsync();
        }

        public async Task GetLicenceInfoAsync()
        {
            try
            {
                List<License> licenses = await _licenseService.GetAllLicensesAsync();
                if (licenses != null)
                {
                    licenseComboBox.ItemsSource = licenses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching products: {ex.Message}");
            }
        }

        private async void licenseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (licenseComboBox.SelectedItem is License selectedLicense)
            {
                var productData = await _productService.GetProductDataAsync(selectedLicense.ProductId);

                productIdBox.Text = selectedLicense.ProductId;
                productDataBox.Text = productData;

                statusLabel.Text = "Status: License selected";

                try
                {
                    LexActivator.SetProductData(productData);
                    LexActivator.SetProductId(selectedLicense.ProductId, LexActivator.PermissionFlags.LA_USER);

                    int status = LexActivator.IsLicenseGenuine();
                    if (status == LexStatusCodes.LA_OK || status == LexStatusCodes.LA_EXPIRED || status == LexStatusCodes.LA_SUSPENDED || status == LexStatusCodes.LA_GRACE_PERIOD_OVER)
                    {
                        uint expiryDate = LexActivator.GetLicenseExpiryDate();
                        int daysLeft = (int)(expiryDate - unixTimestamp()) / 86400;
                        statusLabel.Text = "License genuinely activated! Activation Status: " + status.ToString();
                        activateBtn.Content = "Deactivate";
                        activateTrialBtn.IsEnabled = false;

                        return;
                    }
                    status = LexActivator.IsTrialGenuine();
                    if (status == LexStatusCodes.LA_OK)
                    {
                        uint trialExpiryDate = LexActivator.GetTrialExpiryDate();
                        int daysLeft = (int)(trialExpiryDate - unixTimestamp()) / 86400;
                        statusLabel.Text = "Trial period! Days left:" + daysLeft.ToString();
                        activateTrialBtn.IsEnabled = false;
                    }
                    else if (status == LexStatusCodes.LA_TRIAL_EXPIRED)
                    {
                        statusLabel.Text = "Trial has expired!";
                    }
                    else
                    {
                        statusLabel.Text = "Trial has not started or has been tampered: " + status.ToString();
                    }
                }
                catch (LexActivatorException ex)
                {
                    statusLabel.Text = "Error code: " + ex.Code.ToString() + " Error message: " + ex.Message;
                }
            }
        }

        private void activateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(licenseComboBox.Text) ||
                    string.IsNullOrWhiteSpace(productIdBox.Text) ||
                    string.IsNullOrWhiteSpace(productDataBox.Text))
                {
                    statusLabel.Text = "Please fill in all fields before activating.";
                    return;
                }

                if (activateBtn.Content.ToString() == "Deactivate")
                {
                    int status = LexActivator.DeactivateLicense();
                    if (status == LexStatusCodes.LA_OK)
                    {
                        statusLabel.Text = "License deactivated successfully";
                        activateBtn.Content = "Activate";
                        ClearInputs();
                        activateTrialBtn.IsEnabled = true;
                    }
                    else
                    {
                        statusLabel.Text = "Error deactivating license";
                    }
                }
                else
                {
                    LexActivator.SetLicenseKey(licenseComboBox.Text);
                    int status = LexActivator.ActivateLicense();
                    if (status == LexStatusCodes.LA_OK || status == LexStatusCodes.LA_EXPIRED || status == LexStatusCodes.LA_SUSPENDED)
                    {
                        statusLabel.Text = "Activation successful!";
                        activateBtn.Content = "Deactivate";
                        activateTrialBtn.IsEnabled = false;

                        ClearInputs();
                    }
                    else
                    {
                        statusLabel.Text = "Error activating the license";
                    }
                }
            }
            catch (LexActivatorException ex)
            {
                statusLabel.Text = $"Error code: {ex.Code}, Message: {ex.Message}";
            }
        }

        private void activateTrialBtn_Click(object sender, RoutedEventArgs e)
        {

            LexActivator.SetProductData(productDataBox.Text);
            LexActivator.SetProductId(productIdBox.Text, LexActivator.PermissionFlags.LA_USER);

            try
            {
                LexActivator.SetTrialActivationMetadata("key", "value");
                int status = LexActivator.ActivateTrial();
                if (status != LexStatusCodes.LA_OK)
                {
                    statusLabel.Text = "Error activating the trial";
                    ClearInputs();
                }
                else
                {
                    statusLabel.Text = "Trial activated successfully!";
                }
            }
            catch (LexActivatorException ex)
            {
                statusLabel.Text = $"Error code: {ex.Code}, Message: {ex.Message}";
            }
        }

        private void ClearInputs()
        {
            licenseComboBox.Text = string.Empty;
            productIdBox.Text = string.Empty;
            productDataBox.Text = string.Empty;
            activateBtn.Content = "Activate";
            activateTrialBtn.Content = "Activate Trial";
        }

        private uint unixTimestamp()
        {
            return (uint)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        private void productKeyBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void productIdBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void productDataBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }
    }
}