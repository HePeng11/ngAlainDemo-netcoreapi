using HePeng.Personal.Model.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SqlSugar;
using System;

namespace HePeng.Personal.Model.Entity
{
    [SugarTable("Sys_User")]
    public class SysUser : BaseEntity
    {
        public string Account { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
