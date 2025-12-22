using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace aycsoft.webapp.Middleware
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.11
    /// 描 述：权限中间件
    /// </summary>
    public class MiddlewareEx
    {
        private readonly RequestDelegate _next;

        public MiddlewareEx(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="httpContext">请求连接</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
            return;
        }
    }
}
