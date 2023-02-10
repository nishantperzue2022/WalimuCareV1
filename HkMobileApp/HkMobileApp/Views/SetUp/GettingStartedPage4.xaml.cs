using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.SetUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GettingStartedPage4 : ContentPage
    {

        public GettingStartedPage4()
        {
            InitializeComponent();


            MyCorousels = new List<MyCorousel>()
            {
                //new MyCorousel()
                //{   ImageUrl = "gettingStarted.jpeg",
                //    Title="Protect you and yours",
                //    SubTitle="Buy medical cover for yourself, family and friends",
                //    PreviousBtnText = "SKIP",
                //    NextBtnText = "NEXT",
                //    lblPreviousCommand = new Command<int>((x)=>SetCurrentItem(x)),
                //    lblNextCommand = new Command<int>((x)=>SetCurrentItem(x)),
                //    PreviousPosition = 0,
                //    NextPosition = 1,
                    

                //},
                 new MyCorousel()
                {   ImageUrl = "callcenter2.jpg",
                    Title="24/7 Customer Support",
                    SubTitle="",
                    PreviousBtnText = "SKIP",
                    NextBtnText = "NEXT",
                    lblPreviousCommand = new Command<int>((x)=>SetCurrentItem(x)),
                    lblNextCommand = new Command<int>((x)=>SetCurrentItem(x)),
                    PreviousPosition = 0,
                    NextPosition = 1
                },
                  new MyCorousel()
                {   ImageUrl = "welcomeScreen.jpeg",
                    Title="Automation of Processes",
                    SubTitle="",
                    PreviousBtnText = "BACK",
                    NextBtnText = "NEXT",
                    lblPreviousCommand = new Command<int>((x)=>SetCurrentItem(x)),
                    lblNextCommand = new Command<int>((x)=>SetCurrentItem(x)),
                    PreviousPosition = 1,
                    NextPosition = 2
                },
                   new MyCorousel()
                {   ImageUrl = "gettingStarted4.jpg",
                    Title="Track Utilization and Hospital Visits",
                    SubTitle="",
                    PreviousBtnText = "BACK",
                    NextBtnText = "GET STARTED",
                    lblPreviousCommand = new Command<int>((x)=>SetCurrentItem(x)),
                    lblNextCommand = new Command<int>((x)=>SetCurrentItem(x)),
                    PreviousPosition = 1,
                    NextPosition = 4
                }

            };

            BindingContext = this;
        }

        public List<MyCorousel> MyCorousels { get; set; }


        public async void SetCurrentItem(int position)
        {
            try
            {
                if (position == 0)
                {
                    App.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else if (position == 4)
                {
                    App.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else
                {

                    if (corouselView.Position == 1 && position == 1)
                    {
                        corouselView.Position = 0;
                    }
                    else
                    {
                        corouselView.Position = position;
                    }


                }

            }
            catch (Exception ex)
            {

                await App.Current.MainPage.Navigation.PushPopupAsync(new HkErrorPage("Sorry something went wrong"), true);
            }
        }

    }


    public class MyCorousel
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public string PreviousBtnText { get; set; }
        public string NextBtnText { get; set; }

        public ICommand lblNextCommand { get; set; }

        public ICommand lblPreviousCommand { get; set; }

        public int PreviousPosition { get; set; }

        public int NextPosition { get; set; }

    }
}