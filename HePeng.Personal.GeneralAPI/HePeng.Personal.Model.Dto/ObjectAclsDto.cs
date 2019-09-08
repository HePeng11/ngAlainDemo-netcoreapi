using HePeng.Personal.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto
{
    public class ObjectAclsDto
    {
        /// <summary>
        /// 角色或用户ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 对象类型 角色或用户
        /// </summary>
        public ObjectACLType Type { get; set; }
        /// <summary>
        /// 该角色或用户拥有的权限ids
        /// </summary>
        public List<int> OwnAclIds { get; set; }
        /// <summary>
        /// 未拥有的权限ids
        /// </summary>
        public List<int> NotOwnAclIds { get; set; }
    }
}
