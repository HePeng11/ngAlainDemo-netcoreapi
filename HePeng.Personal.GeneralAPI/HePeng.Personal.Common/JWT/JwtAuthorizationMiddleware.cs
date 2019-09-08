using HePeng.Personal.Common.Consts;
using HePeng.Personal.Common.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Jwt
{
    /// <summary>
    /// 授权中间件
    /// </summary>
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public JwtAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 检测是否包含'Authorization'请求头，如果不包含则直接放行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext, ILoggerFactory loggerFactory)
        {
            ILogger<JwtAuthorizationMiddleware> logger = loggerFactory.CreateLogger<JwtAuthorizationMiddleware>();
            var authorizationKey = "Authorization";
            var bearer = "Bearer";
            if (!httpContext.Request.Headers.ContainsKey(authorizationKey))
            {
                return _next(httpContext);
            }
            var tokenHeader = (httpContext.Request.Headers[authorizationKey] + "").ToString();
            TokenModel tm = null;
            try
            {
                if (tokenHeader.StartsWith(bearer))
                {
                    tokenHeader = tokenHeader.Substring(bearer.Length).Trim();
                }
                tm = JwtHelper.SerializeJWT(tokenHeader);
            }
            catch (Exception e)
            {
                logger.LogInformation(CommonConst.NewLine + e.Message);
                return _next(httpContext);
            }

            //授权
            var claimList = new List<Claim>();
            var claim = new Claim(ClaimTypes.Role, JwtConsts.RoleName);
            claimList.Add(claim);
            var identity = new ClaimsIdentity(claimList);
            var principal = new UserPrincipal(identity, tm.Uid);
            httpContext.User = principal; //此处为关键 设置当前用户的值 否则如果请求的资源需要权限将返回401

            return _next(httpContext);
        }
    }

}