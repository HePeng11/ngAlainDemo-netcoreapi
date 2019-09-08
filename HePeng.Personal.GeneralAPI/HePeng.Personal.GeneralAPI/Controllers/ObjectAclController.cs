using Common.Jwt;
using HePeng.Personal.Common.Consts;
using HePeng.Personal.GeneralAPI.Other;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Enum;
using HePeng.Personal.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HePeng.Personal.GeneralAPI.Controllers
{
    /// <summary>
    /// SysObjectAcl
    /// </summary>
    [Route(CommonConst.controllerPlaceHolder)]
    [EnableCors(CommonConst.CORSPolicyName)]
    [ApiController]
    [Authorize(Roles = JwtConsts.RoleName)]
    [AclAutherizationFilter(AclNames.acl_manage_CUD)]
    public class ObjectAclController : BaseController
    {
        ISysObjectAclService SysObjectAclService { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysObjectAclService"></param>
        public ObjectAclController(ISysObjectAclService sysObjectAclService)
        {
            SysObjectAclService = sysObjectAclService;
        }


        /// <summary>
        /// 更新角色或用户的权限点集合 权限管理页面
        /// </summary>
        /// <param name="objectAclsDto"></param>
        /// <returns></returns>
        [HttpPut]
        public ResponseResult Update([FromBody] ObjectAclsDto objectAclsDto)
        {
            return SysObjectAclService.Update(objectAclsDto);
        }

        /// <summary>
        /// 全部权限查询 权限管理页面
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [AclAutherizationFilter(AclNames.acl_manage_read)]
        public object Query([FromQuery]SysObjectAclQueryParam queryParam)
        {
            var r = SysObjectAclService.QueryAclIds(queryParam);
            return r;
        }


        /// <summary>
        /// 查询当前用户权限集合 每个用户登录成功后调用
        /// </summary>
        /// <returns></returns>
        [HttpGet(CommonConst.actionPlaceHolder)]
        [RemoveAclAutherizationFilter]
        public object QueryAclNames()
        {
            var r = SysObjectAclService.QueryAclNames();
            return r;
        }

    }
}