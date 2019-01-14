using HF.Models;
using HF.Services;
using HF.ViewModels.Dialog;
using HF.Views.Dialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;

namespace HF.ViewModels
{
    public class CategoriesPageViewModel: ViewModelBase
    {
        private readonly IContentProviderApiService _contentProviderApiService;

        public ObservableCollection<ItemGroup> ItemGroups { get; set; }

        public CategoriesPageViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;
            ItemGroups = new ObservableCollection<ItemGroup>(_contentProviderApiService.GetItemGroups());

        }
        public async Task AddCategoryAsync()
        {

            AddEditCategoryDialog dialog = new AddEditCategoryDialog(new AddEditCategoryDialogViewModel());
            ContentDialogResult result = await dialog.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                // mentes
                var itemGroup = new ItemGroup(dialog.ViewModel.CategoryTitle);
                _contentProviderApiService.AddItemGroup(itemGroup);

                // lista frissites
                ItemGroups.Add(itemGroup);
            }
        }
        public void DeleteCategory()
        {
           
        }
        public async Task EditCategoryAsync()
        {
            AddEditCategoryDialog dialog = new AddEditCategoryDialog(new AddEditCategoryDialogViewModel());
            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
