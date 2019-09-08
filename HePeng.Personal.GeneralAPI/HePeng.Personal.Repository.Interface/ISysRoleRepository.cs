using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using System;

namespace HePeng.Personal.Repository.Interface
{
    /// <summary>
    /// 系统角色仓储层接口
    /// </summary>
    public interface ISysRoleRepository : IBaseRepository<SysRole, SysRoleQueryParam>, IScopeInject
    {

    }
}
