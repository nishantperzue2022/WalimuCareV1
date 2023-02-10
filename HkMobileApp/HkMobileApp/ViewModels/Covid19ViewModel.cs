using HkMobileApp.Models.HkApiBody;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class Covid19ViewModel : AppViewModel
    {

        private List<Covid19> myCovid19;
        public List<Covid19> MyCovid19
        {
            get { return myCovid19; }
            set { myCovid19 = value; OnPropertyChanged(); }
        }

        public ICommand GetCovid19ListCommand { get; set; }

        public ICommand Covid19SymptomsCheckerCommand { get; set; }


        public Covid19ViewModel()
        {
            GetCovid19ListCommand = new Command(async () => await GetCovid19List());

            Covid19SymptomsCheckerCommand = new Command(async () => await Covid19SymptomsChecker());

            Task.Run(async () =>
            {
                await GetCovid19List();
            });
        }


        public async Task GetCovid19List()
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
                        IsRefreshing = true;

                        await Task.Delay(2000);

                        MyCovid19 = new List<Covid19>();


                        await ShowLoadingMessage();

                        RestClient client = new RestClient()
                        {
                            BaseUrl = new Uri(ApiDetail.EndPoint)
                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Resource = "/Complaints/GetCovidQuestions",
                            Method = Method.GET
                        };



                        var response = await client.ExecuteAsync(restRequest);

                        if (response.IsSuccessful)
                        {
                            var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<Covid19>>>(response.Content);

                            if (deserializedResponse.success)
                            {
                                MyCovid19 = deserializedResponse.data;
                                await RemoveLoadingMessage();
                            }
                            else
                            {
                                await ShowErrorMessage("Sorry, no Covid 19 were found");
                            }


                        }
                        else
                        {
                            await ShowErrorMessage();
                        }

                    }
                }




                //MyCovid19 = new List<Covid19>()
                //{
                //    new Covid19()
                //    {
                //        Title = "What is Covid 19?",
                //        Content = "Telemedicine allows health care professionals to evaluate, diagnose and treat patients at a distance using telecommunications technology. The approach has been through a striking evolution in the last decade and it is becoming an increasingly important part of the American healthcare infrastructure."
                //    },
                //    new Covid19()
                //    {

                //        Title = "Who is at risk ?",
                //        Content = "Telemedicine allows health care professionals to evaluate"
                //    },
                //     new Covid19()
                //    {

                //        Title = "How to avoid Covid 19",
                //        Content = "Telemedicine allows health care professionals to evaluate, diagnose and treat patients at a distance using telecommunications technology. The approach has been through a striking evolution in the last decade and it is becoming an increasingly important part of the American healthcare infrastructure."
                //    },
                //      new Covid19()
                //    {

                //        Title = "Wearing a Mask",
                //        Content = " treat patients at a distance using telecommunications technology. The approach has been through a striking evolution in the last decade and it is becoming an increasingly important part of the American healthcare infrastructure."
                //    },
                //        new Covid19()
                //    {

                //        Title = "Visitor guidelines",
                //        Content = "The approach has been through a striking evolution in the last decade and it is becoming an increasingly important part of the American healthcare infrastructure."
                //    }
                //};

                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "FAQ");
            }
        }


        public async Task Covid19SymptomsChecker()
        {
            try
            {

                await Browser.OpenAsync("http://stage.healthierkenya.com/Assesment/index", BrowserLaunchMode.SystemPreferred);

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Covid 19");
            }
        }

    }
}
