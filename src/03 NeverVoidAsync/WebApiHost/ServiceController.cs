using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.Windows.Forms;

namespace WebApiHost
{

    public class ServiceController
    {
        private IDisposable myWebApp;

        private const string BASE_ADDRESS = "http://localhost:9000/";

        public void Start(TextBoxBase loggerControl=null)
        {
            Startup.LoggerControl = loggerControl;
            myWebApp = WebApp.Start<Startup>(url: BASE_ADDRESS);

            HasStarted = true;
        }

        public void Stop()
        {
            if (myWebApp != null)
            {
                myWebApp.Dispose();
                HasStarted = false;
            }
        }

        public bool HasStarted { get; set; }
    }
}
