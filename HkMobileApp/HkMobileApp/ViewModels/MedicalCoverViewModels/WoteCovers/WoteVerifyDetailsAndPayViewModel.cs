using HkMobileApp.Models.HkApiBody.MedicalCover;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Models.HkApiResponses.MedicalCover;
using HkMobileApp.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels.MedicalCoverViewModels.WoteCovers
{
    public class WoteVerifyDetailsAndPayViewModel : AppViewModel
    {

        private string totalPremium;
        public string TotalPremium
        {
            get { return totalPremium; }
            set
            {
                totalPremium = value;
                OnPropertyChanged();
            }
        }


        private string coverType;
        public string CoverType
        {
            get { return coverType; }
            set { coverType = value; OnPropertyChanged(); }
        }


        private string period;
        public string Period
        {
            get { return period; }
            set
            {
                period = value;
                OnPropertyChanged();
            }
        }


        private OrderDetail orderDetails;
        public OrderDetail OrderDetails
        {
            get { return orderDetails; }
            set { orderDetails = value; OnPropertyChanged(); }
        }





        private bool isReadAllBenefitVisible;
        public bool IsReadAllBenefitVisible
        {
            get { return isReadAllBenefitVisible; }
            set
            {
                isReadAllBenefitVisible = value;
                OnPropertyChanged();
            }
        }

        public WoteBuyMedicalCoverViewModel WoteBuyMedicalCoverViewModel { get; set; }
        public WoteSelectCoverViewModel WoteSelectCoverViewModel { get; set; }

        public ICommand ReadAllBenefitCommand { get; set; }

        public ICommand SendPaymentRequestCommand { get; set; }

        public WoteVerifyDetailsAndPayViewModel()
        {
            IsReadAllBenefitVisible = false;
            ReadAllBenefitCommand = new Command(ReadAllBenefit);
            PageTitle = "Confirm Order";

            SendPaymentRequestCommand = new Command(async () => await SendPaymentRequest());

            WoteBuyMedicalCoverViewModel = DependencyService.Get<WoteBuyMedicalCoverViewModel>();
            WoteSelectCoverViewModel = DependencyService.Get<WoteSelectCoverViewModel>();

            OrderDetails = WoteSelectCoverViewModel.OrderDetails;

            OrderDetails.principleDependants = WoteBuyMedicalCoverViewModel.FullName;
            OrderDetails.agentName = "N/A";

            GetAndSetData();

        }

        private void ReadAllBenefit()
        {
            try
            {
                if (IsReadAllBenefitVisible)
                {
                    IsReadAllBenefitVisible = false;
                }
                else
                {
                    IsReadAllBenefitVisible = true;
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Verify Details Boma", "", "");
            }
        }

        private void GetAndSetData()
        {
            try
            {

                WoteBuyMedicalCoverViewModel = DependencyService.Get<WoteBuyMedicalCoverViewModel>();
                WoteSelectCoverViewModel = DependencyService.Get<WoteSelectCoverViewModel>();

                TotalPremium = WoteSelectCoverViewModel.TotalPremium;

                string selectedCover = WoteSelectCoverViewModel.WoteCoverType;

                if (selectedCover == "3")
                {
                    CoverType = "M + 3";
                }
                else if (selectedCover == "5")
                {
                    CoverType = "M + 5";
                }


                if (WoteSelectCoverViewModel.Period == "1")
                {
                    Period = "1 Year";
                }
                else if (WoteSelectCoverViewModel.Period == "2")
                {
                    Period = "2 Years";
                }

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Boma Cover", "", "");
            }
        }

        public async Task SendPaymentRequest()
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
                    Resource = "/MedicalCover/CoverPaymentsStkPush",
                    Method = Method.POST
                };


                var phoneNumber = DependencyService.Get<WoteBuyMedicalCoverViewModel>().PhoneNumber;


                PayMedicalCoverBody medicalCoverBody = new PayMedicalCoverBody()
                {
                    phoneNumber = phoneNumber,
                    orderId = OrderDetails.id
                };


                restRequest.AddJsonBody(medicalCoverBody);


                var response = await client.ExecuteAsync(restRequest);


                if (response.IsSuccessful)
                {
                    var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<StkPushResponse>>(response.Content);

                    if (deserializedResponse.success)
                    {

                        await ShowInfoMessage("Please wait to Enter Mpesa Pin on Prompt");


                        Thread.Sleep(5000);

                        await ShowInfoMessage("We are confirming payment Details, this can take up to 5 min ");

                    }
                    else
                    {
                        await ShowErrorMessage();
                    }
                }
                else
                {
                    await ShowErrorMessage();
                }




            }
            catch (Exception ex)
            {

                await ShowErrorMessage();
                SendErrorMessageToAppCenter(ex, "Pay Medical Cover");
            }
        }
    }
}

