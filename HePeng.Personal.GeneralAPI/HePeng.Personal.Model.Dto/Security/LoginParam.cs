using System;

namespace HePeng.Personal.Model.Dto
{
    public class LoginParam
    {
        public string LoginName { get; set; }

        public string Password { get; set; }
    }

    public class LoginParamEntity
    {
        public string LoginName { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }
    }

}
