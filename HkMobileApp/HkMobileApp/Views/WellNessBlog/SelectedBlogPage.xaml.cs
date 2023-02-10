﻿using HkMobileApp.CustomRenderer;
using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HkMobileApp.Views.WellNessBlog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedBlogPage : CustomContentPageRenderer
    {
        public SelectedBlogPage()
        {
            InitializeComponent();
            BindingContext = DependencyService.Get<WellnessBlogViewModel>().SelectedBlog;
        }
    }
}