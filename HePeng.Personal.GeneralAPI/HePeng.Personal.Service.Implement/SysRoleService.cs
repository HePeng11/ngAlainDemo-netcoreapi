using Common.Jwt;
using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using HePeng.Personal.Service.Interface;
using System;

namespace HePeng.Personal.Service.Implement
{
    /// <summary>
    /// 系统角色服务层实现
    /// </summary>
    public class SysRoleService : BaseService<SysRole, SysRoleQueryParam>, ISysRoleService
    {
        ISysRoleRepository SysRoleRepository { get; }
        public SysRoleService(ISysRoleRepository sysRoleRepository)
        {
            Repository = SysRoleRepository = sysRoleRepository;
        }
    }
}
