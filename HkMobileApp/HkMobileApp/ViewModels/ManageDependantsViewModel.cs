using Android.OS;
using HkMobileApp.Models;
using HkMobileApp.Models.HkApiBody;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views;
using HkMobileApp.Views.dependants;
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
    public class ManageDependantsViewModel : AppViewModel
    {

        public string MemberId { get; set; }

        private ObservableCollection<Member> dependants { get; set; }
        public ObservableCollection<Member> Dependants
        {
            get { return dependants; }
            set
            {
                dependants = value;
                OnPropertyChanged(nameof(Dependants));
            }
        }


        private Member selectedDependant;
        public Member SelectedDependant
        {
            get { return selectedDependant; }
            set
            {
                selectedDependant = value;
                OnPropertyChanged("SelectedDependant");
            }
        }


        public string PhoneNumber { get; set; }


        public string MemberNumber { get; set; }


        private bool isAddDependantButtonVisible;
        public bool IsAddDependantButtonVisible
        {
            get { return isAddDependantButtonVisible; }
            set
            {
                isAddDependantButtonVisible = value;
                OnPropertyChanged();
            }
        }



        private string noData;
        public string NoData
        {
            get { return noData; }
            set
            {
                noData = value;
                OnPropertyChanged(nameof(NoData));
            }
        }


        private bool isPullToRefreshTextVisible;
        public bool IsPullToRefreshTextVisible
        {
            get { return isPullToRefreshTextVisible; }
            set { isPullToRefreshTextVisible = value; 
                OnPropertyChanged();
            }
        }



        //private bool isEmptyIllustrationVisible;
        //public bool IsEmptyIllustrationVisible
        //{
        //    get { return isEmptyIllustrationVisible; }
        //    set { isEmptyIllustrationVisible = value; OnPropertyChanged(); }
        //}

        public ICommand GoToDependants { get; set; }
        public ICommand SelectDependantForEditingCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ManageDependantsViewModel()
        {
            try
            {
                PageTitle = "Registered Dependents";
                PageSubTitle = "Add and Edit Dependent Details";

                MemberId = Preferences.Get(nameof(AspNetUsers.memberId), "");
                PhoneNumber = Preferences.Get(nameof(AspNetUsers.memberId), "");
                MemberNumber = Preferences.Get("MemberNumber", "");

                GoToDependants = new Command(ShowAdDependantsPage);

                RefreshCommand = new Command(async () => { await GetDependants(); await GetNumberOfDependants(); });

                SelectDependantForEditingCommand = new Command<string>((x) => SelectDependandForEditing(x));

             

                IsAddDependantButtonVisible = true;

                Task.Run(async () =>
                { 
                    await GetDependants();
                    await GetNumberOfDependants();
                });



            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Manage Dependants", MemberNumber, PhoneNumber);
            }

        }

        public async Task GetDependants()
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
                        if (!string.IsNullOrEmpty(MemberId))
                        {
                            IsRefreshing = true;
                            IsPullToRefreshTextVisible = false;
                            RestClient client = new RestClient()
                            {
                                BaseUrl = new Uri(ApiDetail.EndPoint)
                            };

                            RestRequest restRequest = new RestRequest()
                            {
                                Method = Method.POST,
                                Resource = "/Members/GetMemberDepedants"
                            };

                            restRequest.AddQueryParameter("PrincipalMbrId", MemberId);


                            var response = await Task.Run(() =>
                            {
                                return client.Execute(restRequest);
                            });


                            try
                            {
                                if (response.IsSuccessful)
                                {

                                    IsRefreshing = false;



                                    var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<Member>>>(response.Content);


                                    if (deserializedResponse.success)
                                    {
                                        var data = deserializedResponse.data;


                                        var depts = new ObservableCollection<Member>();
                                        if (data.Count <= 0)
                                        {
                                            NoData = "Ooops!! You dont have any Depedants";
                                            IsEmptyIllustrationVisible = true;
                                            IsPullToRefreshTextVisible = true;
                                        }
                                        else
                                        {
                                            NoData = "";
                                            IsEmptyIllustrationVisible = false;
                                        }

                                        foreach (var item in data)
                                        {
                                            if (item.DateOfBirth.HasValue)
                                            {
                                                item.Age = (DateTime.Now.Year - item.DateOfBirth.Value.Year).ToString();
                                            }
                                            else
                                            {
                                                item.Age = "Not Available";
                                            }

                                            item.FullName = item.FirstName + " " + item.MiddleName;

                                            depts.Add(item);

                                        }

                                        MainThread.BeginInvokeOnMainThread(() =>
                                        {
                                            Dependants = depts;
                                        });

                                        IsRefreshing = false;

                                    }
                                    else
                                    {
                                        //await ShowErrorMessage();
                                        NoData = "Ooops, we are unable to retrieve your dependants";
                                        IsEmptyIllustrationVisible = true;
                                    }

                                }
                                else
                                {

                                    //await ShowErrorMessage();
                                    NoData = "Ooops, we are unable to retrieve your dependants";
                                    IsEmptyIllustrationVisible = true;
                                }


                            }
                            catch (Exception ex)
                            {
                                //EnableLoginBtn = true;
                                NoData = "Ooops, we are unable to retrieve your dependants";
                                IsEmptyIllustrationVisible = true;
                                await ShowErrorMessage();
                                SendErrorMessageToAppCenter(ex, "Manage Dependants", MemberNumber, PhoneNumber);
                            }
                            finally
                            {
                                IsRefreshing = false;
                            }
                        }
                        else
                        {
                            await ShowErrorMessage();
                        }
                    }

                }
                else
                {

                    await ShowErrorMessage("Internet Required, Please switch on your data or connect to wifi before proceeding");
                }

            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Manage Dependants", MemberNumber, PhoneNumber);

                await ShowErrorMessage();

            }
            finally
            {
                IsRefreshing = false;
            }
        }

        public async void ShowAdDependantsPage()
        {
            await Shell.Current.GoToAsync(nameof(AddDependantPage), true);
        }

        public async void SelectDependandForEditing(string DependantId)
        {
            try
            {
                SelectedDependant = Dependants.Where(p => p.Id == DependantId).FirstOrDefault();


                if (SelectedDependant.Status == 0)
                {
                    await Shell.Current.GoToAsync(nameof(EditDependantDetailsPage), true);
                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(EditDependantDetailsPage), true);
                    //await ShowErrorMessage("Sorry You cannot Edit this Dependant");
                }

              
            }
            catch (Exception ex)
            {
                await ShowErrorMessage();
                SendErrorMessageToAppCenter(ex, "Manage Dependants", MemberNumber, PhoneNumber);
            }
        }

        public async Task GetNumberOfDependants()
        {
            try
            {
                if (await CheckInternetConnectivity())
                {
                    if (await CheckIfApiDetailsAreSetUp())
                    {
                        IsRefreshing = true;

                        RestClient client = new RestClient()
                        {
                            BaseUrl = new Uri(ApiDetail.EndPoint)
                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.POST,
                            Resource = "/Members/CheckDepedantLimits"
                        };

                        restRequest.AddQueryParameter("PrincipalMbrId", MemberId);


                        var response = await Task.Run(() =>
                        {
                            return client.Execute(restRequest);
                        });


                        try
                        {
                            if (response.IsSuccessful)
                            {

                                IsRefreshing = false;



                                var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<bool>>(response.Content);

                                if (deserializedResponse.success)
                                {
                                    var data = deserializedResponse.data;

                                    IsAddDependantButtonVisible = data;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                          
                            await ShowErrorMessage();
                            SendErrorMessageToAppCenter(ex, "Manage Dependants", MemberNumber, PhoneNumber);
                        }
                        finally
                        {
                            IsRefreshing = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IsRefreshing = false;

                SendErrorMessageToAppCenter(ex, "Manage Dependants");
            }
        }

    }
}
