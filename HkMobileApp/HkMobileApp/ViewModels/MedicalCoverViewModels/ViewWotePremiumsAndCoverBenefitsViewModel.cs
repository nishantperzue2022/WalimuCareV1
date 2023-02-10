using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views.MedicalCover.WoteCoverViews;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels.MedicalCoverViewModels
{
    public class ViewWotePremiumsAndCoverBenefitsViewModel : AppViewModel
    {

        private ObservableCollection<WotePremiumCategory> wotePremiumCategories;
        public ObservableCollection<WotePremiumCategory> WotePremiumCategories
        {
            get { return wotePremiumCategories; }
            set { wotePremiumCategories = value;
                OnPropertyChanged();
            }
        }

        public List<WotePremiumDetail> WotePremiumDetails { get; set; }

        public ICommand GoToEnterNhifAndBasicDetailsCommand { get; set; }


        public ViewWotePremiumsAndCoverBenefitsViewModel()
        {
            PageTitle = "Wote Premiums and Covers";
            PageSubTitle = "View Premium charges and Cover Benefit Amount";

            Task.Run(async () =>
            {
                await GetWotePremiums();
            });

            GoToEnterNhifAndBasicDetailsCommand = new Command(async () => await GoToEnterNhifAndBasicDetails());
        }


        public async Task GetWotePremiums()
        {
            try
            {
                if(await CheckInternetConnectivity())
                {
                    if(await CheckIfApiDetailsAreSetUp())
                    {
                        await ShowLoadingMessage();

                        RestClient client = new RestClient()
                        {
                            BaseUrl = new Uri(ApiDetail.EndPoint)
                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.GET,
                            Resource = "/MedicalCover/GetWoteMedicalCoverDetails"
                        };

                        var response = await client.ExecuteAsync(restRequest);

                        await RemoveLoadingMessage();

                        if (response.IsSuccessful)
                        {
                            var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<WotePremiumDetail>>>(response.Content);


                            WotePremiumDetails = deserializedResponse.data;

                            var data = new ObservableCollection<WotePremiumCategory>();

                            if (deserializedResponse.success)
                            {

                                var bronze = new WotePremiumCategory()
                                {
                                    Title = "Bronze",
                                    ColorCode = "#cd7f32",
                                    Premiums = new List<WotePremiumSingleDetail>()
                                };

                                foreach (var item in deserializedResponse.data)
                                {
                                    WotePremiumSingleDetail bomaDetail = new WotePremiumSingleDetail()
                                    {
                                        PremiumAmount = item.bronze.ToString("N0"),
                                        BeneficiaryGroup = item.benefit
                                    };

                                    var number = item.benefit.Split('+');
                                    bomaDetail.NumberOfBeneficiariesMinusPrincipal = number[number.Length - 1];

                                    bronze.Premiums.Add(bomaDetail);
                                }

                                data.Add(bronze);


                                var silver = new WotePremiumCategory()
                                {
                                    Title = "Silver",
                                    ColorCode = "#C0C0C0",
                                    Premiums = new List<WotePremiumSingleDetail>()
                                };

                                foreach (var item in deserializedResponse.data)
                                {
                                    WotePremiumSingleDetail silverDetail = new WotePremiumSingleDetail()
                                    {
                                        PremiumAmount = item.silver.ToString("N0"),
                                        BeneficiaryGroup = item.benefit
                                    };

                                    var number = item.benefit.Split('+');
                                    silverDetail.NumberOfBeneficiariesMinusPrincipal = number[number.Length - 1];

                                    silver.Premiums.Add(silverDetail);
                                }

                                data.Add(silver);


                                var gold = new WotePremiumCategory()
                                {
                                    Title = "Gold",
                                    ColorCode = "#d4af37",
                                    Premiums = new List<WotePremiumSingleDetail>()

                                };

                                foreach (var item in deserializedResponse.data)
                                {
                                    WotePremiumSingleDetail goldDetail = new WotePremiumSingleDetail()
                                    {
                                        PremiumAmount = item.gold.ToString("N0"),
                                        BeneficiaryGroup = item.benefit
                                    };

                                    var number = item.benefit.Split('+');
                                    goldDetail.NumberOfBeneficiariesMinusPrincipal = number[number.Length - 1];

                                    gold.Premiums.Add(goldDetail);
                                }

                                data.Add(gold);

                                WotePremiumCategories = data;

                            }
                            else
                            {
                                await ShowErrorMessage();
                            }
                        }
                        else
                        {
                            await ShowErrorMessage("Something went wrong, please try again after sometime");
                        }

                    }
                }



            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Wote Premiums", "", "");
                await ShowErrorMessage();
            }
        }

        public async Task GoToEnterNhifAndBasicDetails()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(WoteAddNhifDetails));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Wote Premiums");
            }
        }

    }

    public class WotePremiumCategory
    {
        public string Title { get; set; }

        public string ColorCode { get; set; }

        public List<WotePremiumSingleDetail> Premiums { get; set; }
    }

    public class WotePremiumSingleDetail
    {
        public string PremiumAmount { get; set; }
        public string NumberOfBeneficiariesMinusPrincipal { get; set; }
        public string BeneficiaryGroup { get; set; }
    }
}
