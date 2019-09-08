using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto.Menus
{
    public class NgMenu
    {
        public int key { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public string ExternalLink { get; set; }
        public string Icon { get; set; }
        public bool Group { get; set; }
        public List<NgMenu> Children { get; set; }
    }
}
