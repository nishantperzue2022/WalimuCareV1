using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.CoordinatorLayout.Widget;
using Google.Android.Material.AppBar;
using HkMobileApp.CustomRenderer;
using HkMobileApp.Droid.CustomRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomShellRenderer), typeof(CustomShellRendererAndroid))]
namespace HkMobileApp.Droid.CustomRenderers
{
   
    public class CustomShellRendererAndroid : ShellRenderer
    {
        public CustomShellRendererAndroid(Context context) : base(context) {}



        protected override IShellFlyoutContentRenderer CreateShellFlyoutContentRenderer()
        {
            var flyoutContent = base.CreateShellFlyoutContentRenderer();

            var cl = ((CoordinatorLayout)flyoutContent.AndroidView);

            for (int index = 0; index < cl.ChildCount; index++)
            {
                var child = cl.GetChildAt(index);

                if (child is AppBarLayout)
                {
                    var view = (AppBarLayout)child;
                    view.OutlineProvider = null;
                }
            }
            return flyoutContent;
        }
    }
}