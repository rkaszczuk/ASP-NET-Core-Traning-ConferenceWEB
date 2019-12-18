using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferanceWeb.Filters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        public LogFilterAttribute(string stringTemplateStart, string stringTemplateEnd)
        {
            StringTemplateStart = stringTemplateStart;
            StringTemplateEnd = stringTemplateEnd;
        }

        public string StringTemplateStart { get; set; }
        public string StringTemplateEnd { get; set; }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine(String.Format(StringTemplateEnd, context.HttpContext.Request.Path));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(String.Format(StringTemplateStart, context.HttpContext.Request.Path));
        }
    }
}
