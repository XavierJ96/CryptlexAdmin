using CryplexAdmin.Models;
using CryplexAdmin.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CryplexAdmin.Pages
{
    /// <summary>
    /// Interaction logic for Form.xaml
    /// </summary>
    public partial class CreateForm : Page
    {
        private readonly ProductService _productService;
        private readonly LicenseService _licenseService;
        public string Context { get; set; }

        public CreateForm(string context)
        {
            InitializeComponent();
            _productService = new ProductService(new HttpClientService());
            _licenseService = new LicenseService(new HttpClientService());
            Context = context;
            InitializeForm(context);

            if (context == "License")
            {
                _ = PopulateLicenseComboBoxAsync();
            }
        }

        public void InitializeForm(string context)
        {
            if (context == "Product")
            {
                ProductFields.Visibility = Visibility.Visible;
                LicenseFields.Visibility = Visibility.Collapsed;
            }
            else if (context == "License")
            {
                ProductFields.Visibility = Visibility.Collapsed;
                LicenseFields.Visibility = Visibility.Visible;
            }
        }
        public async Task PopulateLicenseComboBoxAsync()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                LicenseComboBox.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching products: {ex.Message}");
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var submitButton = sender as Button;
            submitButton.IsEnabled = false;

            if (Context == "Product")
            {
                var product = new ProductRequest
                {
                    Name = NameTextBox.Text,
                    DisplayName = DisplayNameTextBox.Text,
                    Description = DescriptionTextBox.Text,
                    LicenseTemplateId = "d7456051-923c-46c2-8dfd-e7f65de3fc9b",
                };

                try
                {
                    await _productService.CreateProductAsync(product);
                    var productPage = new ProductsPage();
                    NavigationService.Navigate(productPage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to create product: {ex.Message}");
                }
                finally
                {
                    submitButton.IsEnabled = true;
                }
            }
            else if (Context == "License")
            {
                try
                {
                    var selectedProductId = LicenseComboBox.SelectedValue as string;
                    if (string.IsNullOrEmpty(selectedProductId))
                    {
                        MessageBox.Show("Please select a product.");
                        return;
                    }

                    await _licenseService.CreateLicenseAsync(selectedProductId);

                    var licensePage = new LicencesPage();
                    NavigationService.Navigate(licensePage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to create license: {ex.Message}");
                }
                finally
                {
                    submitButton.IsEnabled = true;
                }
            }
        }
    }
}
