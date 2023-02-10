using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using HkMobileApp.Droid.CustomRenderers;
using Xamarin.Forms.Platform.Android;
using HkMobileApp.CustomRenderer;

[assembly: ExportRenderer(typeof(EntryNoBottomBorder), typeof(EntryNoBottomBorderAndroid))]
namespace HkMobileApp.Droid.CustomRenderers
{
   
    public class EntryNoBottomBorderAndroid :EntryRenderer
    {
        public EntryNoBottomBorderAndroid(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);

        }
    }
}