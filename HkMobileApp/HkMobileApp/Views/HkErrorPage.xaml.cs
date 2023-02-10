using HkMobileApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HkErrorPage : PopupPage
    {
        public string MessageText { get; set; } = "Invalid Member Id or Phone Number";
        public HkErrorPage(string messageText)
        {
            InitializeComponent();

            if(messageText!=null || messageText != "")
            {
                MessageText = messageText;
            }
        }

        protected override void OnAppearing()
        {
            lblMessage.Text = MessageText;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await DependencyService.Get<AppViewModel>().RemoveLoadingMessage();
            }
            catch (Exception ex)
            {
                DependencyService.Get<AppViewModel>().SendErrorMessageToAppCenter(ex, "Error Page");
            }
        }
    }
}