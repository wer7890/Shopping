﻿using NLog;
using ShoppingWeb.Response;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ShoppingWeb.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                //類上或方法上有標記[AllowAnonymous]有就return
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                {
                    return;
                }

                if (!actionContext.ModelState.IsValid)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(new BaseResponse
                    {
                        Status = ActionResult.InputError
                    });
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
                actionContext.Response = actionContext.Request.CreateResponse(new BaseResponse
                {
                    Status = ActionResult.Error
                });
            }
        }
    }
}