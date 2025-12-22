using System.Text;
using aycsoft.util;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace aycsoft.webapp
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.02
    /// 描 述：对HtmlHelper类进行扩展
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 往页面中写入js文件
        /// </summary>
        /// <param name="htmlHelper">需要扩展对象</param>
        /// <param name="jsFiles">文件路径</param>
        /// <returns></returns>
        public static HtmlString AppendJsFile(this IHtmlHelper htmlHelper, params string[] jsFiles)
        {
            if (htmlHelper is null)
            {
                throw new System.ArgumentNullException(nameof(htmlHelper));
            }

            string jsFile = "";
            foreach (string file in jsFiles)
            {
                if (jsFile != "")
                {
                    jsFile += ",";
                }
                jsFile += file;
            }
            string jsStr = "";
            if (ConfigHelper.GetValue<string>("env") != "dev")
            {
                jsStr = ConfigHelper.GetCache(jsFile);
            }
            if (string.IsNullOrEmpty(jsStr))
            {
                jsStr = JsCssHelper.ReadJSFile(jsFiles);
                ConfigHelper.SetCache(jsFile, jsStr);
            }

            StringBuilder content = new StringBuilder();
            string jsFormat = "<script>{0}</script>";

            content.AppendFormat(jsFormat, jsStr);
            return new HtmlString(content.ToString());
        }
        /// <summary>
        /// 往页面中写入css样式
        /// </summary>
        /// <param name="htmlHelper">需要扩展对象</param>
        /// <param name="cssFiles">文件路径</param>
        /// <returns></returns>
        public static HtmlString AppendCssFile(this IHtmlHelper htmlHelper, params string[] cssFiles)
        {
            if (htmlHelper is null)
            {
                throw new System.ArgumentNullException(nameof(htmlHelper));
            }

            string cssFile = "";
            foreach (string file in cssFiles)
            {
                if (cssFile != "")
                {
                    cssFile += ",";
                }
                cssFile += file;
            }
            string cssStr = "";
            if (ConfigHelper.GetValue<string>("env") != "dev")
            {
                cssStr = ConfigHelper.GetCache(cssFile);
            }


            if (string.IsNullOrEmpty(cssStr))
            {
                var url ="";

                cssStr = JsCssHelper.ReadCssFile(cssFiles);
                if (url != "/")
                {
                    cssStr = cssStr.Replace("url(", "url(" + url);
                }

                ConfigHelper.SetCache(cssFile, cssStr);
            }
            StringBuilder content = new StringBuilder();
            string cssFormat = "<style>{0}</style>";
            content.AppendFormat(cssFormat, cssStr);
            return new HtmlString(content.ToString());
        }

        public static HtmlString Js(this IHtmlHelper htmlHelper, string strJs)
        {
            if (htmlHelper is null)
            {
                throw new System.ArgumentNullException(nameof(htmlHelper));
            }

            string jsStr = JsCssHelper.CompressJS(strJs);

                        StringBuilder content = new StringBuilder();
            string jsFormat = "<script>{0}</script>";

            content.AppendFormat(jsFormat, jsStr);
            return new HtmlString(content.ToString());
        }

        public static HtmlString Css(this IHtmlHelper htmlHelper, string strCss)
        {
            if (htmlHelper is null)
            {
                throw new System.ArgumentNullException(nameof(htmlHelper));
            }

            string cssStr  = JsCssHelper.CompressCss(strCss);
            StringBuilder content = new StringBuilder();
            string cssFormat = "<style>{0}</style>";
            content.AppendFormat(cssFormat, cssStr);
            return new HtmlString(content.ToString());
        }
        /// <summary>
        /// 获取虚拟目录
        /// </summary>
        /// <returns></returns>
        public static string GetVirtualPath(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper is null)
            {
                throw new System.ArgumentNullException(nameof(htmlHelper));
            }

            return ConfigHelper.GetConfig().VirtualPath;
        }

        #region 权限模块
        /// <summary>
        /// 设置当前页面地址
        /// </summary>
        /// <param name = "htmlHelper" ></ param >
        /// < returns ></ returns >
        public static HtmlString SetCurrentUrl(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper is null)
            {
                throw new System.ArgumentNullException(nameof(htmlHelper));
            }

            string mouldeCode = ContextHelper.GetItem("mouldeCode") as string;
            return new HtmlString("<script>var lrCurrentMouldeCode='" + mouldeCode + "';var lrModuleButtonList;var lrModuleColumnList;var lrModule;var lrModuleForms;</script>");
        }
        #endregion
    }
}
