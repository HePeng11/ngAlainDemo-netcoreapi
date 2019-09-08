using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using SqlSugar;
using System;

namespace HePeng.Personal.Repository.Implement
{
    /// <summary>
    /// 系统访问控制列表仓储层接口
    /// </summary>
    public class SysACLCategoryRepository : BaseRepository<SysACLCategory, CommonQueryParam>, ISysACLCategoryRepository
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public override QueryResult<SysACLCategory> Query(CommonQueryParam queryParam)
        {
            QueryResult<SysACLCategory> result = new QueryResult<SysACLCategory>();
            var totalCount = 0;
            result.List = SqlSugarClient.Queryable<SysACLCategory>()
                .Where(f => f.Status == Model.Enum.Status.正常)
                .WhereIF(!string.IsNullOrWhiteSpace(queryParam.Key), f => f.Name.Contains(queryParam.Key) || f.Code.Contains(queryParam.Key))
                .ToPageList(queryParam.PageIndex, queryParam.PageSize, ref totalCount);
            result.Total = totalCount;

            result.List.ForEach(f =>
            {
                f.ACLs = SqlSugarClient.Queryable<SysACL>().Where(d => d.CateId == f.Id && d.Status == Model.Enum.Status.正常).ToList();
            });

            return result;
        }
    }
}
