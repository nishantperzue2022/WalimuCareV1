using HkMobileApp.CustomRenderer;
using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.Policy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PolicyLimitsPage : CustomContentPageRenderer
    {
        public PolicyLimitsPage()
        {
            InitializeComponent();

            var bd = DependencyService.Get<PolicyLimitsViewModel>();
            BindingContext = bd;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Browser.OpenAsync("https://drive.google.com/u/0/uc?id=1RVwZgaTae5WuPA6UGI4FRxnSv0CEgOpU&export=download");
            }
            catch (Exception ex)
            {
                DependencyService.Get<PolicyLimitsViewModel>().SendErrorMessageToAppCenter(ex, "Policy Details");
            }
        }
    }
}