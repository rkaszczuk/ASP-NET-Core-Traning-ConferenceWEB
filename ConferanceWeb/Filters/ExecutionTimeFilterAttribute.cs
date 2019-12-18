using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferanceWeb.Filters
{
    public class ExecutionTimeFilterAttribute : ActionFilterAttribute
    {
        private const string KeyName = "EXECUTIONG_TIMER";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            context.HttpContext.Items.Add(KeyName, stopWatch);

            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var elapsedMilliseconds = (context.HttpContext.Items[KeyName] as System.Diagnostics.Stopwatch).ElapsedMilliseconds;
            Console.WriteLine($">>>>>{context.HttpContext.Request.Path} {elapsedMilliseconds}ms");
            base.OnActionExecuted(context);
        }
    }
}
