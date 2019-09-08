using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Model.Enum;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Repository.Interface
{
    /// <summary>
    /// 附件、文件从属关系仓储层接口
    /// </summary>
    public interface ISysFileRelationRepository : IBaseRepository<SysFileRelation, QueryParam>, IScopeInject
    {
        List<SysFile> GetFile(List<int> businessIds, UploadType uploadType);
    }
}
