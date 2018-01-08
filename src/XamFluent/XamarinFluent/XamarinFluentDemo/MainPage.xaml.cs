﻿using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFluentDemo
{
	public partial class MainPage : ContentPage
	{
        private int myStateMachineCounter;
        AuthenticationResult myAr;
        private SynchronizationContext mySyncContext = SynchronizationContext.Current;

        public MainPage()
		{
			InitializeComponent();
		}

        private void Logout_Clicked(object sender, EventArgs e)
        {
            SignOut();
            txtLoginState.Text = "You're logout. Please, restart the app.";
            btnLogout.IsEnabled = false;
        }

        protected async override void OnAppearing()
        {
            if (App.IsDesignMode)
            {
                return;
            }

            {
                await Task.Run(async () =>
                {

                    if (myStateMachineCounter == 0)
                    {
                    // let's see if we have a user in our belly already
                    try
                        {
                            mySyncContext.Post((state) =>
                            {
                                txtLoginState.Text = "Trying to log you in silently...";
                            }, null);

                            myAr = await App.PCA.AcquireTokenSilentAsync(App.Scopes, App.PCA.Users.FirstOrDefault());
                            await RefreshUserDataAsync(myAr.AccessToken);
                            Debug.Print("Silent login succeeded.");
                            mySyncContext.Post((state) =>
                            {
                                txtLoginState.Text = "Logged in silently.";
                                btnLogout.IsEnabled = true;
                            }, null);

                            myStateMachineCounter = 2;
                        }
                        catch (Exception eOuter)
                        {
                            Debug.Print(eOuter.Message);

                            try
                            {
                                //No, we don't - so we need to login interactively.
                                mySyncContext.Post((state) =>
                                {
                                    txtLoginState.Text = "Trying to log you in silently failed. Manual Login...";
                                }, null);

                                myStateMachineCounter = 1;
                                myAr = await App.PCA.AcquireTokenAsync(App.Scopes, App.UiParent);
                                Debug.Print("Manual login succeeded.");

                                mySyncContext.Post((state) =>
                                {
                                    btnLogout.IsEnabled = true;
                                    txtLoginState.Text = "Logged in succeeded.";
                                }, null);
                            }
                            catch (Exception eInner)
                            {
                                Debug.Print(eInner.Message);
                            }
                        }

                        if (myStateMachineCounter == 1)
                        {
                            await RefreshUserDataAsync(myAr.AccessToken);
                        }
                    }
                });
            }
        }

        private void SignOut()
        {
            try
            {
                foreach (var user in App.PCA.Users)
                {
                    App.PCA.Remove(user);
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
        }

        public async Task RefreshUserDataAsync(string token)
        {
            //get data from API
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await client.SendAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    JObject user = JObject.Parse(responseString);
                    Debug.Print("Parsing responseString was OK.");
                    Debug.Print($"User's Displayname:{user["displayName"]}");
                    Debug.Print($"User's ID:{user["id"]}");
                    Debug.Print($"User's Surname:{user["surname"]}");
                    Debug.Print($"User's Principal Name:{user["userPrincipalName"]}");
                }
                catch (Exception ex)
                {
                    Debug.Print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Debug.Print("Something went wrong parsing the Response String!");
                    Debug.Print($"Exception Message:{ex.Message}");
                    Debug.Print($"Let's log out...");
                    Debug.Print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    SignOut();
                }
            }
            else
            {
                Debug.Print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Debug.Print("Something went wrong with the API call!");
                Debug.Print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }
    }
}
