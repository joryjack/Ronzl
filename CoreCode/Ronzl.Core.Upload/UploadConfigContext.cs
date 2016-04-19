using Ronzl.Core.Config;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ronzl.Framework.Utility;
using System.IO;

namespace Ronzl.Core.Upload
{
    public class UploadConfigContext
    {
        private static readonly object olock = new object();

        public static UploadConfig UploadConfig = CachedConfigContext.Current.UploadConfig;

        static UploadConfigContext()
        {
        }

        public static string uploadPath;

        #region 文件上传路径
        /// <summary>
        /// 文件上传路径
        /// </summary>
        public static string UploadPath
        {
            get
            {
                if (uploadPath == null)
                {
                    lock (olock)
                    {
                        if (uploadPath == null)
                        {
                            uploadPath = CachedConfigContext.Current.UploadConfig.UploadPath ?? String.Empty;
                            if (HttpContext.Current != null)
                            {
                                var isLocal = Fetch.ServerDomain.IndexOf("", StringComparison.OrdinalIgnoreCase) < 0;
                                if (String.IsNullOrEmpty(UploadConfig.UploadPath) || !Directory.Exists(UploadConfig.UploadPath))
                                {
                                    uploadPath = HttpContext.Current.Server.MapPath("~/" + "Upload");
                                }
                            }
                        }
                    }
                }
                return uploadPath;
            }
        }
        #endregion

        private static Dictionary<string, ThumbnailSize> thumbnailConfigDic;

        #region 文件对象集合
        /// <summary>
        /// 文件对象集合
        /// </summary>
        public static Dictionary<string, ThumbnailSize> ThumbnailConfigDic
        {
            get
            {
                if (thumbnailConfigDic == null)
                {

                    lock (olock)
                    {
                        if (thumbnailConfigDic == null)
                        {
                            thumbnailConfigDic = new Dictionary<string, ThumbnailSize>();
                            foreach (var folder in UploadConfig.UploadFolders)
                            {
                                foreach (var s in folder.ThumbnailSizes)
                                {
                                    var key = string.Format("{0}_{1}_{2}", folder.Path, s.Width, s.Height).ToLower();
                                    if (!thumbnailConfigDic.ContainsKey(key))
                                    {
                                        thumbnailConfigDic.Add(key, s);
                                    }
                                }
                            }
                        }
                    }
                }
                return thumbnailConfigDic;
            }
        } 
        #endregion
    }
}
