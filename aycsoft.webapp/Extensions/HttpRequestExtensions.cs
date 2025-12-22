using Microsoft.AspNetCore.Http;

namespace aycsoft.webapp
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.12
    /// 描 述：对HttpRequest类进行扩展
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 判断是否是ajax请求
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static bool IsAjax(this HttpRequest req)
        {
            bool result = false;

            var xreq = req.Headers.ContainsKey("x-requested-with");
            if (xreq)
            {
                result = req.Headers["x-requested-with"] == "XMLHttpRequest";
            }

            return result;
        }
    }
}
