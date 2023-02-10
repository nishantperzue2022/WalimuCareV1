using Android.Util;
using HkMobileApp.Models.HkApiBody.MedicalCover;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Models.HkApiResponses.MedicalCover;
using HkMobileApp.Services;
using HkMobileApp.Views.MedicalCover;
using HkMobileApp.Views.MedicalCover.WoteCoverViews;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels.MedicalCoverViewModels.WoteCovers
{
    public class WoteSelectCoverViewModel : AppViewModel
    {
        public decimal premiumFromApi { get; set; } = 36000;


        private string woteCoverType;
        public string WoteCoverType
        {
            get { return woteCoverType; }
            set
            {
                woteCoverType = value;
                OnPropertyChanged();
            }
        }


        private List<string> familySizes;
        public List<string> FamilySizes
        {
            get { return familySizes; }
            set
            {
                familySizes = value;
                OnPropertyChanged();
            }
        }


        private string selectedFamilySize;
        public string SelectedFamilySize
        {
            get { return selectedFamilySize; }
            set { selectedFamilySize = value; OnPropertyChanged(); }
        }


        private string period;
        public string Period
        {
            get { return period; }
            set { period = value; OnPropertyChanged(); }
        }


        private string totalPremium;
        public string TotalPremium
        {
            get { return totalPremium; }
            set { totalPremium = value; OnPropertyChanged(); }
        }



        private bool isMplus3Checked;
        public bool IsMplus3Checked
        {
            get { return isMplus3Checked; }
            set { isMplus3Checked = value; OnPropertyChanged(); }
        }


        private bool isMplus5Checked;
        public bool IsMplus5Checked
        {
            get { return isMplus5Checked; }
            set
            {
                isMplus5Checked = value;
                OnPropertyChanged();
            }
        }


        private bool isMplus7Checked;
        public bool IsMplus7Checked
        {
            get { return isMplus7Checked; }
            set
            {
                isMplus7Checked = value;
                OnPropertyChanged();
            }
        }


        private bool isMplus10Checked;
        public bool IsMplus10Checked
        {
            get { return isMplus10Checked; }
            set
            {
                isMplus10Checked = value;
                OnPropertyChanged();
            }
        }



        private bool isPeriod1yrSelected;
        public bool IsPeriod1yrSelected
        {
            get { return isPeriod1yrSelected; }
            set { isPeriod1yrSelected = value; OnPropertyChanged(); }
        }


        private bool isPeriod2yrSelected;
        public bool IsPeriod2yrSelected
        {
            get { return isPeriod2yrSelected; }
            set { isPeriod2yrSelected = value; OnPropertyChanged(); }
        }



        private string bronzeBorderColor;
        public string BronzeBorderColor
        {
            get { return bronzeBorderColor; }
            set { bronzeBorderColor = value; OnPropertyChanged(); }
        }



        private string silverBorderColor;
        public string SilverBorderColor
        {
            get { return silverBorderColor; }
            set { silverBorderColor = value; OnPropertyChanged(); }
        }


        private string goldBorderColor;
        public string GoldBorderColor
        {
            get { return goldBorderColor; }
            set { goldBorderColor = value; OnPropertyChanged(); }
        }



        private string selectedCoverType;
        public string SelectedCoverType
        {
            get { return selectedCoverType; }
            set { selectedCoverType = value; OnPropertyChanged();  }
        }



        public List<WotePremiumDetail> WotePremiumDetails { get; set; }



        public ICommand CompleteBuyCoverCommand { get; set; }

        public ICommand SelectWoteTypeCommand { get; set; }

        public ICommand SelectPeriodCommand { get; set; }

        public ICommand SelectCoverTypeCommand { get; set; }


        private OrderDetail orderDetails;
        public OrderDetail OrderDetails
        {
            get { return orderDetails; }
            set { orderDetails = value; OnPropertyChanged(); }
        }


        public MedicalCoverBody medicalCoverBody { get; set; }


        public WoteSelectCoverViewModel()
        {
            PageTitle = "Select Wote Cover Type";
            PageSubTitle = "";
            CompleteBuyCoverCommand = new Command(async () => await CompleteBuyCover());

            SelectWoteTypeCommand = new Command<string>(x => SelectWoteType(x));
            SelectPeriodCommand = new Command<string>(x => SelectPeriod(x));
            SelectCoverTypeCommand = new Command<string>(x => SelectCoverType(x));


            SelectWoteType("3");
            SetFamilySize(4);
            SelectPeriod("1");
            SelectCoverType("bronze");

            medicalCoverBody = new MedicalCoverBody();

            WotePremiumDetails = DependencyService.Get<ViewWotePremiumsAndCoverBenefitsViewModel>()?.WotePremiumDetails;

            CalculatePremium();

        }

        public async Task CompleteBuyCover()
        {
            try
            {

                GetBasicDetails();
                GetNextOfKin();
                GetMedicalCoverDetails();

                await ShowLoadingMessage();

                RestClient client = new RestClient()
                {
                    BaseUrl = new Uri(ApiDetail.EndPoint)
                };

                RestRequest request = new RestRequest()
                {
                    Resource = "/MedicalCover/SetUpdateMemberDetails",
                    Method = Method.POST
                };

                request.AddJsonBody(medicalCoverBody);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var deseralizedResponse = JsonConvert.DeserializeObject<BaseResponse<OrderDetail>>(response.Content);



                    if (deseralizedResponse.success)
                    {

                        await ShowSuccessMessage("Your Order Details have been placed successfully");

                        Thread.Sleep(2000);

                        var orderData = deseralizedResponse.data;

                        OrderDetails = orderData;

                        await Shell.Current.GoToAsync(nameof(WoteVerifyDetailsAndPayPage));

                    }
                    else
                    {
                        await ShowErrorMessage("Something went wrong when generating order details");


                        //OrderDetails = new OrderDetail()
                        //{
                        //    agentName = "The Real Agent",
                        //    status = 1,
                        //    statusDescription = "true",
                        //    agentId = Guid.NewGuid().ToString(),
                        //    amount = 36000,
                        //    id = Guid.NewGuid().ToString(),
                        //    insuranceCoverId = Guid.NewGuid().ToString(),
                        //    insuranceCoverName = "Medicross Healthier Kenya",
                        //    medicalProductName = "Boma Afya",
                        //    orderDate = DateTime.Now.AddDays(-20),
                        //    orderNumber = "567-4568",
                        //    principleDependants = "Alfonse Muthami",
                        //    receiptNumber = "89898-98255"

                        //};

                        //Need to comment out this line 
                        //await Shell.Current.GoToAsync(nameof(BomaVerifyDetailsAndPayPage));
                    }
                }
                else
                {
                    await ShowErrorMessage("Something went wrong when generating order details");
                }

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Select Boma Cover Type", "", "");
                await ShowErrorMessage("Something went wrong when generating order details");
            }
        }

        private void SelectWoteType(string theSelectedCoverType)
        {
            try
            {
                WoteCoverType = theSelectedCoverType;

              

                if (theSelectedCoverType == "3")
                {

                    if (theSelectedCoverType == "3")
                    {
                        IsMplus3Checked = true;
                        SetFamilySize(4);
                    }
                    else
                    {
                        IsMplus5Checked = false;
                        IsMplus7Checked = false;
                        IsMplus10Checked = false;

                    }
                }


                if (theSelectedCoverType == "5")
                {
                    if (theSelectedCoverType == "5")
                    {
                       
                        IsMplus5Checked = true;
                        SetFamilySize(6);
                    }
                    else
                    {
                        IsMplus3Checked = false;
                        IsMplus7Checked = false;
                        IsMplus10Checked = false;
                    }
                }


                if (theSelectedCoverType == "7")
                {
                    if (theSelectedCoverType == "7")
                    {
                      
                        IsMplus7Checked = true;
                        SetFamilySize(8);
                    }
                    else
                    {
                        IsMplus5Checked = false;
                        IsMplus3Checked = false;
                        IsMplus10Checked = false;
                    }
                }



                if (theSelectedCoverType == "10")
                {
                    if (theSelectedCoverType == "10")
                    {
                       
                        IsMplus10Checked = true;
                        SetFamilySize(11);
                    }
                    else
                    {
                        IsMplus3Checked = false;
                        IsMplus5Checked = false;
                        IsMplus7Checked = false;
                     
                    }
                }

                CalculatePremium();



            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Boma Cover Type", "", "");
            }
        }

        private void SetFamilySize(int Size)
        {
            try
            {
                List<string> myFamilySize = new List<string>();

                for (int i = 1; i <= Size; i++)
                {
                    myFamilySize.Add(i.ToString());
                }

                FamilySizes = myFamilySize;

                SelectedFamilySize = Size.ToString();
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Boma Afya", "", "");
            }
        }

        private void SelectPeriod(string P)
        {
            try
            {
                Period = P;

                if (P == "1")
                {
                    if (P == "1")
                    {
                        IsPeriod1yrSelected = true;
                    }
                    else
                    {
                        IsPeriod2yrSelected = false;
                    }
                }



                if (P == "2")
                {
                    if (P == "2")
                    {
                        IsPeriod1yrSelected = false;
                    }
                    else
                    {
                        IsPeriod2yrSelected = true;
                    }
                }




                CalculatePremium();
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Boma Afya", "", "");
            }
        }

        private void CalculatePremium()
        {
            try
            {

                var thePremiumAmount = WotePremiumDetails.Where(p => p.benefit.Contains(WoteCoverType)).FirstOrDefault();

                if (SelectedCoverType.ToLower() == "bronze")
                {
                    premiumFromApi = (decimal)thePremiumAmount.bronze;
                }
                else if (SelectedCoverType.ToLower() == "silver")
                {
                    premiumFromApi = (decimal)thePremiumAmount.silver;
                }
                else if (SelectedCoverType.ToLower() == "gold")
                {
                    premiumFromApi = (decimal)thePremiumAmount.gold;
                }




                if (Period == "1")
                {
                    TotalPremium = premiumFromApi.ToString("N0");
                }
                else
                {
                    var total = ((premiumFromApi * 2) * 0.9M);

                    TotalPremium = total.ToString("N0");
                }

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Boma Afya Cover", "", "");
            }
        }


        public void GetBasicDetails()
        {
            try
            {

                var data = DependencyService.Get<WoteBuyMedicalCoverViewModel>();

                string nationalIdFile = data.NationalIdFile != null ? Base64.EncodeToString(data.NationalIdFile.DataArray, Base64Flags.NoWrap) : "";
                string nhifFile = data.NhifCardFile != null ? Base64.EncodeToString(data.NhifCardFile.DataArray, Base64Flags.NoWrap) : "";
                string PassportFile = data.PassportFile != null ? Base64.EncodeToString(data.PassportFile.DataArray, Base64Flags.NoWrap) : "";



                medicalCoverBody.mbrBasicDetails = new Mbrbasicdetails();
                medicalCoverBody.mbrBasicDetails.fullName = data.FullName;
                medicalCoverBody.mbrBasicDetails.gender = data.Gender;
                medicalCoverBody.mbrBasicDetails.medicalProductCode = "001";
                medicalCoverBody.mbrBasicDetails.memberId = Guid.NewGuid().ToString();
                medicalCoverBody.mbrBasicDetails.nationalIdFile = nationalIdFile;
                medicalCoverBody.mbrBasicDetails.nationalIDName = data.NationalIdFile != null ? data.NationalIdFile.FileName : "";
                medicalCoverBody.mbrBasicDetails.nhifCardFile = nhifFile;
                medicalCoverBody.mbrBasicDetails.nhifCardFileName = data.NhifCardFile != null ? data.NhifCardFile.FileName : "";
                medicalCoverBody.mbrBasicDetails.nhifNumber = data.NhifNumber;
                medicalCoverBody.mbrBasicDetails.passportFile = PassportFile;
                medicalCoverBody.mbrBasicDetails.passportName = data.PassportFile != null ? data.PassportFile.FileName : "";
                medicalCoverBody.mbrBasicDetails.phoneNumber = data.PhoneNumber;
                medicalCoverBody.mbrBasicDetails.physicalAddress = data.PhysicalAddress;
                medicalCoverBody.mbrBasicDetails.postalAddress = data.PostalAddress;
                medicalCoverBody.mbrBasicDetails.postalCode = data.SelectedPostalCode != null ? data.SelectedPostalCode.code : "";
                medicalCoverBody.mbrBasicDetails.residentCounty = data.SelectedCounty != null ? data.SelectedCounty.county : "";



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void GetNextOfKin()
        {
            try
            {

                var data = DependencyService.Get<WoteNextOfKinDetailsViewModel>();

                medicalCoverBody.nextOfKin = new Nextofkin()
                {
                    emailAddress = data.NextOfKinEmailAddress,
                    name = data.NextOfKinFullNames,
                    phoneNumber = data.NextOfKinPhoneNumber,
                    relationship = data.NextOfKinRelationship,
                    insuranceCoverId = Guid.NewGuid().ToString(),
                    id = Guid.NewGuid().ToString()
                };

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        public void GetMedicalCoverDetails()
        {
            try
            {


                medicalCoverBody.medicalCoverDetails = new Medicalcoverdetails()
                {
                    amount = premiumFromApi,
                    familySize = Convert.ToInt32(SelectedFamilySize),
                    beneficiaries = woteCoverType,
                    discount = 0,
                    insuranceCompany = "Healthier Kenya",
                    medicalCoverProdictType = woteCoverType,
                    medicalCoverProductCode = "001",
                    period = Convert.ToInt32(Period)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void SelectCoverType(string x)
        {
            try
            {

                SelectedCoverType = x;

                if (x == "gold")
                {
                    GoldBorderColor = "Black";
                    BronzeBorderColor = "#cd7f32";
                    SilverBorderColor = "#C0C0C0";
                }


                if (x == "silver")
                {

                    GoldBorderColor = "#d4af37";
                    BronzeBorderColor = "#cd7f32";
                    SilverBorderColor = "Black";
                }


                if (x == "bronze")
                {

                    GoldBorderColor = "#d4af37";
                    BronzeBorderColor = "Black";
                    SilverBorderColor = "#C0C0C0";
                }

                CalculatePremium();

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Wote Select Cover");
            }
        }
    }
}

