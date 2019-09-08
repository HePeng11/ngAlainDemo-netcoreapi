using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Common.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HePeng.Personal.GeneralAPI.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// 系统全局配置
        /// </summary>
        protected APIAppsetting APIAppsetting { get; }

        /// <summary>
        /// 服务器URL前缀
        /// </summary>
        protected string UrlPrefix { get { return HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value; } }

        /// <summary>
        /// constractor
        /// </summary>
        public BaseController()
        {
            APIAppsetting = ServiceLocator.GetService<APIAppsetting>();
        }


    }
}
