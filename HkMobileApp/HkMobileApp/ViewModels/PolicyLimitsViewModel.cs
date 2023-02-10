using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class PolicyLimitsViewModel : AppViewModel
    {
        private string MemberId { get; set; }

        public string PhoneNumber { get; set; }
        public int SchemeId { get; set; }

        public string MemberNumber { get; set; }


        private ObservableCollection<PolicyDetails> policyDetails;

        public ObservableCollection<PolicyDetails> PolicyDetails
        {
            get { return policyDetails; }
            set
            {
                policyDetails = value;
                OnPropertyChanged(nameof(PolicyDetails));
            }
        }

        public ICommand RefreshListView { get; set; }

        public PolicyLimitsViewModel()
        {

            PageTitle = "Policy Limits";
            PageSubTitle = "View your available limits and track your utilization";

            MemberId = Preferences.Get(nameof(AspNetUsers.memberId), "");

            PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");

            SchemeId = Preferences.Get("SchemeId", 0);


            Task.Run(async()=> await GetMemberPolicyDetails());



            RefreshListView = new Command(async()=> await GetMemberPolicyDetails());

        }

        private async Task GetMemberPolicyDetails()
        {
            try
            {

                if (await CheckInternetConnectivity())
                {

                    if (await CheckIfApiDetailsAreSetUp())
                    {
                        if (!string.IsNullOrEmpty(MemberId))
                        {
                            IsRefreshing = true;
                            IsEmptyIllustrationVisible = false;
                            NoDataAvailableMessage = "";
                            IsListViewVisible = true;

                            RestClient client = new RestClient()
                            {
                                BaseUrl = new Uri(ApiDetail.EndPoint)
                            };

                          
                            RestRequest restRequest = new RestRequest()
                            {
                                Method = Method.GET,
                                Resource = "/Members/GetPolicyLimits"
                            };

                            restRequest.AddQueryParameter("MemberId", MemberId);


                            var response = await Task.Run(async () =>
                            {

                                return await client.ExecuteAsync(restRequest);
                            });


                            try
                            {
                                if (response.IsSuccessful )
                                {

                                    var data2 = JsonConvert.DeserializeObject<BaseResponse<List<PolicyDetailsResponseBody>>>(response.Content);


                                    if (data2.success)
                                    {
                                        PolicyDetails = new ObservableCollection<PolicyDetails>();

                                        foreach (var item in data2.data)
                                        {
                                            PolicyDetails policyDetails = new PolicyDetails();

                                            policyDetails.benefitName = item.department.name;
                                            policyDetails.benefitAmount = item.limit;

                                            policyDetails.ChartData = new ObservableCollection<Series>();

                                            policyDetails.ChartData.Add(new Series()
                                            {
                                                Name = "Utilized Amount",
                                                Amount = item.utilised_limit
                                            });

                                            policyDetails.ChartData.Add(new Series()
                                            {
                                                Name = "Available Amount",
                                                Amount = item.available_limit 
                                            });


                                            PolicyDetails.Add(policyDetails);



                                        }
                                    }
                                    else
                                    {
                                        //await ShowErrorMessage();
                                        IsEmptyIllustrationVisible = true;
                                        NoDataAvailableMessage = "Sorry Something went wrong";
                                        IsListViewVisible = false;
                                    }
                                    
                                }
                                else
                                {
                                    //await ShowErrorMessage();
                                    IsEmptyIllustrationVisible = true;
                                    NoDataAvailableMessage = "Sorry Something went wrong";
                                    IsListViewVisible = false;
                                }


                            }
                            catch (Exception ex)
                            {
                                
                                SendErrorMessageToAppCenter(ex, "Policy Details", MemberNumber, PhoneNumber);
                                Console.WriteLine(ex);

                                //await ShowErrorMessage();
                                IsEmptyIllustrationVisible = true;
                                NoDataAvailableMessage = "Sorry Something went wrong";
                                IsListViewVisible = false;
                            }
                            finally
                            {
                                IsRefreshing = false;
                            }
                        }
                        else
                        {
                            //await ShowErrorMessage();
                            IsEmptyIllustrationVisible = true;
                            NoDataAvailableMessage = "Sorry Something went wrong";
                            IsListViewVisible = false;
                        }
                    }


                }
               


            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Policy Details", MemberNumber, PhoneNumber);
                await ShowErrorMessage();
            }
        }

        private async Task GetSchemeId()
        {
            try
            {

                if (SchemeId == 0)
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        if (ApiDetail.EndPoint == null || ApiDetail.EndPoint.Trim() == "")
                        {
                            await App.Current.MainPage.DisplayAlert("Sorry", "Something is not right, please logout and login again", "ok");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(MemberNumber))
                            {


                                RestClient client = new RestClient()
                                {

                                    BaseUrl = new Uri(ApiDetail.EndPoint)

                                };

                                RestRequest restRequest = new RestRequest()
                                {
                                    Method = Method.POST,
                                    Resource = "/Members/VerifyDetails"
                                };


                                object payload = new
                                {
                                    MemberNumber = MemberNumber,
                                    AddressMobileNumber = PhoneNumber
                                };



                                restRequest.AddJsonBody(payload);


                                var response = await Task.Run(() =>
                                {
                                    return client.Execute(restRequest);
                                });


                                try
                                {
                                    if (response.IsSuccessful && response.Content.Length > 2)
                                    {

                                        var verifyMember = JsonConvert.DeserializeObject<VerifyMember>(response.Content);

                                        if (verifyMember.success)
                                        {

                                            Preferences.Set("SchemeId", verifyMember.data.bls_member.scheme_id);

                                            SchemeId = verifyMember.data.bls_member.scheme_id;

                                        }
                                        else
                                        {
                                            await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry, something went wrong"));
                                        }



                                    }
                                    else
                                    {

                                        await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry, something went wrong"));
                                    }


                                }
                                catch (Exception ex)
                                {
                                    EnableSubmitBtn = true;

                                    SendErrorMessageToAppCenter(ex, "Policy Details", MemberNumber, PhoneNumber);

                                    MainThread.BeginInvokeOnMainThread(async () =>
                                    {
                                        await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry, something went wrong"));
                                    });

                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Login", "Please enter Email / Pin", "ok");
                            }
                        }


                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Internet Required", "Please switch on your data or connect to wifi before proceeding", "ok");
                    }
                }


            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Policy Details", MemberNumber, PhoneNumber);
                Console.WriteLine(ex);
            }
        }

    }

    public class Person
    {
        public string Name { get; set; }

        public double Height { get; set; }
    }
}
