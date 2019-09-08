using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Model.Dto.QueryParams
{
    public class QueryParam
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class CommonQueryParam: QueryParam
    {
        public string Key { get; set; }
    }
}
