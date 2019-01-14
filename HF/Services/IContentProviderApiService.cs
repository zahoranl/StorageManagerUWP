using HF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF.Services
{
    public interface IContentProviderApiService
    {
        //USER
        List<User> GetUsers();
        void AddUser(string name, string pass);
        void AddUser(User user);
        User getAccess(string name, string password);
        void setAsLoggedIn(User loggedIn);
        User getLoggedInUser();

        //ITEM
        void DeleteItem(Item itemToDelete);

        //ITEMGROUP
        void AddItemGroup(ItemGroup itemGroup);
        List<ItemGroup> GetItemGroups();
        void DeleteItemGroup(ItemGroup itemGroupToDelete);

        //CHART
        List<ChartData> GetChartDataSellerQunt(Item i);
        List<ChartData> GetChartDataQuantByDate(Item item);

        void LoadData();
        void SaveData();
    }
}
