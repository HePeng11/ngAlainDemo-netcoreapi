using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Repository.Implement
{
    /// <summary>
    /// 系统角色仓储层接口
    /// </summary>
    public class SysRoleRepository : BaseRepository<SysRole, SysRoleQueryParam>, ISysRoleRepository
    {
        public override QueryResult<SysRole> Query(SysRoleQueryParam queryParam)
        {
            QueryResult<SysRole> result = new QueryResult<SysRole>();
            var totalCount = 0;
            result.List = SqlSugarClient.Queryable<SysRole>().Where(f => f.Status == Model.Enum.Status.正常)
                .WhereIF(!string.IsNullOrWhiteSpace(queryParam.Key), f => f.Name.Contains(queryParam.Key) || f.Description.Contains(queryParam.Key))
                .ToPageList(queryParam.PageIndex, queryParam.PageSize, ref totalCount);
            result.Total = totalCount;
            return result;
        }
    }
}
