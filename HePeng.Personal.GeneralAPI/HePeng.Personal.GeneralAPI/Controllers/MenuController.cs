using System;
using System.Collections.Generic;
using Common.Jwt;
using HePeng.Personal.Common.Consts;
using HePeng.Personal.GeneralAPI.Other;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.Menus;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using HePeng.Personal.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HePeng.Personal.GeneralAPI.Controllers
{
    /// <summary>
    /// menucontroller
    /// </summary>
    [Route(CommonConst.controllerPlaceHolder)]
    [ApiController]
    [EnableCors(CommonConst.CORSPolicyName)]
    [Authorize(Roles = JwtConsts.RoleName)]
    [AclAutherizationFilter(AclNames.menus_CUD)]
    public class MenuController : BaseController
    {
        ISysMenuService SysMenuService { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysMenuService"></param>
        public MenuController(ISysMenuService sysMenuService)
        {
            SysMenuService = sysMenuService;
        }

        /// <summary>
        /// 获取当前用户菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("getMenus")]
        [RemoveAclAutherizationFilter]
        public List<NgMenu> GetMenus()
        {
            var r = SysMenuService.GetMenus();
            return r;
        }

        /// <summary>
        /// 获取菜单列表
        /// 菜单管理界面使用
        /// </summary>
        /// <returns></returns>
        [HttpGet("getMenusExcept")]
        public List<NgMenu> GetMenusExcept(int id)
        {
            var r = SysMenuService.GetMenusExcept(id);
            return r;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        [HttpGet]
        [AclAutherizationFilter(AclNames.menus_read)]
        public QueryResult<SysMenuDto> Query([FromQuery]SysMenuQueryParam queryParam)
        {
            var r = SysMenuService.Query(queryParam);
            return r;
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ResponseResult Update(int id, [FromBody] SysMenu menu)
        {
            menu.Id = id;
            return SysMenuService.Update(menu);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ResponseResult Delete(int id)
        {
            return SysMenuService.Delete(id);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Add(SysMenu menu)
        {
            return SysMenuService.Add(menu);
        }

    }
}