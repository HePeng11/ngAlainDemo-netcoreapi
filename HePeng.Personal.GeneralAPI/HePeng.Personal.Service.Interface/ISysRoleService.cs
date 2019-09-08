using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using System;

namespace HePeng.Personal.Service.Interface
{
    /// <summary>
    /// 系统角色服务层接口
    /// </summary>
    public interface ISysRoleService : IBaseService<SysRole, SysRole, SysRoleQueryParam>, IScopeInject
    {
    }
}
