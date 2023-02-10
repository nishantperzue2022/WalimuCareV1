using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HkMobileApp.Models;
using HkMobileApp.Services;
using HkMobileApp.ViewModels;
using HkMobileApp.Views;
using HkMobileApp.Views.Claims;
using HkMobileApp.Views.dependants;
using HkMobileApp.Views.doctor;
using HkMobileApp.Views.Hospitals;
using HkMobileApp.Views.Others;
using HkMobileApp.Views.Policy;
using HkMobileApp.Views.WellNessBlog;
using HkMobileApp.Views.ShellPages;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.AppCenter.Crashes;
using System.Windows.Input;
using HkMobileApp.CustomRenderer;
using HkMobileApp.Views.MedicalCover;
using HkMobileApp.Views.MedicalCover.Base;
using HkMobileApp.Views.MedicalCover.WoteCoverViews;
using HkMobileApp.Views.SetUp;
using Rg.Plugins.Popup.Extensions;
using HkMobileApp.Views.PopUps;

namespace HkMobileApp
{
    public partial class AppShell : CustomShellRenderer
    {

        public string PhoneNumber { get; set; }
        public string MemberNumber { get; set; }

        public ICommand ViewProfileCommand { get; set; }

        public AppShell()
        {
            InitializeComponent();


            BindingContext = DependencyService.Get<AppShellViewModel>();

            RegisterMyRoutes();

            Task.Run(async () =>
            {
                await GetMemberNumber();
                await GetSchemeId();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
            MemberNumber = Preferences.Get("MemberNumber","");

            //Shell.Current.FlyoutHeader

        }


        protected override bool OnBackButtonPressed()
        {

            //App.Current.MainPage = new AppShell();

            //return base.OnBackButtonPressed();


            var theCurrentPage = (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;

            string nameOfCurrentPage = theCurrentPage.ToString();

            //var title = Shell.Current.CurrentItem.CurrentItem.Title;

            if (nameOfCurrentPage == "HkMobileApp.Views.ShellPages.HomePage")
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.Navigation.PushPopupAsync(new ExitPage());
                });

            }
            else
            {
                App.Current.MainPage = new AppShell();
            }

            return true;
        }


     

        private  void OnMenuItemClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync(nameof(TheRealLoginPage));


            try
            {

                var loginModel = DependencyService.Get<TheRealLoginViewModel>();

                var x = loginModel.Pin = "";
                var y = loginModel.LoginCommand.CanExecute(false);


            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Shell Pages", MemberNumber, PhoneNumber);
                Console.WriteLine(ex);
            }

            Application.Current.MainPage = new NavigationPage();
            Application.Current.MainPage = new NavigationPage(new FinalLoginPage());
        }


