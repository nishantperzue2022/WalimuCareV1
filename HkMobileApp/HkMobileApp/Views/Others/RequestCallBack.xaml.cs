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
    public partial class RequestCallBack : CustomContentPageRenderer
    {
        public RequestCallBack()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = DependencyService.Get<RequestCallBackViewModel>();
        }

        private void SfRadioGroupKey_CheckedChanged(object sender, Syncfusion.XForms.Buttons.CheckedChangedEventArgs e)
        {
           string selectedRdBtn = e.CurrentItem.Text;

            DependencyService.Get<RequestCallBackViewModel>().SelectedRdoBtn = selectedRdBtn;

            //if (selectedRdBtn == "Call my number")
            //{
                
            //}
            //phonNoTxt.Text = "Enter phone Number";
        }
    }
}