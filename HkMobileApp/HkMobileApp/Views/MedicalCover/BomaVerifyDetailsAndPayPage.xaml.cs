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
    public partial class BomaVerifyDetailsAndPayPage : ContentPage
    {
        public BomaVerifyDetailsAndPayPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<BomaVerifyDetailsAndPayViewModel>();
        }
    }
}