using Android.OS;
using Android.Preferences;
using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views;
using HkMobileApp.Views.Claims;
using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class TrackClaimViewModel : AppViewModel
    {
        private string schemeId;
        public string SchemeId
        {
            get { return schemeId; }
            set { schemeId = value; OnPropertyChanged(nameof(SchemeId)); }
        }



        private string memberNo;
        public string MemberNo
        {
            get { return memberNo; }
            set { memberNo = value; OnPropertyChanged(nameof(MemberNo)); }
        }



        public string PhoneNumber { get; set; }





        private ObservableCollection<Claim> claims;
        public ObservableCollection<Claim> Claims
        {
            get { return claims; }
            set { claims = value; OnPropertyChanged(nameof(Claims)); }
        }




        public ICommand RefreshCommand { get; set; }




        private SingleClaim selectedClaim;
        public SingleClaim SelectedClaim
        {
            get { return selectedClaim; }
            set
            {
                selectedClaim = value;
                OnPropertyChanged(nameof(SelectedClaim));
            }
        }





        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }





        private List<HospitalService> hospitalServices;
        public List<HospitalService> HospitalServices
        {
            get { return hospitalServices; }
            set { hospitalServices = value; OnPropertyChanged(); }
        }



        private bool isListViewVisible;

        public bool IsListViewVisible
        {
            get { return isListViewVisible; }
            set { isListViewVisible = value;
                OnPropertyChanged(); }
        }



        public TrackClaimViewModel()
        {

            try
            {
                PageTitle = "Hospital Visits";
                PageSubTitle = "View costs, diagnosis concerning your hospital visit";
                MemberNo = Preferences.Get("MemberNumber", "");
                PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
                var SchemeIdInt = Preferences.Get("SchemeId", 0);
                SchemeId = SchemeIdInt.ToString();

                RefreshCommand = new Command(async () => await GetClaims());


                Task.Run(async () =>
                {
                    await GetClaims();
                });


            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Find Hospital", MemberNo, PhoneNumber);
                Console.WriteLine(ex);
            }



        }


        public async Task GetClaims()
        {
            try
            {

                if (SchemeId == "" && MemberNo == "")
                {
                    await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Something is not right, please log out then Login again"));
                    return;
                }


                if(await CheckInternetConnectivity())
                {
                    if(await CheckIfApiDetailsAreSetUp())
                    {
                        IsRefreshing = true;
                        IsActive = true;
                        IsEmptyIllustrationVisible = false;
                        IsListViewVisible = true;

                        RestClient client = new RestClient()
                        {

                            BaseUrl = new Uri(ApiDetail.EndPoint)

                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.POST,
                            Resource = "/Claims/GetMemberClaims"
                        };


                        object payload = new
                        {
                            scheme_id = Convert.ToInt32(SchemeId),
                            member_no = MemberNo
                        };

                        restRequest.AddJsonBody(payload);


                        var response = await Task.Run(async () =>
                        {
                            //Thread.Sleep(2000);
                            return await client.ExecuteAsync(restRequest);
                        });


                        if (response.IsSuccessful)
                        {
                            var serializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<Claim>>>(response.Content);


                            if (serializedResponse.success)
                            {
                                var claimsData = serializedResponse.data ?? new List<Claim>();
                                

                                var claimsObservableCollections = new ObservableCollection<Claim>();


                                foreach (var item in claimsData)
                                {
                                    claimsObservableCollections.Add(item);
                                }

                                Claims = claimsObservableCollections;

                                IsRefreshing = false;
                                IsActive = false;

                                if (claimsData.Count == 0)
                                {
                                    IsEmptyIllustrationVisible = true;
                                    NoDataAvailableMessage = "You do not have any Hospital Visits";
                                    IsListViewVisible = false;
                                }
                            }
                            else
                            {
                                IsRefreshing = false;
                                IsActive = false;
                                //await ShowErrorMessage();
                                IsEmptyIllustrationVisible = true;
                                NoDataAvailableMessage = "Sorry something went wrong, please try again later";
                                IsListViewVisible = false;
                            }


                        }
                        else
                        {
                            await ShowErrorMessage();

                            IsRefreshing = false;
                            IsActive = false;
                        }
                    }
                }

                


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                IsRefreshing = false;
                IsActive = false;
                IsEmptyIllustrationVisible = true;
                NoDataAvailableMessage = "Sorry Something went wrong";
                SendErrorMessageToAppCenter(ex, "Track Hospital Visits", MemberNo, PhoneNumber);
            }
        }


        public async void SelectSelectedClaim(long ClaimId)
        {
            try
            {
                //SelectedClaim = Claims.Where(p => p.claim_id == ClaimId).FirstOrDefault();
                //Preferences.Set("selectedClaim", SelectedClaim.id);

                await ShowLoadingMessage();

                RestClient client = new RestClient()
                {

                    BaseUrl = new Uri(ApiDetail.EndPoint)

                };

                RestRequest restRequest = new RestRequest()
                {
                    Method = Method.POST,
                    Resource = "/Claims/GetClaimDetails"
                };

                //object payload = new
                //{
                //    ClaimID = ClaimId.ToString()
                //};

                //test credentials
                //object payload = new
                //{
                //    scheme_id = 6,
                //    member_no = "720226789"
                //};



                restRequest.AddQueryParameter("ClaimID",ClaimId.ToString());


                var response = await Task.Run(async () =>
                {
                    //Thread.Sleep(2000);
                    return await client.ExecuteAsync(restRequest);
                });


                if (response.IsSuccessful)
                {
                    var serializedResponse = JsonConvert.DeserializeObject<BaseResponse<SingleClaim>>(response.Content);


                    if (serializedResponse.success)
                    {
                        var claimsData = serializedResponse.data;

                        SelectedClaim = claimsData;

                        await Shell.Current.GoToAsync(nameof(ClaimDetailsPage), true);
                        //await App.Current.MainPage.Navigation.PushAsync(new ClaimDetailsPage());

                        await RemoveLoadingMessage();

                        //var claimsObservableCollections = new ObservableCollection<SingleClaim>();


                        //foreach (var item in claimsData)
                        //{
                        //    claimsObservableCollections.Add(item);
                        //}

                        //Claims = claimsObservableCollections;

                        IsRefreshing = false;
                        IsActive = false;

                        await GetHospitalServicesForSelectedPatient();
                    }
                    else
                    {
                        IsRefreshing = false;
                        IsActive = false;
                        await ShowErrorMessage();
                    }


                }
                else
                {
                    await ShowErrorMessage();

                    IsRefreshing = false;
                    IsActive = false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                SendErrorMessageToAppCenter(ex, "Find Hospital", MemberNo, PhoneNumber);
            }
        }


        public async Task GetHospitalServicesForSelectedPatient()
        {
            try
            {
                await Task.Delay(1000);

                HospitalServices = new List<HospitalService>()
                {
                    new HospitalService(){ Name = "Investigation", Amount = 1000 },
                    new HospitalService(){ Name = "Prescription", Amount = 2000 },
                    new HospitalService(){ Name = "Consultation", Amount = 1500 },
                    new HospitalService(){ Name = "Another Service", Amount = 5000 },
                    new HospitalService(){ Name = "Another Service 2", Amount = 7000 }
                };
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Claims");
            }
        }
    }

    public class HospitalService
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
