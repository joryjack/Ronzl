using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronzl.Core.Config
{
    /// <summary>
    ///Config节点基础类
    /// </summary>
    public class ConfigNodeBase
    {
        public ConfigNodeBase()
        {
        }
        public int Id { get; set; }
        public int Order { get; set; }
    }
}
