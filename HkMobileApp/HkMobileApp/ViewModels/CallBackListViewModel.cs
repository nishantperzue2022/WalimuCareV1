using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views;
using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace HkMobileApp.ViewModels
{
    public class CallBackListViewModel: AppViewModel
    {
        public string MemberId { get; set; }

        private List<CallBackrequests> callBackRequests;

        public List<CallBackrequests> CallBackRequests
        {
            get { return callBackRequests; }
            set { callBackRequests = value; }
        }
        public ICommand RefreshCommand { get; set; }
        

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }
        public CallBackListViewModel()
        {
            try
            {
                MemberId = Preferences.Get(nameof(AspNetUsers.memberId), "");

                Task.Run(async () => {

                    await getCallBacklist();
                });

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task getCallBacklist()
        {
            try
            {

                if (MemberId == "" )
                {
                    await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Something is not right, please log out then Login again"));
                    return;
                }

                IsRefreshing = true;
                IsActive = true;

                RestClient client = new RestClient()
                {

                    BaseUrl = new Uri(ApiDetail.EndPoint)

                };

                RestRequest restRequest = new RestRequest()
                {
                    Method = Method.POST,
                    Resource = "/Members/CallBackRequests"
                };


                object payload = new
                {
                    MemberId = MemberId
                };

                //test credentials
                //object payload = new
                //{
                //    scheme_id = 6,
                //    member_no = "720226789"
                //};



                restRequest.AddJsonBody(payload);


                var response = await Task.Run(async () =>
                {
                    //Thread.Sleep(2000);
                    return await client.ExecuteAsync(restRequest);
                });


                if (response.IsSuccessful)
                {
                    var serializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<CallBackrequests>>>(response.Content);


                    if (serializedResponse.success)
                    {
                        var callBacksData = serializedResponse.data;

                        var callBackList = new List<CallBackrequests>();


                        foreach (var item in callBacksData)
                        {
                            callBackList.Add(item);
                        }


                        IsRefreshing = false;
                        IsActive = false;
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
                IsRefreshing = false;
                IsActive = false;
                SendErrorMessageToAppCenter(ex, "CallBack list", MemberId);
            }

        }
    }
}
