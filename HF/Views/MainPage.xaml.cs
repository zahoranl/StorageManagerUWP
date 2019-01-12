using System;
using HF.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

namespace HF.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel => (MainPageViewModel)DataContext;
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
    }
}