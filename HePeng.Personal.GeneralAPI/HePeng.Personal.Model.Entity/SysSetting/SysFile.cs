using HePeng.Personal.Model.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SqlSugar;
using System;

namespace HePeng.Personal.Model.Entity
{
    /// <summary>
    /// 附件 文件 实体类
    /// </summary>
    [SugarTable("Sys_File")]
    public class SysFile : BaseEntity
    {
        /// <summary>
        /// 相对路径
        /// </summary>
        public string Path { get; set; }

        /// 原名
        public string Name { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string ExtName { get; set; }

        /// <summary>
        /// <summary>
        /// 上传类型
        /// </summary>
        public UploadType UploadType { get; set; }

        /// 上传时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}