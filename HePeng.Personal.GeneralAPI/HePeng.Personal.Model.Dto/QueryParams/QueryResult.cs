using HePeng.Personal.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto.QueryParams
{
    public class QueryResult<T>
    {
        public int Total { get; set; }

        public List<T> List { get; set; }
    }
}
