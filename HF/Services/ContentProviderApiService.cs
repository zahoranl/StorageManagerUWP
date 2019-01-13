using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using HF.Models;
using System.Diagnostics;
using User = HF.Models.User;
using Windows.Storage;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HF.Services
{
    public class ContentProviderApiService : IContentProviderApiService
    {
        public List<ItemGroup> itemGroups = new List<ItemGroup>();
        public List<User> userList = new List<User>();
        public static User loggedInUser;
      

        public object Process { get; private set; }

        public ContentProviderApiService()
        {
            TestDataForUser();
            TestDataForItemGroup();
            SaveAsync();
            //LoadAsync();
        }

        public List<ItemGroup> GetItemGroups()
        {
            return itemGroups;
        }

        public List<User> GetUsers()
        {
            return userList;
        }
        public void AddUser(string name)
        {
            userList.Add(new User(name));
        }

        public List<ChartData> GetChartDataSellerQunt(Item i)
        {
            var rtnList = new List<ChartData>();
            foreach (var item in i.ItemHistory)
            {
                var found = SearchNameInChartDataList(rtnList, item.Seller.Name);
                if (item.Difference<0)
                    if (found != null)
                    {
                        found.Sum += Math.Abs(item.Difference);
                    }else
                        rtnList.Add(new ChartData(item.Seller.Name, Math.Abs(item.Difference)));
            }
            return rtnList;
        }
        public List<ChartData> GetChartDataQuantByDate(Item i)
        {
            var rtnList = new List<ChartData>();
            foreach (var item in i.ItemHistory)
            {
                var found = SearchDateInChartDataList(rtnList, item.TimeStamp);
                if (found == null) {
                    var add = new ChartData(item.TimeStamp, 0);
                    add.SetDateToName();
                    rtnList.Add(add);
                }
            }
            foreach (var item in i.ItemHistory)
            {
                var found = SearchDateInChartDataList(rtnList, item.TimeStamp);
                var daybefore = SearchDateInChartDataList(rtnList, item.TimeStamp.AddDays(-1));
                if (daybefore != null)
                    if (!found.Summable)
                    {
                        found.Sum = daybefore.Sum + item.Difference;
                        found.Summable = true;
                    }
                    else
                        found.Sum += item.Difference;
                else
                    found.Sum = item.Difference;
            }
            //for (int k = i.ItemHistory.Count-1; k >= 0 ; k--)
            //{
            //    var item = i.ItemHistory[k];
            //    var found = SearchDateInChartDataList(rtnList, item.TimeStamp);
            //    var daybefore = SearchDateInChartDataList(rtnList, item.TimeStamp.AddDays(-1));
            //    if (daybefore!=null)
            //        found.Sum = daybefore.Sum+item.Difference;
            //}
            return rtnList;
        }
        private ChartData SearchNameInChartDataList(List<ChartData> datas,String name)
        {
            foreach (var item in datas)
            {
                if (item.Name.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }
        private ChartData SearchDateInChartDataList(List<ChartData> datas, DateTime dateTime)
        {
            foreach (var item in datas)
            {
                if (item.DateTime.Year==dateTime.Year && item.DateTime.Month==dateTime.Month && item.DateTime.Day==dateTime.Day)
                {
                    return item;
                }
            }
            return null;
        }


        private void TestDataForUser()
        {
            //10 Db
            userList.Add(new User("Laci"));
            userList.Add(new User("Zoli"));
            userList.Add(new User("Pityu"));
            userList.Add(new User("Matyi"));
            userList.Add(new User("Irén"));
            userList.Add(new User("Iringó"));
            userList.Add(new User("Nárcisz"));
            userList.Add(new User("Edmond"));
            userList.Add(new User("Szundi"));
            userList.Add(new User("Merilin"));
        }
        private void TestDataForItemGroup()
        {
            var itemList = new List<Item>();
            itemList.Add(new Item("Zártszelvény"));
            itemList.Add(new Item("Tégla"));
            itemGroups.Add(new ItemGroup("Építőipari", itemList));

            var itemList2 = new List<Item>();
            itemList2.Add(new Item("List"));
            itemList2.Add(new Item("Cukor"));
            itemList2.Add(new Item("Borsó"));
            itemGroups.Add(new ItemGroup("Élelmiszeripari", itemList2));

            var itemList3 = new List<Item>();
            itemList3.Add(new Item("Zongora"));
            itemList3.Add(new Item("Gitár"));
            itemGroups.Add(new ItemGroup("Hangszer", itemList3));
        }
        public async Task SaveAsync()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile userListFile = await storageFolder.CreateFileAsync("users.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            Windows.Storage.StorageFile itemGroupFile = await storageFolder.CreateFileAsync("itemGroup.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);

            var userSerializer = new XmlSerializer(typeof(List<User>));
            var itemGroupSerializer = new XmlSerializer(typeof(List<ItemGroup>));

            Stream userStream = await userListFile.OpenStreamForWriteAsync();
            Stream itemGroupStream = await itemGroupFile.OpenStreamForWriteAsync();

            using (userStream)
            {
                userSerializer.Serialize(userStream, userList);
            }
            using (itemGroupStream)
            {
                itemGroupSerializer.Serialize(itemGroupStream, itemGroups);
            }

        }
        public async Task LoadAsync()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile userListFile = await storageFolder.GetFileAsync("users.txt");
            Windows.Storage.StorageFile itemGroupFile = await storageFolder.GetFileAsync("itemGroup.txt");

            var userSerializer = new XmlSerializer(typeof(List<User>));
            var itemGroupSerializer = new XmlSerializer(typeof(List<ItemGroup>));

            Stream userStream = await userListFile.OpenStreamForWriteAsync();
            Stream itemGroupStream = await itemGroupFile.OpenStreamForWriteAsync();
            using (userStream)
            {
                List<User> loadedUsers = (List<User>)userSerializer.Deserialize(userStream);
                userList.AddRange(loadedUsers);
            }
            using (itemGroupStream)
            {
                List<ItemGroup> loadedItemGroups = (List<ItemGroup>)itemGroupSerializer.Deserialize(itemGroupStream);
                itemGroups.AddRange(loadedItemGroups);
            }
        }
    }
}
