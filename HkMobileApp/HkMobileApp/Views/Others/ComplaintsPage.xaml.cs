using HkMobileApp.ViewModels;
using HkMobileApp.Views.ShellPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.Others
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComplaintsPage : ContentPage
    {
        public ComplaintsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<SubmitComplaintsViewModel>();
        }

        protected override bool OnBackButtonPressed()
        {



            return true;
        }
    }
}