using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using System;

namespace HePeng.Personal.Service.Interface
{
    /// <summary>
    /// 系统访问控制列表服务层接口
    /// </summary>
    public interface ISysACLCategoryService : IBaseService<SysACLCategory, SysACLCategory, CommonQueryParam>, IScopeInject
    {
    }
}
