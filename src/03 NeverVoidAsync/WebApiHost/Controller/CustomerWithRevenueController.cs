using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web.Http;

namespace WebApiHost.Controller
{

    public class CustomerWithRevenueController : ApiController
    {
        private const int DATA_GENERATE_DELAY = 150;

        public IEnumerable<Customer> GetAllCustomers()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            var customersToReturn = Customer.GenerateRandomCustomers(100);
            Order.GenerateOrders(customersToReturn);
            var waitTimeToFooBar = random.Next(DATA_GENERATE_DELAY);
            Debug.Write("Waiting to produce Customers and Revenues...");
            Thread.Sleep(waitTimeToFooBar);
            Debug.WriteLine("ready.");

            return customersToReturn;

        }

    }
}
