using HkMobileApp.ViewModels.MedicalCoverViewModels.WoteCovers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.MedicalCover.WoteCoverViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WoteNextOfKinDetailsPage : ContentPage
    {
        public WoteNextOfKinDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            BindingContext = DependencyService.Get<WoteNextOfKinDetailsViewModel>();
        }
    }
}