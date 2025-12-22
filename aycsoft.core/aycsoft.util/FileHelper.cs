using System.IO;

namespace aycsoft.util
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.18
    /// 描 述：文件帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 读取根目录下面的文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static byte[] ReadRoot(string filePath) {
            string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/wwwroot";
            string path = rootPath + filePath;
            return File.ReadAllBytes(path);
        }
        /// <summary>
        /// 读取缓存文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static byte[] ReadCache(string fileName)
        {
            string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/cache/";
            string path = rootPath + fileName;
            return File.ReadAllBytes(path);
        }

        /// <summary>
        /// 读取缓存文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="buffer">文件数据</param>
        /// <returns></returns>
        public static void WriteCache(string fileName, byte[] buffer)
        {
            string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/cache/";
            if (!Directory.Exists(rootPath)) {
                Directory.CreateDirectory(rootPath);
            }
            string path = rootPath + fileName;
            FileInfo file = new FileInfo(path);
            
            FileStream fs = file.Create();
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
        }


        /// <summary>
        /// 读取缓存文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static void RemoveCache(string fileName)
        {
            string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/cache/";
            string path = rootPath + fileName;
            File.Delete(path);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <returns></returns>
        public static byte[] Read(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 获取文件的传输类型
        /// </summary>
        /// <param name="fileExt">文件扩展名</param>
        /// <returns></returns>
        public static string getContentType(string fileExt) {
            string contentType = "";
            switch (fileExt?.ToLower())
            {
                case "jpg":
                case "jpeg":
                case "gif":
                case "png":
                case "webp":
                    contentType = "image/" + fileExt.ToLower();
                    break;
                case "bmp":
                    contentType = "application/x-bmp";
                    break;
                case "pdf":
                    contentType = "application/" + fileExt.ToLower();
                    break;
                case "txt":
                    contentType = "text/plain";
                    break;
                case "csv":
                    contentType = "";
                    break;
                case "html":
                    contentType = "text/html"; 
                    break;
                default:
                    contentType = "application/octet-stream";
                    break;
            }

            return contentType;
        }
    }
}
