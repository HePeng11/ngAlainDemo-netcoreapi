using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HePeng.Personal.Model.Dto
{
    /// <summary>
    /// 响应结果
    /// </summary>
    public class ResponseResult
    {
        public string Msg { get; set; }
        public ResponseStatusCode Code { get; set; } = ResponseStatusCode.ERROR;
        public object Data { get; set; }
    }

    /// <summary>
    /// 自定义状态码
    /// </summary>
    public enum ResponseStatusCode
    {
        ERROR,
        OK
    }

}
