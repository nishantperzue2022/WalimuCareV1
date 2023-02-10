 using Android.Util;
using HkMobileApp.Models.HkApiBody.MedicalCover;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Models.HkApiResponses.MedicalCover;
using HkMobileApp.Services;
using HkMobileApp.Views.MedicalCover;
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

namespace HkMobileApp.ViewModels.MedicalCoverViewModels
{
    public class BomaSelectCoverViewModel:AppViewModel
    {
        public decimal premiumFromApi  { get; set; } = 36000;

        private string bomaCoverType;
        public string BomaCoverType
        {
            get { return bomaCoverType; }
            set { bomaCoverType = value;
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

        public ICommand CompleteBuyCoverCommand { get; set; }

        public ICommand  SelectBomaTypeCommand { get; set; }

        public ICommand SelectPeriodCommand { get; set; }


        public List<BomaPremiumDetail> BomaPremiumDetails { get; set; }


        private OrderDetail orderDetails;
        public OrderDetail OrderDetails
        {
            get { return orderDetails; }
            set { orderDetails = value; OnPropertyChanged(); }
        }


        public MedicalCoverBody medicalCoverBody { get; set; }


        public BomaSelectCoverViewModel()
        {
            PageTitle = "Select Boma Cover Type";
            PageSubTitle = "";
            CompleteBuyCoverCommand = new Command(async () => await CompleteBuyCover());

            SelectBomaTypeCommand = new Command<string>(x => SelectBomaType(x));
            SelectPeriodCommand = new Command<string>(x => SelectPeriod(x));

            BomaPremiumDetails = DependencyService.Get<ViewBomaBenefitCoverAndPremiumsViewModel>()?.OriginalPremiums;


            SelectBomaType("3");
            SetFamilySize(4);
            SelectPeriod("1");
            //CalculatePremium();

            medicalCoverBody = new MedicalCoverBody();

           


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

                        await ShowSuccessMessage("Your Order Details have been places successfully");

                        Thread.Sleep(2000);

                        var orderData = deseralizedResponse.data;

                        OrderDetails = orderData;

                        await Shell.Current.GoToAsync(nameof(BomaVerifyDetailsAndPayPage));

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

        private void SelectBomaType(string theSelectedCoverType)
        {
            try
            {
                BomaCoverType = theSelectedCoverType;

                if(theSelectedCoverType == "3")
                {
                    if (theSelectedCoverType == "3")
                    {
                        IsMplus3Checked = true;
                        SetFamilySize(4);
                    }
                    else
                    {
                        IsMplus5Checked = false;
                    }
                }


                if (theSelectedCoverType == "5")
                {
                    if (theSelectedCoverType == "5")
                    {
                        IsMplus3Checked = false;
                        SetFamilySize(6);
                    }
                    else
                    {
                        IsMplus5Checked = true;
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

                if(BomaCoverType == "3")
                {
                    premiumFromApi = BomaPremiumDetails.Where(p => p.beneficiaries == "M + 3").FirstOrDefault().premium;
                }
                else
                {
                    premiumFromApi = BomaPremiumDetails.Where(p => p.beneficiaries == "M + 5").FirstOrDefault().premium;
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

                var data = DependencyService.Get<BomaBuyMedicalCoverViewModel>();

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

                var data = DependencyService.Get<BomaNextOfKinDetailsViewModel>();

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
                    beneficiaries = bomaCoverType,
                    discount = 0,
                    insuranceCompany = "Healthier Kenya",
                    medicalCoverProdictType = bomaCoverType,
                    medicalCoverProductCode = "001",
                    period = Convert.ToInt32(Period)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
