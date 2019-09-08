using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Enum
{
    /// <summary>
    /// acl权限名称
    /// </summary>
    public sealed class AclNames
    {
        public const string menus_read = "menus.read";
        public const string menus_CUD = "menus.CUD";

        public const string users_read = "users.read";
        public const string users_CUD = "users.CUD";

        public const string roles_read = "roles.read";
        public const string roles_CUD = "roles.CUD";

        public const string acls_read = "acls.read";
        public const string acls_CUD = "acls.CUD";

        public const string acl_manage_read = "acl_manage.read";
        public const string acl_manage_CUD = "acl_manage.CUD";

    }

}
