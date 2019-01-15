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

        private ItemGroup selectedItemGroup;

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
                var itemGroup = new ItemGroup(dialog.ViewModel.CategoryTitle);
                _contentProviderApiService.AddItemGroup(itemGroup);

                ItemGroups.Add(itemGroup);
            }
        }
        public void DeleteCategory()
        {
            _contentProviderApiService.DeleteItemGroup(selectedItemGroup);

            ItemGroups.Remove(selectedItemGroup);
            selectedItemGroup = null;
        }
        public async Task EditCategoryAsync()
        {
            AddEditCategoryDialog dialog = new AddEditCategoryDialog(new AddEditCategoryDialogViewModel());
            dialog.ViewModel.CategoryTitle = selectedItemGroup.Title;
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                selectedItemGroup.Title = dialog.ViewModel.CategoryTitle;
                _contentProviderApiService.SaveData();
            }
        }
        public void ItemSelectionChanged(object sender, ItemClickEventArgs e)
        {
            selectedItemGroup = (ItemGroup)e.ClickedItem;
        }
    }
}
