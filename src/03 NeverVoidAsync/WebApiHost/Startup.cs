using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;
using System.Web.Http.Tracing;
using System.Net.Http;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebApiHost
{

    public class Startup
    {
        public static TextBoxBase LoggerControl { get; set; }

        public void Configuration(IAppBuilder appbuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { ID = RouteParameter.Optional });

            //config.Routes.MapHttpAttributeRoutes()
         
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            //config.Formatters.JsonFormatter.SerializerSettings.TraceWriter = new JSonTraceWriter();
            config.MessageHandlers.Add(new LoggingHandler(LoggerControl));
            appbuilder.UseWebApi(config);
        }
    }

    public class JSonTraceWriter : Newtonsoft.Json.Serialization.ITraceWriter
    {
        public System.Diagnostics.TraceLevel LevelFilter => System.Diagnostics.TraceLevel.Verbose;

        public void Trace(System.Diagnostics.TraceLevel level, string message, Exception ex)
        {
            Debug.Print(message);
        }
    }

    public class SimpleTraceWriter : ITraceWriter
    {
        public void Trace(HttpRequestMessage request, string category, System.Web.Http.Tracing.TraceLevel level, Action<TraceRecord> traceAction)
        {

            var rec = new TraceRecord(request, category, level);
            traceAction(rec);
            System.Diagnostics.Debug.Print(rec.Request.ToString() + Environment.NewLine + rec.Message.ToString() + Environment.NewLine + "--------------------");
        }
    }
}
