using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using HePeng.Personal.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HePeng.Personal.Repository.Implement
{
    /// <summary>
    /// 系统角色或用户拥有的权限仓储层接口
    /// </summary>
    public class SysObjectAclRepository : BaseRepository<SysObjectAcl, QueryParam>, ISysObjectAclRepository
    {
        /// <summary>
        /// 根据角色或用户id查询相关权限
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public List<int> QueryAclIds(SysObjectAclQueryParam queryParam)
        {
            var result = SqlSugarClient.Queryable<SysObjectAcl>()
                 .Where(f => f.Status == Model.Enum.Status.正常 && f.ObjectType == queryParam.ObjectType && f.ObjectId == queryParam.ObjectId)
                 .Select(f => f.ACLId).ToList();
            return result;
        }

        /// <summary>
        /// 查询用户权限集合
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public List<string> QueryAclNames(int userId)
        {
            var result = SqlSugarClient.Ado.UseStoredProcedure().SqlQuery<string>("sp_sysObjectAcl_getUserACL", new
            {
                userId,
                enum_userType = ObjectACLType.用户,
                enum_roleType = ObjectACLType.角色
            });
            return result;
        }


        /// <summary>
        /// 更新角色或用户的权限点集合
        /// </summary>
        /// <param name="objectAclsDto"></param>
        /// <returns></returns>
        public bool Update(ObjectAclsDto objectAclsDto)
        {
            SqlSugarClient.Ado.UseStoredProcedure().SqlQueryDynamic("sp_sysObjectAcl_update", new
            {
                id = objectAclsDto.Id,
                type = objectAclsDto.Type,
                ownAclIds = objectAclsDto.OwnAclIds.ToCommaString(),
                notOwnAclIds = objectAclsDto.NotOwnAclIds.ToCommaString()
            });

            return true;
        }

    }
}
