using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views.Others;
using HkMobileApp.Views.ShellPages;
using Newtonsoft.Json;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class RequestCallBackViewModel : AppViewModel
    {
        public string MemberId { get; set; }
        public string checkController { get; set; }

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private string reason;

        public string Reason
        {
            get { return reason; }
            set { 
                reason = value;
                OnPropertyChanged(nameof(Reason));
            }
        }

        private List<CallBackrequests> callBackrequests;

        public List<CallBackrequests> CallBackrequests
        {
            get { return callBackrequests; }
            set { callBackrequests = value; OnPropertyChanged(); }
        }



        private string selectedRdoBtn;

        public string SelectedRdoBtn
        {
            get { return selectedRdoBtn; }
            set {


                selectedRdoBtn = value;
                OnPropertyChanged(nameof(SelectedRdoBtn));
                rdBtnChecked();
            }
        }

        private bool isPhnNoEditorEnabled;

        public bool IsPhnNoEditorEnabled
        {
            get { return isPhnNoEditorEnabled; }
            set {
                isPhnNoEditorEnabled = value;
                OnPropertyChanged(nameof(IsPhnNoEditorEnabled));
            }
        }

        public int MyProperty { get; set; }

        public ICommand GetMyNumber { get; set; }
        public ICommand GetOtherNumber { get; set; }
        public ICommand GetMemberRequestsCommand { get; set; }
        public ICommand SubmitCallBackRequest { get; set; }
        public ICommand viewCallBackLstCommand { get; set; }

        public ICommand AddNewRequestCommand { get; set; }

        public RequestCallBackViewModel()
        {
            try
            {
                IsPhnNoEditorEnabled = false;
                SubmitCallBackRequest = new Command(async () => await SaveCallBackRequests());
                viewCallBackLstCommand = new Command( async () => await GoToLists());
                GetMemberRequestsCommand = new Command(async () => await GetMemberRequests());
                AddNewRequestCommand = new Command(async () => await AddNewCallBackRequest());
                selectedRdoBtn = "Call my number";
                Preferences.Set("rdbtnController", "owner");
                PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
                MemberId = Preferences.Get(nameof(AspNetUsers.memberId), "");
               checkController = Preferences.Get("rdbtnController", "owner");

                Task.Run(async () =>
                {
                    await GetMemberRequests();
                });


                PageTitle = "Request Call";
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Request A call back", "acha jokes", PhoneNumber);
            }
        }
        public void rdBtnChecked()
        {
            if (checkController == "owner")
            {
                IsPhnNoEditorEnabled = false;

                PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");
                checkController = "Request";
            }
            else
            {
                IsPhnNoEditorEnabled = true;

                PhoneNumber = "";

                checkController = Preferences.Get("rdbtnController", "owner");

            }
        }
        public async Task AddNewCallBackRequest()
        {
            try
            {
                //await Shell.Current.GoToAsync(nameof(RequestCallBack));
                await Shell.Current.Navigation.PushAsync(new RequestCallBack());
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Request Call Back");
            }
        }

        public async Task GetMemberRequests()
        {
            try
            {

                IsListViewVisible = true;
                IsEmptyIllustrationVisible = false;
                NoDataAvailableMessage = "";
                IsRefreshing = true;

                CallBackrequests = new List<CallBackrequests>();




                RestClient client = new RestClient()
                {
                    BaseUrl = new Uri(ApiDetail.EndPoint)
                };

                RestRequest restRequest = new RestRequest()
                {
                    Resource = "/Members/CallBackRequests",
                    Method = Method.GET
                };

                var memberId = Preferences.Get(nameof(AspNetUsers.memberId), "");

                restRequest.AddQueryParameter("MemberId", memberId);

                var response = await client.ExecuteAsync(restRequest);

                if (response.IsSuccessful)
                {
                    var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<CallBackrequests>>>(response.Content);

                    if (deserializedResponse.success)
                    {
                        CallBackrequests = deserializedResponse.data;
                        await RemoveLoadingMessage();

                        if (CallBackrequests.Count > 0)
                        {
                            IsListViewVisible = true;
                            IsEmptyIllustrationVisible = false;
                            NoDataAvailableMessage = "";
                            IsRefreshing = false;
                        }
                        else
                        {
                            IsListViewVisible = false;
                            IsEmptyIllustrationVisible = true;
                            NoDataAvailableMessage = "Sorry you dont have any call requests lodged";
                            IsRefreshing = false;
                        }
                    }
                    else
                    {

                        await RemoveLoadingMessage();
                        IsListViewVisible = false;
                        IsEmptyIllustrationVisible = true;
                        NoDataAvailableMessage = "Something went wrong, Please try again";
                        IsRefreshing = false;
                    }


                }
                else
                {
                    await RemoveLoadingMessage();
                    IsListViewVisible = false;
                    IsEmptyIllustrationVisible = true;
                    NoDataAvailableMessage = "Something went wrong, Please try again";
                    IsRefreshing = false;
                }


            }
            catch (Exception ex)
            {
                await ShowErrorMessage();
                SendErrorMessageToAppCenter(ex, "Request Call Back");

                IsListViewVisible = false;
                IsEmptyIllustrationVisible = true;
                NoDataAvailableMessage = "Something went wrong, Please try again";
                IsRefreshing = false;
            }
        }

        public async Task SaveCallBackRequests()
        {
            try
            {
                if (await CheckInternetConnectivity())
                {
                    if (!await CheckIfApiDetailsAreSetUp())
                    {

                    }
                    else
                    {

                        RestClient client = new RestClient()
                        {

                            BaseUrl = new Uri(ApiDetail.EndPoint)

                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.POST,
                            Resource = "/Members/MemberCallBackRequest"
                        };

                        CallBackrequests nRgst = new CallBackrequests()
                        {
                            MemberId = MemberId,
                            PhoneNumber = PhoneNumber,
                            RequestDate = DateTime.UtcNow,
                            RequestStatus = 1,
                            Reason = Reason
                        };

                        restRequest.AddJsonBody(nRgst);


                        var response = await Task.Run(() =>
                        {
                            return client.Execute(restRequest);
                        });

                        bool isSuccesful = false;

                        try
                        {
                            if (response.IsSuccessful)
                            {
                                var serializedResponse = JsonConvert.DeserializeObject<BaseResponse<CallBackrequests>>(response.Content);


                                if (serializedResponse.success)
                                {
                                    isSuccesful = serializedResponse.success;

                                    await ShowSuccessMessage("Call Back request Submitted successfuly");

                                    Thread.Sleep(2000);
                                    //await DependencyService.Get<ManageDependantsViewModel>().GetDependants();

                                    //await App.Current.MainPage.Navigation.PopPopupAsync();
                                    await Shell.Current.Navigation.PopAsync();

                                    //await App.Current.MainPage.Navigation.PushAsync(new HomePage());
                                    //await Shell.Current.GoToAsync(nameof(HomePage));


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

                            SendErrorMessageToAppCenter(e, "Request CallBack ", MemberId, PhoneNumber);
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                await ShowErrorMessage();
                SendErrorMessageToAppCenter(ex, "Request Call back", MemberId, PhoneNumber);
            }

        }
        
        public async Task GoToLists()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(CallBackList), true);
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Request A call back", "acha jokes", PhoneNumber);
            }
        }


    }
}
