using Android.OS;
using HkMobileApp.CustomRenderer;
using HkMobileApp.Models;
using HkMobileApp.Models.HkApiResponses;
using HkMobileApp.Services;
using HkMobileApp.ViewModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using Map = Xamarin.Forms.GoogleMaps.Map;

namespace HkMobileApp.Views.Hospitals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindHospitalPage : CustomContentPageRenderer
    {

        public FindHospitalPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<FindHospitalViewModel>();

        }

    }
}