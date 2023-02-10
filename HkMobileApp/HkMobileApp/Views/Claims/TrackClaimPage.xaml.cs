using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HkMobileApp.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.Claims
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackClaimPage : CustomContentPageRenderer
    {
        public TrackClaimPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<TrackClaimViewModel>();
        }

        private  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var claim = e.Item as Claim;

                DependencyService.Get<TrackClaimViewModel>().SelectSelectedClaim(claim.id);
 
                
            }
            catch (Exception ex)
            {

                DependencyService.Get<TrackClaimViewModel>().SendErrorMessageToAppCenter(ex, "Track Claims");

                await DependencyService.Get<TrackClaimViewModel>().ShowErrorMessage();
            }
        }
    }
}