using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.operat;
using aycsoft.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace aycsoft.webapi
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {

        /// <summary>
        /// 发生异常时进入
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                context.Result = new ContentResult
                {
                    Content = new ResParameter { code = ResponseCode.exception, info = context.Exception.Message, data = "" }.ToJson(),//这里是把异常抛出。也可以不抛出。
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "text/html;charset=utf-8"
                };
            }
            context.ExceptionHandled = true;


            var logIBLL = IocManager.Instance.GetService<LogIBLL>();
            LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 4;
            logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
            logEntity.F_OperateAccount = ContextHelper.GetItem("account") as string;
            logEntity.F_OperateUserId = ContextHelper.GetItem("userId") as string ;
            logEntity.F_Module = context.HttpContext.Request.Path;
            logEntity.F_IPAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            logEntity.F_ExecuteResult = -1;
            logEntity.F_ExecuteResultJson = logIBLL.ExceptionFormat(context.Exception);
            logIBLL.Write(logEntity).GetAwaiter().GetResult();
        }
    }
}
