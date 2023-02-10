using Rg.Plugins.Popup.Extensions;
using HkMobileApp.Models;
using HkMobileApp.Services;
using HkMobileApp.Views;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using HkMobileApp.Models.HkApiResponses;
using System.Threading;

namespace HkMobileApp.ViewModels.SetUpViewModels
{
    public class SignUpViewModel : AppViewModel
    {

        private string memberNumber;

        public string MemberNumber
        {
            get { return memberNumber; }
            set
            {
                memberNumber = value;
                OnPropertyChanged(nameof(MemberNumber));
            }
        }


        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public bool IsPopUpInStack = false;

        private VerifyMember verifyMember;

        public VerifyMember VerifyMember
        {
            get { return verifyMember; }
            set { verifyMember = value; }
        }


        public SignUpViewModel()
        {
            SignUpCommand = new Command(async()=>await SignUp());
            EnableSubmitBtn = true;
        }

        public ICommand SignUpCommand { get; set; }

        public async Task SignUp()
        {
            try
            {
              
                IsPopUpInStack = true;

                if (await CheckInternetConnectivity())
                {
                    if(await CheckIfApiDetailsAreSetUp())
                    {
                        if (!string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(MemberNumber))
                        {
                            IsBusy = true;

                            EnableSubmitBtn = false;
                           
                            await ShowLoadingMessage("Please wait as we verify your details");

                            Preferences.Clear();

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
                                MemberNumber = MemberNumber,
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
                                        VerifyMember = verifyMember;

                                        Preferences.Set("SchemeId", verifyMember.data.bls_member.scheme_id);
                                        Preferences.Set("MemberNumber", verifyMember.data.bls_member.member_id);


                                        EnableSubmitBtn = true;

                                        if (await SendOtp())
                                        {
                                          

                                            MainThread.BeginInvokeOnMainThread(() =>
                                            {
                                                App.Current.MainPage = new NavigationPage(new ConfirmOtpPage());
                                            });
                                        }
                                       

                                       
                                    }
                                    else
                                    {

                                        await ShowErrorMessage("Sorry, Member Number and Phone Number do not match");
                                        EnableSubmitBtn = true;
                                    }



                                }
                                else if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                                {
                                    await ShowErrorMessage("Sorry something went wrong please try again later");
                                }
                                else
                                {
                                    EnableSubmitBtn = true;
                                    await ShowErrorMessage("Sorry something went wrong please try again later");
                                }


                            }
                            catch (Exception ex)
                            {
                                EnableSubmitBtn = true;

                                await ShowErrorMessage();
                                SendErrorMessageToAppCenter(ex, "Sign Up", MemberNumber, PhoneNumber);

                            }
                        }
                        else
                        {
                            await ShowErrorMessage("Please enter Email / Pin");

                        }
                    }

                }
               

            }
            catch (Exception ex)
            {
                await ShowErrorMessage();
                SendErrorMessageToAppCenter(ex, "Sign Up", MemberNumber, PhoneNumber);
            }
            finally
            {
                EnableSubmitBtn = true;
            }
        }

        public async Task<bool> SendOtp()
        {
            try
            {
                IsBusy = true;
                EnableSubmitBtn = false;

                await ShowLoadingMessage("Member details verified successfully, we are sending you a One Time PIN. ");

                RestClient client = new RestClient()
                {

                    BaseUrl = new Uri(ApiDetail.EndPoint)

                };

                RestRequest restRequest = new RestRequest()
                {
                    Method = Method.POST,
                    Resource = "/Members/SendOTP"
                };


                object payload = new
                {
                    AddressMobileNumber = PhoneNumber,
                };



                restRequest.AddJsonBody(payload);


                var response = await Task.Run(() =>
                {
                    return client.Execute(restRequest);
                });


                var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<object>>(response.Content);

                if (deserializedResponse.success)
                {

                     
                    await ShowSuccessMessage("We have sent an One Time Pin to your phone number");

                    Thread.Sleep(3000);

                    await App.Current.MainPage.Navigation.PopPopupAsync();

                    return true;

                }
                else
                {

                    await ShowErrorMessage("Sorry, something went wrong when sending One Time Pin, please try again later");

                    return false;
                }

               

            }
            catch (Exception ex)
            {
                
                await ShowErrorMessage();
                SendErrorMessageToAppCenter(ex, "Sign Up", MemberNumber, PhoneNumber);
                return false;
            }
            finally
            {
                EnableSubmitBtn = true;
            }
        }

    }
}
