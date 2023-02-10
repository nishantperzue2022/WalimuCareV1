using HkMobileApp.Extensions;
using HkMobileApp.Models;
using HkMobileApp.Views;
using HkMobileApp.Views.Claims;
using HkMobileApp.Views.dependants;
using HkMobileApp.Views.Hospitals;
using HkMobileApp.Views.MedicalCover;
using HkMobileApp.Views.MedicalCover.Base;
using HkMobileApp.Views.Others;
using HkMobileApp.Views.Policy;
using HkMobileApp.Views.ShellPages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class HomePageViewModel : AppViewModel
    {

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }


        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }



        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(LastName));
            }
        }


        private string fullName;
        public string FullName
        {
            get { return FirstName.ToTitleCase() + " " + LastName.ToTitleCase(); }
            set
            {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string PhoneNumber { get; set; }

        private string avatorUrl;

        public string AvatorUrl
        {
            get { return avatorUrl; }
            set { avatorUrl = value;
                OnPropertyChanged(nameof(AvatorUrl));
            }
        }

        private string greetings;
        public string Greetings
        {
            get { return greetings; }
            set { greetings = value; }
        }

        private List<ByMedicalCoverCarousel> medicalCoverCarousels;

        public List<ByMedicalCoverCarousel> MedicalCoverCarousels
        {
            get { return medicalCoverCarousels; }
            set { medicalCoverCarousels = value;
                OnPropertyChanged();
            }
        }



        public ICommand GoToMedicalDetails { get; set; }
        public ICommand GoToFindHospital { get; set; }
        public ICommand GoToTrackClaims { get; set; }
        public ICommand GoToManageDependants { get; set; }
        public ICommand OpenBrowserToBuyMedicalCover { get; set; }
        public ICommand OpenBrowserToTelemedicine { get; set; }
        public ICommand OpenBrowserToChronicMedication { get; set; }
        public ICommand GoToUserProfileCommand { get; set; }

        public ICommand GoToViewPremiumsAndBenefitsCommand { get; set; }

        public ICommand GoToBomaCoverCommand { get; set; }
        public ICommand GoToWoteCoverCommand { get; set; }

        public ICommand GoToAskLindaCommand { get; set; }

        public ICommand GoToPolicyLimitsCommand { get; set; }
        public ICommand GoToPolicyDetailsCommand { get; set; }

        public ICommand GoToSubmitComplaintsCommand { get; set; }
       
        public HomePageViewModel()
        {
            FirstName = Preferences.Get(nameof(AspNetUsers.firstName), "");
            LastName = Preferences.Get(nameof(AspNetUsers.lastName), "");
            PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
            AvatorUrl = Preferences.Get("ProfilePhoto", "avator.png");

            GetProfilePicture();

            PageTitle = "Healthier Kenya";
            PageSubTitle = "Manage all your cover details";


            var otherNames = LastName.Split(' ');

            LastName = otherNames[0];

            Username = Preferences.Get(nameof(AspNetUsers.userName), "");
            

            GoToMedicalDetails = new Command(GoToMedicalCoverDetails);
            GoToFindHospital = new Command(GoToFindHospitalPage);
            GoToTrackClaims = new Command(GoToTrackClaimsPage);
            GoToManageDependants = new Command(GoToManageDependantsPage);
            OpenBrowserToBuyMedicalCover = new Command(GoToBuyMedicalCover);
            OpenBrowserToTelemedicine = new Command(GoToTelemedicine);
            GoToUserProfileCommand = new Command(async () => await GoToUserProfile());
            GoToViewPremiumsAndBenefitsCommand = new Command<int>(async x => await ViewMedicalCoverPremiumAndBenefits(x));
            GoToBomaCoverCommand = new Command(async () => await GoToBomaCover());
            GoToWoteCoverCommand = new Command(async () => await GoToWoteCover());
            OpenBrowserToChronicMedication = new Command(async ()=> await GoToBuyOrderChronicMedication());
            GoToAskLindaCommand = new Command(async () => await GoToAskLinda());
            GoToPolicyLimitsCommand = new Command(async () => await GoToPolicyLimits());
            GoToPolicyDetailsCommand = new Command(async () => await GoToPolicyDetails());
            GoToSubmitComplaintsCommand = new Command(async() => await GoToSubmitComplaints());


            GetMedicalCoverDetails();
            SetGreetings();

        }

        private string imageUrl;

        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }

        public async void GoToMedicalCoverDetails()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(SelectBomaOrWotePage));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);
            }
        }

        public async void GoToFindHospitalPage()
        {
            try
            {
               
                await Shell.Current.GoToAsync(nameof(FindHospitalPage));
                //await Shell.Current.Navigation.PushAsync(new FindHospitalPage());
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);

            }
        }

        public async void GoToTrackClaimsPage()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(TrackClaimPage));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);
            }
        }
        
        public async void GoToManageDependantsPage()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ManageDependantsPage));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);
            }
        }

        public async void GoToBuyMedicalCover()
        {
            try
            {
                await App.Current.MainPage.Navigation.PushPopupAsync(new HkLoaderPage("Please wait ..."));

                await Browser.OpenAsync("https://healthierkenya.co.ke/",BrowserLaunchMode.SystemPreferred);

                await App.Current.MainPage.Navigation.PopPopupAsync();

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);
                Console.WriteLine(ex);
            }
        }

        public async void GoToTelemedicine()
        {
            try
            {
                //await App.Current.MainPage.Navigation.PushPopupAsync(new HkLoaderPage("Please wait ..."));

                //await Browser.OpenAsync("https://careathome.medbookafrica.com/#/DashBoard", BrowserLaunchMode.SystemPreferred);

                //await App.Current.MainPage.Navigation.PopPopupAsync();

                await Shell.Current.GoToAsync(nameof(TeleMedicineCallOrWebLinkPage));

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);
                Console.WriteLine(ex);
            }
        }

        public async Task GoToBuyOrderChronicMedication()
        {
            try
            {

                await Shell.Current.GoToAsync(nameof(OrderChronicMedicineCallOrWebLinkPage));

                //await App.Current.MainPage.Navigation.PushPopupAsync(new HkLoaderPage("Please wait ..."));

                //await Browser.OpenAsync("https://appointment.medbookafrica.com/", BrowserLaunchMode.SystemPreferred);

                //await App.Current.MainPage.Navigation.PopPopupAsync();

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);
                Console.WriteLine(ex);
            }
        }

        public async Task GoToUserProfile()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ProfilePage));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Home Screen", "", PhoneNumber);
            }
        }

        public async void GetMedicalCoverDetails()
        {
            try
            {
                List<ByMedicalCoverCarousel> byMedicalCoverCarousels = new List<ByMedicalCoverCarousel>()
                {
                     new ByMedicalCoverCarousel()
                    {
                        Id=1,
                        Title = "Boma Afya",
                        SubTitle ="Cover yourself and your family",
                        ImageUrl = "mwavuli.jpg",
                        Description = new List<string>()
                        {
                            "Family cover of principal and 5 dependents(M + 5)",
                            "Covers One(1) legal spouse and four(4) dependent children",
                            "No waiting period for all outpatient claims",
                            "No waiting period for optical and dental services"
                        }

                    },
                     new ByMedicalCoverCarousel()
                    {
                        Id=2,
                        Title = "Wote Afya",
                        SubTitle ="Cover your family and friends",
                        ImageUrl = "dependentplans.jpg",
                        Description = new List<string>()
                        {
                            "Cover upto 10 dependents(M + 10)",
                            "All dependents whether biological or not are eligible for the scheme",
                            "No waiting period for all outpatient claims",
                            "No waiting period for optical and dental services"
                        }

                    }
                };

                MedicalCoverCarousels = byMedicalCoverCarousels;



            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Home Screen", "", PhoneNumber);
            }
        }

        public async Task GoToSubmitComplaints()
        {
            try
            {

                await Shell.Current.GoToAsync(nameof(SubmitComplaintsPage));
                //await App.Current.MainPage.Navigation.PushAsync(new SubmitComplaintsPage());
                DependencyService.Get<SubmitComplaintsViewModel>().IsNavBarVisible = false;
                DependencyService.Get<SubmitComplaintsViewModel>().IsCustomNavBarVisible = true;

            }
            catch (Exception ex) 
            {
                SendErrorMessageToAppCenter(ex, "Home Page");
            }
        }

        public async Task ViewMedicalCoverPremiumAndBenefits(int Id)
        {
            try
            {
                if (Id == 1)
                {
                    await Shell.Current.GoToAsync(nameof(ViewBomaCoverBenefitsAndPremiumsPage));
                    //Preferences.Set("MedicalCoverProductCode", "002");
                    ///await Browser.OpenAsync("https://healthierkenya.co.ke/Api/IndividualCover", BrowserLaunchMode.SystemPreferred);


                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(ViewWoteCoverBenefitsAndPremiumsPage));
                    //Preferences.Set("MedicalCoverProductCode", "001");

                    await Browser.OpenAsync("https://healthierkenya.co.ke/Api/DependentCover", BrowserLaunchMode.SystemPreferred);
                }
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);
            }
        }

        public void SetGreetings()
        {
            try
            {
                var timeOfDay = DateTime.Now.TimeOfDay;

                if (timeOfDay > new TimeSpan(5, 0, 0) && timeOfDay < new TimeSpan(12, 0, 0))
                {
                    Greetings = "Good Morning, ";
                }
                else if (timeOfDay >= new TimeSpan(12, 0, 0) && timeOfDay < new TimeSpan(16, 0, 0))
                {
                    Greetings = "Good Afternoon, ";
                }
                else if (timeOfDay >= new TimeSpan(16, 0, 0) && timeOfDay < new TimeSpan(19, 0, 0))
                {
                    Greetings = "Good Evening, ";
                }
                else
                {
                    Greetings = "Welcome back,  ";
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);
                Greetings = "Welcome back, ";
            }
        }

        public async Task GoToBomaCover()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ViewBomaCoverBenefitsAndPremiumsPage));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);
                await ShowErrorMessage();
            }
        }

        public async Task GoToWoteCover()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(ViewWoteCoverBenefitsAndPremiumsPage));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "DashBoard", "", PhoneNumber);
                await ShowErrorMessage();
            }
        }

        public async Task GoToAskLinda()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AskLindaWriteUpPage));
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Dashboard");
            }
        }

        public async Task GoToPolicyLimits()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(PolicyLimitsPage));
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Dashboard");
            }
        }

        public async Task GoToPolicyDetails()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(PolicyDetailsPage));
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Dashboard");
            }
        }

        public void GetProfilePicture()
        {
            try
            {
                string theImageUrl = Preferences.Get("ProfilePhoto", "avator.png");

                ImageUrl = theImageUrl;
                AvatorUrl = theImageUrl;
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "App Shell");
            }
        }

    }
}
