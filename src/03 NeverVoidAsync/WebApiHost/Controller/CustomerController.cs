using ModelLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiHost.Controller
{
    public class CustomersController : ApiController
    {
        private const int GET_FOO_BAR_PRODUCE_TIME = 1000;

        [HttpGet]
        public IHttpActionResult GetAllCustomers()
        {
            var customersToReturn = (List<Customer>) Customer.GenerateRandomCustomers(100);
            return Ok(customersToReturn);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomersAsync()
        {
            await Task.Delay(0).ConfigureAwait(false);
            var customersToReturn = (List<Customer>)Customer.GenerateRandomCustomers(100);
            return Ok(customersToReturn);
        }


    }
}
