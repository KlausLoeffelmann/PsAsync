using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UwpClient.ViewModels;
using UwpClient.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CustomerViewModel myCurrentViewModel;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnViewAllCustomers_Click(object sender, RoutedEventArgs e)
        {
            AllCustomersView allCustomersView = new AllCustomersView();
            this.mySplitViewContent.Content = allCustomersView;
            var instanceWithAllCustomers = await CustomerViewModel.GetInstanceWithAllCustomers();
            instanceWithAllCustomers.ViewTitel = "All Customers";
            allCustomersView.DataContext = instanceWithAllCustomers;
            if (instanceWithAllCustomers.Customers != null)
            {
                instanceWithAllCustomers.SelectedCustomerIndex = 0;
            }
        }

        private async void btnTopButtonCustomers_Click(object sender, RoutedEventArgs e)
        {
            AllCustomersView allCustomersView = new AllCustomersView();
            this.mySplitViewContent.Content = allCustomersView;
            this.myCurrentViewModel = new CustomerViewModel();
            await this.LoadCustomersAndOrdersIntoViewModel();
            await this.SomeWorkLoad(100);
            this.myCurrentViewModel.ViewTitel = "Customers with Revenue (Best/Worst Report)";
            allCustomersView.DataContext = this.myCurrentViewModel;
            if (this.myCurrentViewModel.Customers != null)
            {
                this.myCurrentViewModel.SelectedCustomerIndex = 0;
            }

        }

        private async Task SomeWorkLoad(int ms)
        {
            await Task.Delay(ms);
        }

        private async Task LoadCustomersAndOrdersIntoViewModel()
        {
            var sw = Stopwatch.StartNew();
            var response = await new HttpClient().GetAsync("http://localhost:9000/api/customerwithrevenue");
            var customerList = from customer in await response.Content.ReadAsAsync<IEnumerable<Customer>>()
                               orderby customer.RevenueInCurrentYear descending
                               select customer;

            this.myCurrentViewModel.Customers = new ObservableCollection<Customer>(customerList);
            Debug.WriteLine($"Requested ViewModel Data from WebAPI in {sw.ElapsedMilliseconds} ms.");
        }
    }
}
