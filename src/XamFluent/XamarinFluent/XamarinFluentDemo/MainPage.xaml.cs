using Microsoft.Graph;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFluentDemo.ViewModels;

namespace XamarinFluentDemo
{
	public partial class MainPage : ContentPage
	{
        private int myStateMachineCounter;
        AuthenticationResult myAr;
        private SynchronizationContext mySyncContext = SynchronizationContext.Current;
        private MainViewModel myMainViewModel;

        public MainPage()
		{
			InitializeComponent();
            myMainViewModel = new MainViewModel();
            this.BindingContext = myMainViewModel;
		}

        private void Logout_Clicked(object sender, EventArgs e)
        {
            SignOut();
            txtLoginState.Text = "You're logout. Please, restart the app.";
            btnLogout.IsEnabled = false;
        }

        protected async override void OnAppearing()
        {
            if (!App.IsAppRunning)
            {
                return;
            }

            if (myStateMachineCounter == 0)
            {
                // let's see if we have a user in our belly already
                try
                {
                    myMainViewModel.StatusLine = "Trying to log you in silently...";

                    myAr = await App.PCA.AcquireTokenSilentAsync(App.Scopes, App.PCA.Users.FirstOrDefault());
                    await RefreshUserDataAsync(myAr.AccessToken);
                    Debug.Print("Silent login succeeded.");
                    myMainViewModel.StatusLine = "Logged in silently.";
                    btnLogout.IsEnabled = false;
                    myStateMachineCounter = 2;
                }
                catch (Exception eOuter)
                {
                    Debug.Print(eOuter.Message);

                    try
                    {
                        //No, we don't - so we need to login interactively.
                        myMainViewModel.StatusLine = "Trying to log you in silently failed. Manual Login...";

                        myStateMachineCounter = 1;
                        myAr = await App.PCA.AcquireTokenAsync(App.Scopes, App.UiParent);
                        await RefreshUserDataAsync(myAr.AccessToken);
                        Debug.Print("Manual login succeeded.");
                        btnLogout.IsEnabled = true;
                        myMainViewModel.StatusLine = "Logged in succeeded.";
                        myStateMachineCounter = 2;

                        // On Apearing will be called a second time, we need to 
                        // Directly execute the State-2-Code then.
                        // This is why we return. Finding the CameraRollFolder should
                        // be working with a visible view!
                        return;

                    }
                    catch (Exception eInner)
                    {
                        Debug.Print(eInner.Message);
                    }
                }

                if (myStateMachineCounter == 2)
                {
                    var folderId = await myMainViewModel.FindCameraRollFolderAsync();
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
