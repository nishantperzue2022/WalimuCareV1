using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using HkMobileApp.Views.SetUp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushPopupAsync(new CallForAssistancePage());
            }
            catch (Exception ex)
            {

                Console.Write(ex);
            }
        }
    }
}