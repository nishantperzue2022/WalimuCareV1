using HkMobileApp.Services;
using HkMobileApp.Views;
using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class BaseContentPage : ContentPage
    {

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

                await Application.Current.MainPage.Navigation.PushPopupAsync(new HkLoaderPage(msg));

            }
            catch (Exception ex)
            {

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
