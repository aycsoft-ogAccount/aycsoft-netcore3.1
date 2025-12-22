using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapi
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.11
    /// 描 述：基础控制器
    /// </summary>
    [TypeFilter(typeof(ExceptionFilter))]
    [ApiController]
    [Route("[controller]/[action]")]
    public class MvcControllerBase : Controller
    {
        #region 获取基础信息
        private UserEntity userInfo;
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        protected async Task<UserEntity> CurrentUser(string userId = null)
        {
            var userBLL = IocManager.Instance.GetService<UserIBLL>();
            if (string.IsNullOrEmpty(userId))
            {
                if (userInfo == null)
                {
                    userInfo = await userBLL.GetEntity();
                }
                return userInfo;
            }
            else
            {
                return await userBLL.GetEntity(userId);
            }

        }
        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="code">编码规则编码</param>
        /// <returns></returns>
        protected Task<string> GetRuleCode(string code)
        {
            var codeRuleIBLL = IocManager.Instance.GetService<CodeRuleIBLL>();
            return codeRuleIBLL.GetBillCode(code);
        }
        /// <summary>
        /// 占用编码
        /// </summary>
        /// <param name="code">编码规则编码</param>
        /// <returns></returns>
        protected async Task UseRuleSeed(string code)
        {
            var codeRuleIBLL = IocManager.Instance.GetService<CodeRuleIBLL>();
            await codeRuleIBLL.UseRuleSeed(code);
        }

        /// <summary>
        /// 获取登录者用户名称
        /// </summary>
        /// <returns></returns>
        protected string GetUserName()
        {
            return ContextHelper.GetItem("userName") as string;
        }
        /// <summary>
        /// 获取登录者用户Id
        /// </summary>
        /// <returns></returns>
        protected string GetUserId()
        {
            return ContextHelper.GetItem("userId") as string;
        }
        /// <summary>
        /// 获取登录者用户账号
        /// </summary>
        /// <returns></returns>
        protected string GetUserAccount()
        {
            return ContextHelper.GetItem("account") as string;
        }

        #endregion 

        #region 请求响应
        /// <summary>
        /// 返回成功数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual IActionResult Success(object data)
        {
            return Content(new ResParameter { code = ResponseCode.success, info = "响应成功", data = data }.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual IActionResult Success(string info, object data)
        {
            return Content(new ResParameter { code = ResponseCode.success, info = info, data = data }.ToJson());
        }
        /// <summary>
        /// 返回成功数据
        /// </summary>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected virtual IActionResult SuccessInfo(string info)
        {
            return Content(new ResParameter { code = ResponseCode.success, info = info }.ToJson());
        }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="info">消息</param>
        /// <returns></returns>
        protected virtual IActionResult Fail(string info)
        {
            return Content(new ResParameter { code = ResponseCode.fail, info = info }.ToJson());
        }
        #endregion

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
    }
}
