using CryplexAdmin.Models;
using CryplexAdmin.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace CryplexAdmin.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private readonly ProductService _productService;

        public ProductsPage()
        {
            InitializeComponent();
            _productService = new ProductService(new HttpClientService());
            _ = LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                List<ProductResponse> products = await _productService.GetAllProductsAsync();
                if (products != null)
                {
                    ProductsListView.ItemsSource = products;
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

        private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var deleteButton = sender as Button;
            deleteButton.IsEnabled = false;

            try
            {
                var selectedProducts = ProductsListView.Items.OfType<ProductResponse>().Where(l => l.IsSelected).ToList();

                foreach (var product in selectedProducts)
                {
                    await _productService.DeleteProductAsync(product.Id);
                }
                await LoadProductsAsync();
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

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var createForm = new CreateForm("Product");
            NavigationService.Navigate(createForm);
        }
    }
}
