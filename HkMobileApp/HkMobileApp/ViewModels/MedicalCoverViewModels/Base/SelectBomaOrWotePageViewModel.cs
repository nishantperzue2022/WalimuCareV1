using HkMobileApp.Models;
using HkMobileApp.Views.MedicalCover;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HkMobileApp.ViewModels.MedicalCoverViewModels.Base
{
    public class SelectBomaOrWotePageViewModel :AppViewModel
    {
        private List<ByMedicalCoverCarousel> medicalCoverCarousels;
        public List<ByMedicalCoverCarousel> MedicalCoverCarousels
        {
            get { return medicalCoverCarousels; }
            set
            {
                medicalCoverCarousels = value;
                OnPropertyChanged();
            }
        }


        public ICommand GoToViewPremiumsAndBenefitsCommand { get; set; }


        public SelectBomaOrWotePageViewModel()
        {

            GetMedicalCoverDetails();

            GoToViewPremiumsAndBenefitsCommand = new Command<int>(async x => await ViewMedicalCoverPremiumAndBenefits(x));
        }

        public  void GetMedicalCoverDetails()
        {
            try
            {
                List<ByMedicalCoverCarousel> byMedicalCoverCarousels = new List<ByMedicalCoverCarousel>()
                {
                     new ByMedicalCoverCarousel()
                    {
                        Id=1,
                        Title = "Boma Afya",
                        SubTitle ="Cover yourself and your family",
                        ImageUrl = "mwavuli.jpg",
                        Description = new List<string>()
                        {
                            "Family cover of principal and 5 dependents(M + 5)",
                            "Covers One(1) legal spouse and four(4) dependent children",
                            "No waiting period for all outpatient claims",
                            "No waiting period for optical and dental services"
                        }

                    },
                     new ByMedicalCoverCarousel()
                    {
                        Id=2,
                        Title = "Wote Afya",
                        SubTitle ="Cover your family and friends",
                        ImageUrl = "dependentplans.jpg",
                        Description = new List<string>()
                        {
                            "Cover upto 10 dependents(M + 10)",
                            "All dependents whether biological or not are eligible for the scheme",
                            "No waiting period for all outpatient claims",
                            "No waiting period for optical and dental services"
                        }

                    }
                };

                MedicalCoverCarousels = byMedicalCoverCarousels;



            }
            catch (Exception ex)
            {

                SendErrorMessageToAppCenter(ex, "Home Screen", "", "");
            }
        }


        public async Task ViewMedicalCoverPremiumAndBenefits(int Id)
        {
            try
            {
                if (Id == 1)
                {
                    await Shell.Current.GoToAsync(nameof(ViewBomaCoverBenefitsAndPremiumsPage));
                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(ViewWoteCoverBenefitsAndPremiumsPage));
                }
            }
            catch (Exception ex)
            {
                SendErrorMessageToAppCenter(ex, "DashBoard", "", "");
            }
        }
    }
}
