using HePeng.Personal.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto.QueryParams
{
    /// <summary>
    /// 角色查询参数
    /// </summary>
    public class SysRoleQueryParam : CommonQueryParam
    {
    }

    /// <summary>
    /// 角色用户查询参数
    /// </summary>
    public class SysRoleUserQueryParam : QueryParam
    {
        public int RoleId { get; set; }
        public string Key { get; set; }
        public Gender? Gender { get; set; }
    }
}
