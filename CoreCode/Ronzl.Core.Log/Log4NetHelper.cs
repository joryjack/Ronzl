using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ronzl.Core.Config;
using System.IO;
using log4net;
using Newtonsoft.Json;

namespace Ronzl.Core.Log
{
    /// <summary>
    /// Log4Net助手类
    /// </summary>
    public static class Log4NetHelper
    {
        static Log4NetHelper()
        {
            //初始化log4Net配置
            var config = CachedConfigContext.Current.ConfigService.GetConfig("log4net");
            //重写log4net配置里的连接字符串
            config = config.Replace("{connectionString}", CachedConfigContext.Current.DaoConfig.Log);
            var ms = new MemoryStream(Encoding.Default.GetBytes(config));
            log4net.Config.XmlConfigurator.Configure(ms);
        }

        public static void Debug(LoggerType loggerType, object message, Exception e)
        {
            var logger = LogManager.GetLogger(loggerType.ToString());
            logger.Debug(SerializeObject(message), e);
        }

        public static void Error(LoggerType loggerType, object message, Exception e)
        {
            var logger = LogManager.GetLogger(loggerType.ToString());
            logger.Error(SerializeObject(message), e);
        }

        public static void Info(LoggerType loggerType, object message, Exception e)
        {
            var logger = LogManager.GetLogger(loggerType.ToString());
            logger.Info(SerializeObject(message), e);
        }

        public static void Fatal(LoggerType loggerType, object message, Exception e)
        {
            var logger = LogManager.GetLogger(loggerType.ToString());
            logger.Fatal(SerializeObject(message), e);
        }

        public static void Warn(LoggerType loggerType, object message, Exception e)
        {
            var logger = LogManager.GetLogger(loggerType.ToString());
            logger.Warn(SerializeObject(message), e);
        }

        private static object SerializeObject(object message)
        {
            if (message is string || message == null)
                return message;
            else
                return JsonConvert.SerializeObject(message, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }

    public enum LoggerType
    {
        WebExceptionLog,
        ServiceExceptionLog
    }

}
