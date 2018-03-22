using ActiveDevelop.MvvmBaseLib.Mvvm;
using ModelLibrary;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace UwpClient.ViewModels
{
    public class CustomerViewModel : MvvmViewModelBase
    {
        private Customer mySelectedCustomer;

        private int mySelectedCustomerIndex;

        private string myViewTitel;

        public ObservableCollection<Customer> Customers { get; set; }

        public Customer SelectedCustomer
        {
            get
            {
                return this.mySelectedCustomer;
            }
            set
            {
                base.SetProperty<Customer>(ref this.mySelectedCustomer, value);
            }
        }

        public int SelectedCustomerIndex
        {
            get
            {
                return this.mySelectedCustomerIndex;
            }
            set
            {
                if (base.SetProperty<int>(ref this.mySelectedCustomerIndex, value))
                {
                    if (this.Customers != null)
                    {
                        this.SelectedCustomer = this.Customers[value];
                    }
                }
            }
        }

        public string ViewTitel
        {
            get
            {
                return this.myViewTitel;
            }
            set
            {
                base.SetProperty<string>(ref this.myViewTitel, value);
            }
        }

        public CustomerViewModel()
        {
            this.mySelectedCustomerIndex = -1;
        }

        public static async Task<CustomerViewModel> GetInstanceWithAllCustomers()
        {
            HttpResponseMessage response = await (new HttpClient()).GetAsync("http://localhost:9000/api/customers");

            CustomerViewModel customerViewModel = new CustomerViewModel();

            IEnumerable<Customer> enumerable = await response.Content.ReadAsAsync<IEnumerable<Customer>>();
            customerViewModel.Customers = new ObservableCollection<Customer>(enumerable);
            CustomerViewModel customerViewModel1 = customerViewModel;
            return customerViewModel1;
        }
    }
}
