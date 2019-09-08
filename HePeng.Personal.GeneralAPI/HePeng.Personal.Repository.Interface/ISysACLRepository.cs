using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using System;

namespace HePeng.Personal.Repository.Interface
{
    /// <summary>
    /// 访问控制列表仓储层接口
    /// </summary>
    public interface ISysACLRepository : IBaseRepository<SysACL,QueryParam>, IScopeInject
    {

    }
}
