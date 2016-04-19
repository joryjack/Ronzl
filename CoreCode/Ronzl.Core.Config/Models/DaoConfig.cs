using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronzl.Core.Config
{
    /// <summary>
    /// 数据库配置类
    /// </summary>
    [Serializable]
    public class DaoConfig : ConfigFileBase
    {
        public DaoConfig() { }

        #region 序列化属性
        public string conStr { get; set; }

        public string Log { get; set; }
        #endregion
    }
}
