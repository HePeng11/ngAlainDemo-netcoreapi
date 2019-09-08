using System;
using System.Collections.Generic;
using HePeng.Personal.Model.Dto;
using System.Text;
using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto.Menus;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Dto.QueryParams;

namespace HePeng.Personal.Service.Interface
{
    /// <summary>
    /// 系统菜单服务层
    /// </summary>
    public interface ISysMenuService : IBaseService<SysMenu, SysMenuDto, SysMenuQueryParam>, IScopeInject
    {
        List<NgMenu> GetMenus();

        List<NgMenu> GetMenusExcept(int id);
    }
}
