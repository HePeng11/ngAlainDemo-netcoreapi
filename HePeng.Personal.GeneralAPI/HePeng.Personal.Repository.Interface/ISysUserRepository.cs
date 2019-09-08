using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using System;

namespace HePeng.Personal.Repository.Interface
{
    public interface ISysUserRepository : IBaseRepository<SysUser, SysUserQueryParam>, IScopeInject
    {
        SysUser GetEntity(string account);
    }
}
