using aycsoft.operat;
using aycsoft.util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.webapp
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.11
    /// 描 述：权限中间件
    /// </summary>
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOperator _operator;

        public AuthorizeMiddleware(RequestDelegate next, IOperator ioperator)
        {
            _next = next;
            _operator = ioperator;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="httpContext">请求连接</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            //string url = SetUrl(httpContext);
            var endpoint = httpContext.GetEndpoint();
            if (httpContext.Request.IsAjax() && endpoint != null && endpoint.Metadata.GetMetadata<IAllowAnonymous>() == null)
            {
                // 获取请求值
                if (!httpContext.Request.Headers["token"].IsEmpty())
                {
                    string token = httpContext.Request.Headers["token"].ToString();
                    var res =  _operator.DecodeToken(token);
                    if (res == "TokenExpiredException")
                    {
                        await RespondWithJson(httpContext.Response, new ResParameter { code = ResponseCode.nologin, info = "登录信息过期" });
                        return;
                    }
                    else if (res == "SignatureVerificationException")
                    {
                        await RespondWithJson(httpContext.Response, new ResParameter { code = ResponseCode.nologin, info = "非法密钥" });
                        return;
                    }
                    else
                    {
                        var payload = res.ToObject<Payload>(); 
                        ContextHelper.SetItem("account", payload.Account);
                        ContextHelper.SetItem("userId", payload.UserId);
                        ContextHelper.SetItem("userName", payload.UserName);
                    }
                }
                else
                {
                    await RespondWithJson(httpContext.Response, new ResParameter { code = ResponseCode.nologin, info = "权限验证失败" });
                    return;
                }
            }
            else
            {
                if (httpContext.Request.Query.ContainsKey("lrmcode"))
                {
                    string mouldeCode = httpContext.Request.Query["lrmcode"];
                    ContextHelper.SetItem("mouldeCode", mouldeCode);
                }

                if (httpContext.Request.Query.ContainsKey("lraccount"))
                {
                    string account = httpContext.Request.Query["lraccount"];
                    ContextHelper.SetItem("account", account);
                }
            }

            await _next(httpContext);
            return;
        }

        /// <summary>
        /// 设置url地址
        /// </summary>
        /// <param name="httpContext">请求上下文</param>
        /// <returns></returns>
        private string SetUrl(HttpContext httpContext)
        {
            string url = httpContext.Request.Path + httpContext.Request.QueryString.Value;
            ContextHelper.SetItem("currentUrl", url);
            return url;
        }

        /// <summary>
        /// 返回请求信息
        /// </summary>
        /// <param name="response">返回头</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        private async Task RespondWithJson(HttpResponse response, object data)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json;charset=utf-8";
            await response.WriteAsync(data.ToJson(), new UTF8Encoding(false));
        }
    }
}
