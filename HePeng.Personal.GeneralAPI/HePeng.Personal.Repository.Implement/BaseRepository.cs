using HePeng.Personal.Common.Consts;
using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Common.Dtos;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using SqlSugar;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HePeng.Personal.Repository.Implement
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T, Q> : IBaseRepository<T, Q>
        where T : BaseEntity, new()
        where Q : QueryParam
    {
        public BaseRepository()
        {
            SqlSugarClient = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ServiceLocator.GetService<APIAppsetting>().DBconection.ConectStr,
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样
            });
            //打印SQL
            SqlSugarClient.Aop.OnLogExecuting = (sql, pars) =>
            {
                System.Diagnostics.Debug.WriteLine(CommonConst.LongDottedLine);
                System.Diagnostics.Debug.WriteLine(sql);
                System.Diagnostics.Debug.WriteLine(SqlSugarClient.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                System.Diagnostics.Debug.WriteLine(CommonConst.LongDottedLine);
            };
        }
        public SqlSugarClient SqlSugarClient;//用来处理事务多表查询和复杂的操作
        public SimpleClient<T> SimpleClient { get { return new SimpleClient<T>(SqlSugarClient); } }//用来处理常用操作

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual T Add(T t)
        {
            return SqlSugarClient.Saveable(t).ExecuteReturnEntity();
        }

        /// <summary>
        /// update Status = Model.Enum.Status.删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(int id)
        {
            var r = SqlSugarClient.Updateable<T>().UpdateColumns(it => new T() { Status = Model.Enum.Status.删除 })
                .Where(it => it.Id == id).ExecuteCommand();
            return r > 0;
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool PhysicalDelete(int id)
        {
            var r = SqlSugarClient.Deleteable<T>().Where(it => it.Id == id).ExecuteCommand();
            return r > 0;
        }

        public virtual bool Update(T t)
        {
            var r = SqlSugarClient.Updateable(t).ExecuteCommand();
            return r > 0;
        }

        public virtual bool Update(T t, Expression<Func<T, object>> columns)
        {
            var r = SqlSugarClient.Updateable(t).UpdateColumns(columns).ExecuteCommand();
            return r > 0;
        }

        public virtual T GetEntity(int id)
        {
            return SqlSugarClient.Queryable<T>().Where(f => f.Id == id).First();
        }

        /// <summary>
        /// 可被重写的通用查询方法
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public virtual QueryResult<T> Query(Q queryParam)
        {
            QueryResult<T> result = new QueryResult<T>();
            var totalCount = 0;
            result.List = SqlSugarClient.Queryable<T>().Where(f => f.Status == Model.Enum.Status.正常).ToPageList(queryParam.PageIndex, queryParam.PageSize, ref totalCount);
            result.Total = totalCount;
            return result;
        }



    }

}
