using ce.autofac.extension;
using Microsoft.AspNetCore.Http;

namespace aycsoft.util
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.02
    /// 描 述：web帮助类
    /// </summary>
    public static class WebHelper
    {
        /// <summary>
        /// 获取访问的客户客户端IP
        /// </summary>
        public static string GetClinetIP()
        {
            var accessor = IocManager.Instance.GetService<IHttpContextAccessor>();
            return accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
