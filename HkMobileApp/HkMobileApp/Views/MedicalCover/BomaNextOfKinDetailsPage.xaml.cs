using HkMobileApp.ViewModels.MedicalCoverViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.MedicalCover
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BomaNextOfKinDetailsPage : ContentPage
    {
        public BomaNextOfKinDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<BomaNextOfKinDetailsViewModel>();
        }


    }
}