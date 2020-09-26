using Microsoft.AspNetCore.Http;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Learn.WebApp.Server.Middleware
{
    public class ActivityMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            if (next is null) throw new ArgumentNullException(nameof(next));

            RequestContext.ActivityId = Guid.NewGuid();
            RequestContext.PropagateActivityId = true;

            context.TraceIdentifier = RequestContext.ActivityId.ToString("D");

            return next.Invoke(context);
        }
    }
}