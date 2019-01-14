using HF.Models;
using HF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace HF.ViewModels
{
    public class CategoriesPageViewModel: ViewModelBase
    {
        private readonly IContentProviderApiService _contentProviderApiService;
        private List<ItemGroup> _itemGroup;
        public List<ItemGroup> ItemGroups
        {
            get { return _itemGroup; }
            set { Set(ref _itemGroup, value); }
        }
        public CategoriesPageViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;
            ItemGroups = _contentProviderApiService.GetItemGroups();

        }
        public void AddCategory()
        {

        }
        public void DeleteCategory()
        {

        }
        public void EditCategory()
        {

        }
    }
}
