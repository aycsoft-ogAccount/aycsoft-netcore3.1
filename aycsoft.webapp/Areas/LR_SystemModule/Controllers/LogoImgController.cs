using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.23
    /// 描 述：logo图片设置
    /// </summary>
    [Area("LR_SystemModule")]
    public class LogoImgController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// PC端图片设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PCIndex()
        {
            return View();
        }
        /// <summary>
        /// 移动端图片设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AppIndex()
        {
            return View();
        }
        #endregion
    }
}
