using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// string.Format(str, args)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>
        /// 获取文件扩展名称
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFileExtName(this string str)
        {
            var i = str.LastIndexOf('.');
            return str.Substring(i, str.Length - i);
        }
    }
}
