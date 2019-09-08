using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Dto.User;
using HePeng.Personal.Model.Entity;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Service.Interface
{
    /// <summary>
    /// 系统角色用户服务层接口
    /// </summary>
    public interface ISysRoleUserService : IBaseService<SysRoleUser, SysRoleUser, QueryParam>, IScopeInject
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
        QueryResult<SysUserDto> QueryIsNotRole(SysRoleUserQueryParam queryParam);

        /// <summary>
        /// 新增角色用户
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        ResponseResult Add(SysRoleUser[] roleUsers);

        /// <summary>
        /// 批量删除角色用户
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        ResponseResult Delete(int[] ids);

    }
}
