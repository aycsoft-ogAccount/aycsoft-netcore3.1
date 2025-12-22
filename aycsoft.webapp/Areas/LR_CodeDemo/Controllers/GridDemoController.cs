using Microsoft.AspNetCore.Mvc;

namespace aycsoft.webapp.Areas.LR_CodeDemo.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.27
    /// 描 述：表格应用
    /// </summary>
    [Area("LR_CodeDemo")]
    public class GridDemoController : MvcControllerBase
    {
        /// <summary>
        /// 普通表格
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CommonIndex()
        {
            return View();
        }

        /// <summary>
        /// 编辑表格
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditIndex()
        {
            return View();
        }

        /// <summary>
        /// 报表表格
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ReportIndex()
        {
            return View();
        }
    }
}