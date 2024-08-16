using CryplexAdmin.Services;
using Cryptlex;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CryplexAdmin.Pages
{
    /// <summary>
    /// Interaction logic for ActivatiionForm.xaml
    /// </summary>
    public partial class ActivationForm : Page
    {
        public ActivationForm()
        {
            InitializeComponent();
        }

        private void activateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productKeyBox.Text) ||
                    string.IsNullOrWhiteSpace(productIdBox.Text) ||
                    string.IsNullOrWhiteSpace(productDataBox.Text))
                {
                    statusLabel.Text = "Please fill in all fields before activating.";
                    return;
                }

                LexActivator.SetProductData(productDataBox.Text);
                LexActivator.SetProductId(productIdBox.Text, LexActivator.PermissionFlags.LA_USER);

                if (activateBtn.Content.ToString() == "Deactivate")
                {
                    int status = LexActivator.DeactivateLicense();
                    if (status == LexStatusCodes.LA_OK)
                    {
                        statusLabel.Text = "License deactivated successfully";
                        activateBtn.Content = "Activate";
                        activateTrialBtn.IsEnabled = true;
                    }
                    else
                    {
                        statusLabel.Text = "Error deactivating license";
                    }
                }
                else
                {
                    LexActivator.SetLicenseKey(productKeyBox.Text);
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
            try
            {
                LexActivator.SetTrialActivationMetadata("key", "value");
                int status = LexActivator.ActivateTrial();
                if (status == LexStatusCodes.LA_OK)
                {
                    statusLabel.Text = "Trial activated successfully!";

                    ClearInputs();
                }
                else
                {
                    statusLabel.Text = "Error activating the trial";
                }
            }
            catch (LexActivatorException ex)
            {
                statusLabel.Text = $"Error code: {ex.Code}, Message: {ex.Message}";
            }
        }

        private void ClearInputs()
        {
            productKeyBox.Text = string.Empty;
            productIdBox.Text = string.Empty;
            productDataBox.Text = string.Empty;
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
