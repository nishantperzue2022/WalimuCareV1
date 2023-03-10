using HkMobileApp.ViewModels;
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
    public partial class FAQPage : ContentPage
    {
        public FAQPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<FAQPageViewModel>();
        }
    }
}