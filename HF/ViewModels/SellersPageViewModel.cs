using HF.Models;
using HF.Services;
using HF.Views;
using HF.Views.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HF.ViewModels
{
    public class SellersPageViewModel : ViewModelBase
    {
        private readonly IContentProviderApiService _contentProviderApiService;
        public List<User> userList { get; set; }
        public SellersPageViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;    
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            userList = _contentProviderApiService.GetUsers();
        }
        public async void AddSellerAsync()
        {
            userList.Add(new User(""));
            AddSellerDialog dialog = new AddSellerDialog(userList.Last());
            ContentDialogResult result = await dialog.ShowAsync();
            
        }
    }
}
