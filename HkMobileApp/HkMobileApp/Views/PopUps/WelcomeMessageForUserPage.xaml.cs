using HkMobileApp.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeMessageForUserPage : PopupPage
    {

        private string msgPartOne;
        public string MsgPartOne
        {
            get { return msgPartOne; }
            set { msgPartOne = value; OnPropertyChanged(); }
        }




        private string msgPartTwo;
        public string MsgPartTwo
        {
            get { return msgPartTwo; }
            set { msgPartTwo = value; OnPropertyChanged(); }
        }




        private string msgPartThree;    
        public string MsgPartThree
        {
            get { return msgPartThree; }
            set { msgPartThree = value;  OnPropertyChanged(); }
        }




        private string msgPartFour;
        public string MsgPartFour
        {
            get { return msgPartFour; }
            set { msgPartFour = value;  OnPropertyChanged(); }
        }



        private string msgPartFive;
        public string MsgPartFive
        {
            get { return msgPartFive; }
            set { msgPartFive = value;  OnPropertyChanged(); }
        }



        public string fullName { get; set; }

        public ICommand GoToWebsiteCommand { get; set; }

        public ICommand DailPhoneCommand { get; set; }



        public WelcomeMessageForUserPage()
        {
            InitializeComponent();

           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            string firstName = Preferences.Get(nameof(AspNetUsers.firstName), "");
            string LastName = Preferences.Get(nameof(AspNetUsers.lastName), "");

            fullName = firstName + " " + LastName;

            MsgPartOne = $"Dear {fullName}, welcome to TSC Medical Scheme. Please visit ";
            MsgPartTwo = "https://healthierkenya.co.ke/ ";
            MsgPartThree = "or dial  ";
            MsgPartFour = "*506#  ";
            MsgPartFive = " (Your pin is same as on mobile app) to register dependants , view policy limits and track your hospital visits.";

            GoToWebsiteCommand = new Command(async () => await GoToWebSite());
            DailPhoneCommand = new Command(async () => await DailPhone());

            BindingContext = this;
        }

        private async Task GoToWebSite()
        {
            try
            {
                await Browser.OpenAsync("https://healthierkenya.co.ke/", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception )
            {

               
            }
        }


        public async Task DailPhone()
        {
            try
            {
                 PhoneDialer.Open("*506#");
            }
            catch (Exception)
            {

      
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopPopupAsync();
            }
            catch (Exception)
            {

            }
        }
    }
}