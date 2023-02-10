using HkMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace HkMobileApp.ViewModels
{
    public class SingleClaimViewModel : AppViewModel
    {
        public long claimId { get; set; }

        private SingleClaim selectedClaim;

        public SingleClaim SelectedClaim
        {
            get { return selectedClaim; }
            set
            {
                selectedClaim = value;
                OnPropertyChanged(nameof(SelectedClaim));
            }
        }

        public SingleClaimViewModel()
        {
            try
            {
                //claimId = Preferences.Get("ClaimId")

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
