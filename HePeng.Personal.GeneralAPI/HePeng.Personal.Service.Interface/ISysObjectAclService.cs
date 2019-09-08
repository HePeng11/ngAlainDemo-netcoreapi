using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Service.Interface
{
    /// <summary>
    /// 系统角色或用户拥有的权限服务层接口
    /// </summary>
    public interface ISysObjectAclService : IBaseService<SysObjectAcl, SysObjectAcl, QueryParam>, IScopeInject
    {
        /// <summary>
        /// 根据角色或用户id查询相关权限
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        List<int> QueryAclIds(SysObjectAclQueryParam queryParam);

        /// <summary>
        /// 更新角色或用户的权限点集合
        /// </summary>
        /// <param name="objectAclsDto"></param>
        /// <returns></returns>
        ResponseResult Update(ObjectAclsDto objectAclsDto);

        /// <summary>
        /// 查询当前用户权限名称集合
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        List<string> QueryAclNames();

        /// <summary>
        /// 查询当前用户是否拥有其中一个权限
        /// </summary>
        /// <param name="aclName"></param>
        /// <returns></returns>
        bool ExistAcl(string[] aclName);
    }
}
