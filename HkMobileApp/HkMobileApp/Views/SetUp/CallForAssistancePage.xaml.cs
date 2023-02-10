using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using HkMobileApp.ViewModels.SetUpViewModels;

namespace HkMobileApp.Views.SetUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallForAssistancePage : PopupPage
    {
        public CallForAssistancePage()
        {
            InitializeComponent();

            var bindingContext  = DependencyService.Get<ConfirmMemberDetailsViewModel>();
            BindingContext = bindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

          
        }

    }
}