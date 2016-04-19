using Castle.DynamicProxy;
using Ronzl.Core.Log;
using Ronzl.Framework.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronzl.Core.Service
{
    public partial class ServiceHelper
    {
        /// <summary>
        /// 使用引用服务方式, 如需使用注入或者WCF服务方式可自行改造
        /// </summary>
        public static ServiceFactory serviceFactory = new RefServiceFactory();

        #region  创建服务根据接口
        /// <summary>
        ///  创建服务根据接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateService<T>() where T : class
        {
            var service = serviceFactory.CreateService<T>();

            //拦截,写入日志
            var generator = new ProxyGenerator();
            var dynamicProxy = generator.CreateInterfaceProxyWithTargetInterface(service, new InvokeInterceptor());
            return dynamicProxy;
        } 
        #endregion

        internal class InvokeInterceptor : IInterceptor
        {
            public InvokeInterceptor()
            {
            }

            #region 拦截方法
            /// <summary>
            /// 拦截方法
            /// </summary>
            /// <param name="invocation"></param>
            public void Intercept(IInvocation invocation)
            {
                try
                {
                    invocation.Proceed();
                }
                catch (Exception exception)
                {
                    if (exception is BusinessException)
                        throw;

                    var message = new
                    {
                        exception = exception.Message,
                        exceptionContext = new
                        {
                            method = invocation.Method.ToString(),
                            arguments = invocation.Arguments,
                            returnValue = invocation.ReturnValue
                        }
                    };

                    Log4NetHelper.Error(LoggerType.ServiceExceptionLog, message, exception);
                    throw;
                }
            } 
            #endregion
        }
    }
}
