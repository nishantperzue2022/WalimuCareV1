using HkMobileApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace HkMobileApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}