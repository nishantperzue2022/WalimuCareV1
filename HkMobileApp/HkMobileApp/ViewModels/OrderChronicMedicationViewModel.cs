using HkMobileApp.Views.PopUps;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels
{
    public class OrderChronicMedicationViewModel : AppViewModel
    {
        private bool isAppointmentsVisible;
        public bool IsAppointmentsVisible
        {
            get { return isAppointmentsVisible; }
            set { isAppointmentsVisible = value; OnPropertyChanged(); }
        }


        public ICommand PhoneNumberTappedCommand { get; set; }

        public ICommand OrderChronicMedicationCommand { get; set; }

        public OrderChronicMedicationViewModel()
        {
            PhoneNumberTappedCommand = new Command<string>(async x => await PhoneNumberTapped(x));
            OrderChronicMedicationCommand = new Command(async () => await OrderChronicMedication());
        }


        public async Task PhoneNumberTapped(string Type)
        {
            try
            {
                if (Type == "1")
                {
                    PhoneDialer.Open("+254730604000");
                }
                else if (Type == "2")
                {
                    PhoneDialer.Open("1528");
                }
                else if (Type == "3")
                {
                    PhoneDialer.Open("0719044799");
                }
                else if (Type == "4")
                {
                    PhoneDialer.Open("0719044999");
                }
             
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Order Chronic Medication");
                await ShowErrorMessage();
            }
        }

        public async Task OrderChronicMedication()
        {
            try
            {
                //await Application.Current.MainPage.Navigation.PushPopupAsync(new Appointments());
                await Browser.OpenAsync("https://appointment.medbookafrica.com/", BrowserLaunchMode.SystemPreferred);

            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "Order Chronic Medication");
                await ShowErrorMessage();
            }
        }


    }
}
