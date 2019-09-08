using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Entity
{
    /// <summary>
    /// Access Control List 访问控制列表
    /// </summary>

    [SugarTable("Sys_ACL")]
    public class SysACL : BaseEntity
    {
        /// <summary>
        /// SysACLCategory id
        /// </summary>
        public int CateId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
