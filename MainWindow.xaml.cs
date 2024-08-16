using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using CryplexAdmin.Pages;

namespace CryplexAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Welcome();
        }

        private void NavigateToProducts(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ProductsPage();
        }

        private void NavigateToLicenses(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LicencesPage();
        }
    }
}
