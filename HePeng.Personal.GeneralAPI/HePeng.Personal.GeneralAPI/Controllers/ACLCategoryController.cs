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
    /// SysACLCategory
    /// </summary>
    [Route(CommonConst.controllerPlaceHolder)]
    [EnableCors(CommonConst.CORSPolicyName)]
    [ApiController]
    [Authorize(Roles = JwtConsts.RoleName)]
    [AclAutherizationFilter(AclNames.acls_CUD)]
    public class ACLCategoryController : BaseController
    {
        ISysACLCategoryService SysACLCategoryService { get; }
        IHostingEnvironment Env { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysACLCategoryService"></param>
        /// <param name="env"></param>
        public ACLCategoryController(ISysACLCategoryService sysACLCategoryService, IHostingEnvironment env)
        {
            SysACLCategoryService = sysACLCategoryService;
            Env = env;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sysACLCategory"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Add(SysACLCategory sysACLCategory)
        {
            return SysACLCategoryService.Add(sysACLCategory);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ResponseResult Delete(int id)
        {
            return SysACLCategoryService.Delete(id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sysACLCategory"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ResponseResult Update(int id, [FromBody] SysACLCategory sysACLCategory)
        {
            sysACLCategory.Id = id;
            return SysACLCategoryService.Update(sysACLCategory);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [AclAutherizationFilter(AclNames.acls_read)]
        public object Query([FromQuery]CommonQueryParam queryParam)
        {
            var r = SysACLCategoryService.Query(queryParam);
            return r;
        }

    }
}