using HePeng.Personal.Model.Enum;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Entity
{

    [SugarTable("Sys_ObjectACL")]
    public class SysObjectAcl : BaseEntity
    {
        /// <summary>
        /// 角色或者用户id
        /// </summary>
        public int ObjectId { get; set; }

        public ObjectACLType ObjectType { get; set; }

        /// <summary>
        /// SysACL id
        /// </summary>
        public int ACLId { get; set; }

    }
}
