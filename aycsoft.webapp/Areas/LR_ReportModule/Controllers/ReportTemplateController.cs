using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

namespace aycsoft.webapp.Areas.LR_ReportModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：报表模板
    /// </summary>
    [Area("LR_ReportModule")]
    public class ReportTemplateController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 采购报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PurchaseReport()
        {
            return View();
        }
        /// <summary>
        /// 销售报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SalesReport()
        {
            return View();
        }
        /// <summary>
        /// 仓库报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult StockReport()
        {
            return View();
        }
        /// <summary>
        /// 收支报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FinanceReport()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取采购报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public IActionResult GetPurchaseReportList()
        {
            string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/wwwroot/";
            var data = ExcelHelper.ExcelImport(rootPath + "reportData/PurchaseReport.xlsx");
            return Success(data);
        }
        /// <summary>
        /// 获取销售报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public IActionResult GetSalesReportList()
        {
            string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/wwwroot/";
            var data = ExcelHelper.ExcelImport(rootPath + "reportData/SalesReport.xlsx");
            return Success(data);
        }
        /// <summary>
        /// 获取仓库报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetStockReportList()
        {
            string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/wwwroot/";
            var data = ExcelHelper.ExcelImport(rootPath + "reportData/StockReport.xlsx");
            return Success(data);
        }
        /// <summary>
        /// 获取收支报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFinanceReportList()
        {
            string rootPath = ConfigHelper.GetValue<string>("baseDir") + "/wwwroot/";
            var data = ExcelHelper.ExcelImport(rootPath + "reportData/FinanceReport.xlsx");
            return Success(data);
        }
        #endregion
    }
}