using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace HePeng.Personal.Model.Entity
{
    /// <summary>
    /// 角色用户关系
    /// </summary>
    [SugarTable("Sys_RoleUser")]
    public class SysRoleUser : BaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
    }
}
