using HePeng.Personal.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto.QueryParams
{
    /// <summary>
    /// 系统用户查询参数
    /// </summary>
    public class SysUserQueryParam : CommonQueryParam
    {
        public Gender? Gender { get; set; }
    }
}
