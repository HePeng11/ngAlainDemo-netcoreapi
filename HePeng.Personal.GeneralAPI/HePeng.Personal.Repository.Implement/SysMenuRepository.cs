using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Repository.Implement
{
    public class SysMenuRepository : BaseRepository<SysMenu, SysMenuQueryParam>, ISysMenuRepository
    {

        public List<SysMenu> GetAllMenus()
        {
            var list = SqlSugarClient.Ado.UseStoredProcedure().SqlQuery<SysMenu>("sp_GetMenus");
            return list;
        }

        public override QueryResult<SysMenu> Query(SysMenuQueryParam queryParam)
        {
            QueryResult<SysMenu> result = new QueryResult<SysMenu>();
            var total_outParam = new SugarParameter("@total", null, true);
            //var list = SqlSugarClient.Queryable<SysMenu>().Where(f => Convert.ToBoolean(string.IsNullOrWhiteSpace(queryParam.MenuText)) || f.Text.Contains(queryParam.MenuText)).ToPageList(queryParam.PageIndex, queryParam.PageSize);//Convert.ToBoolean 是重点 =》CAST(@constant0 AS BIT)=1
            result.List = SqlSugarClient.Ado.UseStoredProcedure()
                .SqlQuery<SysMenu>("sp_QueryMenus",
                new SugarParameter("@pageIndex", queryParam.PageIndex),
                new SugarParameter("@pageSize", queryParam.PageSize),
                total_outParam
                );
            result.Total = Convert.ToInt32(total_outParam.Value);
            return result;
        }

    }
}
