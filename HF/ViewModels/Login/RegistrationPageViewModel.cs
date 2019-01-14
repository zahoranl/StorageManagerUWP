using HF.Models;
using HF.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace HF.ViewModels
{
    public class RegistrationPageViewModel: ViewModelBase
    {
        private readonly IContentProviderApiService _contentProviderApiService;

        public User userLoggedIn;
        public string Name { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string Message { get; set; }

        public RegistrationPageViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            userLoggedIn = _contentProviderApiService.getLoggedInUser();
           // await OnNavigatedToAsync(parameter, mode, suspensionState);
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void Back()
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
        public void Reg(object sender)
        {
            User newUser;
            if ((Password == Password2) && !Name.Equals("") && !Password.Equals(""))
            {
                if (userLoggedIn == null)
                {
                    newUser = new User(Name, Password);
                    userLoggedIn = newUser;
                    _contentProviderApiService.AddUser(newUser);
                }
                else
                {
                    userLoggedIn = _contentProviderApiService.getAccess(Name, Password);
                }
                NavigationService.Navigate(typeof(Views.LogoutPage), 2);
            }
            else
            {
                FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            }
            
        }
    }
}
