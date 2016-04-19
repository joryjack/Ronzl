using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronzl.Core.Config
{
    /// <summary>
    /// ConfigFileBase抽象类
    /// </summary>
    public abstract class ConfigFileBase
    {
        /// <summary>
        /// 索引
        /// </summary>
        public int id { get; set; }

        public virtual bool ClusteredByIndex
        {
            get
            {
                return false;
            }
        }
        #region 无参构造函数
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ConfigFileBase()
        { }
        #endregion

        /// <summary>
        ///save虚方法
        /// </summary>
        internal virtual void Save()
        { }

        #region 对Config中节点进行重新排序
        /// <summary>
        /// 对Config中节点进行重新排序
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="nodeList">泛型集合</param>
        internal virtual void UpdateNodeList<T>(List<T> nodeList) where T : ConfigNodeBase
        {
            foreach (var node in nodeList)
            {
                if (node.Id > 0)
                {
                    continue;
                }
                node.Id = nodeList.Max(n => n.Id) + 1;
            }
        }
        #endregion
    }


}
