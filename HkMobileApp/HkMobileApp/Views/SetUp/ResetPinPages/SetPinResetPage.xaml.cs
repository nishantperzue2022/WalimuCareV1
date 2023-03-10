using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.ViewModels;
using HkMobileApp.ViewModels.SetUpViewModels;
using HkMobileApp.Views.Others;
using HkMobileApp.Views.PopUps;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
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
    public partial class SetPinResetPage : ContentPage
    {
        public string Pin { get; set; }


        public string ConfirmPin { get; set; }


        public string PhoneNumber { get; set; }


        public bool IsChangePasswordRequest { get; set; }



        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                OnPropertyChanged(nameof(PageTitle));
            }
        }



        private bool isBackButtonVisible;
        public bool IsBackButtonVisible
        {
            get { return isBackButtonVisible; }
            set {
                isBackButtonVisible = value;  
                OnPropertyChanged();
            }
        }


        public SetPinResetPage(bool IsChangPasswordRequest = false)
        {
            InitializeComponent();

            if (IsChangPasswordRequest)
            {
                PageTitle = "Change PIN";
            }
            else
            {
                PageTitle = "Set New PIN";
            }

            IsChangePasswordRequest = IsChangPasswordRequest;

            if (IsChangePasswordRequest)
            {
                IsBackButtonVisible = true;
            }
            else
            {
                IsBackButtonVisible = false;
            }

            PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");


        }

        protected override void OnAppearing()
        {
            BindingContext = this;

            txtPin1.Focus();
            txtPin1.TextChanged += TxtPin1_TextChanged;
            txtPin2.TextChanged += TxtPin2_TextChanged;
            txtPin3.TextChanged += TxtPin3_TextChanged;
            txtPin4.TextChanged += TxtPin4_TextChanged;

            txtConfirmPin1.TextChanged += TxtConfirmPin1_TextChanged;
            txtConfirmPin2.TextChanged += TxtConfirmPin2_TextChanged;
            txtConfirmPin3.TextChanged += TxtConfirmPin3_TextChanged;
            txtConfirmPin4.TextChanged += TxtConfirmPin4_TextChanged;

            var data = DependencyService.Get<ConfirmMemberDetailsViewModel>();
            PhoneNumber = data.PhoneNumber;

            
        }

        private async void TxtConfirmPin4_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue != "")
                {
                    ConfirmPin = txtConfirmPin1.Text + txtConfirmPin2.Text + txtConfirmPin3.Text + txtConfirmPin4.Text;

                    if (Pin == ConfirmPin)
                    {
                        //await App.Current.MainPage.Navigation.PushPopupAsync(new HkSuccessPage("Pin is confirmed"));
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Pin Do not match"));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TxtConfirmPin3_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue != "")
                {
                    txtConfirmPin4.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TxtConfirmPin2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue != "")
                {
                    txtConfirmPin3.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TxtConfirmPin1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue != "")
                {
                    txtConfirmPin2.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TxtPin4_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue != "")
                {
                    txtConfirmPin1.Focus();

                    Pin = txtPin1.Text + txtPin2.Text + txtPin3.Text + txtPin4.Text;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TxtPin3_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue != "")
                {
                    txtPin4.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TxtPin2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue != "")
                {
                    txtPin3.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TxtPin1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (e.NewTextValue != "")
                {
                    txtPin2.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async void SetPin()
        {
            try
            {


                RestClient client = new RestClient()
                {

                    BaseUrl = new Uri(ApiDetail.EndPoint)

                };

                RestRequest restRequest = new RestRequest()
                {
                    Method = Method.POST,
                    Resource = "/Members/ReSetMemberPin"
                };

                string memberNumber = Preferences.Get(nameof(AspNetUsers.memberId), "");

                object payload = new
                {
                    MemberId = memberNumber,
                    PinHash = Pin
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

                        var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<PasswordSetViewModel>>(response.Content);

                        if (deserializedResponse.success)
                        {
                            await Navigation.PopAllPopupAsync();
                            await App.Current.MainPage.Navigation.PushPopupAsync(new HkSuccessPage("Pin Set Successfully"));

                            Thread.Sleep(1000);

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                try
                                {
                                    await Navigation.PopAllPopupAsync();
                                }
                                catch (Exception)
                                {


                                }

                                if (!IsChangePasswordRequest)
                                {

                                    await Task.Run(async () => await SubmitLogin());
                                }
                                else
                                {
                                    App.Current.MainPage = new AppShell();
                                    await Shell.Current.GoToAsync(nameof(ProfilePage));

                                }

                               
                            });

                        }
                        else
                        {
                            await Navigation.PopAllPopupAsync();
                            await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry something went wrong when setting pin"));

                        }



                    }
                    else
                    {
                        await Navigation.PopAllPopupAsync();
                        await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry something went wrong when setting pin"));
                    }


                }
                catch (Exception ex)
                {
                    SendErrorMessageToAppCenter(ex, "Set Pin", "", PhoneNumber);
                    await Navigation.PopAllPopupAsync();
                    await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry something went wrong when setting pin"));
                }
            }
            catch (Exception ex)
            {
                await Navigation.PopAllPopupAsync();
                await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry something went wrong when setting pin"));
            }
        }

        public async void btnSetPin_Clicked(object sender, EventArgs e)
        {
            try
            {
                Pin = "";
                Pin = txtPin1.Text.Trim() + txtPin2.Text.Trim() + txtPin3.Text.Trim() + txtPin4.Text.Trim();

                ConfirmPin = "";
                ConfirmPin = txtConfirmPin1.Text.Trim() + txtConfirmPin2.Text.Trim() + txtConfirmPin3.Text.Trim() + txtConfirmPin4.Text.Trim();


                if (Pin != ConfirmPin)
                {
                    await DependencyService.Get<AppViewModel>().ShowErrorMessage("Pin and Confirm Pin Do Not Match");
                    return;
                }

                await DependencyService.Get<AppViewModel>().ShowLoadingMessage("Please wait as we set up your Pin");


                await Task.Run(() =>
                {
                    SetPin();
                });


            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Set Pin", "", PhoneNumber);
                await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry something went wrong when setting pin"));

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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!IsChangePasswordRequest)
                {
                    await Navigation.PushAsync(new FinalLoginPage());
                }
                else
                {
                    App.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync(nameof(ProfilePage));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        } 
        
        private  void ussd_Tapped(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("*506#");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task SubmitLogin()
        {
            try
            {
                PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");

                if (await CheckInternetConnectivity())
                {
                    if (await CheckIfApiDetailsAreSetUp())
                    {
                        if (!string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(Pin))
                        {

                            await ShowLoadingMessage("Please wait as we sign you in");

                            RestClient client = new RestClient()
                            {

                                BaseUrl = new Uri(ApiDetail.EndPoint)

                            };

                            RestRequest restRequest = new RestRequest()
                            {
                                Method = Method.POST,
                                Resource = "/Registration/GetUser"
                            };

                            string phoneNumberMain = PhoneNumber.StartsWith("0") ? PhoneNumber.TrimStart('0') : PhoneNumber;

                            object registration = new
                            {
                                pin = Convert.ToInt32(Pin),
                                PhoneNumber = phoneNumberMain
                            };


                            restRequest.AddJsonBody(registration);


                            var response = await Task.Run(() =>
                            {
                                return client.Execute(restRequest);
                            });

                            AspNetUsers user = new AspNetUsers();

                            try
                            {
                                if (response.IsSuccessful && response.Content.Length > 2)
                                {

                                  

                                    var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<AspNetUsers>>(response.Content);

                                    if (deserializedResponse.success)
                                    {

                                        user = deserializedResponse.data;


                                        Preferences.Set(nameof(user.email), user.email);
                                        Preferences.Set(nameof(user.pinHash), user.pinHash);
                                        Preferences.Set(nameof(user.firstName), user.firstName);
                                        Preferences.Set(nameof(user.lastName), user.lastName);
                                        Preferences.Set(nameof(user.userName), user.userName);
                                        Preferences.Set(nameof(user.id), user.id);
                                        Preferences.Set(nameof(user.memberId), user.memberId);
                                        Preferences.Set(nameof(user.phoneNumber), user.phoneNumber.ToString());

                                        if (string.IsNullOrEmpty(user.schemeId))
                                        {
                                            Preferences.Set("SchemeId", Convert.ToInt32(user.schemeId));
                                        }





                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            Application.Current.MainPage = new AppShell();

                                        });

                                        await RemoveLoadingMessage();
                                    }
                                    else if (deserializedResponse.message.ToLower().Contains("wrong"))
                                    {
                                        Pin = "";
                                        await ShowErrorMessage("Sorry,you have entered a wrong pin ,please try again");
                                    }
                                    else
                                    {
                                        Pin = "";
                                        await ShowErrorMessage();

                                    }


                                }
                                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                                {
                                   
                                    Pin = "";

                                    try
                                    {

                                        await ShowErrorMessage("Ooops, Something is not right, try again later. If this persists please consult the Admin");

                                        return;


                                    }
                                    catch (Exception ex)
                                    {
                                        SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);
                                    }
                                }
                                else
                                {
                                   
                                    Pin = "";

                                    try
                                    {

                                        await ShowErrorMessage("Ooops Login failed , Incorrect username / password combination");

                                        return;


                                    }
                                    catch (Exception ex)
                                    {
                                        SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);
                                    }
                                }


                            }
                            catch (Exception ex)
                            {
                               
                                Pin = "";

                                try
                                {

                                    SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

                                    await ShowErrorMessage("Ooops Login failed , Something went wrong please try again later");

                                    Thread.Sleep(4000);
                                }
                                catch (Exception e)
                                {


                                }

                            }
                        }
                        else
                        {

                            try
                            {

                                await ShowErrorMessage("Please enter Phone Number / Pin");

                                return;
                            }
                            catch (Exception ex)
                            {
                                SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

                            }




                        }
                    }


                }
                else
                {

                    try
                    {

                        await ShowErrorMessage("Please switch on your data or connect to wifi before proceeding");

                        return;
                    }
                    catch (Exception ex)
                    {
                        SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);

                    }

                }



            }
            catch (Exception ex)
            {
               

                try
                {


                    SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);
                    await ShowErrorMessage();


                    Thread.Sleep(2000);
                }
                catch (Exception e)
                {


                }

            }
            finally
            {
                IsBusy = false;
              
                Pin = "";
                try
                {
                    //await App.Current.MainPage.Navigation.PopAllPopupAsync();
                }
                catch (Exception ex)
                {
                    SendErrorMessageToAppCenter(ex, "Login", "", PhoneNumber);
                    Console.WriteLine(ex);
                }

            }

        }

        public async Task ShowErrorMessage(string Message = "")
        {

            try
            {
                await Application.Current.MainPage.Navigation.PopAllPopupAsync();
            }
            catch (Exception ex)
            {

            }

            try
            {
                string msg = "Sorry something went wrong, please try again after sometime";

                if (Message != null && Message.Trim() != "")
                {
                    msg = Message;
                }

                await Application.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage(msg));

            }
            catch (Exception ex)
            {

            }
        }

        public async Task ShowSuccessMessage(string Message = "")
        {

            try
            {
                await Application.Current.MainPage.Navigation.PopAllPopupAsync();
            }
            catch (Exception ex)
            {

            }

            try
            {
                string msg = "Successful";

                if (Message != null && Message.Trim() != "")
                {
                    msg = Message;
                }

                await Application.Current.MainPage.Navigation.PushPopupAsync(new HkSuccessPage(msg));


                //await MaterialDialog.Instance.SnackbarAsync(msg, 7000 , snackbarConfiguration);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task ShowLoadingMessage(string Message = "")
        {

            try
            {
                await Application.Current.MainPage.Navigation.PopAllPopupAsync();
            }
            catch (Exception ex)
            {

            }

            try
            {
                string msg = "Please wait";

                if (Message != null && Message.Trim() != "")
                {
                    msg = Message;
                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new HkLoaderPage(msg));

                    //await MaterialDialog.Instance.LoadingSnackbarAsync(msg, snackbarConfiguration);
                    //await MaterialDialog.Instance.LoadingDialogAsync(msg);

                });



            }
            catch (Exception ex)
            {

            }
        }

        public async Task RemoveLoadingMessage()
        {
            try
            {
                await App.Current.MainPage.Navigation.PopAllPopupAsync();
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "App View Model", "", "");
            }
        }

        public async Task<bool> CheckInternetConnectivity()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    return true;
                }
                else
                {
                    await ShowErrorMessage("Sorry Please switch on your data or connect to wifi before proceeding");
                    return false;
                }


            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Base View Model");
                return false;
            }
        }

        public async Task<bool> CheckIfApiDetailsAreSetUp()
        {
            try
            {

                if (ApiDetail.EndPoint == null || ApiDetail.EndPoint.Trim() == "")
                {

                    await ShowErrorMessage("Sorry Something is not right, please logout and login again");
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Base View Model");
                return false;
            }
        }

    }
}