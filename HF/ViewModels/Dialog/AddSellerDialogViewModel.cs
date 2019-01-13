using HF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF.ViewModels.Dialog
{
    public class AddSellerDialogViewModel
    {
        public string sellerName = "Add New Seller";
        private IContentProviderApiService _contentProviderApiService;

        public AddSellerDialogViewModel()
        {
            _contentProviderApiService = new ContentProviderApiService();
        }

        public AddSellerDialogViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;

        }

        public void SecondaryButtonClick() {
            if (sellerName != null || !sellerName.Equals(""))
            {
                _contentProviderApiService.AddUser(sellerName, sellerName);
            }
        }
    }
}
