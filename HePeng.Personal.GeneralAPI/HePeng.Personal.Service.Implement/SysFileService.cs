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
    /// 系统附件服务层实现
    /// </summary>
    public class SysFileService : BaseService<SysFile, QueryParam>, ISysFileService
    {
        ISysFileRepository SysFileRepository { get; }
        public SysFileService(ISysFileRepository sysFileRepository)
        {
            Repository = SysFileRepository = sysFileRepository;
        }

    }
}
