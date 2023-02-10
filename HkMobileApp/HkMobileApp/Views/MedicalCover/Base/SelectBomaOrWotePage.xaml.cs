using HkMobileApp.ViewModels.MedicalCoverViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.MedicalCover.Base
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class SelectBomaOrWotePage : ContentPage
    {
        public SelectBomaOrWotePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<SelectBomaOrWotePageViewModel>();
        }
    }
}