using HF.Models;
using HF.ViewModels.Dialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HF.Views.Dialog
{
    public sealed partial class AddSellerDialog : ContentDialog
    {
        public AddSellerDialogViewModel ViewModel = new AddSellerDialogViewModel();
        public User newSeller;
        public AddSellerDialog(User user)
        {
            this.InitializeComponent();
            if (user != null)
                newSeller = user;
            else
                newSeller = new User();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ViewModel.SecondaryButtonClick();
            Hide();
        }
    }
}
