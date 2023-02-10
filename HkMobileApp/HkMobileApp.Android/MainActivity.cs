using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XF.Material.Forms;

namespace HkMobileApp.Droid
{
    [Activity(Label = "HkMobileApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //Rg.Plugins.Popup.Popup.Init(this,savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
           

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //XF.Material.Forms.Material.Init();
           // XF.Material.Droid.Material.Init(this, savedInstanceState);

            
            //global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            //global::Xamarin.Forms.Forms.SetFlags("CarouselView_Experimental");
            //global::Xamarin.Forms.Forms.SetFlags("RadioButton_Experimental");
            //global::Xamarin.Forms.Forms.SetFlags("IndicatorView_Experimental");

            //Xamarin.Forms.Forms.SetFlags(new string[] { "CollectionView_Experimental", "CarouselView_Experimental", "MediaElement_Experimental", "SwipeView_Experimental", "Shapes_Experimental", "Brush_Experimental" , "RadioButton_Experimental" });


            Xamarin.FormsMaps.Init(this,savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

         
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            var dum = new FFImageLoading.Forms.CachedImage();

            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}