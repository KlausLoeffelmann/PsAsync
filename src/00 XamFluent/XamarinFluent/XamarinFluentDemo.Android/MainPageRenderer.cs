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
using Microsoft.Identity.Client;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using XamarinFluentDemo;
using XamarinFluentDemo.Android;

[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageRenderer))]
namespace XamarinFluentDemo.Android
{
    class MainPageRenderer : PageRenderer
    {
        MainPage page;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            page = e.NewElement as MainPage;
            var activity = this.Context as Activity;           
        }

    }
}