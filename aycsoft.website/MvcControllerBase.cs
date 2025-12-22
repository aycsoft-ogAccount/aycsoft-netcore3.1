using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

namespace aycsoft.website
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.11
    /// 描 述：基础控制器
    /// </summary>
    public class MvcControllerBase : Controller
    {
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
    }
}
