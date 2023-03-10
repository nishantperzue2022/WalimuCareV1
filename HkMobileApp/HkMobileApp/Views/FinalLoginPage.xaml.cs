using HkMobileApp.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalLoginPage : ContentPage
    {

        public TheRealLoginViewModel theRealLoginViewModel { get; set; }
        public FinalLoginPage()
        {
            InitializeComponent();

             theRealLoginViewModel = DependencyService.Get<TheRealLoginViewModel>();

            ///theRealLoginViewModel.Pin = null;

            BindingContext = theRealLoginViewModel;
        }

        protected override void OnAppearing()
        {
            
        }

        private async void pinView_PinSubmit(object sender, SkorXam.Pin.PinSubmitEventArg e)
        {
            try
            {
                await theRealLoginViewModel.SubmitLogin();
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry somethig went wrong"));
            }
        }
    }
}