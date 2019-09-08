using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Service.Implement
{
    public class CacheKeys
    {
        /// <summary>
        /// 用户菜单缓存
        /// 0 userid
        /// </summary>
        public const string SysMenu = "SysMenu_{0}";

        /// <summary>
        /// 所有菜单数据集合
        /// </summary>
        public const string SysAllMenu = "SysMenu_All";

        /// <summary>
        /// 用户头像缓存
        /// 0 userid
        /// </summary>
        public const string SysHeadImg = "SysHeadImg_{0}";

        /// <summary>
        /// 用户acl权限缓存
        /// 0 userid
        /// </summary>
        public const string AclNames = "AclNames_{0}";
    }
}
