using Common.Jwt;
using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using HePeng.Personal.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Service.Implement
{
    /// <summary>
    /// 系统角色或用户拥有的权限服务层实现
    /// </summary>
    public class SysObjectAclService : BaseService<SysObjectAcl, QueryParam>, ISysObjectAclService
    {
        IMemoryCache Cache { get; }
        ISysObjectAclRepository SysObjectAclRepository { get; }
        ISysRoleUserRepository SysRoleUserRepository { get; }
        public SysObjectAclService(ISysObjectAclRepository sysObjectAclRepository, ISysRoleUserRepository sysRoleUserRepository, IMemoryCache cache)
        {
            Repository = SysObjectAclRepository = sysObjectAclRepository;
            SysRoleUserRepository = sysRoleUserRepository;
            Cache = cache;
        }

        /// <summary>
        /// 根据角色或用户id查询相关权限
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public List<int> QueryAclIds(SysObjectAclQueryParam queryParam)
        {
            return SysObjectAclRepository.QueryAclIds(queryParam);
        }

        /// <summary>
        /// 更新角色或用户的权限点集合
        /// </summary>
        /// <param name="objectAclsDto"></param>
        /// <returns></returns>
        public ResponseResult Update(ObjectAclsDto objectAclsDto)
        {
            if (SysObjectAclRepository.Update(objectAclsDto))
            {
                Result.Code = ResponseStatusCode.OK;
                Result.Msg = "更新成功";
                if (objectAclsDto.Type == Model.Enum.ObjectACLType.用户)
                {
                    Cache.Remove(CacheKeys.AclNames.Format(objectAclsDto.Id));
                }
                else
                {
                    var list = SysRoleUserRepository.Query(objectAclsDto.Id);
                    list.ForEach(f =>
                    {
                        Cache.Remove(CacheKeys.AclNames.Format(f.Id));
                    });
                }
            }
            return Result;
        }

        /// <summary>
        /// 查询当前用户权限集合
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public List<string> QueryAclNames()
        {
            var list = Cache.GetOrCreate(CacheKeys.AclNames.Format(CurrentUser.Id), (a) =>
            {
                return SysObjectAclRepository.QueryAclNames(CurrentUser.Id);
            });
            return list;
        }

        /// <summary>
        /// 查询当前用户是否拥有此权限
        /// </summary>
        /// <param name="aclName"></param>
        /// <returns></returns>
        public bool ExistAcl(string[] aclNames)
        {
            var list = QueryAclNames();
            foreach (var aclName in aclNames)
            {
                if (list.Contains(aclName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
