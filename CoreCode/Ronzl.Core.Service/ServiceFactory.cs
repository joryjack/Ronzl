using Ronzl.Core.Cache;
using Ronzl.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronzl.Core.Service
{
    #region 服务工厂
    /// <summary>
    /// 服务工厂
    /// </summary>
    public abstract class ServiceFactory
    {
        public abstract T CreateService<T>() where T : class;
    }
    #endregion

    #region 直接引用提供服务
    /// <summary>
    /// 直接引用提供服务
    /// </summary>
    public class RefServiceFactory : ServiceFactory
    {
        public override T CreateService<T>()
        {
            //第一次通过反射创建服务实例，然后缓存起来
            var interfaceName = typeof(T).Name;
            return CacheHelper.Get<T>(String.Format("Service_{0}", interfaceName), () => { return AssemblyHelper.FindTypeByInterface<T>(); });
        }
    }
    #endregion

    #region 通过Wcf提供服务
    /// <summary>
    /// 通过Wcf提供服务
    /// </summary>
    public class WcfServiceFactory : ServiceFactory
    {
        public override T CreateService<T>()
        {
            //需实现WCF Uri来自配置文件
            var uri = string.Empty;
            var proxy = WcfServiceProxy.CreateServiceProxy<T>(uri);
            return proxy;
        }
    }
    #endregion

}
