using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Linq;
using Android.Content;
using System.Diagnostics;

namespace XamarinFluentDemo.Droid
{
    [Activity(Label = "XamarinFluentDemo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //Client ID (AD Testapp)
        public static string clientId = "a2305275-6b37-4907-92d3-01678a3d0834";
        public static string commonAuthority = "https://login.windows.net/common";
        //Redirect URI
        public static Uri returnUri = new Uri("http://xamarin-fluent-redirect");
        //Graph URI if you've given permission to Azure Active Directory
        const string graphResourceUri = "https://graph.windows.net";
        public static string graphApiVersion = "2013-11-08";
        //AuthenticationResult will hold the result after authentication completes
        AuthenticationResult authResult = null;

        ////Client ID (Legatro)
        //public static string clientId = "000000004816013F";
        //public static string clientSecret = "anZOId6x4CF2SJOwaOvqWHRAWcCULrgO";
        //public static string commonAuthority = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
        ////Redirect URI
        //public static Uri returnUri = new Uri("legatro://easyauth.callback");
        ////Graph URI if you've given permission to Azure Active Directory
        //const string graphResourceUri = "https://graph.windows.net";
        //public static string graphApiVersion = "2013-11-08";
        ////AuthenticationResult will hold the result after authentication completes
        //AuthenticationResult authResult = null;

        protected async override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            var returnUri = await GetAccessToken(graphResourceUri, this);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }

        public static async Task<AuthenticationResult> GetAccessToken
                    (string serviceResourceId, Activity activity)
        {
            try
            {
                var authContext = new AuthenticationContext(commonAuthority);
                if (authContext.TokenCache.ReadItems().Count() > 0)
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
                var authResult = await authContext.AcquireTokenAsync(serviceResourceId, clientId, returnUri,
                    new PlatformParameters(activity));
                return authResult;
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.Print(ex.Message);
                    Debugger.Break();
                }
                return null;
            }
        }
    }
}

