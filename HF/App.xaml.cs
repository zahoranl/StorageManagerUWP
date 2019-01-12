using Windows.UI.Xaml;
using System.Threading.Tasks;
using HF.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using Autofac;
using HF.Services;
using Template10.Services.NavigationService;
using HF.Views;
using HF.ViewModels;
using HF.Views.Dialog;

namespace HF
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region app settings

            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;

            #endregion

            ConfigureDependencies();
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog
            {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(service),
                ModalContent = new Views.Busy(),
            };
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(Views.MainPage));
        }

        private IContainer _container;

        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<CookbookApiService>().As<ICookbookApiService>().InstancePerLifetimeScope();
           // builder.Register(c => RestService.For<ICookbookApiService>("https://bmecookbook.azurewebsites.net/")).InstancePerDependency();
            builder.RegisterType<MainPageViewModel>().InstancePerDependency();
            builder.RegisterType<DetailPageViewModel>().InstancePerDependency();
            builder.RegisterType<SellersPageViewModel>().InstancePerDependency();
            builder.RegisterType<StatisticPageViewModel>().InstancePerDependency();
            builder.RegisterType<CategoriesPageViewModel>().InstancePerDependency();
            builder.RegisterType<AddSellerDialog>().InstancePerDependency();
            builder.RegisterType<ContentProviderApiService>().As<IContentProviderApiService>().InstancePerLifetimeScope();


            _container = builder.Build();
        }

        public override INavigable ResolveForPage(Page page, NavigationService navigationService)
        {
            if (page is MainPage)
            {
                return _container.Resolve<MainPageViewModel>();
            }
            else if (page is DetailPage)
            {
                return _container.Resolve<DetailPageViewModel>();
            }
            else if (page is SellersPage)
            {
                return _container.Resolve<SellersPageViewModel>();
            }
            else if (page is StatisticPage)
            {
                return _container.Resolve<StatisticPageViewModel>();
            }
            else if (page is CategoriesPage)
            {
                return _container.Resolve<CategoriesPageViewModel>();
            }
            else 
            {
                return base.ResolveForPage(page, navigationService);
            }
        }


    }
}
