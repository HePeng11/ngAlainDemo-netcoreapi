using HePeng.Personal.Model.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto.User
{
    public class SysUserDto
    {
        public string Account { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public int Id { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Status Status { get; set; } = Status.正常;

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadImgUrl { get; set; }

    }

    public class SysRoleUserDto: SysUserDto
    {
        public int RoleUserId { get; set; }

    }
}
