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
    /// SysACL
    /// </summary>
    [Route(CommonConst.controllerPlaceHolder)]
    [EnableCors(CommonConst.CORSPolicyName)]
    [ApiController]
    [Authorize(Roles = JwtConsts.RoleName)]
    [AclAutherizationFilter(AclNames.acls_CUD)]
    public class ACLController : BaseController
    {
        ISysACLService SysACLService { get; }
        IHostingEnvironment Env { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysACLService"></param>
        /// <param name="env"></param>
        public ACLController(ISysACLService sysACLService, IHostingEnvironment env)
        {
            SysACLService = sysACLService;
            Env = env;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sysACL"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Add(SysACL sysACL)
        {
            return SysACLService.Add(sysACL);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ResponseResult Delete(int id)
        {
            return SysACLService.Delete(id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sysACL"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ResponseResult Update(int id, [FromBody] SysACL sysACL)
        {
            sysACL.Id = id;
            return SysACLService.Update(sysACL);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [AclAutherizationFilter(AclNames.acls_read)]
        public object Query([FromQuery]QueryParam queryParam)
        {
            var r = SysACLService.Query(queryParam);
            return r;
        }

    }
}