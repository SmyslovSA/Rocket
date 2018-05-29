using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Rocket.Web.Filters
{
    public class CustomExceptionAttribute : ExceptionFilterAttribute
    {
        public Type ExceptionType { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception != null
            && actionExecutedContext.Exception.GetType() == ExceptionType)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(StatusCode, Message);
            }
            return Task.FromResult<object>(null);
        }
    }
}