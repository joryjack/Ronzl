using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronzl.Core.Config
{
    /// <summary>
    /// Config接口
    /// </summary>
    public interface IConfigService
    {
        string GetConfig(string name);
        void SaveConfig(string name, string content);
        string GetFilePath(string name);

    }
}
