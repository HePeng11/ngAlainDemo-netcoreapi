using Common.Jwt;
using HePeng.Personal.Common.Consts;
using HePeng.Personal.GeneralAPI.Other;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using HePeng.Personal.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HePeng.Personal.GeneralAPI.Controllers
{
    /// <summary>
    /// RoleUser
    /// </summary>
    [Route(CommonConst.controllerPlaceHolder)]
    [EnableCors(CommonConst.CORSPolicyName)]
    [ApiController]
    [Authorize(Roles = JwtConsts.RoleName)]
    [AclAutherizationFilter(AclNames.roles_CUD)]
    public class RoleUserController : BaseController
    {
        ISysRoleUserService SysRoleUserService { get; }
        IHostingEnvironment Env { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysRoleUserService"></param>
        /// <param name="env"></param>
        public RoleUserController(ISysRoleUserService sysRoleUserService, IHostingEnvironment env)
        {
            SysRoleUserService = sysRoleUserService;
            Env = env;
        }

        /// <summary>
        /// 给角色批量分配用户
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Add(SysRoleUser[] roleUsers)
        {
            return SysRoleUserService.Add(roleUsers);
        }

        /// <summary>
        /// 批量删除角色用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public ResponseResult Delete(int[] ids)
        {
            return SysRoleUserService.Delete(ids);
        }

        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [AclAutherizationFilter(AclNames.roles_read)]
        public object Query([FromQuery]SysRoleUserQueryParam queryParam)
        {
            var r = SysRoleUserService.Query(queryParam);
            return r;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet("queryIsNotRole")]
        public object QueryIsNotRole([FromQuery]SysRoleUserQueryParam queryParam)
        {
            var r = SysRoleUserService.QueryIsNotRole(queryParam);
            return r;
        }

       
    }
}