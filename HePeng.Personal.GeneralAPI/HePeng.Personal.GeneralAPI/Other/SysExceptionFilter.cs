using HePeng.Personal.Common.Consts;
using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HePeng.Personal.GeneralAPI.Other
{
    /// <summary>
    /// 异常过滤器 
    /// 暂不使用此系统异常过滤器 已使用了异常中间件
    /// </summary>
    public class SysExceptionFilter : ServiceFilterAttribute //TypeFilterAttribute不依靠IOC容器依靠反射来提供实例 serverfilter依靠容器
    {
        /// <summary>
        /// 
        /// </summary>
        public SysExceptionFilter() : base(typeof(SysExceptionFilterImpl))
        {
        }

        /// <summary>
        ///
        /// </summary>
        public class SysExceptionFilterImpl : IExceptionFilter
        {
            private readonly ISysUserService SysUserService;
            private readonly ILoggerFactory LoggerFactory;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="sysUserService"></param>
            /// <param name="loggerFactory"></param>
            public SysExceptionFilterImpl(ISysUserService sysUserService, ILoggerFactory loggerFactory)
            {
                SysUserService = sysUserService;
                LoggerFactory = loggerFactory;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void OnException(ExceptionContext context)
            {
                ILogger<SysExceptionFilter> logger = LoggerFactory.CreateLogger<SysExceptionFilter>();
                logger.LogError($"{CommonConst.NewLine + context.ActionDescriptor.DisplayName}执行报错,user:{SysUserService.CurrentUser.Name}({SysUserService.CurrentUser.Id})  {CommonConst.NewLine + context.Exception.Message} \r\n {context.Exception.StackTrace}");
                context.Result = new ContentResult { StatusCode = StatusCodes.Status500InternalServerError, Content = "请求失败,请联系管理员" };
            }

        }

    }
}
