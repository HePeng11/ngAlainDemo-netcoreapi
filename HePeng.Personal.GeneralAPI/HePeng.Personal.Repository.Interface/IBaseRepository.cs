using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HePeng.Personal.Repository.Interface
{
    public interface IBaseRepository<T, Q> where T : BaseEntity
        where Q : QueryParam
    {
        /// <summary>
        /// update Status = Model.Enum.Status.删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Add(T t);

        bool Update(T t);

        bool Update(T t, Expression<Func<T, object>> columns);

        T GetEntity(int id);

        QueryResult<T> Query(Q queryParam);
    }
}
