using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Extensions.Filters
{
    public class LoggerFilterAttribute : Attribute, IActionFilter
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "logger.txt");

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string[] classPathSplited = context.Controller.ToString().Split('.');
            File.AppendAllText(filePath, "response to " + (string)context.RouteData.Values["action"] + " " + classPathSplited[classPathSplited.Length - 1] + "\n");

        }
    }
}
