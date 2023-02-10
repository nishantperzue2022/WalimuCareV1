using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HkMobileApp.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.ShellPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : CustomContentPageRenderer
    {
     
        public HomePage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<HomePageViewModel>();
           
        }
    }
}