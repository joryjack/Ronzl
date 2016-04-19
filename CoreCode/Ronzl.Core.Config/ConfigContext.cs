using Ronzl.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronzl.Core.Config
{
    public class ConfigContext
    {
        #region 接口属性
        /// <summary>
        /// 接口属性
        /// </summary>
        public IConfigService ConfigService { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigContext() : this(new FileConfigService())
        {
        }
        public ConfigContext(IConfigService pageContentConfigService)
        {
            ConfigService = pageContentConfigService;
        }
        #endregion

        #region 验证配置文件是否配置分区索引
        /// <summary>
        /// 验证配置文件是否配置分区索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configFile"></param>
        /// <param name="index"></param>
        public virtual void VilidateClusteredByIndex<T>(T configFile, string index) where T : ConfigFileBase
        {
            //if (configFile.ClusteredByIndex && string.IsNullOrEmpty(index))
            //    throw new Exception("调用时没有提供配置文件的分区索引");
        }
        #endregion

        #region 获取文件名称
        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual string GetConfigFileName<T>(string index = null)
        {
            var fileName = typeof(T).Name;
            if (!string.IsNullOrEmpty(index))
            {
                fileName = string.Format("{0}_{1}", fileName, index);
            }
            return fileName;
        }
        #endregion

        #region 读取config内容
        /// <summary>
        /// 读取config内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        private T GetConfigFile<T>(string index = null) where T : ConfigFileBase, new()
        {
            var result = new T();
            var fileName = GetConfigFileName<T>(index);
            var content = ConfigService.GetConfig(fileName);
            if (content == null)
            {
                ConfigService.SaveConfig(fileName, string.Empty);
            }
            else if (!string.IsNullOrEmpty(content))
            {
                try
                {
                    result = (T)SerializationHelper.XmlDeserialize(typeof(T), content);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return result;
        }
        #endregion

        public virtual T Get<T>(string index = null) where T : ConfigFileBase, new()
        {
            var result = new T();
            VilidateClusteredByIndex(result, index);
            result = GetConfigFile<T>(index);
            return result;
        }

        #region 保存Confing
        /// <summary>
        /// 保存Confing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configFile"></param>
        /// <param name="index"></param>
        public void Save<T>(T configFile, string index = null) where T : ConfigFileBase, new()
        {
            VilidateClusteredByIndex(configFile, index);
            configFile.Save();
            var fileName = GetConfigFileName<T>(index);
            ConfigService.SaveConfig(fileName, SerializationHelper.XmlSerialize(configFile));
        }
        #endregion
    }
}
