using HkMobileApp.ViewModels.SetUpViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmMemberDetails : ContentPage
    {
        public ConfirmMemberDetails()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<ConfirmMemberDetailsViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //btnContinue.IsEnabled = false;
        }

    }
}