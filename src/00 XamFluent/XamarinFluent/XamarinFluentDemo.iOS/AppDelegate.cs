using System;
using System.Collections.Generic;
using System.Linq;

using XamarinFluentDemo.ViewModels;
using Foundation;
using Microsoft.Identity.Client;
using UIKit;

namespace XamarinFluentDemo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            var tempApp = new App();
            LoadApplication(tempApp);

            App.PCA.RedirectUri = "msal59712ae4-2173-44f3-aff9-b01accc59a72://auth";
            var result = base.FinishedLaunching(app, options);

            // Handle - for iPhone X - the upper "Gap":
            if (UIApplication.SharedApplication.KeyWindow != null)
            {
                MainViewModel.IPhoneSafeRec = new Xamarin.Forms.Rectangle(UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Left,
                                                                          UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Top,
                                                                          UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Right-
                                                                          UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Left,
                                                                          UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom-
                                                                          UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Top);

                if (tempApp.MainPage.BindingContext is MainViewModel mainViewModel)
                {
                    mainViewModel.PhoneStatusLineMargin = (float) MainViewModel.IPhoneSafeRec.Top;
                }
            }

            return result;
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
            return true;
        }

    }
}
