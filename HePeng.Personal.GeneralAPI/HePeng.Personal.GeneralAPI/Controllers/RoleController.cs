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
    /// Role
    /// </summary>
    [Route(CommonConst.controllerPlaceHolder)]
    [EnableCors(CommonConst.CORSPolicyName)]
    [ApiController]
    [Authorize(Roles = JwtConsts.RoleName)]
    [AclAutherizationFilter(AclNames.roles_CUD)]
    public class RoleController : BaseController
    {
        ISysRoleService SysRoleService { get; }
        IHostingEnvironment Env { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysRoleService"></param>
        /// <param name="env"></param>
        public RoleController(ISysRoleService sysRoleService, IHostingEnvironment env)
        {
            SysRoleService = sysRoleService;
            Env = env;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Add(SysRole Role)
        {
            return SysRoleService.Add(Role);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ResponseResult Delete(int id)
        {
            return SysRoleService.Delete(id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ResponseResult Update(int id, [FromBody] SysRole Role)
        {
            Role.Id = id;
            return SysRoleService.Update(Role);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [AclAutherizationFilter(AclNames.roles_read)]
        public object Query([FromQuery]SysRoleQueryParam queryParam)
        {
            var r = SysRoleService.Query(queryParam);
            return r;
        }

    }
}