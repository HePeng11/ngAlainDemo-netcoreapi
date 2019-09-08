using Common.Jwt;
using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Common.Helpers;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Dto.User;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using HePeng.Personal.Repository.Interface;
using HePeng.Personal.Service.Interface;
using System;
using System.Linq;

namespace HePeng.Personal.Service.Implement
{
    /// <summary>
    /// 系统用户服务层实现
    /// </summary>
    public class SysUserService : BaseService<SysUser, SysUserQueryParam>, ISysUserService
    {
        ISysUserRepository SysUserRepository { get; }

        ISysFileRelationService SysFileRelationService { get; }
        public SysUserService(ISysUserRepository sysUserRepository, ISysFileRelationService sysFileRelationService)
        {
            Repository = SysUserRepository = sysUserRepository;
            SysFileRelationService = sysFileRelationService;
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="loginParam"></param>
        /// <returns></returns>
        ResponseResult ISysUserService.Login(LoginParam loginParam)
        {
            var user = SysUserRepository.GetEntity(loginParam.LoginName);
            if (user == null)
            {
                Result.Msg = "账号或密码错误";
                Result.Code = ResponseStatusCode.ERROR;
                return Result;
            }
            var enPassword = KeyDerivationHelper.GetEncriptPassword(loginParam.Password, user.Salt);
            // byte[]存到binary =》 从十进制转换成了十六进制
            if (!user.Password.SequenceEqual(enPassword))
            {
                Result.Msg = "账号或密码错误";
                Result.Code = ResponseStatusCode.ERROR;
                return Result;
            }
            var token = JwtHelper.IssueJWT(new TokenModel() { Uid = user.Id}, APIAppsetting.JWTSecretKey);
            var path = SysFileRelationService.GetUserHeadImgPath(user.Id, Model.Enum.UploadType.UserPicture);
            Result.Data = new { token, name = user.Name, avatar = path, email = user.Phone };
            Result.Msg = "登录成功";
            Result.Code = ResponseStatusCode.OK;
            return Result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public QueryResult<SysUserDto> QueryDto(SysUserQueryParam q)
        {
            QueryResult<SysUserDto> result = new QueryResult<SysUserDto>();
            var r = base.Query(q);
            result.Total = r.Total;
            result.List = r.List.Select(f =>
            {
                var d = new SysUserDto();
                d.CopyFrom(f);
                d.HeadImgUrl = SysFileRelationService.GetUserHeadImgPath(d.Id, Model.Enum.UploadType.UserPicture);
                return d;
            }).ToList();
            return result;
        }

        public override ResponseResult Add(SysUser t)
        {
            if (SysUserRepository.GetEntity(t.Account) != null)
            {
                Result.Msg = "该用户名已存在";
                return Result;
            }

            t.Password = KeyDerivationHelper.GetEncriptPassword(t.Account, out byte[] bt);
            t.Salt = bt;
            return base.Add(t);
        }


        public override ResponseResult Delete(int id)
        {
            if (CurrentUser.Id == id)
            {
                Result.Msg = "无法删除自身账户";
                return Result;
            }
            return base.Delete(id);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ResponseResult UpdatePassword(int id, string password)
        {
            if (password == null)
            {
                Result.Msg = "密码不能为空";
                return Result;
            }
            var user = SysUserRepository.GetEntity(id);
            if (user == null)
            {
                Result.Msg = "该用户不存在";
                return Result;
            }
            var encriptPassword = KeyDerivationHelper.GetEncriptPassword(password, user.Salt);
            user.Password = encriptPassword;
            var r = SysUserRepository.Update(user, f => new { f.Password });
            if (!r)
            {
                Result.Msg = "更新密码失败，请联系管理员";
                return Result;
            }

            Result.Msg = "更新密码成功";
            Result.Code = ResponseStatusCode.OK;
            return Result;
        }

    }
}
