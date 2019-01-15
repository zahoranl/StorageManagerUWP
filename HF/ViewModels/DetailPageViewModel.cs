using HF.Models;
using HF.Services;
using HF.ViewModels.Dialog;
using HF.Views;
using HF.Views.Dialog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.StartScreen;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Data.Xml.Dom;

namespace HF.ViewModels
{

    public class DetailPageViewModel : ViewModelBase
    {
        private readonly IContentProviderApiService _contentProviderApiService;

        public ObservableCollection<ChartData> pieChartData { get; set; }
        public ObservableCollection<ChartData> lineChartData { get; set; }
        public ObservableCollection<HistroyPoint> history { get; set; } 
        public string numberOfSoldItems { get; set; }
        public int numberOfSells { get; set; }
        public List<User> userList { get; set; }
        private Item _item;

        public Item Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public DetailPageViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Item = (Item)parameter;
            Item = _contentProviderApiService.ASzallito;
            pieChartData = new ObservableCollection<ChartData>(_contentProviderApiService.GetChartDataSellerQunt(_item));
            lineChartData =new ObservableCollection<ChartData>(_contentProviderApiService.GetChartDataQuantByDate(_item));
            userList = _contentProviderApiService.GetUsers();
            history = new ObservableCollection<HistroyPoint>(Item.ItemHistory);
           
            
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
        }

      
        public void soldButon_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int negativ = -int.Parse(numberOfSoldItems);
            history.Add(new HistroyPoint(DateTime.Now, negativ, _contentProviderApiService.getLoggedInUser()));
            Item.ItemHistory.Add(new HistroyPoint(DateTime.Now, -int.Parse(numberOfSoldItems), _contentProviderApiService.getLoggedInUser()));
            _contentProviderApiService.SaveData();
            pieChartData = new ObservableCollection<ChartData>(_contentProviderApiService.GetChartDataSellerQunt(_item));
            lineChartData = new ObservableCollection<ChartData>(_contentProviderApiService.GetChartDataQuantByDate(_item));

            Item.Quantity += negativ;
            if (Item.Quantity < Item.CriticalQuantity)
            {
                dropNotification();
            }
        }

        public bool IsItemPinned => SecondaryTile.Exists(Item.Id.ToString());

        public async void PinItemSecondaryTile()
        {
            var tile = new SecondaryTile(
                Item.Id.ToString(),
                Item.Title,
                JsonConvert.SerializeObject(Item),
                new Uri("ms-appx:///Assets/SmallTile.png"),
                TileSize.Square150x150);

            tile.VisualElements.Wide310x150Logo =
                new Uri("ms-appx:///Assets/LargeTile.png");
            tile.VisualElements.ShowNameOnSquare150x150Logo = true;
            tile.VisualElements.ShowNameOnWide310x150Logo = true;
            tile.VisualElements.ShowNameOnSquare310x310Logo = true;

            var success = await tile.RequestCreateAsync();
            if (success)
            {
                RaisePropertyChanged(nameof(IsItemPinned));
            }
        }
       
        public async Task EditItemAsync()
        {
            var adf = _contentProviderApiService.getItemGroupForItem(_item);

            var viewModel = new AddEditItemDialogViewModel();
            viewModel.editedItem = Item;
            viewModel.itemGroups = _contentProviderApiService.GetItemGroups();
            var itemGroup = _contentProviderApiService.getItemGroupForItem(Item);
            viewModel.selectedItemGroup = itemGroup;
            AddEditItemDialog dialog = new AddEditItemDialog(viewModel);
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                itemGroup.itemList.Remove(Item);
                viewModel.selectedItemGroup.itemList.Add(Item);

                _contentProviderApiService.SaveData();
            }
        }
        public void DeleteItem()
        {
            var itemGroup = _contentProviderApiService.getItemGroupForItem(Item);
            itemGroup.itemList.Remove(Item);
            _contentProviderApiService.DeleteItem(Item);
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
        public void RefreshItem()
        {
          
        }
       
        public async void UnPinItemSecondaryTile()
        {
            var tile = new SecondaryTile(Item.Id.ToString());
            var success = await tile.RequestDeleteAsync();
            if (success)
            {
                RaisePropertyChanged(nameof(IsItemPinned));
            }
        }
        
        private void dropNotification()
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(Item.Title));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode("Level below critical: "+ Item.Quantity));
            XmlNodeList toastImageElements = toastXml.GetElementsByTagName("image");
            IXmlNode toasNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toasNode).SetAttribute("duration", "long");

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
