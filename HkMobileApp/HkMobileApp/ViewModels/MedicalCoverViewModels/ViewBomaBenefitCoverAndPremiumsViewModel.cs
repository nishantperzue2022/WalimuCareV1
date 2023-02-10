using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views.MedicalCover;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class ViewBomaBenefitCoverAndPremiumsViewModel : AppViewModel
    {
        private List<Cover> covers;
        public List<Cover> Covers
        {
            get { return covers; }
            set
            {
                covers = value;
                OnPropertyChanged();
            }
        }


        private List<Premium> premiums;
        public List<Premium> Premiums
        {
            get { return premiums; }
            set
            {
                premiums = value;
                OnPropertyChanged();
            }
        }


        public List<BomaPremiumDetail> OriginalPremiums  { get; set; }


        public ICommand ContinueCommand { get; set; }

        public ViewBomaBenefitCoverAndPremiumsViewModel()
        {
            Task.Run(async () =>
            {
                await ShowLoadingMessage();
                await GetBomaPremiums();
                await GetBomaCoverDetails();
                await RemoveLoadingMessage();
            });

            ContinueCommand = new Command(async () => await Continue());

            PageTitle = "Premium and Benefits";
            PageSubTitle = "View Premium amounts to be paid and Benefit Details";
           
        }

        private async Task Continue()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(BomaAddNhifDetails));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "View Benefits", "", "");
            }
        } 


        public async Task GetBomaPremiums()
        {
            try
            {
                if(await CheckInternetConnectivity())
                {
                    if(await CheckIfApiDetailsAreSetUp())
                    {
                        

                        RestClient client = new RestClient()
                        {
                            BaseUrl = new Uri(ApiDetail.EndPoint)
                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.GET,
                            Resource = "/MedicalCover/GetBomaMedicalCoverDetails"
                        };


                        var response = await client.ExecuteAsync(restRequest);


                        if (response.IsSuccessful)
                        {
                            var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<BomaPremiumDetail>>>(response.Content);

                            if (deserializedResponse.success)
                            {

                                OriginalPremiums = deserializedResponse.data;

                                List<Premium> thePremiums = new List<Premium>();

                                foreach (var item in deserializedResponse.data)
                                {
                                    Premium premium = new Premium()
                                    {
                                        Beneficiary = item.beneficiaries,
                                        Amount = item.premium.ToString("N0")
                                    };


                                    var bens = item.beneficiaries.Split('+');
                                    int NumberOfChildren = 0;

                                    bool successfulConvertion = int.TryParse(bens[bens.Length - 1], out NumberOfChildren);

                                    if (successfulConvertion)
                                    {
                                        premium.NoOfChildren = (NumberOfChildren - 1).ToString();
                                    }

                                    thePremiums.Add(premium);

                                }

                                Premiums = thePremiums;
                            }
                        }
                        else
                        {
                            await RemoveLoadingMessage();
                            await ShowErrorMessage("Something wetnt wrong when getting Boma Premiums");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "View Benefits Boma", "", "");
                await ShowErrorMessage("Sorry something went wrong getting boma premium");
            }
        }


        public async Task GetBomaCoverDetails()
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
                            Resource = "/MedicalCover/GetBomaCoverBenefits"
                        };


                        var response = await client.ExecuteAsync(restRequest);


                        if (response.IsSuccessful)
                        {
                            var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<BomaCoverBenefit>>>(response.Content);

                            if (deserializedResponse.success)
                            {
                                List<Cover> theBenefits = new List<Cover>();

                                foreach (var item in deserializedResponse.data)
                                {
                                    Cover coverbenefit = new Cover()
                                    {
                                         Name = item.department,
                                         Mplus3 = item.m3.ToString("N0"),
                                         Mplus5 = item.m5.ToString("N0")

                                    };

                                    theBenefits.Add(coverbenefit);

                                }

                                Covers = theBenefits;
                            }
                        }
                        else
                        {
                            await RemoveLoadingMessage();
                            await ShowErrorMessage("Something wetnt wrong when getting Boma Cover Benefits");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "View Benefits Boma", "", "");
            }
        }

    }

    public class Cover
    {
        public string Name { get; set; }
        public string Mplus3 { get; set; }
        public string Mplus5 { get; set; }

    }

    public class Premium
    {
        public string Beneficiary { get; set; }
        public string Amount { get; set; }
        public string NoOfChildren { get; set; }

    }
}
