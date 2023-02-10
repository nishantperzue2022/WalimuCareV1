using HkMobileApp.CustomRenderer;
using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.MedicalCover
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewBomaCoverBenefitsAndPremiumsPage : CustomContentPageRenderer
    {
        public ViewBomaCoverBenefitsAndPremiumsPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<ViewBomaBenefitCoverAndPremiumsViewModel>();
        }
    }
}