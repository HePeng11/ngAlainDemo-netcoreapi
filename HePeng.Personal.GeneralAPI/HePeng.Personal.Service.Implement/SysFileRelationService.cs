using Common.Jwt;
using HePeng.Personal.Common.Consts;
using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using HePeng.Personal.Repository.Interface;
using HePeng.Personal.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HePeng.Personal.Service.Implement
{
    /// <summary>
    /// 附件、文件从属关系服务层实现
    /// </summary>
    public class SysFileRelationService : BaseService<SysFileRelation, QueryParam>, ISysFileRelationService
    {
        ISysFileRelationRepository SysFileRelationRepository { get; }
        ISysFileRepository SysFileRepository { get; }
        IMemoryCache Cache { get; }
        public SysFileRelationService(ISysFileRelationRepository sysFileRelationRepository, ISysFileRepository sysFileRepository, IMemoryCache cache)
        {
            Repository = SysFileRelationRepository = sysFileRelationRepository;
            SysFileRepository = sysFileRepository;
            Cache = cache;
        }

        /// <summary>
        /// 绑定文件从属关系
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override ResponseResult Add(SysFileRelation t)
        {
            var file = SysFileRepository.GetEntity(t.FileId);
            if (file == null)
            {
                Result.Msg = "该文件不存在";
                return Result;
            }
            t.UploadType = file.UploadType;
            if (t.UploadType == UploadType.UserPicture)
            {
                Result = base.Add(t);
                if (Result.Code == ResponseStatusCode.OK)
                {
                    Cache.Remove(CacheKeys.SysHeadImg.Format(t.BusinessId));
                    Result.Msg = "修改头像成功";
                    return Result;
                }
            }

            return Result;
        }

        /// <summary>
        /// 获取用户头像 已处理Path UrlPrefix
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="uploadType"></param>
        public string GetUserHeadImgPath(int userId, UploadType uploadType)
        {
            var path = Cache.GetOrCreate(CacheKeys.SysHeadImg.Format(userId), (a) =>
            {
                a.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CommonConst.CacheMinutes);
                var list = SysFileRelationRepository.GetFile(new List<int>() { userId }, uploadType);
                if (list.Count > 0)
                {
                    var f = list.Last();
                    return UrlPrefix + f.Path;
                }
                return UrlPrefix + "/uploads/364e1238d22d4f28aa6cdd6230f2731c.jpeg";
            });
            return path;
        }


    }
}
