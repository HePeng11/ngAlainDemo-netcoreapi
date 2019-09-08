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
    /// 系统访问控制列表服务层实现
    /// </summary>
    public class SysACLCategoryService : BaseService<SysACLCategory, CommonQueryParam>, ISysACLCategoryService
    {
        ISysACLCategoryRepository SysACLCategoryRepository { get; }
        ISysACLRepository SysACLRepository { get; }
        public SysACLCategoryService(ISysACLCategoryRepository sysACLCategoryRepository, ISysACLRepository sysACLRepository)
        {
            Repository = SysACLCategoryRepository = sysACLCategoryRepository;
            SysACLRepository = sysACLRepository;
        }

        public override ResponseResult Add(SysACLCategory t)
        {
            var r = base.Add(t);
            if (r.Code == ResponseStatusCode.OK)
            {
                SysACLRepository.Add(new SysACL() { CateId = t.Id, Code = "read", Name = "查看", Description = "查看相关菜单及数据" });
                SysACLRepository.Add(new SysACL() { CateId = t.Id, Code = "CUD", Name = "增-删-改", Description = "创建、删除、更改" });
            }
            return r;
        }
    }
}
