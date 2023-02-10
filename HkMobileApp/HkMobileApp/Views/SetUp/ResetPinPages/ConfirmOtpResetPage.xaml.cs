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

namespace HkMobileApp.Views.SetUp.ResetPinPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmOtpResetPage : ContentPage
    {
        public ConfirmOtpResetPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = DependencyService.Get<ConfirmOtpViewModelReset>();
        }
    }
}