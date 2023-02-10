using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.Hospitals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectCountyOrSpeciality : ContentPage
    {
        public SelectCountyOrSpeciality()
        {
            InitializeComponent();

            BindingContext = DependencyService.Get<FindHospitalViewModel>();

        }

        private void SfRadioButton_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {

        }

        private void sfChipGroupSearchOptions_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                DependencyService.Get<FindHospitalViewModel>().SetSearchByOption(e.AddedItem.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}