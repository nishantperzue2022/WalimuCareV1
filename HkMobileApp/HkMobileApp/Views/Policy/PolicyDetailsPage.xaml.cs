using HkMobileApp.CustomRenderer;
using HkMobileApp.Services;
using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.Policy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PolicyDetailsPage : CustomContentPageRenderer
    {
        public PolicyDetailsPage()
        {
            InitializeComponent();

          
            BindingContext = DependencyService.Get<PolicyDetailsViewModel>();
           
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                //lstMain.ScrollTo(e.SelectedItem, ScrollToPosition.Center, false);
            }
            catch (Exception )
            {

            }
        }

        private async void Button_Clicked(object sender, EventArgs ega)
        {
            try
            {
                //DependencyService.Get<IFileSavePdf>().DownloadPdf();

                await Browser.OpenAsync("https://drive.google.com/u/0/uc?id=1RVwZgaTae5WuPA6UGI4FRxnSv0CEgOpU&export=download");
            }
            catch (Exception ex)
            {
                DependencyService.Get<PolicyDetailsViewModel>().SendErrorMessageToAppCenter(ex, "Policy Details");
            }
        }
    }

   
}