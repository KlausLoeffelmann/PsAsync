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
        //Client ID (AD Testapp)
        public static string ClientID = "a2305275-6b37-4907-92d3-01678a3d0834";

        ////Client ID (Legatro)
        //public static string ClientID = "000000004816013F";

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
