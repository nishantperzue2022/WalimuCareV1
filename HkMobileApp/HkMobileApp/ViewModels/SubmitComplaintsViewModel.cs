using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views.Others;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class SubmitComplaintsViewModel : AppViewModel
    {

        private List<Complaint> complaintsType;
        public List<Complaint> ComplaintsType
        {
            get { return complaintsType; }
            set { complaintsType = value;  OnPropertyChanged(); }
        }


        private List<MemberComplaint> memberComplaints;
        public List<MemberComplaint> MemberComplaints
        {
            get { return memberComplaints; }
            set { memberComplaints = value; OnPropertyChanged(); }
        }


        private string complaintDescription;
        public string ComplaintDescription
        {
            get { return complaintDescription; }
            set { complaintDescription = value; OnPropertyChanged(); }
        }


        private Complaint selectedComplaintType;
        public Complaint SelectedComplaintType
        {
            get { return selectedComplaintType; }
            set { selectedComplaintType = value; OnPropertyChanged(); }
        }


        public ICommand SubmitComplaintCommand { get; set; }

        public ICommand GetMemberComplaintsCommand { get; set; }

        public ICommand AddNewComplaintCommand { get; set; }

        public ICommand GoToMyComplaintsCommand { get; set; }

        public SubmitComplaintsViewModel()
        {
            IsCustomNavBarVisible = true;
            SubmitComplaintCommand = new Command(async () => await SubmitComplaint());
            GetMemberComplaintsCommand = new Command(async()=> await GetMemberComplaints());
            AddNewComplaintCommand = new Command(async () => await AddNewComplaint());
            GoToMyComplaintsCommand = new Command(async () => await GoToMyComplaints());

            Task.Run(async () =>
            {
                //await GetMemberComplaints();
                await GetComplaintsTypes();
            });


        }

        public async Task GetComplaintsTypes()
        {
            try
            {

                ComplaintsType = new List<Complaint>();
               

                await ShowLoadingMessage();

                RestClient client = new RestClient()
                {
                    BaseUrl = new Uri(ApiDetail.EndPoint)
                };

                RestRequest restRequest = new RestRequest()
                {
                    Resource = "/Complaints/GetComplaintTopics",
                    Method = Method.GET
                };

       

                var response = await client.ExecuteAsync(restRequest);

                if (response.IsSuccessful)
                {
                    var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<Complaint>>>(response.Content);

                    if (deserializedResponse.success)
                    {
                        ComplaintsType = deserializedResponse.data;
                        await RemoveLoadingMessage();
                    }
                    else
                    {
                        await ShowErrorMessage("Sorry, no Complaint types were found");
                    }

                    
                }
                else
                {
                    await ShowErrorMessage();
                }


            }
            catch (Exception ex)
            {
                await ShowErrorMessage();
                SendErrorMessageToAppCenter(ex, "Submit Complaints");
            }
        }

        public async Task SubmitComplaint()
        {
            try
            {
                if (SelectedComplaintType == null)
                {
                    await ShowErrorMessage("Please Select one Complaint");
                    return;
                }

                if (ComplaintDescription == null || (ComplaintDescription!=null&& ComplaintDescription.Trim()==""))
                {
                    await ShowErrorMessage("Please Add Some Description to your complaint");
                    return;
                }

                await ShowLoadingMessage();
               
                RestClient client = new RestClient()
                {
                    BaseUrl = new Uri(ApiDetail.EndPoint)
                };

                RestRequest restRequest = new RestRequest()
                {
                    Resource = "/Complaints/SubmitComplaint",
                    Method = Method.POST
                };

                var barry = new
                {
                    memberId = Preferences.Get(nameof(AspNetUsers.memberId), ""),
                    topicId = SelectedComplaintType.id,
                    description = ComplaintDescription
                };

                restRequest.AddJsonBody(barry);

                var response = await client.ExecuteAsync(restRequest);

                if (response.IsSuccessful)
                {
                    await ShowSuccessMessage("Your Issue has been Submitted successfully kindly await a call in 30 minutes");

                    ComplaintDescription = "";

                    SelectedComplaintType = null;

                    await Task.Delay(2000);

                    await GetMemberComplaints();

                    await Shell.Current.GoToAsync(nameof(ComplaintsPage));


                }
                else
                {
                    await ShowErrorMessage();
                }


            }
            catch (Exception ex)
            {
                await ShowErrorMessage();
                SendErrorMessageToAppCenter(ex, "Submit Complaints");
            }
        }

        public async Task GetMemberComplaints()
        {
            try
            {

                IsListViewVisible = true;
                IsEmptyIllustrationVisible = false;
                NoDataAvailableMessage = "";
                IsRefreshing = true;

                await ShowLoadingMessage();

                RestClient client = new RestClient()
                {
                    BaseUrl = new Uri(ApiDetail.EndPoint)
                };

                RestRequest restRequest = new RestRequest()
                {
                    Resource = "/Complaints/GetMemberComplaints",
                    Method = Method.GET
                };

                var memberId = Preferences.Get(nameof(AspNetUsers.memberId), "");

                restRequest.AddQueryParameter("MemberId", memberId);

                var response = await client.ExecuteAsync(restRequest);

                if (response.IsSuccessful)
                {
                    var deserializedResponse = JsonConvert.DeserializeObject<BaseResponse<List<MemberComplaint>>>(response.Content);

                    if (deserializedResponse.success)
                    {
                        MemberComplaints = deserializedResponse.data.OrderByDescending(x=>x.dateSubmitted).ToList();
                        await RemoveLoadingMessage();

                        if (MemberComplaints.Count > 0)
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
                            NoDataAvailableMessage = "Sorry you dont have any complaints lodged";
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
                SendErrorMessageToAppCenter(ex, "Submit Complaints");
               
                IsListViewVisible = false;
                IsEmptyIllustrationVisible = true;
                NoDataAvailableMessage = "Something went wrong, Please try again";
                IsRefreshing = false;
            }
        }

        public async Task AddNewComplaint()
        {
            try
            {

                await GetComplaintsTypes();

                await Shell.Current.GoToAsync(nameof(SubmitComplaintsPage));
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Submit Complaint");
            }
        }

        public async Task GoToMyComplaints()
        {
            try
            {
                
                await GetMemberComplaints();
              

                await Shell.Current.GoToAsync(nameof(ComplaintsPage));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Submit Complaints");
            }
        }
    }
}
