using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.Views.WellNessBlog;
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
    public class WellnessBlogViewModel:AppViewModel
    {
        private List<Blog> blogs;
        public List<Blog> Blogs
        {
            get { return blogs; }
            set 
            { 
                blogs = value;
                OnPropertyChanged(nameof(Blogs));
            }
        }


        public List<Blog> OriginalBlogList { get; set; }


        public string PhoneNumber { get; set; }


        private Blog selectedBlog;
        public Blog SelectedBlog
        {
            get { return selectedBlog; }
            set 
            { 
                selectedBlog = value;
                OnPropertyChanged(nameof(SelectedBlog));
            }
        }



        private string searchString { get; set; }
        public string SearchString 
        { 
            get
            {
                return searchString;
            }
            set
            {
                searchString = value;
                OnPropertyChanged(nameof(SearchString));
                SearchBlog();
            }
        }


        public ICommand RefreshCommand { get; set; }

        public ICommand ShowSelectedBlogCommand { get; set; }

        public ICommand ShareContentCommand { get; set; }

        public WellnessBlogViewModel()
        {
            PhoneNumber = Preferences.Get(nameof(AspNetUsers.phoneNumber), "");

            PageTitle = "Wellness blog";
            PageSubTitle = "Read on how to maintain a healthy life";

            Task.Run(async () =>
            {
                await GetBlogs();
            });

            RefreshCommand = new Command(async () => await GetBlogs());

            ShowSelectedBlogCommand = new Command<int>(async (x) => await ShowSelectedBlog(x));

            ShareContentCommand = new Command<string>(async (x) => await ShareContent(x));

        }

        public async Task GetBlogs()
        {
            try
            {
                if(await CheckInternetConnectivity())
                {
                    if(await CheckIfApiDetailsAreSetUp())
                    {
                        IsRefreshing = true;
                        SearchString = "";
                        IsListViewVisible = true;
                        IsEmptyIllustrationVisible = false;

                        Blogs = new List<Blog>();

                        RestClient client = new RestClient()
                        {
                            BaseUrl = new Uri(ApiDetail.EndPoint)
                            
                        };

                        RestRequest restRequest = new RestRequest()
                        {
                            Method = Method.GET,
                            Resource = "/Blog/GetBlogList"
                        };


                        var response = await Task.Run(() =>
                        {
                            return client.Execute(restRequest);
                        });


                        if (response.IsSuccessful)
                        {
                            var resData = JsonConvert.DeserializeObject<BaseResponse<List<Blog>>>(response.Content);

                            if (resData.success)
                            {
                                Blogs = resData.data;

                                Blogs.ForEach(x => { x.ShareLink = "https://healthierkenya.co.ke/Api/Detail/" + x.pkid; });

                                OriginalBlogList = Blogs; 
                            }
                            else
                            {
                                //await ShowErrorMessage("Sorry something went wrong");
                                IsListViewVisible = false;
                                NoDataAvailableMessage = "Sorry something went wrong";
                                IsEmptyIllustrationVisible = true;
                            }
                        }
                        else
                        {
                            //await ShowErrorMessage("Sorry something went wrong");
                            IsListViewVisible = false;
                            NoDataAvailableMessage = "Sorry something went wrong";
                            IsEmptyIllustrationVisible = true;
                        }

                        IsRefreshing = false;

                    }
                }
            }
            catch (Exception ex)
            {
                //await ShowErrorMessage("Sorry something went wrong");
                SendErrorMessageToAppCenter(ex, "Wellness Blog","",PhoneNumber);
                IsRefreshing = false;
                IsListViewVisible = false;
                NoDataAvailableMessage = "Sorry something went wrong";
                IsEmptyIllustrationVisible = true;
            }
        }

        public async Task ShowSelectedBlog(int Id)
        {
            try
            {
                SelectedBlog = Blogs.Where(p => p.pkid == Id).FirstOrDefault();

                if(SelectedBlog != null)
                {
                    if(SelectedBlog.title.Length > 60)
                    {
                        SelectedBlog.TrimmedTitle = SelectedBlog.title.Substring(0, 60) + " ...";
                    }
                    else
                    {
                        SelectedBlog.TrimmedTitle = SelectedBlog.title;
                    }

                    PageSubTitle = "";


                    await Shell.Current.GoToAsync(nameof(SelectedBlogPage), true);
                }
                else
                {
                    await ShowErrorMessage();
                }
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Blog", "", PhoneNumber);
                await ShowErrorMessage();
            }
        }

        public async Task ShareContent(string LinkOnWebSite)
        {
            try
            {
                await Share.RequestAsync(LinkOnWebSite);
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Wellness blog", "", PhoneNumber);
            }
        }

        public async void SearchBlog()
        {
            try
            {

                await Task.Run(() =>
                {
                    if (SearchString != null && SearchString.Trim() != "")
                    {

                        Blogs = OriginalBlogList.Where(p => p.title.ToLower().Trim().Contains(SearchString.ToLower().Trim())).ToList();
                    }
                    else
                    {
                        Blogs = OriginalBlogList;
                    }
                });
               
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Wellness blog", "", PhoneNumber);
            }
        }
    }
}
