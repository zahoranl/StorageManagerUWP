using HF.Models;
using HF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace HF.ViewModels
{
    public class LogoutPageViewModel : ViewModelBase
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

        public LogoutPageViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            UserGroups = _contentProviderApiService.GetUsers();
            await base.OnNavigatedToAsync(parameter, mode, suspensionState);
            Services.SettingsServices.SettingsService _settings;
            _settings = Services.SettingsServices.SettingsService.Instance;
            _settings.IsFullScreen = false;
            Message = "Login as: "+ _contentProviderApiService.getLoggedInUser();
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void Reg()
        {
            User loggedIn = _contentProviderApiService.getAccess(Name, Password);
            if (loggedIn == null)
            {
                Message = "Access Denied \n Wrong User name or Password!";
            }
            else
            {
                _contentProviderApiService.setAsLoggedIn(loggedIn);
                NavigationService.Navigate(typeof(Views.MainPage), 2);
            }
        }

        public void Help()
        {
            _contentProviderApiService.setAsLoggedIn(new User("Guest", ""));
            NavigationService.Navigate(typeof(Views.MainPage), 2);
        }
        public void Logout()
        {
            NavigationService.Navigate(typeof(Views.LoginPage), 3);
        }

        public void Edit()
        {

        }
    }
}
