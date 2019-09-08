using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Jwt;
using HePeng.Personal.Common.Consts;
using HePeng.Personal.Common.Dtos;
using HePeng.Personal.GeneralAPI.Other;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using HePeng.Personal.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HePeng.Personal.GeneralAPI.Controllers
{
    /// <summary>
    /// user
    /// </summary>
    [Route(CommonConst.controllerPlaceHolder)]
    [EnableCors(CommonConst.CORSPolicyName)]
    [ApiController]
    [Authorize(Roles = JwtConsts.RoleName)]
    [AclAutherizationFilter(AclNames.users_CUD)]
    public class UserController : BaseController
    {
        ISysUserService SysUserService { get; }
        IHostingEnvironment Env { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysUserService"></param>
        /// <param name="env"></param>
        public UserController(ISysUserService sysUserService, IHostingEnvironment env)
        {
            SysUserService = sysUserService;
            Env = env;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginParam"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [RemoveAclAutherizationFilter]
        public ResponseResult Login(LoginParam loginParam)
        {
            var r = SysUserService.Login(loginParam);
            return r;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [AclAutherizationFilter(AclNames.users_read)]
        public object Query([FromQuery]SysUserQueryParam queryParam)
        {
            var r = SysUserService.QueryDto(queryParam);
            return r;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Add(SysUser user)
        {
            return SysUserService.Add(user);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ResponseResult Delete(int id)
        {
            return SysUserService.Delete(id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ResponseResult Update(int id, [FromBody] SysUser user)
        {
            user.Id = id;
            return SysUserService.Update(user);
        }

        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ///  'Content-Type', 'application/json'
        [HttpPut("updatePassword/{id}")]
        public ResponseResult UpdatePassword(int id, [FromBody] string password)
        {
            return SysUserService.UpdatePassword(id, password);
        }

        /// <summary>
        /// 更改当前用户的密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        ///  'Content-Type', 'application/json'
        [HttpPut("updatePassword")]
        public ResponseResult UpdatePassword([FromBody] string password)
        {
            return SysUserService.UpdatePassword(SysUserService.CurrentUser.Id, password);
        }

    }
}