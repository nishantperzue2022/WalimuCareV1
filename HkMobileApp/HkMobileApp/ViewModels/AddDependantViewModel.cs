using Android.Preferences;
using Android.Util;
using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views;
using HkMobileApp.Views.dependants;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class AddDependantViewModel : AppViewModel
    {

        public string MemberId { get; set; }


        private string userEmail;
        public string UserEmail
        {
            get { return userEmail; }
            set
            {
                userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }


        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }


        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private string relationship;
        public string Relationship
        {
            get { return relationship; }
            set
            {
                relationship = value;
                OnPropertyChanged(nameof(Relationship));
                ShowHideProofOfSchooling();
            }
        }


        private string passportPhotoName;
        public string PassportPhotoName
        {
            get { return passportPhotoName; }
            set
            {
                if (value != null)
                {
                    passportPhotoName = value;
                }
                else
                {
                    passportPhotoName = "Select File";
                }

                OnPropertyChanged(nameof(PassportPhotoName));
            }
        }


        private string iDPhotoName;
        public string IDPhotoName
        {
            get { return iDPhotoName; }
            set
            {
                if (value != null)
                {
                    iDPhotoName = value;
                }
                else
                {
                    iDPhotoName = "Select Photo";
                }

                OnPropertyChanged(nameof(IDPhotoName));
            }
        }


        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
                ShowHideProofOfSchooling();
            }
        }


        private FileData nationalId;
        public FileData NationalId
        {
            get { return nationalId; }
            set
            {
                nationalId = value;
                OnPropertyChanged(nameof(NationalId));
                IsNationalIdFrameVisible = nationalId != null ? true : false;
            }
        }


        private bool isNationalIdVisible;
        public bool IsNationalIdVisible
        {
            get { return isNationalIdVisible; }
            set
            {
                isNationalIdVisible = value;
                OnPropertyChanged();

            }
        }


        private bool isNationalIdFrameVisible;
        public bool IsNationalIdFrameVisible
        {
            get { return isNationalIdFrameVisible; }
            set { isNationalIdFrameVisible = value; OnPropertyChanged(); }
        }


        private FileData passport;
        public FileData Passport
        {
            get { return passport; }
            set
            {
                passport = value;
                OnPropertyChanged(nameof(Passport));
            }
        }



        private string phoneNumberDependant;
        public string PhoneNumberDependant
        {
            get { return phoneNumberDependant; }
            set
            {
                phoneNumberDependant = value;
                OnPropertyChanged(nameof(PhoneNumberDependant));
            }
        }



        private string emailDependant;
        public string EmailDependant
        {
            get { return emailDependant; }
            set { emailDependant = value; OnPropertyChanged(nameof(EmailDependant)); }
        }



        private string nationalIdOrBirthCertificateNumber;
        public string NationalIdOrBirthCertificateNumber
        {
            get { return nationalIdOrBirthCertificateNumber; }
            set
            {
                nationalIdOrBirthCertificateNumber = value;
                OnPropertyChanged();
            }
        }



        private FileData marriageCertificate;
        public FileData MarriageCertificate
        {
            get { return marriageCertificate; }
            set
            {
                marriageCertificate = value;
                OnPropertyChanged();
                IsFrameMarriageCertVisible = MarriageCertificate != null ? true : false;
            }
        }



        private bool isMarriageCerificateVisible;
        public bool IsMarriageCerificateVisible
        {
            get { return isMarriageCerificateVisible; }
            set
            {
                isMarriageCerificateVisible = value;
                OnPropertyChanged();
            }
        }



        private bool isFrameMarriageCertVisible;
        public bool IsFrameMarriageCertVisible
        {
            get { return isFrameMarriageCertVisible; }
            set { isFrameMarriageCertVisible = value; OnPropertyChanged(); }
        }




        private FileData proofOfSchooling;
        public FileData ProofOfSchooling
        {
            get { return proofOfSchooling; }
            set
            {
                proofOfSchooling = value;
                OnPropertyChanged();
                IsFrameProofOfSchoolingVisible = ProofOfSchooling != null ? true : false;
            }
        }



        private bool isProofOfSchoolingVisible;
        public bool IsProofOfSchoolingVisible
        {
            get { return isProofOfSchoolingVisible; }
            set
            {
                isProofOfSchoolingVisible = value;
                OnPropertyChanged();
            }
        }




        private bool isFrameProofOfSchoolingVisible;
        public bool IsFrameProofOfSchoolingVisible
        {
            get { return isFrameProofOfSchoolingVisible; }
            set { isFrameProofOfSchoolingVisible = value; OnPropertyChanged(); }
        }




        private bool isDisabled;
        public bool IsDisabled
        {
            get { return isDisabled; }
            set
            {
                isDisabled = value;
                OnPropertyChanged();
                ShowHideDisability();
            }
        }



        private FileData proofOfDisability;
        public FileData ProofOfDisability
        {
            get { return proofOfDisability; }
            set
            {
                proofOfDisability = value;
                OnPropertyChanged();
                IsFrameProofOfDisabilityVisible = ProofOfDisability != null ? true : false;
            }
        }



        private bool isProofOfDisabilityVisible;
        public bool IsProofOfDisabilityVisible
        {
            get { return isProofOfDisabilityVisible; }
            set
            {
                isProofOfDisabilityVisible = value;
                OnPropertyChanged();
            }
        }



        private bool isFrameProofOfDisabilityVisible;
        public bool IsFrameProofOfDisabilityVisible
        {
            get { return isFrameProofOfDisabilityVisible; }
            set { isFrameProofOfDisabilityVisible = value; OnPropertyChanged(); }
        }



        private List<string> rshps;
        public List<string> Rshps
        {
            get { return rshps; }
            set { rshps = value; OnPropertyChanged(); }
        }




        private DateTime maxDateOfBirth;
        public DateTime MaxDateOfBirth
        {
            get { return maxDateOfBirth; }
            set { maxDateOfBirth = value; OnPropertyChanged(); }
        }



        private DateTime minDateOfBirth;
        public DateTime MinDateOfBirth
        {
            get { return minDateOfBirth; }
            set { minDateOfBirth = value; OnPropertyChanged(); }
        }





        public string PhoneNumber { get; set; }
        public string MemberNumber { get; set; }


        public ICommand SubmitDependantDetails { get; set; }

        public ICommand SelectNationalId { get; set; }
        public ICommand SelectPassport { get; set; }
        public ICommand SelectMarriageCertificateCommand { get; set; }
        public ICommand SelectProofOfSchoolingCommand { get; set; }

        public ICommand SelectProofOfDisabilityCommand { get; set; }

        public ICommand RemoveFileCommand { get; set; }


        public AddDependantViewModel()
        {
            try
            {
                PageTitle = "Add Depedant";
                PageSubTitle = "Enter your dependent details";
                MemberId = Preferences.Get(nameof(AspNetUsers.memberId), "");
                SubmitDependantDetails = new Command(async () => await SaveDependantDetails());

                SelectPassport = new Command(async () => await SelectPassportPhoto());

                SelectNationalId = new Command(async () => await SelectNationalIdentity());

                SelectMarriageCertificateCommand = new Command(async () => await SelectMarriageCertificate());

                SelectProofOfSchoolingCommand = new Command(async () => await SelectProofOfSchooling());

                SelectProofOfDisabilityCommand = new Command(async () => await SelectProofOfDisability());


                dateOfBirth = DateTime.Now;
                PassportPhotoName = "Select File";
                IDPhotoName = "Select File";

                PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
                MemberNumber = Preferences.Get("MemberNumber", "");

                IsNationalIdVisible = true;

                SetRelationShipList();

                MaxDateOfBirth = DateTime.Now;
                //MinDateOfBirth = DateTime.Now.AddYears(-25);

                RemoveFileCommand = new Command<string>(x => RemoveFile(x));

                IsNationalIdFrameVisible = false;
                isFrameMarriageCertVisible = false;
                IsFrameProofOfSchoolingVisible = false;
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Add Dependant", MemberNumber, PhoneNumber);
            }

        }

        public async Task SaveDependantDetails()
        {
            try
            {
                if (await CheckInternetConnectivity())
                {
                    if (await CheckIfApiDetailsAreSetUp())
                    {



                        string passportphotonameToString = Passport != null ? Base64.EncodeToString(Passport.DataArray, Base64Flags.NoWrap) : "";
                        string IDphotonameToString = NationalId != null ? Base64.EncodeToString(NationalId.DataArray, Base64Flags.NoWrap) : "";

                        string theMarriageCertificate = MarriageCertificate != null ? Base64.EncodeToString(MarriageCertificate.DataArray, Base64Flags.NoWrap) : "";
                        string theProofOfSchooling = ProofOfSchooling != null ? Base64.EncodeToString(ProofOfSchooling.DataArray, Base64Flags.NoWrap) : "";
                        string theProofOfDisability = ProofOfDisability != null ? Base64.EncodeToString(ProofOfDisability.DataArray, Base64Flags.NoWrap) : "";

                        if (string.IsNullOrEmpty(FullName))
                        {
                            await ShowErrorMessage("Please Enter Full Name");
                            return;
                        }


                        if (string.IsNullOrEmpty(Gender))
                        {
                            await ShowErrorMessage("Please Select Gender");
                            return;
                        }


                        if (string.IsNullOrEmpty(Relationship))
                        {
                            await ShowErrorMessage("Please Select Relationship");
                            return;
                        }


                        if (string.IsNullOrEmpty(NationalIdOrBirthCertificateNumber))
                        {
                            await ShowErrorMessage("Please Enter National Id Or Birth Certificate Number");
                            return;
                        }


                        if (string.IsNullOrEmpty(PhoneNumber))
                        {
                            await ShowErrorMessage("Please Enter Phone Number");
                            return;
                        }


                        if (string.IsNullOrEmpty(EmailDependant))
                        {
                            await ShowErrorMessage("Please Enter Email");
                            return;
                        }


                        if (!IsValidEmail(EmailDependant))
                        {
                            await ShowErrorMessage("Please Enter Valid Email");
                            return;
                        }



                        if (Relationship == "Spouse")
                        {
                            //We check if the user has uploaded proof of marriage 
                            if (theMarriageCertificate == "")
                            {
                                await ShowErrorMessage("Please attach marriage certificate");
                                return;
                            }
                        }

                        if (Relationship == "Son" || Relationship == "Daughter")
                        {
                            if (theProofOfSchooling == "")
                            {
                                await ShowErrorMessage("Please upload Proof of Schooling Document");
                                return;
                            }
                        }


                        if (IsProofOfDisabilityVisible == true)
                        {
                            if (theProofOfDisability == "")
                            {
                                await ShowErrorMessage("Please Upload Proof of disability document");
                                return;
                            }
                        }






                        await ShowLoadingMessage();

                        RestClient client = new RestClient()
                        {

                            BaseUrl = new Uri(ApiDetail.EndPoint)

                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.POST,
                            Resource = "/Members/AddMemberDepedants"
                        };

                        AddDependantMember addDependantMember = new AddDependantMember()
                        {
                            id = Guid.NewGuid().ToString(),
                            FullName = FullName,
                            Gender = Gender,
                            Relationship = Relationship,
                            UserEmail = EmailDependant,
                            DateOfBirth = DateOfBirth,
                            ParentId = MemberId,
                            phoneNumber = PhoneNumberDependant,
                            isDisabled = IsDisabled,
                            nationalIDorBirthCert = NationalIdOrBirthCertificateNumber,
                            //isdisabled , phone number , national id or birth certificate number 
                            //files 
                            NationalIdOrBirthCertificateFile = IDphotonameToString,
                            PassportPhotoFile = passportphotonameToString,
                            MarriageCertificateFile = theMarriageCertificate,
                            ProofOfSchoolingFile = theProofOfSchooling,
                            ProofOfDisabilityFile = theProofOfDisability,
                            //filenames
                            NationalIdOrBirthCertificateFileName = NationalId != null ? NationalId.FileName : "",
                            PassportPhotoFileName = Passport != null ? Passport.FileName : "",
                            MarriageCertificateFileName = MarriageCertificate != null ? MarriageCertificate.FileName : "",
                            ProofOfSchoolingFileName = ProofOfSchooling != null ? ProofOfSchooling.FileName : "",
                            ProofOfDisabilityFileName = ProofOfDisability != null ? ProofOfDisability.FileName : ""

                        };



                        restRequest.AddJsonBody(addDependantMember);


                        var response = await Task.Run(() =>
                        {
                            return client.Execute(restRequest);
                        });



                        bool isSuccesful = false;

                        try
                        {
                            if (response.IsSuccessful)
                            {
                                var serializedResponse = JsonConvert.DeserializeObject<BaseResponse<AddDependantMember>>(response.Content);


                                if (serializedResponse.success)
                                {
                                    isSuccesful = serializedResponse.success;

                                    await ShowSuccessMessage("Dependant Details saved successfully");

                                    Thread.Sleep(2000);

                                    await DependencyService.Get<ManageDependantsViewModel>().GetDependants();

                                    SetRelationShipList();

                                    ResetForms();

                                    await Shell.Current.GoToAsync(nameof(ManageDependantsPage), true);


                                }
                                else
                                {
                                    await ShowErrorMessage();
                                }
                            }
                            else
                            {
                                isSuccesful = false;


                                await ShowErrorMessage();

                            }


                        }
                        catch (Exception e)
                        {
                            await ShowErrorMessage();

                            SendErrorMessageToAppCenter(e, "Add Dependant", MemberNumber, PhoneNumber);
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                await ShowErrorMessage();
                SendErrorMessageToAppCenter(ex, "Add Dependant", MemberNumber, PhoneNumber);
            }
        }

        public async Task SelectNationalIdentity()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking


                NationalId = fileData;
                IDPhotoName = fileData.FileName;


                NationalId = fileData;


                //txtNationalId.Text = fileData.FileName;
                //string fileName = fileData.FileName;
                //string contents = System.Text.Encoding.UTF8.GetString(fileData.DataArray);

                //System.Console.WriteLine("File name chosen: " + fileName);
                //System.Console.WriteLine("File data: " + contents);
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Add Dependant", MemberNumber, PhoneNumber);
            }
        }

        public async Task SelectPassportPhoto()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking


                Passport = fileData;
                PassportPhotoName = fileData.FileName;

                Passport = fileData;
                //txtNationalId.Text = fileData.FileName;
                //string fileName = fileData.FileName;
                //string contents = System.Text.Encoding.UTF8.GetString(fileData.DataArray);

                //System.Console.WriteLine("File name chosen: " + fileName);
                //System.Console.WriteLine("File data: " + contents);
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Add Dependant", MemberNumber, PhoneNumber);
            }
        }

        public async void ShowHideProofOfSchooling()
        {
            try
            {
                if (Relationship != null && Relationship != "")
                {
                    if (!Relationship.ToLower().Trim().Contains("spouse"))
                    {

                        IsMarriageCerificateVisible = false;
                        //this means its either a son or a daughter

                        var age = CalculateYourAge(DateOfBirth);

                        if (age < 25)
                        {
                            //show upload proof of schooling and 
                            IsProofOfSchoolingVisible = true;
                        }
                        else
                        {
                            IsProofOfSchoolingVisible = false;
                            await ShowErrorMessage("Dependants Over 25 yrs are not covered");
                            Relationship = "";
                        }
                    }
                    else
                    {
                        IsProofOfSchoolingVisible = false;
                        IsMarriageCerificateVisible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Add Dependant", MemberNumber, PhoneNumber);
            }
        }

        public void ShowHideDisability()
        {
            try
            {
                if (IsDisabled)
                {
                    IsProofOfDisabilityVisible = true;
                }
                else
                {
                    IsProofOfDisabilityVisible = false;
                }
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Add Dependant", MemberNumber, PhoneNumber);
            }
        }


        public int CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;
            //return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //Years, Months, Days, Hours, Seconds);

            return Years;
        }

        public async Task SelectMarriageCertificate()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                {
                    return; // user canceled file picking
                }

                MarriageCertificate = fileData;

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Add Dependant", MemberNumber, PhoneNumber);
            }
        }

        public async Task SelectProofOfSchooling()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                {
                    return; // user canceled file picking
                }

                ProofOfSchooling = fileData;

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Add Dependant", MemberNumber, PhoneNumber);
            }
        }

        public async Task SelectProofOfDisability()
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                {
                    return; // user canceled file picking
                }

                ProofOfDisability = fileData;

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Add Dependant", MemberNumber, PhoneNumber);
            }
        }


        public void SetRelationShipList()
        {
            try
            {


                var dependantsData = DependencyService.Get<ManageDependantsViewModel>().Dependants;

                var hasSpouse = dependantsData.Any(p => p.Relationship.ToLower().Contains("spouse"));

                if (hasSpouse)
                {
                    Rshps = new List<string>()
                    {
                        "Daughter",
                        "Son"
                    };
                }
                else
                {
                    Rshps = new List<string>()
                    {
                        "Spouse",
                        "Daughter",
                        "Son"
                    };
                }

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Add Dependants");
            }
        }



        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void RemoveFile(string FileType)
        {
            try
            {
                if (FileType == "ID")
                {
                    NationalId = null;
                }


                if (FileType == "Marriage")
                {
                    MarriageCertificate = null;
                }

                if (FileType == "School")
                {
                    ProofOfSchooling = null;
                }

                if (FileType == "Disabled")
                {
                    ProofOfDisability = null;
                }
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Add Dependant");
            }
        }

        public void ResetForms()
        {
            try
            {
                FullName = null;
                MaxDateOfBirth = DateTime.Now;
                IsFrameProofOfDisabilityVisible = false;
                IsProofOfDisabilityVisible = false;
                ProofOfDisability = null;
                IsDisabled = false;
                IsFrameProofOfSchoolingVisible = false;
                IsProofOfSchoolingVisible = false;
                ProofOfSchooling = null;
                IsFrameMarriageCertVisible = false;
                IsMarriageCerificateVisible = false;
                MarriageCertificate = null;
                NationalIdOrBirthCertificateNumber = null;
                EmailDependant = null;
                PhoneNumberDependant = null;
                Passport = null;
                IsNationalIdFrameVisible = false;
                IsNationalIdVisible = false;
                UserEmail = null;
                Gender = null;
                Relationship = null;
                PassportPhotoName = null;
                IDPhotoName = null;
                DateOfBirth = DateTime.Now;
                NationalId = null;

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Add Dependant");
            }
        }
    }
}