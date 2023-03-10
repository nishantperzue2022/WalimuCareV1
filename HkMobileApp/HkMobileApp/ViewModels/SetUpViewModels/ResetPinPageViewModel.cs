using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views.SetUp.ResetPinPages;
using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels.SetUpViewModels
{
    public class ResetPinPageViewModel : AppViewModel
    {

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged(); }
        }


        private string originalPhoneNumber;
        public string OriginalPhoneNumber
        {
            get { return originalPhoneNumber; }
            set { originalPhoneNumber = value; OnPropertyChanged(); }
        }


        public ICommand SendOtpCommand { get; set; }

        public ICommand GoBackToLoginCommand { get; set; }

        public ResetPinPageViewModel()
        {
            SendOtpCommand = new Command(async () => await SendOtp());
            GoBackToLoginCommand = new Command(async () => await GoBackToLogin());
            OriginalPhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
            PhoneNumber = FormatPhoneNumberWithX(OriginalPhoneNumber);
        }


        public string StandardizePhoneNumber(string thePhoneNumber)
        {
            try
            {
                string NewPhoneNumber = "";
                NewPhoneNumber = thePhoneNumber.StartsWith("+") ? "0" + thePhoneNumber.Substring(4) : thePhoneNumber;
                NewPhoneNumber = NewPhoneNumber.StartsWith("2") ? "0" + NewPhoneNumber.Substring(3) : NewPhoneNumber;

                string PhoneNumberWithZero = NewPhoneNumber.StartsWith("0") ? NewPhoneNumber : "0" + NewPhoneNumber;

                return PhoneNumberWithZero;

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "The Real Login", "", PhoneNumber);
                return PhoneNumber;
            }
        }



        public string FormatPhoneNumberWithX(string theString)
        {
            try
            {

                theString = StandardizePhoneNumber(theString);

                var aStringBuilder = new StringBuilder(theString);

                //aStringBuilder.Remove(6,2); first argument represents position, next argument represents number of characters
                aStringBuilder.Remove(4, 4);
                aStringBuilder.Insert(4, "XXXX");
                theString = aStringBuilder.ToString();

                return theString;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured");
                return null;
            }
        }

        public async Task SendOtp()
        {
            try
            {
                IsBusy = true;
                EnableSubmitBtn = false;

               

                await ShowLoadingMessage("Please wait as we Send OTP");

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
                    AddressMobileNumber = OriginalPhoneNumber

                };


                restRequest.AddJsonBody(payload);


                var response = await Task.Run(() =>
                {
                    return client.Execute(restRequest);
                });

                var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<object>>(response.Content);

                if (deserializedResponse.success)
                {

                    await ShowSuccessMessage("OTP has been Sent successully");
                    
                    Thread.Sleep(2000);

                    await App.Current.MainPage.Navigation.PushAsync(new ConfirmOtpResetPage());

                    await RemoveLoadingMessage();

                  
                }
                else
                {
                    await ShowErrorMessage("Sorry, something went wrong, please resend OTP");

                }

                IsBusy = false;
                EnableSubmitBtn = true;
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Confirm Otp", "", OriginalPhoneNumber);

                await ShowErrorMessage();

               
            }
        }

        public async Task GoBackToLogin()
        {
            try
            {

                await App.Current.MainPage.Navigation.PopAsync();

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Reset Pin");
            }
        }


    }
}
