using HkMobileApp.Views.MedicalCover;
using HkMobileApp.Views.MedicalCover.WoteCoverViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels.MedicalCoverViewModels.WoteCovers
{
    public class WoteNextOfKinDetailsViewModel : AppViewModel
    {

        private string nextOfKinFullNames;
        public string NextOfKinFullNames
        {
            get { return nextOfKinFullNames; }
            set { nextOfKinFullNames = value; OnPropertyChanged(); }
        }


        private string nextOfKinPhoneNumber;
        public string NextOfKinPhoneNumber
        {
            get { return nextOfKinPhoneNumber; }
            set
            {
                nextOfKinPhoneNumber = value;
                OnPropertyChanged();
            }
        }


        private string nextOfKinEmailAddress;
        public string NextOfKinEmailAddress
        {
            get { return nextOfKinEmailAddress; }
            set
            {
                nextOfKinEmailAddress = value;
                OnPropertyChanged();
            }
        }


        private string nextOfKinRelationship;
        public string NextOfKinRelationship
        {
            get { return nextOfKinRelationship; }
            set
            {
                nextOfKinRelationship = value;
                OnPropertyChanged();
            }
        }



        public WoteNextOfKinDetailsViewModel()
        {
            SelectCoverCommand = new Command(async () => await SelectCover());

            PageTitle = "Add Next Of Kin Details";
            PageSubTitle = "Enter Details";
        }


        public ICommand SelectCoverCommand { get; set; }



        public async Task SelectCover()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(WoteSelectCoverPage));
            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Buy Medical Cover", "", "");
                await ShowErrorMessage();
            }
        }

    }
}

