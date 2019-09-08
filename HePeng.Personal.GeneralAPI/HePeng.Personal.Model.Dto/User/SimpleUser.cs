using HePeng.Personal.Model.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto.User
{
    public class SimpleUser
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public string Phone { get; set; }
    }
}
