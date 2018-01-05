using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace XamarinFluentDemo
{
	public partial class App : Application
	{
        //Client ID (Lippstapp Testapp)
        public static string ClientID = "d39fb30f-3e0c-4cc8-bee1-53e5fe4606d9";

        public static PublicClientApplication PCA = null;
        public static string[] Scopes = { "User.Read" };
        public static string Username = string.Empty;

        public static UIParent UiParent = null;

		public App ()
		{
			InitializeComponent();

            // default redirectURI; each platform specific project will have to override it with its own
            PCA = new PublicClientApplication(ClientID);
			MainPage = new XamarinFluentDemo.MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
