using HkMobileApp.ViewModels.SetUpViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.SetUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPinPage : ContentPage
    {
        public ResetPinPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<ResetPinPageViewModel>();
        }
    }
}