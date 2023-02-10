using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;
using HkMobileApp.CustomRenderer;

namespace HkMobileApp.Views.dependants
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDependantDetailsPage : CustomContentPageRenderer
    {
        public EditDependantDetailsPage()
        {
            InitializeComponent();
            BindingContext = DependencyService.Get<EditDependantViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DependencyService.Get<EditDependantViewModel>().SetDetailsToBeEdited();
        }



    }
}