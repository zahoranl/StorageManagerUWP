using HF.Models;
using HF.Services;
using HF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace HF.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IContentProviderApiService _contentProviderApiService;

        private List<User> _userGroup;
        public List<User> UserGroups
        {
            get { return _userGroup; }
            set { Set(ref _userGroup, value); }
        }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }

        public LoginPageViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            UserGroups = _contentProviderApiService.GetUsers();
            await base.OnNavigatedToAsync(parameter, mode, suspensionState);
            Services.SettingsServices.SettingsService _settings;
            _settings = Services.SettingsServices.SettingsService.Instance;
            _settings.IsFullScreen = true;
            Message = "asdf";
        }
  
        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void Login(){
            User loggedIn =_contentProviderApiService.getAccess(Name, Password);
            if (loggedIn == null)
            {
                Message = "Access Denied \n Wrong User name or Password!";
            }
            else
            {
                _contentProviderApiService.setAsLoggedIn(loggedIn);
                NavigationService.Navigate(typeof(Views.LogoutPage), 2);
            }
        }

        public void LoginAsGuest()
        {
            _contentProviderApiService.setAsLoggedIn(new User("Guest", ""));
            NavigationService.Navigate(typeof(Views.MainPage), 2);
        }
        public void Help()
        {

        }

        public void Reg()
        {

        }
    }
}
