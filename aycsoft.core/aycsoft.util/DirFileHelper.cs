using System.IO;

namespace aycsoft.util
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.02
    /// 描 述：文件夹，文件操作类
    /// </summary>
    public static class DirFileHelper
    {
        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
