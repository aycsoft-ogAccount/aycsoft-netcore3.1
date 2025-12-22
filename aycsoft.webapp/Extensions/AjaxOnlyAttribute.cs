using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using System;

namespace aycsoft.webapp
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.12
    /// 描 述：仅允许Ajax操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// 验证请求有效性
        /// </summary>
        /// <param name="routeContext"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            return routeContext.HttpContext.Request.IsAjax();
        }
    }
}
