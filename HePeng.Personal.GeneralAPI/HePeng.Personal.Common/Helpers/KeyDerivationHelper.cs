using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace HePeng.Personal.Common.Helpers
{
    /// <summary>
    /// PBKDF2加密方式
    /// </summary>
    public class KeyDerivationHelper
    {
        /// <summary>
        /// 得到加密结果和盐值
        /// </summary>
        /// <param name="password"></param>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static byte[] GetEncriptPassword(string password, out byte[] bt)
        {
            var salt = Encoding.Default.GetBytes(Guid.NewGuid().ToString());
            bt = salt;
            var p = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 1000, 24);
            return p;
        }

        /// <summary>
        /// 已知盐值和明文密码 得到加密结果
        /// </summary>
        /// <param name="password"></param>
        /// <param name="bt"></param>
        /// <returns></returns>
        public static byte[] GetEncriptPassword(string password, byte[] bt)
        {
            var p = KeyDerivation.Pbkdf2(password, bt, KeyDerivationPrf.HMACSHA256, 1000, 24);
            return p;
        }
    }
}
