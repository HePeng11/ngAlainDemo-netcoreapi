using HePeng.Personal.Model.Enum;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Model.Entity
{
    [SugarTable("Sys_Menu")]
    public class SysMenu : BaseEntity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Text { get; set; }
        public string Link { get; set; }
        /// <summary>
        /// 是否为外部链接
        /// </summary>
        public bool IsExternalLink { get; set; }
        public string Icon { get; set; }
        public bool Disabled { get; set; }
        public int ParentId { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int ShowOrder { get; set; }
        /// <summary>
        /// acl权限编码
        /// </summary>
        public string AclCode { get; set; }

    }
}
