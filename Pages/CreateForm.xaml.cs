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
        private readonly LicenseTemplateService _licenseTemplateService;
        public string Context { get; set; }

        public CreateForm(string context)
        {
            InitializeComponent();
            _productService = new ProductService(new HttpClientService());
            _licenseService = new LicenseService(new HttpClientService());
            _licenseTemplateService = new LicenseTemplateService(new HttpClientService());

            Context = context;
            InitializeForm(context);

            if (context == "License")
            {
                _ = PopulateLicenseComboBoxAsync();
            }
            else if (context == "Product")
            {
                LoadLicenseTemplatesAsync();
            }

        }

        private async void LoadLicenseTemplatesAsync()
        {
            try
            {
                var licenseTemplates = await _licenseTemplateService.GetAllLicenseTemplatesAsync();
                licenseTemplateComboBox.ItemsSource = licenseTemplates;
                licenseTemplateComboBox.DisplayMemberPath = "Type";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading license templates: {ex.Message}");
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

            try
            {
                if (Context == "Product")
                {
                    if (licenseTemplateComboBox.SelectedItem is LicenseTemplate selectedTemplate)
                    {
                        var product = new ProductRequest
                        {
                            Name = NameTextBox.Text,
                            DisplayName = DisplayNameTextBox.Text,
                            Description = DescriptionTextBox.Text,
                            LicenseTemplateId = selectedTemplate.Id, 
                        };

                        await _productService.CreateProductAsync(product);
                        var productPage = new ProductsPage();
                        NavigationService.Navigate(productPage);
                    }
                    else
                    {
                        MessageBox.Show("Please select a license template.");
                    }
                }
                else if (Context == "License")
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Operation failed: {ex.Message}");
            }
            finally
            {
                submitButton.IsEnabled = true; 
            }
        }


        private void licenseTemplateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
