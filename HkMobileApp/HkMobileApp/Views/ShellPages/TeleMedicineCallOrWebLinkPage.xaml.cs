using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.ShellPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeleMedicineCallOrWebLinkPage : ContentPage
    {
        public TeleMedicineCallOrWebLinkPage()
        {
            InitializeComponent();
        }

        private void PhoneNumber_Tapped(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("+254730604000");
            }
            catch (Exception ex)
            {
                //SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
                Console.WriteLine(ex);
            }
        }

        private void PhoneNumber2_Tapped(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("1528");
            }
            catch (Exception ex)
            {
                //SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
                Console.WriteLine(ex);
            }
        }
        private void PhoneNumber3_Tapped(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("0719044799");
            }
            catch (Exception ex)
            {
                //SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
                Console.WriteLine(ex);
            }
        }
        private void PhoneNumber4_Tapped(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("0719044999");
            }
            catch (Exception ex)
            {
                //SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
                Console.WriteLine(ex);
            }
        }


        private async void Website_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://appointment.medbookafrica.com/", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                //SendErrorMessageToAppCenter(ex, "Contact Us", MemberNumber, PhoneNumber);
                Console.WriteLine(ex);
            }
        }
    }
}