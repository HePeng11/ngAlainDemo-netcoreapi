using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Common.Dtos;
using HePeng.Personal.Common.JWT;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Dto.Security;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using HePeng.Personal.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Service.Implement
{
    /// <summary>
    /// 基础服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Q"></typeparam>
    public class BaseService<T, Q> where T : BaseEntity
        where Q : QueryParam
    {
        /// <summary>
        /// 系统全局配置
        /// </summary>
        protected APIAppsetting APIAppsetting { get; }

        /// <summary>
        /// 请求结果
        /// </summary>
        protected ResponseResult Result { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        private CurrentUser _currentUser { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public CurrentUser CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    var u = ServiceLocator.GetService<IHttpContextAccessor>().HttpContext.User as UserPrincipal;
                    if (u.Id > 0)
                    {
                        var user = ServiceLocator.GetServiceInScope<ISysUserRepository>().GetEntity(u.Id);
                        if (user != null)
                        {
                            _currentUser = new CurrentUser
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Account = user.Account
                            };
                        }
                    }
                }
                return _currentUser;
            }
        }

        /// <summary>
        /// 服务器URL前缀
        /// </summary>
        protected string UrlPrefix
        {
            get
            {
                var httpContext = ServiceLocator.GetService<IHttpContextAccessor>().HttpContext;
                return httpContext.Request.Scheme + "://" + httpContext.Request.Host.Value;
            }
        }

        protected IBaseRepository<T, Q> Repository;

        /// <summary>
        /// constractor
        /// </summary>
        public BaseService()
        {
            APIAppsetting = ServiceLocator.GetService<APIAppsetting>();
            Result = new ResponseResult();
        }

        protected string UpdateMsgSuccess = "更新成功";
        protected string UpdateMsgError = "更新失败";
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual ResponseResult Update(T t)
        {
            var r = Repository.Update(t);
            if (r)
            {
                Result.Code = ResponseStatusCode.OK;
                Result.Msg = UpdateMsgSuccess;
            }
            else
            {
                Result.Msg = UpdateMsgError;
            }
            return Result;
        }
        protected string DeleteMsgSuccess = "删除成功";
        protected string DeleteMsgError = "删除失败";
        /// <summary>
        /// update Status = Model.Enum.Status.删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ResponseResult Delete(int id)
        {
            var r = Repository.Delete(id);
            if (r)
            {
                Result.Code = ResponseStatusCode.OK;
                Result.Msg = DeleteMsgSuccess;
            }
            else
            {
                Result.Msg = DeleteMsgError;
            }
            return Result;
        }

        protected string AddMsgSuccess = "添加成功";
        protected string AddMsgError = "添加失败";
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual ResponseResult Add(T t)
        {
            var r = Repository.Add(t);
            if (r.Id > 0)
            {
                Result.Code = ResponseStatusCode.OK;
                Result.Msg = AddMsgSuccess;
            }
            else
            {
                Result.Msg = AddMsgError;
            }
            return Result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public virtual QueryResult<T> Query(Q q)
        {
            var r = Repository.Query(q);
            return r;
        }
    }
}
