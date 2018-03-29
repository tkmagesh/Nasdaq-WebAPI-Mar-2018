using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;

namespace MyApiProject.Filters
{
    public class TimeAttribute : Attribute, IActionFilter
    {
        public async Task<System.Net.Http.HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<System.Net.Http.HttpResponseMessage>> continuation)
        {
            var sw = Stopwatch.StartNew();
            Debug.WriteLine("Before Continuation");
            HttpResponseMessage result = await continuation();
            
            var elapsedTicks = sw.ElapsedTicks;
            result.Headers.Add("Elapsed-Time", elapsedTicks.ToString());
            Debug.WriteLine("Elapsed time : {0} ticks, {1} {2}", elapsedTicks, actionContext.Request.Method, actionContext.Request.RequestUri);
            Debug.WriteLine("After Continuation");
            return result;
        }

        public bool AllowMultiple
        {
            get { throw new NotImplementedException(); }
        }
    }
}