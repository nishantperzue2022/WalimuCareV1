using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views;
using HkMobileApp.Views.Others;
using HkMobileApp.Views.PopUps;
using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class UserProfileViewModel:AppViewModel
    {

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }



        private string memberNumber;
        public string MemberNumber
        {
            get { return memberNumber; }
            set { memberNumber = value;
                OnPropertyChanged(nameof(MemberNumber));
            }
        }


        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }



        private string email;
        public string Email
        {
            get { return email; }
            set { email = value;
                OnPropertyChanged(nameof(Email));
            }
        }



        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }



        private string schemeName;
        public string SchemeName
        {
            get { return schemeName; }
            set { schemeName = value;
                OnPropertyChanged(nameof(SchemeName));
            }
        }



        private string jobGroup;
        public string JobGroup
        {
            get { return jobGroup; }
            set { jobGroup = value;
                OnPropertyChanged(nameof(JobGroup));
            }
        }


        private string photoPath;
        public string PhotoPath
        {
            get { return photoPath; }
            set { photoPath = value;
                OnPropertyChanged(nameof(PhotoPath));
            }
        }


        private string newPhoneNumber;
        public string NewPhoneNumber
        {
            get { return newPhoneNumber; }
            set { newPhoneNumber = value; OnPropertyChanged(nameof(NewPhoneNumber)); CheckIfPhoneNumberIsCorrect(); }
        }


        private string dateOfBirth;
        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; 
                OnPropertyChanged(nameof(DateOfBirth)); }
        }


        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value;
                OnPropertyChanged(nameof(Gender)); }
        }


        public ICommand ShowChangePhoneNumberCommand { get; set; }

        public ICommand TakePhotoCommand { get; set; }

        public ICommand ReturnToHomePageCommand { get; set; }

        public ICommand UpdateNewPhoneNumberCommand { get; set; }

        public ICommand ShowChangePinPageCommand { get; set; }

        public ICommand PickPictureCommand { get; set; }

        public ICommand SelectOptionsCommand { get; set; }

        public ICommand RemovePhotoCommand { get; set; }

        public ICommand ClosePopUpCommand { get; set; }


        public UserProfileViewModel()
        {
            try
            {
                SetData();
                ShowChangePhoneNumberCommand = new Command(async () => await ShowChangePhoneNumberPage());
                TakePhotoCommand = new Command(async () => await TakePhoto());
                ReturnToHomePageCommand = new Command(ReturnToHomePage);
                UpdateNewPhoneNumberCommand = new Command(async () => await UpdateNewPhoneNumber());
                ShowChangePinPageCommand = new Command(ShowChangePinPage);
                SelectOptionsCommand = new Command(async () => await SelectOptions());
                RemovePhotoCommand = new Command(RemovePhoto);
                PickPictureCommand = new Command(async () => await PickPicture());
                ClosePopUpCommand = new Command(async()=> await ClosePopUp());
                EnableSubmitBtn = false;

                PageTitle = "User Profile";
                PageSubTitle = "View and Edit your details";
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile", "", "");

            }
        }

        public async Task ShowChangePhoneNumberPage()
        {
            try
            {
                //await App.Current.MainPage.Navigation.PushAsync(new ChangePhoneNumber());
                await Shell.Current.GoToAsync(nameof(ChangePhoneNumber));
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile", "", "");
            }
        }

        public async void SetData()
        {
            try
            {

                PageTitle = "Profile";
                string firstName = Preferences.Get(nameof(AspNetUsers.firstName), "");
                string SecondName = Preferences.Get(nameof(AspNetUsers.lastName), "");
                FullName = firstName + " " + SecondName;

                PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), null);
                MemberNumber = Preferences.Get("MemberNumber", "");
                Email = Preferences.Get(nameof(AspNetUsers.email), "");
                UserName = Preferences.Get(nameof(AspNetUsers.userName), "");

                string theDateOfBirth = Preferences.Get("dateofbirth", "");
                int theGender = Preferences.Get("gender", 3);

                DateOfBirth = theDateOfBirth;

                if(theGender != 3)
                {
                    Gender = theGender == 1 ? "Male" : "Female";
                }

                DateTime usersBirthYear = new DateTime();

                var changeIsSuccessful = DateTime.TryParse(theDateOfBirth, out usersBirthYear);

                if (changeIsSuccessful)
                {
                    DateOfBirth = usersBirthYear.ToString("dd-MMM-yyyy");
                }
                else
                {
                    DateOfBirth = theDateOfBirth;

                }


                await GetSchemeNameAndJobGroup();

                PhotoPath = Preferences.Get("ProfilePhoto", "avator");
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
            }
        }

        public async Task GetSchemeNameAndJobGroup()
        {
            try
            {
                await ShowLoadingMessage();
                RestClient client = new RestClient()
                {

                    BaseUrl = new Uri(ApiDetail.EndPoint)

                };

                RestRequest restRequest = new RestRequest()
                {
                    Method = Method.POST,
                    Resource = "/Members/GetSchemeNameAndJobGroup"
                };


                restRequest.AddQueryParameter("MemberNumber", MemberNumber);


                var response = await Task.Run(() =>
                {
                    return client.Execute(restRequest);
                });

                await RemoveLoadingMessage();
                if (response.IsSuccessful)
                {
                    var result = JsonConvert.DeserializeObject<BaseResponse<SchemeAndJobGroup>>(response.Content);

                    if (result.success)
                    {
                      
                        JobGroup = result.data.jobGroup;
                        SchemeName = result.data.schemeName;
                    }
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
            }
        }

        public async Task TakePhoto()
        {
            try
            {

                var photo = await MediaPicker.CapturePhotoAsync();
                //var photo = await MediaPicker.PickPhotoAsync();
                // canceled
                if (photo == null)
                {
                    PhotoPath = null;
                    return;
                }

                long FileSize = 0;

                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);

                    FileSize = stream.Length;
                }


                if (FileSize > 5000000)
                {
                    await ShowInfoMessage("File should be less than 5 mbz");
                    return;
                }

                Preferences.Set("ProfilePhoto", newFile);

                PhotoPath = newFile;

                DependencyService.Get<HomePageViewModel>().GetProfilePicture();
                DependencyService.Get<AppShellViewModel>().GetProfilePicture();
                await RemoveLoadingMessage();
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
            }
        }

        public void ReturnToHomePage()
        {
            try
            {
                Application.Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
            }
        }

        public async Task UpdateNewPhoneNumber()
        {
            try
            {
                await ShowLoadingMessage();
                RestClient client = new RestClient()
                {

                    BaseUrl = new Uri(ApiDetail.EndPoint)

                };

                //RestRequest restRequest = new RestRequest()
                //{
                //    Method = Method.POST,
                //    Resource = "/Members/GetSchemeNameAndJobGroup"
                //};


                //restRequest.AddQueryParameter("MemberNumber", MemberNumber);


                //var response = await Task.Run(() =>
                //{
                //    return client.Execute(restRequest);
                //});

                await Task.Delay(5000);

                await RemoveLoadingMessage();

                await ShowSuccessMessage("Phone Number Update Request Sent Successfully");

                await App.Current.MainPage.Navigation.PopAsync();

                await RemoveLoadingMessage();

                //if (response.IsSuccessful)
                //{
                //    var result = JsonConvert.DeserializeObject<BaseResponse<SchemeAndJobGroup>>(response.Content);

                //    if (result.success)
                //    {

                //        JobGroup = result.data.jobGroup;
                //        SchemeName = result.data.schemeName;
                //    }
                //}
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Change Phone Number", MemberNumber, PhoneNumber);
            }
        }

        private void CheckIfPhoneNumberIsCorrect()
        {
            try
            {
                if(NewPhoneNumber!=null && NewPhoneNumber.Length >= 10)
                {
                    EnableSubmitBtn = true;
                }
                else
                {
                    EnableSubmitBtn = false;
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Change Phone Number", MemberNumber, PhoneNumber);
            }
        }

        private void ShowChangePinPage()
        {
            try
            {
                // await Shell.Current.GoToAsync($"{nameof(SetPinPage)}?IsChangPasswordRequest=true");
                App.Current.MainPage = new NavigationPage(new SetPinPage(true));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile", MemberNumber, PhoneNumber);
            }
        }

        public async Task PickPicture()
        {
            try
            {
                //var photo = await MediaPicker.CapturePhotoAsync();
                var photo = await MediaPicker.PickPhotoAsync();
                // canceled
                if (photo == null)
                {
                    PhotoPath = null;
                    return;
                }

                long FileSize = 0;

                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);

                    FileSize = stream.Length;
                }


                if (FileSize > 5000000)
                {
                    await ShowInfoMessage("File should be less than 5 mbz");
                    return;
                }

                Preferences.Set("ProfilePhoto", newFile);

                PhotoPath = newFile;

                DependencyService.Get<HomePageViewModel>().GetProfilePicture();
                DependencyService.Get<AppShellViewModel>().GetProfilePicture();
                await RemoveLoadingMessage();
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile"); 
            }
        }

        public async Task SelectOptions()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new SelectMediaPage());
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "User Profile");
            }
        }

        public async void RemovePhoto()
        {
            try
            {
                Preferences.Set("ProfilePhoto", "avator.png");

                PhotoPath = "avator.png";

                DependencyService.Get<HomePageViewModel>().GetProfilePicture();
                DependencyService.Get<AppShellViewModel>().GetProfilePicture();
                await RemoveLoadingMessage();
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile");
            }
        }

        public async Task ClosePopUp()
        {
            try
            {
                await RemoveLoadingMessage();
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "User Profile");
            }
        }

    }
}
