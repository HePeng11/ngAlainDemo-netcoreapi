using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Dto.User;
using HePeng.Personal.Model.Entity;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Repository.Interface
{
    /// <summary>
    /// 系统角色用户仓储层接口
    /// </summary>
    public interface ISysRoleUserRepository : IBaseRepository<SysRoleUser, QueryParam>, IScopeInject
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        QueryResult<SysRoleUserDto> Query(SysRoleUserQueryParam queryParam);

        /// <summary>
        /// 非该角色用户 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        QueryResult<SysUser> QueryIsNotRole(SysRoleUserQueryParam queryParam);

        /// <summary>
        /// 新增角色用户
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        bool Add(SysRoleUser[] roleUsers);

        /// <summary>
        /// 批量删除角色用户
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        bool Delete(int[] ids);

        /// <summary>
        /// 查询角色的所以用户
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        List<SysUser> Query(int roleId);
    }
}
