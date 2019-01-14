using HF.Models;
using HF.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
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
            if (userLoggedIn == null)
                Message = "Regisztráció";
            else
            {
                Message = "Edit user: " + userLoggedIn.Name;
                Name = userLoggedIn.Name;
            }
            await OnNavigatedToAsync(parameter, mode, suspensionState);
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
        public void Reg()
        {

            User newUser;
            if ((Password == Password2) && !Name.Equals(""))
                if (userLoggedIn == null) { 
                    newUser = new User(Name, Password);
                }else
                {
                    userLoggedIn.Name = Name;
                    userLoggedIn.Password = Password;
                }
            NavigationService.Navigate(typeof(Views.LogoutPage), 2);
        }
    }
}
