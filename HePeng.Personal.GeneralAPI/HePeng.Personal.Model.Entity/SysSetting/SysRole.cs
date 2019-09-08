using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace HePeng.Personal.Model.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    [SugarTable("Sys_Role")]
    public class SysRole : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
