using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Ronzl.Core.Config
{
    public class FileConfigService : IConfigService
    {
        #region 配置文件夹路径
        /// <summary>
        /// 配置文件夹路径
        /// </summary>
        private readonly string configFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
        #endregion

        #region 读取config内容
        /// <summary>
        /// 读取config内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetConfig(string fileName)
        {
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }
            var configPath = GetFilePath(fileName);
            if (!File.Exists(configPath))
            {
                return null;
            }
            else
            {
                return File.ReadAllText(configPath);
            }
        }
        #endregion

        #region 返回文件路径
        /// <summary>
        /// 返回文件路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFilePath(string fileName)
        {
            return String.Format("{0}/{1}.xml", configFolder, fileName);
        }
        #endregion

        #region 写入Config文件文本
        /// <summary>
        /// 写入Config文件文本
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="content">文件内容</param>
        public void SaveConfig(string fileName, string content)
        {
            var configPath = GetConfig(fileName);
            File.WriteAllText(configPath, content);
        } 
        #endregion
    }
}
