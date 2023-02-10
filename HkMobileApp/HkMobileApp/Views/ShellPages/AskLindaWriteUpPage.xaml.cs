using HkMobileApp.Extensions;
using HkMobileApp.Models;
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
    public partial class AskLindaWriteUpPage : ContentPage
    {

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }

        public AskLindaWriteUpPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = this;


            FirstName = Preferences.Get(nameof(AspNetUsers.firstName), "").ToPascalCase();

            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://wa.me/254781704000", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
            }
        }
    }
}