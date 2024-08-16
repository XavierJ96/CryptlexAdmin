using CryplexAdmin.Models;
using CryplexAdmin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CryplexAdmin.Pages
{
    /// <summary>
    /// Interaction logic for LicencesPage.xaml
    /// </summary>
    public partial class LicencesPage : Page
    {
        private readonly LicenseService _licenseService;

        public LicencesPage()
        {
            InitializeComponent();
            _licenseService = new LicenseService(new HttpClientService());
            _ = LoadLicensesAsync();
        }

        private async Task LoadLicensesAsync()
        {
            try
            {
                List<License> licenses = await _licenseService.GetAllLicensesAsync();
                if (licenses != null)
                {
                    LicensesListView.ItemsSource = licenses;
                }
                else
                {
                    MessageBox.Show("No products found or an error occurred.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading products: {ex.Message}");
            }
        }

        private async void DeleteLicense_Click(object sender, RoutedEventArgs e)
        {
            var deleteButton = sender as Button;
            deleteButton.IsEnabled = false;

            try
            {
                var selectedLicenses = LicensesListView.Items.OfType<License>().Where(l => l.IsSelected).ToList();

                foreach (var license in selectedLicenses)
                {
                    await _licenseService.DeleteLicenseAsync(license.Id);
                }
                await LoadLicensesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                deleteButton.IsEnabled = true;
            }
        }

        private void AddLicense_Click(object sender, RoutedEventArgs e)
        {
            var createForm = new CreateForm("License");
            NavigationService.Navigate(createForm);
        }

        private void ActivateLicense_Click(object sender, RoutedEventArgs e)
        {
            var activationForm = new ActivationForm();
            NavigationService.Navigate(activationForm);
        }
    }
}
