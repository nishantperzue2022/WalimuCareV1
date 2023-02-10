using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views.MedicalCover;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels.MedicalCoverViewModels
{
    public class BomaBuyMedicalCoverViewModel : AppViewModel
    {
        //private AgentLead agentLeadDTO;
        //public AgentLead AgentLeadDTO
        //{
        //    get { return agentLeadDTO; }
        //    set { agentLeadDTO = value; OnPropertyChanged(); }
        //}

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value;

                OnPropertyChanged();
                CheckIfModelIsValid();
            }
        }


        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value;
                OnPropertyChanged();
                CheckIfModelIsValid();
            }
        }


        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value;
                OnPropertyChanged();
                CheckIfModelIsValid();
            }
        }


        private string nhifNumber;
        public string NhifNumber
        {
            get { return nhifNumber; }
            set { nhifNumber = value;OnPropertyChanged(); CheckIfModelIsValid(); }
        }



        private PostalCode selectedPostalCode;
        public PostalCode SelectedPostalCode
        {
            get { return selectedPostalCode; }
            set
            {
                selectedPostalCode = value;
                OnPropertyChanged();
            }
        }


        private string postalAddress;
        public string PostalAddress
        {
            get { return postalAddress; }
            set { postalAddress = value; OnPropertyChanged(); CheckIfModelIsValid(); }
        }


        private string physicalAddress;
        public string PhysicalAddress
        {
            get { return physicalAddress; }
            set { physicalAddress = value; OnPropertyChanged(); CheckIfModelIsValid(); }
        }


        private County selectedCounty;
        public County SelectedCounty
        {
            get { return selectedCounty; }
            set { selectedCounty = value;
                OnPropertyChanged();
                CheckIfModelIsValid();
            }
        }


        private FileData nationalIdFile;
        public FileData NationalIdFile
        {
            get { return nationalIdFile != null ? nationalIdFile : nationalIdFile = new FileData() { FileName = "No File Selected" }; }
            set { nationalIdFile = value; OnPropertyChanged(); }
        }


        private FileData passportFile;
        public FileData PassportFile
        {
            get { return passportFile != null ? passportFile : passportFile = new FileData() { FileName = "No File Selected" }; }
            set { passportFile = value; OnPropertyChanged(); }
        }

        private FileData nhifCardFile;
        public FileData NhifCardFile
        {
            get { return nhifCardFile != null ? nhifCardFile : nhifCardFile = new FileData() { FileName = "No File Selected" }; }
            set
            {
                nhifCardFile = value;
                OnPropertyChanged();
            }
        }


        private bool agreeToTermsAndConditions;
        public bool AgreeToTermsAndConditions
        {
            get { return agreeToTermsAndConditions; }
            set { agreeToTermsAndConditions = value; OnPropertyChanged(); EnableDisableSubmitButton(); CheckIfModelIsValid(); }
        }


        private List<PostalCode> postalCodes;
        public List<PostalCode> PostalCodes
        {
            get { return postalCodes; }
            set { postalCodes = value; OnPropertyChanged(); }
        }


        private List<County> counties;
        public List<County> Counties
        {
          
            get { return counties; }
            set { counties = value;
                OnPropertyChanged(); }
        }


        private bool enableProceedButton;
        public bool EnableProceedButton
        {
            get { return enableProceedButton; }
            set { enableProceedButton = value;OnPropertyChanged(); }
        }

        private bool isMaleSelected;
        public bool IsMaleSelected
        {
            get { return isMaleSelected; }
            set { isMaleSelected = value; 
                OnPropertyChanged(); }
        }


        private bool isFeMaleSelected;
        public bool IsFeMaleSelected
        {
            get { return isFeMaleSelected; }
            set
            {
                isFeMaleSelected = value;
                OnPropertyChanged();
            }
        }


        public bool SubmitButtonHasBeenClicked { get; set; }

        public ICommand ShowNextOfKinCommand { get; set; }

        public ICommand SelectGenderCommand { get; set; }

        public ICommand UploadDocumentsPageCommand { get; set; }

        public ICommand SelectNhifCardCommand { get; set; }
        public ICommand SelectNationalIdFileCommand { get; set; }
        public ICommand SelectPassportPhotoCommand { get; set; }

        public BomaBuyMedicalCoverViewModel()
        {
            ShowNextOfKinCommand = new Command(async () => await ShowNextOfKin());
            SelectGenderCommand = new Command<string>(x => SelectGender(x));
            UploadDocumentsPageCommand = new Command(async () => await UploadDocumentsPage());
            SelectNhifCardCommand = new Command(async () => await SelectNhifCard());
            SelectNationalIdFileCommand = new Command(async () => await SelectNationalIdFile());
            SelectPassportPhotoCommand = new Command(async () => await SelectPassportPhoto());


            PageTitle = "Boma Afya Cover";
            PageSubTitle = "Buy Medical Scheme";

            EnableProceedButton = true;
            SubmitButtonHasBeenClicked = false;

            GetUserDetails();
            GetLeadDetails();

            Task.Run(async () =>
            {
                await Task.Delay(1000);
                await GetAllCounties();
                await GetPostalCodes();
            });
        }

        public async Task ShowNextOfKin()
        {
            try
            {
                bool allFilesUploaded = true;


                if( NhifCardFile == null)
                {
                    await ShowErrorMessage("Please Upload NHIF Card");
                    allFilesUploaded = false;
                    return;
                }
                else
                {
                    if(NhifCardFile.FileName.ToLower().Contains("no file selected"))
                    {
                        await ShowErrorMessage("Please Upload NHIF Card");
                        allFilesUploaded = false;
                        return;
                    }
                    
                }

                if (NationalIdFile == null)
                {
                    await ShowErrorMessage("Please Upload  National ID");
                    allFilesUploaded = false;
                    return;
                }
                else
                {
                    if (NationalIdFile.FileName.ToLower().Contains("no file selected"))
                    {
                        await ShowErrorMessage("Please Upload National ID Card");
                        allFilesUploaded = false;
                        return;
                    }

                }


                if (PassportFile == null)
                {
                    await ShowErrorMessage("Please Upload  Passport");
                    allFilesUploaded = false;
                    return;
                }
                else
                {
                    if (PassportFile.FileName.ToLower().Contains("no file selected"))
                    {
                        await ShowErrorMessage("Please Upload Passport ");
                        allFilesUploaded = false;
                        return;
                    }

                }



                if (allFilesUploaded)
                {
                    await Shell.Current.GoToAsync(nameof(BomaNextOfKinDetailsPage));
                }
               
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Buy Medical Cover", "", "");
                await ShowErrorMessage();
            }
        }

        public async Task GetAllCounties()
        {
            try
            {

                if (await CheckInternetConnectivity())
                {
                    if (await CheckIfApiDetailsAreSetUp())
                    {
                        RestClient client = new RestClient()
                        {
                            BaseUrl = new Uri(ApiDetail.EndPoint)
                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.GET,
                            Resource = "/MedicalCover/GetCounties"
                        };

                        var response = await client.ExecuteAsync(restRequest);

                        if (response.IsSuccessful)
                        {

                            var deserializedData = JsonConvert.DeserializeObject<BaseResponse<List<County>>>(response.Content);

                            if (deserializedData.success)
                            {
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    Counties = deserializedData.data;
                                });
                               
                            }
                            else
                            {
                                await ShowErrorMessage("Something went wrong when getting list of counties, please try again later");
                            }

                        }
                        else
                        {
                            await ShowErrorMessage("Something went wrong when getting list of counties, please try again later");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Boma Afya Cover", "", PhoneNumber);
                Console.Write(ex);
            }

        }

        public async Task GetPostalCodes()
        {
            try
            {

                if (await CheckInternetConnectivity())
                {
                    if (await CheckIfApiDetailsAreSetUp())
                    {
                        RestClient client = new RestClient()
                        {
                            BaseUrl = new Uri(ApiDetail.EndPoint)
                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.GET,
                            Resource = "/MedicalCover/GetPostalCodes"
                        };

                        var response = await client.ExecuteAsync(restRequest);

                        if (response.IsSuccessful)
                        {

                            var deserializedData = JsonConvert.DeserializeObject<BaseResponse<List<PostalCode>>>(response.Content);

                            if (deserializedData.success)
                            {

                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    PostalCodes = deserializedData.data;
                                });

                               
                            }
                            else
                            {
                                await ShowErrorMessage("Something went wrong when getting list of postal codes, please try again later");
                            }

                        }
                        else
                        {
                            await ShowErrorMessage("Something went wrong when getting list of counties, please try again later");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Boma Afya Cover", "", PhoneNumber);
                Console.Write(ex);
               await ShowErrorMessage();
            }

        }

        private void EnableDisableSubmitButton()
        {
            try
            {
                

                if (AgreeToTermsAndConditions)
                {
                    EnableProceedButton = true;
                }
                else
                {
                    EnableProceedButton = false;
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Boma Afya Cover", "", PhoneNumber);
            }
        }

        private void SelectGender(string selectedGender ="")
        {
            try
            {
               
                Gender = selectedGender ;

                if (Gender == "Male")
                {
                    IsMaleSelected = true;
                }
                else
                {
                    IsMaleSelected = false;
                }


                if (Gender == "Female")
                {
                    IsFeMaleSelected = true;
                }
                else
                {
                    IsFeMaleSelected = false;
                }


            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Add Nhif Details", "", "");
            }
        }

        public void GetLeadDetails()
        {
            //var Details = DependencyService.Get<AgentLeadsPageViewModel>().AgentLeadDTO;
            //if(Details != null)
            //{
            //    FullName = Details.firstName + " " + Details.lastName;
            //    PhoneNumber = Details.phoneNumber;
            //    PostalAddress = Details.postalAddress;
            //}
            
        }
        private void GetUserDetails()
        {
            try
            {
                FullName = Preferences.Get(nameof(AspNetUsers.firstName), "") + " " + Preferences.Get(nameof(AspNetUsers.lastName), "");
                PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
               
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Add Nhif Details for Boma Afya", "", "");
            }
        }

        private async Task UploadDocumentsPage()
        {
            try
            {
                SubmitButtonHasBeenClicked = true;
                var result = IsModelValid();

                if (result)
                {
                    await Shell.Current.GoToAsync(nameof(UploadBomaDocuments));
                }

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Boma Buy Medical Cover", "", PhoneNumber);
            }
        }

        private async Task SelectNhifCard()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                NhifCardFile = fileData;

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Boma Buy Cover", "", PhoneNumber);
            }
        }


        private async Task SelectNationalIdFile()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                NationalIdFile = fileData;

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Boma Buy Cover", "", PhoneNumber);
            }
        }


        private async Task SelectPassportPhoto()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                PassportFile = fileData;

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Boma Buy Cover", "", PhoneNumber);
            }
        }


        private bool IsModelValid()
        {
            try
            {
                GenderHasError = string.IsNullOrEmpty(Gender) == true ? true : false;
                FullNameHasError = string.IsNullOrEmpty(FullName) == true ? true : false;
                PhoneNumberHasError = string.IsNullOrEmpty(PhoneNumber) == true ? true : false;
                NhifNumberHasError = string.IsNullOrEmpty(NhifNumber) == true ? true : false;
                SelectedPostalCodeHasError = SelectedPostalCode == null ? true : false;
                PostalAddressHasError = string.IsNullOrEmpty(PostalAddress) == true ? true : false;
                PhysicalAddressHasError = string.IsNullOrEmpty(PhysicalAddress) == true ? true : false;
                SelectedCountyHasError = SelectedCounty == null ? true : false;


                if(GenderHasError == true  || FullNameHasError == true || PhoneNumberHasError ==true || NhifNumberHasError ==true || SelectedPostalCodeHasError == true || PostalAddressHasError == true || SelectedCountyHasError == true || PhysicalAddressHasError ==true)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Boma Afya");
                return false;
            }
        }

        private void CheckIfModelIsValid()
        {
            if (SubmitButtonHasBeenClicked)
            {
                IsModelValid();
            }
         
        }


        #region Error Models 

        private bool fullNameHasError;
        public bool FullNameHasError
        {
            get { return fullNameHasError; }
            set
            {
                fullNameHasError = value;
                OnPropertyChanged();
            }
        }


        private bool genderHasError;
        public bool GenderHasError
        {
            get { return genderHasError; }
            set
            {
                genderHasError = value;
                OnPropertyChanged();
            }
        }


        private bool phoneNumberHasError;
        public bool PhoneNumberHasError
        {
            get { return phoneNumberHasError; }
            set
            {
                phoneNumberHasError = value;
                OnPropertyChanged();
            }
        }


        private bool nhifNumberHasError;
        public bool NhifNumberHasError
        {
            get { return nhifNumberHasError; }
            set { nhifNumberHasError = value; OnPropertyChanged(); }
        }



        private bool selectedPostalCodeHasError;
        public bool SelectedPostalCodeHasError
        {
            get { return selectedPostalCodeHasError; }
            set
            {
                selectedPostalCodeHasError = value;
                OnPropertyChanged();
            }
        }


        private bool postalAddressHasError;
        public bool PostalAddressHasError
        {
            get { return postalAddressHasError; }
            set { postalAddressHasError = value; OnPropertyChanged(); }
        }


        private bool physicalAddressHasError;
        public bool PhysicalAddressHasError
        {
            get { return physicalAddressHasError; }
            set { physicalAddressHasError = value; OnPropertyChanged(); }
        }


        private bool selectedCountyHasError;
        public bool SelectedCountyHasError
        {
            get { return selectedCountyHasError; }
            set
            {
                selectedCountyHasError = value;
                OnPropertyChanged();
            }
        }
        #endregion


    }
}
