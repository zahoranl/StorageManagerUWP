using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using HF.Models;
using HF.Services;
using Windows.UI.Xaml.Controls;
using HF.Views;
using Windows.UI.Xaml.Media.Animation;
using HF.Views.Dialog;

namespace HF.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        private readonly IContentProviderApiService _contentProviderApiService;

        private List<ItemGroup> _itemGroup;
        public List<ItemGroup> ItemGroups
        {
            get { return _itemGroup; }
            set { Set(ref _itemGroup, value); }
        }

        public MainPageViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;
        }
       
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
           
            ItemGroups = _contentProviderApiService.GetItemGroups();
            await base.OnNavigatedToAsync(parameter, mode, suspensionState);
            Services.SettingsServices.SettingsService _settings;
            _settings = Services.SettingsServices.SettingsService.Instance;
            _settings.IsFullScreen = false;
        }
  
        
        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
         
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoDetailsPage() => NavigationService.Navigate(typeof(Views.DetailPage), 3);

        public void GotoSettings() => NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() => NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() => NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        public void GotoDetail(object sender, ItemClickEventArgs e)
        {
            var Item = (Item)e.ClickedItem;
            NavigationService.Navigate(typeof(DetailPage), Item, new SuppressNavigationTransitionInfo());
        }
        public async Task AddItemAsync()
        {
            AddEditItemDialog dialog = new AddEditItemDialog();
            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
