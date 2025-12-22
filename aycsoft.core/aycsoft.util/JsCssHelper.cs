using System;
using System.IO;
using System.Text;
using Yahoo.Yui.Compressor;

namespace aycsoft.util
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.02
    /// 描 述：js,css压缩类
    /// </summary>
    public static class JsCssHelper
    {
        private static readonly JavaScriptCompressor javaScriptCompressor = new JavaScriptCompressor();
        private static readonly CssCompressor cssCompressor = new CssCompressor();

        #region Js 文件操作
        /// <summary>
        /// 读取js文件内容并压缩
        /// </summary>
        /// <param name="filePathlist"></param>
        /// <returns></returns>
        public static string ReadJSFile(string[] filePathlist)
        {
            StringBuilder jsStr = new StringBuilder();
            try
            {
                string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/wwwroot";
                foreach (var filePath in filePathlist)
                {
                    string path = rootPath + filePath;
                    if (DirFileHelper.IsExistFile(path))
                    {
                        string content = File.ReadAllText(path, Encoding.Default);
                        if (ConfigHelper.GetValue<string>("env") != "dev")
                        {
                            content = javaScriptCompressor.Compress(content);
                        }
                        jsStr.Append(content);
                    }
                }
                return jsStr.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region Css 文件操作
        /// <summary>
        /// 读取css 文件内容并压缩
        /// </summary>
        /// <param name="filePathlist"></param>
        /// <returns></returns>
        public static string ReadCssFile(string[] filePathlist)
        {
            StringBuilder cssStr = new StringBuilder();
            try
            {
                string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/wwwroot"; 
                foreach (var filePath in filePathlist)
                {
                    string path = rootPath + filePath;
                    if (DirFileHelper.IsExistFile(path))
                    {
                        string content = File.ReadAllText(path, Encoding.Default);
                        content = cssCompressor.Compress(content);
                        cssStr.Append(content);
                    }
                }
                return cssStr.ToString();
            }
            catch (Exception)
            {
                return cssStr.ToString();
            }
        }
        #endregion


        #region js 操作
        /// <summary>
        /// 压缩js
        /// </summary>
        /// <param name="strJs"></param>
        /// <returns></returns>
        public static string CompressJS(string strJs)
        {
            if (ConfigHelper.GetValue<string>("env") != "dev")
            {
                strJs = javaScriptCompressor.Compress(strJs);
            }
            return strJs;
        }
        /// <summary>
        /// 压缩Css
        /// </summary>
        /// <param name="strCss"></param>
        /// <returns></returns>
        public static string CompressCss(string strCss)
        {
            if (ConfigHelper.GetValue<string>("env") != "dev")
            {
                strCss = cssCompressor.Compress(strCss);
            }
            return strCss;
        }

        #endregion

        #region css 操作

        #endregion

        #region 读取文件
        /// <summary>
        /// 读取js文件
        /// </summary>
        /// <param name="filePath">文件夹目录</param>
        /// <returns></returns>
        public static string ReadJS(string filePath)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                string rootPath = AppContext.BaseDirectory;
                rootPath = rootPath.Replace("\\", "/");
                string path = rootPath + filePath;
                if (DirFileHelper.IsExistFile(path))
                {
                    string content = File.ReadAllText(path, Encoding.UTF8);
                    if (ConfigHelper.GetValue<string>("env") != "dev")
                    {
                        content = javaScriptCompressor.Compress(content);
                    }
                    str.Append(content);
                }
                return str.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 读取css文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadCss(string filePath)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                string rootPath = AppContext.BaseDirectory;
                rootPath = rootPath.Replace("\\", "/");

                string path = rootPath + filePath;
                if (DirFileHelper.IsExistFile(path))
                {
                    string content = File.ReadAllText(path, Encoding.UTF8);
                    content = cssCompressor.Compress(content);
                    str.Append(content);
                }
                return str.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }
}
