using HePeng.Personal.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto.Menus
{
    public class SysMenuDto:SysMenu
    {
        public bool Group { get; set; }

        public int Key { get; set; }

        public List<SysMenuDto> Children { get; set; }

        public int Order { get; set; }
    }
}
