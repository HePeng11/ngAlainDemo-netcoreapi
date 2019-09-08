using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Dto.User;
using HePeng.Personal.Model.Entity;
using System;

namespace HePeng.Personal.Service.Interface
{
    /// <summary>
    /// 系统用户服务层接口
    /// </summary>
    public interface ISysUserService : IBaseService<SysUser, SysUser, SysUserQueryParam>, IScopeInject
    {
        ResponseResult Login(LoginParam loginParam);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        QueryResult<SysUserDto> QueryDto(SysUserQueryParam q);

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ResponseResult UpdatePassword(int id, string password);
    }
}
