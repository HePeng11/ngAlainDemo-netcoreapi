using HePeng.Personal.Common.Consts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// copy object propertys
        /// </summary>
        /// <param name="targetObj"></param>
        /// <param name="fromObj"></param>
        public static void CopyFrom(this object targetObj, object fromObj)
        {
            var propertys = targetObj.GetType().GetProperties();
            var fromPropertys = fromObj.GetType().GetProperties().ToList();
            foreach (var property in propertys)
            {
                try
                {
                    var fromProperty = fromPropertys.FirstOrDefault(f => f.Name == property.Name && f.PropertyType == property.PropertyType);
                    if (fromProperty != null)
                    {
                        var v = fromProperty.GetValue(fromObj);
                        property.SetValue(targetObj, v);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(CommonConst.LongDottedLine);
                    Debug.WriteLine($"CopyFrom Error when get {targetObj.GetType().Name}.{property.Name} from {fromObj.GetType().Name}");
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(CommonConst.LongDottedLine);
                }
            }
        }

        public static T CloneJson<T>(this T source)
        {
            if (source == null)
            {
                return default;
            }

            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        /// <summary>
        /// 用逗号隔开 组成字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToCommaString<T>(this List<T> list)
        {
            if (list == null)
            {
                return string.Empty;
            }
            if (typeof(T) != typeof(int) && typeof(T) != typeof(string))
            {
                return string.Empty;
            }

            return string.Join(CommonConst.CommaSymbol, list);
        }

        /// <summary>
        /// 在output窗口打印异常
        /// </summary>
        /// <param name="e"></param>
        public static void WriteExceptionOutput(this Exception e)
        {
            Debug.WriteLine(CommonConst.LongDottedLine);
            Debug.WriteLine(e.Message);
            Debug.WriteLine(e.StackTrace);
            Debug.WriteLine(CommonConst.LongDottedLine);
        }

        /// <summary>
        /// 允许跨域
        /// </summary>
        /// <param name="response"></param>
        public static void AllowCors(this HttpResponse response)
        {
            if (!response.Headers.Keys.Contains("Access-Control-Allow-Origin"))
            {
                response.Headers.Add("Access-Control-Allow-Origin", "*");
            }
        }
        
    }
}
