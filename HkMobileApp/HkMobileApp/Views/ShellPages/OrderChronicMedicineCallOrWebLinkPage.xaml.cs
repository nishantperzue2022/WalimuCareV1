using HkMobileApp.ViewModels;
using HkMobileApp.Views.PopUps;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.ShellPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderChronicMedicineCallOrWebLinkPage : ContentPage
    {
       


        public OrderChronicMedicineCallOrWebLinkPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<OrderChronicMedicationViewModel>();
 
        }

    }
}