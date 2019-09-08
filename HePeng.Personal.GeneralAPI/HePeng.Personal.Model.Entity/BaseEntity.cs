using HePeng.Personal.Model.Enum;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HePeng.Personal.Model.Entity
{
    public class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public Status Status { get; set; } = Status.正常;
    }
}
