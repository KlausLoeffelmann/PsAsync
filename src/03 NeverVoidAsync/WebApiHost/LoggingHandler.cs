using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.ServiceModel.Channels;
using Microsoft.Owin;

namespace WebApiHost
{
    class LoggingHandler : DelegatingHandler
    {
        private TextBoxBase myLocalLogger;

        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage =
            "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public LoggingHandler()
        {
        }
        public LoggingHandler(TextBoxBase localLogger)
        {
            myLocalLogger = localLogger;
        }

        public object RemoteEndpointMessageProperty { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (myLocalLogger != null)
            {
                var clientIp = GetClientIp(request);
                myLocalLogger.WriteLine($"WEBAPI CALL from {clientIp} at {DateTime.Now.TimeOfDay}: { request.RequestUri}");
            }

            return base.SendAsync(request, cancellationToken);
        }

        private string GetClientIp(HttpRequestMessage request)
        {
            // Self-hosting using Owin
            if (request.Properties.ContainsKey(OwinContext))
            {
                OwinContext owinContext = (OwinContext)request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }
    }
}
