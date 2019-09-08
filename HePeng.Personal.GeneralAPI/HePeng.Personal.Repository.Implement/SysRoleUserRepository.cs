using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Dto.User;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Repository.Implement
{
    /// <summary>
    /// 系统角色用户仓储层接口
    /// </summary>
    public class SysRoleUserRepository : BaseRepository<SysRoleUser, QueryParam>, ISysRoleUserRepository
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public QueryResult<SysRoleUserDto> Query(SysRoleUserQueryParam queryParam)
        {
            QueryResult<SysRoleUserDto> result = new QueryResult<SysRoleUserDto>();
            var totalCount = 0;
            result.List = SqlSugarClient.Queryable<SysRoleUser, SysUser>((ru, u) =>
                    new object[] { JoinType.Inner, ru.UserId == u.Id })
                .Where((ru, u) => ru.Status == Model.Enum.Status.正常 && u.Status == Model.Enum.Status.正常 && ru.RoleId == queryParam.RoleId)
                .WhereIF(!string.IsNullOrWhiteSpace(queryParam.Key), (ru, u) => u.Name.Contains(queryParam.Key) || u.Account.Contains(queryParam.Key) || u.Phone.Contains(queryParam.Key))
                .WhereIF(queryParam.Gender != null, (ru, u) => u.Gender == queryParam.Gender)
                .Select((ru, u) => new SysRoleUserDto() { Name = u.Name, Account = u.Account, Adress = u.Adress, Birthday = u.Birthday, Gender = u.Gender, Id = u.Id, Phone = u.Phone, RoleUserId = ru.Id })
                .ToPageList(queryParam.PageIndex, queryParam.PageSize, ref totalCount);
            result.Total = totalCount;
            return result;
        }

        /// <summary>
        /// 非该角色用户 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public QueryResult<SysUser> QueryIsNotRole(SysRoleUserQueryParam queryParam)
        {
            QueryResult<SysUser> result = new QueryResult<SysUser>();
            var totalCount = 0;
            result.List = SqlSugarClient.Queryable<SysUser>()
               .Where(u => u.Status == Model.Enum.Status.正常 && SqlFunc.Subqueryable<SysRoleUser>().Where(ru => ru.RoleId == queryParam.RoleId && ru.Status == Model.Enum.Status.正常 && ru.UserId == u.Id).NotAny())
               .WhereIF(!string.IsNullOrWhiteSpace(queryParam.Key), f => f.Account.Contains(queryParam.Key) || f.Name.Contains(queryParam.Key) || f.Phone.Contains(queryParam.Key) || f.Adress.Contains(queryParam.Key))
               .WhereIF(queryParam.Gender != null, f => f.Gender == queryParam.Gender)
               .ToPageList(queryParam.PageIndex, queryParam.PageSize, ref totalCount);
            result.Total = totalCount;
            return result;
        }

        /// <summary>
        /// 新增角色用户
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        public bool Add(SysRoleUser[] roleUsers)
        {
            var result = SqlSugarClient.Ado.UseTran(() =>
            {
                foreach (var roleUser in roleUsers)
                {
                    base.Add(roleUser);
                }
                return true;
            });
            if (!result.IsSuccess)
            {
                throw result.ErrorException;
            }
            return result.Data;
        }

        /// <summary>
        /// 批量删除角色用户
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        public bool Delete(int[] ids)
        {
            var result = SqlSugarClient.Ado.UseTran(() =>
            {
                foreach (var id in ids)
                {
                    base.Delete(id);
                }
                return true;
            });
            if (!result.IsSuccess)
            {
                throw result.ErrorException;
            }
            return result.Data;
        }

        /// <summary>
        /// 查询角色的所以用户
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public List<SysUser> Query(int roleId)
        {
            var list = SqlSugarClient.Queryable<SysRoleUser, SysUser>((ru, u) =>
                    new object[] { JoinType.Inner, ru.UserId == u.Id })
                .Where((ru, u) => ru.Status == Model.Enum.Status.正常 && u.Status == Model.Enum.Status.正常 && ru.RoleId == roleId)
                .Select((ru, u) => new SysUser { Name = u.Name, Account = u.Account, Adress = u.Adress, Birthday = u.Birthday, Gender = u.Gender, Id = u.Id, Phone = u.Phone }).ToList();
            return list;
        }
    }
}
