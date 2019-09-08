using HePeng.Personal.Common.APIExts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Common.Dtos
{
    public class APIAppsetting
    {
        public DBconection DBconection { get; set; }

        public string JWTSecretKey { get; set; }
    }

    public class DBconection
    {
        public DBconectionType Type { get; set; }
        public string ConectStr { get; set; }

    }

    public enum DBconectionType
    {
        sqlserver = 0,
        oracle = 1
    }
}
