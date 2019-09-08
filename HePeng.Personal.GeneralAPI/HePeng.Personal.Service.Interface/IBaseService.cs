using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Dto.Security;
using HePeng.Personal.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Service.Interface
{
    /// <summary>
    /// 服务接口基类
    /// </summary>
    /// <typeparam name="T">实体类型或相关Dto</typeparam>
    /// <typeparam name="Q">返回类型</typeparam>
    /// <typeparam name="P">查询参数类型</typeparam>
    public interface IBaseService<T, Q, P>
        where P : QueryParam
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        CurrentUser CurrentUser { get; }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        ResponseResult Update(T t);
        /// <summary>
        /// update Status = Model.Enum.Status.删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseResult Delete(int id);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        ResponseResult Add(T t);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        QueryResult<Q> Query(P queryParam);
    }
}
