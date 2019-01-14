using HF.ViewModels;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using System.Collections.Generic;
using System;

namespace HF.Views
{
    public sealed partial class DetailPage : Page
    {
        public DetailPageViewModel ViewModel => (DetailPageViewModel)DataContext;
        public DetailPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
            
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.DeleteItem();
            DeleteFlyout.Hide();
        }
    }
    public class FinancialStuff
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
