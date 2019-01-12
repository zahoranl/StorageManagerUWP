using HF.Models;
using HF.Services;
using HF.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace HF.ViewModels
{

    public class DetailPageViewModel : ViewModelBase
    {
        private readonly IContentProviderApiService _contentProviderApiService;
        public List<FinancialStuff> financialStuffList = new List<FinancialStuff>();
        public List<ChartData> pieChartData { get; set; }
        public List<ChartData> lineChartData { get; set; }
        public string numberOfSoldItems { get; set; }
        public int numberOfSells { get; set; }
        public User selectedComboBoxSeller { get; set; }
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
            pieChartData = _contentProviderApiService.GetChartDataSellerQunt(_item);
            lineChartData = _contentProviderApiService.GetChartDataQuantByDate(_item);
            userList = _contentProviderApiService.GetUsers();
            if (userList.Count!=0)
                selectedComboBoxSeller = userList[0];
           
            
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
        }

      
        public void soldButon_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //try
            //{
            //    if (numberOfSoldItems != null|| !numberOfSoldItems.Equals("")) { 
            //    numberOfSells = Int32.Parse(numberOfSoldItems);
            //    Item.ItemHistory.Add(new HistroyPoint(DateTime.Now, -numberOfSells, selectedComboBoxSeller));
            //    Item.Quantity -= numberOfSells;
            //    numberOfSoldItems = "0";
            //    if (userList.Count != 0)
            //        selectedComboBoxSeller = userList[0];
            //}
            //}
            //catch (Exception c)
            //{
            //    numberOfSoldItems = "0";
            //}
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
       
        public void EditItem()
        {

        }
        public void DeleteItem()
        {

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
        
    }
}
