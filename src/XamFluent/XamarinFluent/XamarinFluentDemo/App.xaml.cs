﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace XamarinFluentDemo
{
	public partial class App : Application
	{
        //Client ID (AndroidTestapp)
        public static string ClientID = "59712ae4-2173-44f3-aff9-b01accc59a72";

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
