using SmsMis.Models.Console.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmsMisWeb.Filters
{
    public class CutomAttribute: ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //get model data
            //...
            //var aad = filterContext.Controller.ViewData.Model;
        }

        public void OnException(ExceptionContext filterContext)
        {
            var a = filterContext;
        }
        
    }
}