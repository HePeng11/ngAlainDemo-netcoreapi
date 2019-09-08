using Common.Jwt;
using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Model.Dto;
using HePeng.Personal.Model.Dto.QueryParams;
using HePeng.Personal.Model.Entity;
using HePeng.Personal.Repository.Interface;
using HePeng.Personal.Service.Interface;
using System;

namespace HePeng.Personal.Service.Implement
{
    /// <summary>
    /// 访问控制列表服务层实现
    /// </summary>
    public class SysACLService : BaseService<SysACL, QueryParam>, ISysACLService
    {
        ISysACLRepository SysACLRepository { get; }
        public SysACLService(ISysACLRepository sysACLRepository)
        {
            Repository = SysACLRepository = sysACLRepository;
        }

    }
}
