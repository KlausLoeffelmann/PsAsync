using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFluentDemo
{
	public partial class MainPage : ContentPage
	{
        private int myStateMachineCounter;
        AuthenticationResult myAr;

        public MainPage()
		{
			InitializeComponent();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (myStateMachineCounter==0)
            {
                // let's see if we have a user in our belly already
                try
                {
                        
                    myAr=await App.PCA.AcquireTokenSilentAsync(App.Scopes, App.PCA.Users.FirstOrDefault());
                    RefreshUserData(myAr.AccessToken);
                    Debug.Print("Silent login succeeded.");
                    myStateMachineCounter = 2;
                }
                catch (Exception eOuter)
                {
                    Debug.Print(eOuter.Message);

                    try
                    {
                        //No, we don't - so we need to login interactively.
                        myStateMachineCounter = 1;
                        myAr = await App.PCA.AcquireTokenAsync(App.Scopes, App.UiParent);
                    }
                    catch (Exception eInner)
                    {
                        Debug.Print(eInner.Message);
                    }
                }

                if (myStateMachineCounter==1)
                {
                    RefreshUserData(myAr.AccessToken);
                }
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

        public async void RefreshUserData(string token)
        {
            //get data from API
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await client.SendAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                JObject user = JObject.Parse(responseString);

                //slUser.IsVisible = true;
                //lblDisplayName.Text = user["displayName"].ToString();
                //lblGivenName.Text = user["givenName"].ToString();
                //lblId.Text = user["id"].ToString();
                //lblSurname.Text = user["surname"].ToString();
                //lblUserPrincipalName.Text = user["userPrincipalName"].ToString();

                //// just in case
                //btnSignInSignOut.Text = "Sign out";


            }
            else
            {
                //DisplayAlert("Something went wrong with the API call", responseString, "Dismiss");
            }
        }

    }
}
