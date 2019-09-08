using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Repository.Interface
{
    public interface ISysMenuRepository : IBaseRepository<SysMenu, SysMenuQueryParam>, IScopeInject
    {
        List<SysMenu> GetAllMenus();

    }
}
