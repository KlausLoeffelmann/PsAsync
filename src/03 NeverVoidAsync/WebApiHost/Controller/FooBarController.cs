using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHost.Controller
{
    using ModelLibrary;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Web.Http;

    public class FooBarController : ApiController
    {
        private const int GET_FOO_BAR_PRODUCE_TIME = 1000;

        [HttpGet]
        public FooBar GetFooBar()
        {
            FooBar fooBarToReturn = new FooBar();
            Random random = new Random(DateTime.Now.Millisecond);
            var waitTimeToFooBar = random.Next(GET_FOO_BAR_PRODUCE_TIME);
            Debug.Write("Waiting to get FooBar...");
            Thread.Sleep(waitTimeToFooBar);
            Debug.WriteLine("ready.");
            fooBarToReturn.Foo = "It took " + waitTimeToFooBar + " ms to get FooBar!";
            fooBarToReturn.Bar = DateTime.Now;
            return fooBarToReturn;
        }
    }
}
