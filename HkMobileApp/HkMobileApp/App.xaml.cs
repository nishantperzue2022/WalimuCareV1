using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HkMobileApp.Services;
using HkMobileApp.Views;
using HkMobileApp.ViewModels;
using Xamarin.Essentials;
using HkMobileApp.Models;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using HkMobileApp.ViewModels.SetUpViewModels;
using System.Diagnostics;
using HkMobileApp.Views.SetUp;
using HkMobileApp.Views.Hospitals;
using HkMobileApp.Views.ShellPages;
using HkMobileApp.Views.Others;
using HkMobileApp.Views.Policy;
using HkMobileApp.ViewModels.MedicalCoverViewModels;
using HkMobileApp.Views.MedicalCover;
using HkMobileApp.ViewModels.MedicalCoverViewModels.Base;
using HkMobileApp.ViewModels.MedicalCoverViewModels.WoteCovers;
using HkMobileApp.Interfaces;

namespace HkMobileApp
{

    public partial class App : Application
    {

        public App()
        {


            InitializeComponent();

            try
            {


                XF.Material.Forms.Material.Init(this);


                SetApiDetails();

                RegisterDependencyModels();

                

                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzIzOTY0QDMxMzgyZTMyMmUzME1UWEN6YklZaUJQeCtzTlpSRVRmYk9oeE1mbVoxZXJjc1p6SDdDK2VBaUE9");

                var firstName = Preferences.Get(nameof(AspNetUsers.firstName), null);
                var phoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), null);

                if (firstName == null || phoneNumber == null)
                {
                    MainPage = new LoadingScreen();
                }
                else
                {
                    MainPage = new NavigationPage(new FinalLoginPage());
                }

            }
            catch (Exception ex)
            {
                MainPage = new TheRealLoginPage();
                Crashes.TrackError(ex);
                Console.WriteLine(ex);
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=ecc15e99-4e54-42b5-828c-a025d1e74bdb;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));

            //Crashes.GenerateTestCrash();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
          
        }

        public void SetApiDetails()
        {
            if (Debugger.IsAttached)
            {
                ApiDetail.EndPoint = ApiDetail.LocalEndPoint;
            }
            else
            {
                //means we are on production
                ApiDetail.EndPoint = ApiDetail.PublicEndPoint;
                //ApiDetail.EndPoint = ApiDetail.LocalEndPoint;
            }
        }

        public void RegisterDependencyModels()
        {
            try
            {

                DependencyService.Register<MockDataStore>();
                DependencyService.Register<AppShellViewModel>();

                DependencyService.Register<PolicyLimitsViewModel>();

                DependencyService.Register<SignUpViewModel>();

                DependencyService.Register<ConfirmOtpViewModel>();

                DependencyService.Register<ConfirmMemberDetailsViewModel>();

                DependencyService.Register<TheRealLoginViewModel>();

                DependencyService.Register<ManageDependantsViewModel>();

                DependencyService.Register<AddDependantViewModel>();

                DependencyService.Register<EditDependantViewModel>();

                DependencyService.Register<TrackClaimViewModel>();

                DependencyService.Register<PolicyDetailsViewModel>();

                DependencyService.Register<HomePageViewModel>();

                DependencyService.Register<FeedbackViewModel>();

                DependencyService.Register<RequestCallBackViewModel>();

                DependencyService.Register<WellnessBlogViewModel>();

                DependencyService.Register<FindHospitalViewModel>();

                DependencyService.Register<UserProfileViewModel>();

                DependencyService.Register<ViewBomaBenefitCoverAndPremiumsViewModel>();

                DependencyService.Register<ViewWotePremiumsAndCoverBenefitsViewModel>();

                DependencyService.Register<BomaBuyMedicalCoverViewModel>();

                DependencyService.Register<BomaNextOfKinDetailsViewModel>();

                DependencyService.Register<BomaVerifyDetailsAndPayViewModel>();

                DependencyService.Register<BomaSelectCoverViewModel>();

                DependencyService.Register<CallBackListViewModel>();

                DependencyService.Register<SelectBomaOrWotePageViewModel>();

                DependencyService.Register<WoteBuyMedicalCoverViewModel>();
                DependencyService.Register<WoteNextOfKinDetailsViewModel>();
                DependencyService.Register<WoteVerifyDetailsAndPayViewModel>();
                DependencyService.Register<WoteSelectCoverViewModel>();
                DependencyService.Register<MedicalCoverOrderViewModel>();
                DependencyService.Register<SubmitComplaintsViewModel>();
                DependencyService.Register<ILocSettings>();
                DependencyService.Register<EnableGpsViewModel>();
                DependencyService.Register<FAQPageViewModel>();
                DependencyService.Register<IFileSavePdf>();
                DependencyService.Register<OrderChronicMedicationViewModel>();
                DependencyService.Register<ConfirmOtpViewModelReset>();
                DependencyService.Register<ResetPinPageViewModel>();
                DependencyService.Register<Covid19ViewModel>();
                DependencyService.Register<UserLocationViewModel>();
            }
            catch (Exception ex)
            {

                Console.Write(ex);
            }
        }
    }
}
