using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using System;
using System.Linq.Expressions;

namespace HePeng.Personal.Repository.Implement
{
    public class SysUserRepository : BaseRepository<SysUser, SysUserQueryParam>, ISysUserRepository
    {
        public SysUser GetEntity(string account)
        {
            var list = SimpleClient.GetList(f => f.Account == account && f.Status == Model.Enum.Status.正常);
            if (list.Count >= 1)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }


        public override QueryResult<SysUser> Query(SysUserQueryParam queryParam)
        {
            QueryResult<SysUser> result = new QueryResult<SysUser>();
            var totalCount = 0;
            result.List = SqlSugarClient.Queryable<SysUser>()
                .Where(f => f.Status == Model.Enum.Status.正常)
                .WhereIF(!string.IsNullOrWhiteSpace(queryParam.Key), f => f.Account.Contains(queryParam.Key) || f.Name.Contains(queryParam.Key) || f.Phone.Contains(queryParam.Key) || f.Adress.Contains(queryParam.Key))
                .WhereIF(queryParam.Gender != null, f => f.Gender == queryParam.Gender)
                .ToPageList(queryParam.PageIndex, queryParam.PageSize, ref totalCount);
            result.Total = totalCount;

            result.List.ForEach(f => f.Password = null);
            return result;
        }

        public override bool Update(SysUser t)
        {
            var r = SqlSugarClient.Updateable(t).UpdateColumns(f => new { f.Name, f.Account, f.Adress, f.Birthday, f.Gender, f.Phone }).ExecuteCommand();
            return r > 0;
        }

    }
}
