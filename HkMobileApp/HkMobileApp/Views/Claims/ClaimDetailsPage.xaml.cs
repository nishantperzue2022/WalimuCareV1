using HkMobileApp.CustomRenderer;
using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.Claims
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClaimDetailsPage : CustomContentPageRenderer
    {
        public ClaimDetailsPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<TrackClaimViewModel>();
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("..");

            return true;
        }

    }
}