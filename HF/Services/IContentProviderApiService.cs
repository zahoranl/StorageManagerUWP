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
        List<ItemGroup> GetItemGroups();
        List<User> GetUsers();
        void AddUser(string name, string pass);
        List<ChartData> GetChartDataSellerQunt(Item i);
        Task LoadAsync();
        Task SaveAsync();
        List<ChartData> GetChartDataQuantByDate(Item item);
        User getAccess(string name, string password);
        void setAsLoggedIn(User loggedIn);
        User getLoggedInUser();
    }
}
