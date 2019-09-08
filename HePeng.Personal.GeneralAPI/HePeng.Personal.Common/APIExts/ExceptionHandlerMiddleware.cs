using HePeng.Personal.Common.Consts;
using HePeng.Personal.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HePeng.Personal.Common.APIExts
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext, ILoggerFactory loggerFactory)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                ILogger<ExceptionHandlerMiddleware> logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
                logger.LogError(CommonConst.NewLine + ex.Message + CommonConst.NewLine + ex.StackTrace + CommonConst.NewLine);
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            context.Response.ContentType = "text/plain";
            context.Response.AllowCors();
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(exception.Message).ConfigureAwait(false);
        }
    }
}
