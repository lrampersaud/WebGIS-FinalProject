﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;


namespace WebGIS.App_Start
{
    public class AddCustomHeaderFilter: System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            filterContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
        }
    }

    public class AddCustomHeaderFilter2 : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}