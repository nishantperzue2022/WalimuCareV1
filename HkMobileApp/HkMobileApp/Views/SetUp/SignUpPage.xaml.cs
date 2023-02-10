using HkMobileApp.ViewModels.SetUpViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public int CountTimesSignInTried { get; set; }

        public SignUpPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<SignUpViewModel>();
        }

        private async void btnSignUp_Clicked(object sender, EventArgs e)
        {

            //try
            //{
            //    if (CountTimesSignInTried < 1)
            //    {
            //        await Navigation.PushPopupAsync(new HkLoaderPage("Signup in progress..."));

            //        await Task.Run(() =>
            //        {
            //            Thread.Sleep(5000);
            //        });

            //        MainThread.BeginInvokeOnMainThread(() =>
            //        {
            //            Navigation.PopPopupAsync(false);
            //        });


                   
            //        await Navigation.PushPopupAsync(new HkErrorPage("Invalid Member Id or Phone Number"));

            //        CountTimesSignInTried++;
            //    }
            //    else
            //    {
            //        await Navigation.PushAsync(new ConfirmOtpPage());
            //    }
            //}
            //catch (Exception ex)
            //{

            //    await DisplayAlert("Oooops", "Something went wrong", "OK");
            //}

        }

        private async void backImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                var x = await App.Current.MainPage.Navigation.PopAsync();

                if(x == null)
                {
                    Application.Current.MainPage = new NavigationPage(new FinalLoginPage());
                }



            }
            catch (Exception ex)
            {
                
                
            }
        }
    }
}