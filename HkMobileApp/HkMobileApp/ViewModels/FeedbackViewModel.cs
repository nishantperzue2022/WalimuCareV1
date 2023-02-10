using Java.Util.Prefs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using HkMobileApp.Models;
using Rg.Plugins.Popup.Extensions;
using HkMobileApp.Views;
using System.Threading;
using Plugin.Connectivity;
using HkMobileApp.Services;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using HkMobileApp.Models.HkApiResponses;
using Preferences = Xamarin.Essentials.Preferences;
using HkMobileApp.Views.ShellPages;

namespace HkMobileApp.ViewModels
{
    public class FeedbackViewModel : AppViewModel
    {

        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; OnPropertyChanged(nameof(FullName)); }
        }


        private string feedBackType;

        public string FeedbBackType
        {
            get { return feedBackType; }
            set { feedBackType = value; OnPropertyChanged(nameof(FeedbBackType)); }
        }

        private FeedBackClinics selectedFacility;

        public FeedBackClinics SelectedFacility
        {
            get { return selectedFacility; }
            set { selectedFacility = value; OnPropertyChanged(nameof(SelectedFacility)); }
        }

        private string feedBackDescription;

        public string FeedbackDescription
        {
            get { return feedBackDescription; }
            set { feedBackDescription = value; OnPropertyChanged(nameof(FeedbackDescription)); }
        }

        private ObservableCollection<string> facilities;

        public ObservableCollection<string> Facilities
        {
            get { return facilities; }
            set { facilities = value; OnPropertyChanged(nameof(Facilities)); }
        }

        private ObservableCollection<FeedBackClinics> clinics;

        public ObservableCollection<FeedBackClinics> Clinics
        {
            get { return clinics; }
            set { clinics = value; OnPropertyChanged(nameof(Clinics)); }
        }

        public List<Hospital> lstHospitals { get; set; }
        public List<Hospital> LstHospitals
        {
            get { return lstHospitals; }
            set
            {
                lstHospitals = value;
                OnPropertyChanged(nameof(lstHospitals));
            }
        }

        private string location;

        public string Location
        {
            get { return location; }
            set { location = value;  OnPropertyChanged(nameof(Location)); }
        }

        private string userIcon;

        public string UserIcon
        {
            get { return userIcon; }
            set { userIcon = value;
                OnPropertyChanged(nameof(UserIcon));
            }
        }

        public ICommand SubmitFeedback { get; set; }

        public FeedbackViewModel()
        {
            SubmitFeedback = new Command(SaveFeedBack);

            GetFacilities();
            GetClinicsAsync();

            string Name = Xamarin.Essentials.Preferences.Get(nameof(AspNetUsers.firstName), "") + " ";
             Name += Xamarin.Essentials.Preferences.Get(nameof(AspNetUsers.lastName), "");

            FullName = Name;
            Location = "Nairobi";
            UserIcon = "avator.png";
            PageTitle = "Feedback";
        }

        public async void SaveFeedBack()
        {
            //try
            //{
            //    await App.Current.MainPage.Navigation.PushPopupAsync(new HkLoaderPage("Saving Feddback"));

            //    Thread.Sleep(5000);

            //   await App.Current.MainPage.Navigation.PopPopupAsync();

            //    await App.Current.MainPage.Navigation.PushPopupAsync(new HkSuccessPage("Saved Successfully"));

            //    Thread.Sleep(5000);

            //    await App.Current.MainPage.Navigation.PopPopupAsync();
            //}
            //catch (Exception ex)
            //{

            //}
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    if (ApiDetail.EndPoint == null || ApiDetail.EndPoint.Trim() == "")
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Something is not right, please logout and login again"));

                        Thread.Sleep(2000);

                        await App.Current.MainPage.Navigation.PopPopupAsync();

                    }
                    else
                    {

                        await App.Current.MainPage.Navigation.PushPopupAsync(new HkLoaderPage("Please wait as we submit your feedback"));

                        RestClient client = new RestClient()
                        {

                            BaseUrl = new Uri(ApiDetail.EndPoint)

                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.POST,
                            Resource = "/Members/PostMemberFeedback"
                        };
                        Guid MbrId = Guid.Parse( Preferences.Get(nameof(AspNetUsers.memberId),""));
                        FeedBack addFeedback = new FeedBack()
                        {
                            MemberId = MbrId,
                            Remarks = feedBackDescription,
                            FeedBackType = 1,
                            clinicId = selectedFacility.clinicId


                        };



                        restRequest.AddJsonBody(addFeedback);


                        var response = await Task.Run(() =>
                        {
                            return client.Execute(restRequest);
                        });



                        bool isSuccesful = false;

                        try
                        {
                            if (response.IsSuccessful && response.Content.Length > 2)
                            {
                                isSuccesful = response.IsSuccessful; //JsonConvert.DeserializeObject<bool>(response.Content);

                                await App.Current.MainPage.Navigation.PopPopupAsync();

                                await App.Current.MainPage.Navigation.PushPopupAsync(new HkSuccessPage("FeedBack send successfuly"));

                                Thread.Sleep(2000);

                                await App.Current.MainPage.Navigation.PopPopupAsync();

                                await App.Current.MainPage.Navigation.PushAsync(new HomePage());

                                //DependencyService.Get<ManageDependantsViewModel>().GetDependants();
                            }
                            else
                            {
                                isSuccesful = false;


                                await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Something went wrong while adding dependant details"));


                            }


                        }
                        catch (Exception e)
                        {
                            await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Something went wrong while adding dependant details"));



                        }
                    }


                }
                else
                {
                    //await App.Current.MainPage.DisplayAlert("Internet Required", "Please switch on your data or connect to wifi before proceeding", "ok");

                    await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Please switch on your data or connect to wifi before proceeding"));



                }
            }
            catch (Exception ex)
            {

            }
        }


        public void GetFacilities()
        {
            try
            {
                Facilities = new ObservableCollection<string>();

                Facilities.Add("Bliss Medical Center");
                Facilities.Add("Medicross Medical Center");
                Facilities.Add("Lifecare Hospital");
            }
            catch (Exception ex)
            {

            }
        }

        public async void GetClinicsAsync()
        {
            try
            {
                clinics = new ObservableCollection<FeedBackClinics>();
                try
                {

                    if (CrossConnectivity.Current.IsConnected)
                    {
                        if (ApiDetail.EndPoint == null || ApiDetail.EndPoint.Trim() == "")
                        {

                            await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Something is not right, please logout and login again"));

                        }
                        else
                        {

                            //await App.Current.MainPage.Navigation.PushPopupAsync(new HkLoaderPage("Loading Data"));

                            await Task.Delay(1000);

                            RestClient client = new RestClient()
                            {

                                BaseUrl = new Uri(ApiDetail.EndPoint)

                            };

                            RestRequest restRequest = new RestRequest()
                            {
                                Method = Method.GET,
                                Resource = "/Hospitals/GetAllClinics"
                            };

                            var response = await client.ExecuteAsync(restRequest);

                            if (response.IsSuccessful)
                            {
                               var  ListHospitals = JsonConvert.DeserializeObject<BaseResponse<List<Hospital>>>(response.Content);

                                foreach (var hospital in ListHospitals.data)
                                {
                                    clinics.Add(new FeedBackClinics() { clinicId = hospital.pkid, ClinicName = hospital.name });

                                }


                            }
                        }

                    }
                    else
                    {

                        try
                        {
                            //await App.Current.MainPage.DisplayAlert("Internet Required", "Please switch on your data or connect to wifi before proceeding", "ok");
                            await App.Current.MainPage.Navigation.PopAllPopupAsync();
                            await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Please switch on your data or connect to wifi before proceeding"));

                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex);

                        }

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



    }
}
