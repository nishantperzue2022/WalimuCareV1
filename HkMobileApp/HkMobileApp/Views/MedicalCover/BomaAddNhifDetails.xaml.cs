using HkMobileApp.CustomRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HkMobileApp.ViewModels.MedicalCoverViewModels;
using HkMobileApp.ViewModels.MedicalCoverViewModels.WoteCovers;

namespace HkMobileApp.Views.MedicalCover
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BomaAddNhifDetails : CustomContentPageRenderer
    {
        public BomaAddNhifDetails()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<BomaBuyMedicalCoverViewModel>();

        }
    }
}