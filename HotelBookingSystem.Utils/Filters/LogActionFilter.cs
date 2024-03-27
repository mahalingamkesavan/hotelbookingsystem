using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HotelBookingSystem.Utils.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger _Logger;
        public LogActionFilter(ILoggerFactory factory)
        {
            this._Logger = factory.CreateLogger("LogActionFilter");
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var bodyStr = "HTTP Request :-";
            bodyStr += "\n";
            bodyStr += "\nURL path : " + context.HttpContext.Request.Host.Value
                + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
            bodyStr += GetLogMessage(context.RouteData);
            bodyStr += "\n";
            bodyStr += "\nRequest Header";
            foreach (var item in context.HttpContext.Request.Headers)
            {
                bodyStr += "\n" + item.Key + ":" + item.Value;
            }
            if (context.HttpContext.Request.Cookies.Count > 0)
            {
                bodyStr += "\n";
                bodyStr += "\nCookies";
                foreach (var item in context.HttpContext.Request.Cookies)
                {
                    bodyStr += "\n" + item.Key + " : " + item.Value;
                }
            }
            bodyStr += "\n";
            bodyStr += "\nAction Parameters";
            foreach (var item in context.ActionArguments)
            {
                bodyStr += "\n" + item.Key + " : " + JsonSerializer.Serialize(item.Value);
                ;
            }
            _Logger.LogInformation(bodyStr);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Object res = "";
            if (context.Result != null)
            {
                var actionValueProperty = context.Result.GetType().GetProperty("Value");
                if (actionValueProperty != null)
                {
                    res = actionValueProperty.GetValue(context.Result) ?? "";
                }
            }
            //OkObjectResult res = context.Result as OkObjectResult;
            var bodyStr = "HTTP Response :-";
            bodyStr += "\n";
            bodyStr += "\nURL path : " + context.HttpContext.Request.Host.Value + context.HttpContext.Request.Path;
            bodyStr += GetLogMessage(context.RouteData);
            bodyStr += "\n";
            bodyStr += "\nResponse Header";
            foreach (var item in context.HttpContext.Response.Headers)
            {
                bodyStr += "\n" + item.Key + ":" + item.Value;
            }
            if (context.HttpContext.Request.Cookies.Count > 0)
            {
                bodyStr += "\n";
                bodyStr += "\nCookies";
                foreach (var item in context.HttpContext.Request.Cookies)
                {
                    bodyStr += "\n" + item.Key + " : " + item.Value;
                }
            }
            bodyStr += "\n";
            bodyStr += "\nResponse Body";
            if (res != null)
                //if(res is TaskAwaiter)
                bodyStr += "\n" + JsonSerializer.Serialize(res,new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition=JsonIgnoreCondition.WhenWritingNull,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
            //bodyStr += "\n" + res.Value;
            _Logger.LogInformation(bodyStr);
        }
        private string GetLogMessage(RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            string data = "\n";
            data += "\nControler : " + controllerName;
            data += "\nAction Method : " + actionName;
            return data;
        }
    }
}
