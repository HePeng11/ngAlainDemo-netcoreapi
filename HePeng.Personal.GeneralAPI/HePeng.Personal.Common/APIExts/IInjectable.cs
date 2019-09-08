using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Common.APIExts
{
    public interface IInjectable
    {
    }

    /// <summary>
    /// 每次获取时都实例化
    /// </summary>
    public interface ITransentInject : IInjectable
    {
    }
    /// <summary>
    /// 每次会话使用同一个实例
    /// </summary>
    public interface IScopeInject : IInjectable
    {
    }

    /// <summary>
    /// 单例
    /// </summary>
    public interface ISingletonInject : IInjectable
    {
    }
}
