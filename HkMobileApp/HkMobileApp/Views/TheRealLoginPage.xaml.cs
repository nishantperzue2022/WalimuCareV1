using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TheRealLoginPage : ContentPage
    {
        public TheRealLoginPage()
        {
            InitializeComponent();

            BindingContext = new TheRealLoginViewModel();
        }



        private void btnogin_Clicked(object sender, EventArgs e)
        {

        }

        private async void backImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {


            }
        }
    }
}