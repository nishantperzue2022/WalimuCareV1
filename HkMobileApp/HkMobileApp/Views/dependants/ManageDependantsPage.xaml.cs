using Android.OS;
using HkMobileApp.CustomRenderer;
using HkMobileApp.Models;
using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.dependants
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManageDependantsPage : CustomContentPageRenderer
    {
        public ManageDependantsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<ManageDependantsViewModel>();

            //DependencyService.Get<ManageDependantsViewModel>()


        }

        private async void btnAddDependant_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddDependantPage),true);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(AddDependantPage), true);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var dependantSelected = e.Item as Member;

                DependencyService.Get<ManageDependantsViewModel>().SelectDependandForEditing(dependantSelected.Id);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
               
            }
        }

       
    }
}