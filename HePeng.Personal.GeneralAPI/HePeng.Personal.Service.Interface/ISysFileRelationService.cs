using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using System;

namespace HePeng.Personal.Service.Interface
{
    /// <summary>
    /// 附件、文件从属关系服务层接口
    /// </summary>
    public interface ISysFileRelationService : IBaseService<SysFileRelation, SysFileRelation, QueryParam>, IScopeInject
    {
        /// <summary>
        /// 获取用户头像 已处理Path UrlPrefix
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="uploadType"></param>
        string GetUserHeadImgPath(int userId, UploadType uploadType);
    }
}
