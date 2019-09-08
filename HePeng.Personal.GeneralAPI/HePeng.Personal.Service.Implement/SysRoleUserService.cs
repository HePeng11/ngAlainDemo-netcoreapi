using Common.Jwt;
using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Dto.User;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using HePeng.Personal.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HePeng.Personal.Service.Implement
{
    /// <summary>
    /// 系统角色用户服务层实现
    /// </summary>
    public class SysRoleUserService : BaseService<SysRoleUser, QueryParam>, ISysRoleUserService
    {
        ISysRoleUserRepository SysRoleUserRepository { get; }
        ISysFileRelationService SysFileRelationService { get; }
        public SysRoleUserService(ISysRoleUserRepository sysRoleUserRepository, ISysFileRelationService sysFileRelationService)
        {
            Repository = SysRoleUserRepository = sysRoleUserRepository;
            SysFileRelationService = sysFileRelationService;
        }

        /// <summary>
        /// 该角色用户 query
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public QueryResult<SysRoleUserDto> Query(SysRoleUserQueryParam queryParam)
        {
            var r = SysRoleUserRepository.Query(queryParam);
            r.List = r.List.Select(f =>
            {
                var d = new SysRoleUserDto();
                d.CopyFrom(f);
                d.HeadImgUrl = SysFileRelationService.GetUserHeadImgPath(d.Id, Model.Enum.UploadType.UserPicture);
                return d;
            }).ToList();
            return r;
        }

        /// <summary>
        /// 非该角色用户 分页查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public QueryResult<SysUserDto> QueryIsNotRole(SysRoleUserQueryParam queryParam)
        {
            QueryResult<SysUserDto> result = new QueryResult<SysUserDto>();
            var r = SysRoleUserRepository.QueryIsNotRole(queryParam);
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

        /// <summary>
        /// 批量新增角色用户
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        public ResponseResult Add(SysRoleUser[] roleUsers)
        {
            var b = SysRoleUserRepository.Add(roleUsers);
            if (b)
            {
                Result.Code = ResponseStatusCode.OK;
                Result.Msg = "分配角色成功";
            }
            return Result;
        }

        /// <summary>
        /// 批量删除角色用户
        /// </summary>
        /// <param name="roleUsers"></param>
        /// <returns></returns>
        public ResponseResult Delete(int[] ids)
        {
            var b = SysRoleUserRepository.Delete(ids);
            if (b)
            {
                Result.Code = ResponseStatusCode.OK;
                Result.Msg = "移除成功";
            }
            return Result;
        }

    }
}
