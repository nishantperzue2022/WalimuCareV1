using HkMobileApp.Models;
using HkMobileApp.Services;
using HkMobileApp.Views;
using HkMobileApp.Views.Others;
using HkMobileApp.Views.ShellPages;
using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace HkMobileApp.ViewModels
{
    public class AppViewModel : INotifyPropertyChanged
    {
        MaterialSnackbarConfiguration snackbarConfiguration = new MaterialSnackbarConfiguration() { BackgroundColor = Color.FromHex("3FAC49"), MessageTextColor = Color.Black, Margin = 0, CornerRadius = 0 };

        public AppViewModel()
        {
            IsRefreshing = false;
            BackButtonCommand = new Command<string>(async x => await BackButton(x));
            IsNavBarVisible = false;
        }

        private bool isBusy { get; set; }

        public bool IsBusy
        {

            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private bool enableSubmitBtn { get; set; }

        public bool EnableSubmitBtn
        {
            get { return enableSubmitBtn; }
            set
            {
                enableSubmitBtn = value;
                OnPropertyChanged(nameof(EnableSubmitBtn));
            }
        }

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));

                Console.WriteLine($"Is Refreshing {IsRefreshing}");
            }
        }

        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                OnPropertyChanged();
            }
        }


        private string pageSubTitle;
        public string PageSubTitle
        {
            get { return pageSubTitle; }
            set { pageSubTitle = value; }
        }

        private bool isEmptyIllustrationVisible;
        public bool IsEmptyIllustrationVisible
        {
            get { return isEmptyIllustrationVisible; }
            set
            {
                isEmptyIllustrationVisible = value;
                OnPropertyChanged();
            }
        }




        private string noDataAvailableMessage;
        public string NoDataAvailableMessage
        {
            get { return noDataAvailableMessage; }
            set
            {
                noDataAvailableMessage = value;
                OnPropertyChanged();
            }
        }



        private bool isListViewVisible;
        public bool IsListViewVisible
        {
            get { return isListViewVisible; }
            set { isListViewVisible = value; OnPropertyChanged(); }
        }


        private bool isNavBarVisible;
        public bool IsNavBarVisible
        {
            get { return isNavBarVisible; }
            set { isNavBarVisible = value;  OnPropertyChanged(); }
        }


        private bool isCustomNavBrVisible;
        public bool IsCustomNavBarVisible
        {
            get { return isCustomNavBrVisible; }
            set { isCustomNavBrVisible = value;  OnPropertyChanged(); }
        }





        public ICommand BackButtonCommand { get; set; }



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

        public async Task ShowInfoMessage(string Message = "")
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
                string msg = "This is some info";

                if (Message != null && Message.Trim() != "")
                {
                    msg = Message;
                }

                await Application.Current.MainPage.Navigation.PushPopupAsync(new HkInfoPage(msg));

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void SendErrorMessageToAppCenter(Exception ex, string NameOfModule, string MemberNumber = "", string PhoneNumber = "")
        {
            MemberNumber = Preferences.Get("MemberNumber", "");
            PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
            string ErrorMessage = ex.Message;

            var properties = new Dictionary<string, string>
                                    {
                                        { "NameOfModule", NameOfModule },
                                        { "MemberNumber", MemberNumber},
                                        { "PhoneNumber", PhoneNumber},
                                        { "ErrorMessage", ErrorMessage}

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


        public async Task GoToHomePage()
        {
            try
            {
               

                //var count =  Shell.Current.Navigation.NavigationStack.Count;

                //if (count > 1)
                //{
                //    //await Shell.Current.Navigation.PopAsync();
                //}
                //else
                //{
                //    await Shell.Current.GoToAsync(nameof(ContactUsPage));
                //}

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "App View Model");
            }
        }


        public async Task BackButton(string Route)
        {
            try
            {
                // await App.Current.MainPage.Navigation.PopAsync();

                //if (Route.ToLower().Contains("SubmitComplaintsPage".ToLower()))
                //{
                //    DependencyService.Get<SubmitComplaintsViewModel>().IsNavBarVisible = true;
                //    DependencyService.Get<SubmitComplaintsViewModel>().IsCustomNavBarVisible = false;
                //}



                //await Shell.Current.GoToAsync(Route);

                if (Route.Contains("HomePage"))
                {
                    await Shell.Current.GoToAsync(Route);
                }

                await Shell.Current.GoToAsync("..");


            }
            catch (Exception ex)
            {

                Route = Route.TrimStart('/');

                await Shell.Current.GoToAsync(Route);

                SendErrorMessageToAppCenter(ex, "App View Model");
            }
        }




    }
}
