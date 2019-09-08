using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Entity
{
    /// <summary>
    /// Access Control List Category 访问控制列表类别
    /// </summary>
    [SugarTable("Sys_ACLCategory")]
    public class SysACLCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<SysACL> ACLs { get; set; }
    }
}
