using HkMobileApp.CustomRenderer;
using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.Others
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : CustomContentPageRenderer
    {
        public ProfilePage()
        {
            InitializeComponent();

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                BindingContext = DependencyService.Get<UserProfileViewModel>();
            }
            catch (Exception ex)
            {

               
            }
        }
    }
}