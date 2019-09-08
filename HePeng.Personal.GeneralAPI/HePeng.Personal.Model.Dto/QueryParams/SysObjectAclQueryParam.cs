using HePeng.Personal.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto.QueryParams
{
    public class SysObjectAclQueryParam
    {
        /// <summary>
        /// 角色或者用户id
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// 权限类型 角色或用户
        /// </summary>
        public ObjectACLType ObjectType { get; set; }
    }
}