        private async Task GetSchemeId()
        {
            try
            {

                int SchemeId = Preferences.Get("SchemeId", 0);
                string MemberNo  = Preferences.Get("MemberNumber", "");

                string PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
                


                if (SchemeId == 0 )
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        if (ApiDetail.EndPoint == null || ApiDetail.EndPoint.Trim() == "")
                        {
                            await App.Current.MainPage.DisplayAlert("Sorry", "Something is not right, please logout and login again", "ok");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(MemberNo))
                            {


                                RestClient client = new RestClient()
                                {

                                    BaseUrl = new Uri(ApiDetail.EndPoint)

                                };

                                RestRequest restRequest = new RestRequest()
                                {
                                    Method = Method.POST,
                                    Resource = "/Members/VerifyDetails"
                                };


                                object payload = new
                                {
                                    MemberNumber = MemberNo,
                                    AddressMobileNumber = PhoneNumber
                                };



                                restRequest.AddJsonBody(payload);


                                var response = await Task.Run(() =>
                                {
                                    return client.Execute(restRequest);
                                });


                                try
                                {
                                    if (response.IsSuccessful)
                                    {

                                        var verifyMember = JsonConvert.DeserializeObject<VerifyMember>(response.Content);

                                        if (verifyMember.success)
                                        {

                                            Preferences.Set("SchemeId", verifyMember.data.bls_member.scheme_id);
                                            Preferences.Set("dateofbirth", verifyMember.data.bls_member.date_of_birth);
                                            Preferences.Set("gender", verifyMember.data.bls_member.gender);

                                            //Preferences.Set("MemberNumber", verifyMember.data.bls_member.member_id);

                                            SchemeId = verifyMember.data.bls_member.scheme_id;

                                        }
                                        else
                                        {
                                            //await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry, something went wrong"));
                                        }



                                    }
                                    else
                                    {

                                       // await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry, something went wrong"));
                                    }


                                }
                                catch (Exception ex)
                                {

                                    SendErrorMessageToAppCenter(ex, "Shell Pages", MemberNumber, PhoneNumber);
                                    //EnableSubmitBtn = true;

                                    //MainThread.BeginInvokeOnMainThread(async () =>
                                    //{
                                    //    await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry, something went wrong"));
                                    //});

                                }
                            }
                            else
                            {
                                //await App.Current.MainPage.DisplayAlert("Login", "Please enter Email / Pin", "ok");
                               await GetMemberNumber();
                            }
                        }


                    }
                    else
                    {
                        //await App.Current.MainPage.DisplayAlert("Internet Required", "Please switch on your data or connect to wifi before proceeding", "ok");
                    }
                }


            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Shell Pages", MemberNumber, PhoneNumber);
                Console.WriteLine(ex);
            }
        }

        private async Task GetMemberNumber()
        {
            try
            {

                string PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
                string MemberNumber = Preferences.Get("MemberNumber", "");


                if (MemberNumber == "")
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        if (ApiDetail.EndPoint == null || ApiDetail.EndPoint.Trim() == "")
                        {
                            await App.Current.MainPage.DisplayAlert("Sorry", "Something is not right, please logout and login again", "ok");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(PhoneNumber))
                            {


                                RestClient client = new RestClient()
                                {

                                    BaseUrl = new Uri(ApiDetail.EndPoint)

                                };

                                RestRequest restRequest = new RestRequest()
                                {
                                    Method = Method.GET,
                                    Resource = "/Members/GetPrincipalMemberNumber"
                                };



                                restRequest.AddQueryParameter("PhoneNumber", PhoneNumber);


                                var response = await Task.Run(() =>
                                {
                                    return client.Execute(restRequest);
                                });


                                try
                                {
                                    if (response.IsSuccessful && response.Content.Length > 2)
                                    {

                                        var verifyMember = JsonConvert.DeserializeObject<string>(response.Content);

                                        if (verifyMember != null && verifyMember.Trim() != "")
                                        {

                                            Preferences.Set("MemberNumber", verifyMember);

                                        }
                                        else
                                        {
                                            //await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry, something went wrong"));
                                        }



                                    }
                                    else
                                    {

                                        // await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry, something went wrong"));
                                    }


                                }
                                catch (Exception ex)
                                {
                                    SendErrorMessageToAppCenter(ex, "Shell Pages", MemberNumber, PhoneNumber);
                                    //EnableSubmitBtn = true;

                                    //MainThread.BeginInvokeOnMainThread(async () =>
                                    //{
                                    //    await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry, something went wrong"));
                                    //});

                                }
                            }
                            else
                            {
                                //await App.Current.MainPage.DisplayAlert("Login", "Please enter Email / Pin", "ok");
                            }
                        }


                    }
                    else
                    {
                        //await App.Current.MainPage.DisplayAlert("Internet Required", "Please switch on your data or connect to wifi before proceeding", "ok");
                    }
                }


            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Shell Pages", MemberNumber, PhoneNumber);
                Console.WriteLine(ex);
            }
        }

        public void SendErrorMessageToAppCenter(Exception ex, string NameOfModule, string MemberNumber = "", string PhoneNumber = "")
        {
            var properties = new Dictionary<string, string>
                                    {
                                        { "NameOfModule", NameOfModule },
                                        { "MemberNumber", MemberNumber},
                                        { "PhoneNumber", PhoneNumber}

                                    };
            Crashes.TrackError(ex, properties);
        }

      
        public void RegisterMyRoutes()
        {
            try
            {
                Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
                Routing.RegisterRoute(nameof(TrackClaimPage), typeof(TrackClaimPage));
                Routing.RegisterRoute(nameof(FindHospitalPage), typeof(FindHospitalPage));
                Routing.RegisterRoute(nameof(ChatWithDoctorPage), typeof(ChatWithDoctorPage));
                Routing.RegisterRoute(nameof(ManageDependantsPage), typeof(ManageDependantsPage));
                Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));
                Routing.RegisterRoute(nameof(ContactUsPage), typeof(ContactUsPage));
                Routing.RegisterRoute(nameof(ClaimDetailsPage), typeof(ClaimDetailsPage));
                Routing.RegisterRoute(nameof(AddDependantPage), typeof(AddDependantPage));
                Routing.RegisterRoute(nameof(TheRealLoginPage), typeof(TheRealLoginPage));
                Routing.RegisterRoute(nameof(PolicyLimitsPage), typeof(PolicyLimitsPage));
                Routing.RegisterRoute(nameof(PolicyDetailsPage), typeof(PolicyDetailsPage));
                Routing.RegisterRoute(nameof(EditDependantDetailsPage), typeof(EditDependantDetailsPage));
                Routing.RegisterRoute(nameof(BlogArticles), typeof(BlogArticles));
                Routing.RegisterRoute(nameof(SelectedBlogPage), typeof(SelectedBlogPage));
                Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
                Routing.RegisterRoute(nameof(ChangePhoneNumber), typeof(ChangePhoneNumber));
                Routing.RegisterRoute(nameof(SetPinPage), typeof(SetPinPage));
                Routing.RegisterRoute(nameof(ViewBomaCoverBenefitsAndPremiumsPage), typeof(ViewBomaCoverBenefitsAndPremiumsPage));
                Routing.RegisterRoute(nameof(ViewWoteCoverBenefitsAndPremiumsPage), typeof(ViewWoteCoverBenefitsAndPremiumsPage));
                Routing.RegisterRoute(nameof(BomaAddNhifDetails), typeof(BomaAddNhifDetails));
                Routing.RegisterRoute(nameof(BomaSelectCoverPage), typeof(BomaSelectCoverPage));
                Routing.RegisterRoute(nameof(BomaNextOfKinDetailsPage), typeof(BomaNextOfKinDetailsPage));
                Routing.RegisterRoute(nameof(BomaVerifyDetailsAndPayPage), typeof(BomaVerifyDetailsAndPayPage));
                Routing.RegisterRoute(nameof(UploadBomaDocuments), typeof(UploadBomaDocuments));
                Routing.RegisterRoute(nameof(AskLindaWriteUpPage), typeof(AskLindaWriteUpPage));
                Routing.RegisterRoute(nameof(TeleMedicineCallOrWebLinkPage), typeof(TeleMedicineCallOrWebLinkPage));
                Routing.RegisterRoute(nameof(OrderChronicMedicineCallOrWebLinkPage), typeof(OrderChronicMedicineCallOrWebLinkPage));
                Routing.RegisterRoute(nameof(SelectBomaOrWotePage), typeof(SelectBomaOrWotePage));
                Routing.RegisterRoute(nameof(WoteAddNhifDetails), typeof(WoteAddNhifDetails));
                Routing.RegisterRoute(nameof(WoteNextOfKinDetailsPage), typeof(WoteNextOfKinDetailsPage));
                Routing.RegisterRoute(nameof(WoteSelectCoverPage), typeof(WoteSelectCoverPage));
                Routing.RegisterRoute(nameof(WoteVerifyDetailsAndPayPage), typeof(WoteVerifyDetailsAndPayPage));
                Routing.RegisterRoute(nameof(UploadWoteDocuments), typeof(UploadWoteDocuments));
                Routing.RegisterRoute(nameof(MedicalCoverOrdersPage), typeof(MedicalCoverOrdersPage));
                Routing.RegisterRoute(nameof(SubmitComplaintsPage), typeof(SubmitComplaintsPage));
                Routing.RegisterRoute(nameof(ComplaintsPage), typeof(ComplaintsPage));
                Routing.RegisterRoute(nameof(EnableGpsPage), typeof(EnableGpsPage));
                Routing.RegisterRoute(nameof(CallBacksPage), typeof(CallBacksPage));
                Routing.RegisterRoute(nameof(RequestCallBack), typeof(RequestCallBack));
                Routing.RegisterRoute(nameof(FAQPage), typeof(FAQPage));
                Routing.RegisterRoute(nameof(ResetPinPage), typeof(ResetPinPage));
                Routing.RegisterRoute(nameof(Covid19Page), typeof(Covid19Page));

            }
            catch (Exception ex)
            {

            }
        }
    }
}
