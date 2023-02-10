using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Models.HkApiResponses.MedicalCover;
using HkMobileApp.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels.MedicalCoverViewModels.Base
{
    public class MedicalCoverOrderViewModel :AppViewModel
    {
        //public ObservableCollection<OrderDetail> 

        private ObservableCollection<OrderDetail> orders;
        public ObservableCollection<OrderDetail> Orders
        {
            get { return orders; }
            set { orders = value; OnPropertyChanged(); }
        }


        public ICommand GetOrdersCommand { get; set; }

        public MedicalCoverOrderViewModel()
        {
            GetOrdersCommand = new Command(async ()=> await GetOrders());

            Task.Run(async () =>
            {
                await GetOrders();
            });
        }


        public async Task GetOrders()
        {
            try
            {

                IsEmptyIllustrationVisible = false;
                NoDataAvailableMessage = "";
                IsRefreshing = true;
                IsListViewVisible = true;




                RestClient client = new RestClient()
                {
                    BaseUrl = new Uri(ApiDetail.EndPoint)
                };

                RestRequest restRequest = new RestRequest()
                {
                    Method = Method.GET,
                    Resource = "/MedicalCover/GetMemberOrders"
                };


                string phoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");

                restRequest.AddQueryParameter("PhoneOrEmailFrmDB", phoneNumber);

                var response = await client.ExecuteAsync(restRequest);

                if (response.IsSuccessful)
                {
                    var deserilizedResponse = JsonConvert.DeserializeObject<BaseResponse<List<OrderDetail>>>(response.Content);

                    if (deserilizedResponse.success)
                    {

                        if(deserilizedResponse.data!=null && deserilizedResponse.data.Count > 0)
                        {
                            Orders = new ObservableCollection<OrderDetail>();

                            foreach (var item in deserilizedResponse.data)
                            {
                                Orders.Add(item);
                            }

                        }
                        else
                        {
                            IsEmptyIllustrationVisible = true;
                            NoDataAvailableMessage = "You dont have any orders yet";
                            IsListViewVisible = false;
                        }

                    }
                    else
                    {
                        IsEmptyIllustrationVisible = true;
                        NoDataAvailableMessage = "Ooops something went wrong";
                        IsListViewVisible = false;
                    }
                }
                else
                {
                    IsEmptyIllustrationVisible = true;
                    NoDataAvailableMessage = "Ooops something went wrong";
                    IsListViewVisible = false;
                }


            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Dashboard");
                IsEmptyIllustrationVisible = true;
                NoDataAvailableMessage = "Ooops something went wrong";
                IsRefreshing = false;
                IsListViewVisible = false;
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
