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
    public partial class HkInfoPage : PopupPage
    {
        public string MessageText { get; set; } = "The operation was successful";
        public HkInfoPage(string messageText)
        {
            InitializeComponent();

            if (messageText != null || messageText != "")
            {
                MessageText = messageText;
            }
        }

        protected override void OnAppearing()
        {
            lblMessage.Text = MessageText;
        }
    }
}