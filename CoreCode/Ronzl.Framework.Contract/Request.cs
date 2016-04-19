using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronzl.Framework.Contract
{
    /// <summary>
    /// 用于BLL方法提传入条件
    /// </summary>
    public class Request : ModelBase
    {
        public Request()
        {
            PageSize = 100000;
        }

        public int Top
        {
            set
            {
                this.PageSize = value;
                this.PageIndex = 1;
            }
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
