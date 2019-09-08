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
    /// acl验证 已设置action会覆盖controller的特性
    /// </summary>
    public class AclAutherizationFilter : ServiceFilterAttribute //TypeFilterAttribute不依靠IOC容器依靠反射来提供实例 serverfilter依靠容器
    {
        private readonly string[] AclNames;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aclNames"></param>
        public AclAutherizationFilter(params string[] aclNames) : base(typeof(AclAutherationImpl))
        {
            AclNames = aclNames;
        }

        /// <summary>
        /// acl验证 已设置action会覆盖controller的特性
        /// </summary>
        public class AclAutherationImpl : IActionFilter
        {
            private readonly ISysObjectAclService SysObjectAclService;
            private readonly ILoggerFactory LoggerFactory;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="sysObjectAclService"></param>
            /// <param name="loggerFactory"></param>
            public AclAutherationImpl(ISysObjectAclService sysObjectAclService, ILoggerFactory loggerFactory)
            {
                SysObjectAclService = sysObjectAclService;
                LoggerFactory = loggerFactory;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context.ActionDescriptor.EndpointMetadata.Where(f => f.GetType() == typeof(RemoveAclAutherizationFilter)).Count() > 0) return;
                var filterDes = context.ActionDescriptor.FilterDescriptors.LastOrDefault(f => f.Filter.GetType() == typeof(AclAutherizationFilter));
                if (filterDes != null)
                {
                    var filter = filterDes.Filter as AclAutherizationFilter;
                    if (!SysObjectAclService.ExistAcl(filter.AclNames))
                    {
                        ILogger<AclAutherizationFilter> logger = LoggerFactory.CreateLogger<AclAutherizationFilter>();
                        logger.LogWarning($"{SysObjectAclService.CurrentUser.Name}请求{context.ActionDescriptor.DisplayName},权限不足,请检查权限配置情况");
                        context.Result = new ContentResult { StatusCode = StatusCodes.Status403Forbidden, Content = "您的权限不足,请联系管理员" };
                    }
                }
            }
        }

    }

    /// <summary>
    /// 当controller上存在aclfilter时 移除此action上的acl验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RemoveAclAutherizationFilter : Attribute
    {

    }

}
