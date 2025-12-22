using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace aycsoft.webapi
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.17
    /// 描 述：给api添加权限验证信息参数
    /// </summary>

    public class AddAuthTokenHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// 给接口添加隐性参数
        /// </summary>
        /// <param name="operation">api操作接口</param>
        /// <param name="context">上下文</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;
            //先判断是否是匿名访问,
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                //非匿名的方法,链接中添加accesstoken值
                if (!isAnonymous)
                {
                    if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = "token",
                        Description = "登录密钥",
                        In = ParameterLocation.Header,
                        Schema = new OpenApiSchema() { Type = "string" },
                        Required = true //是否必选
                    });
                }
            }

        }
    }
}
