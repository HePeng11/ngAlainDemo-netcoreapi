using HePeng.Personal.Model.Enum;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Entity
{
    [SugarTable("Sys_FileRelation")]
    public class SysFileRelation : BaseEntity
    {
        /// <summary>
        /// sysfile id
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// 相关业务表ID
        /// </summary>
        public int BusinessId { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// <summary>
        /// 上传类型
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public UploadType UploadType { get; set; }
    }
}

