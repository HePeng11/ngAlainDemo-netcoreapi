using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HePeng.Personal.Common.CoreHelpers
{
    /// <summary>
    /// 服务实例对象获取帮助类
    /// </summary>
    public static class ServiceLocator
    {
        public static IServiceProvider Instance { get; set; }

        public static T GetService<T>() where T : class
        {
            return Instance.GetService(typeof(T)) as T;
        }

        public static T GetServiceInScope<T>() where T : class
        {
            using (var serviceScope = Instance.CreateScope()) //手动高亮
            {
                return serviceScope.ServiceProvider.GetService(typeof(T)) as T;
            }
        }

    }
}
