using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_ReportModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：详细信息维护
    /// </summary>
    [Area("LR_ReportModule")]
    public class RptSchemeController : MvcControllerBase
    {
        private readonly RptSchemeIBLL _rptSchemeIBLL;

        public RptSchemeController(RptSchemeIBLL rptSchemeIBLL) {
            _rptSchemeIBLL = rptSchemeIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 浏览页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Preview()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _rptSchemeIBLL.GetPageList(paginationobj, keyword);
            var jsonData = new
            {
                rows = data,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntity(string keyValue)
        {
            var data = await _rptSchemeIBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 获取报表数据
        /// </summary>
        /// <param name="reportId">报表主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetReportData(string reportId)
        {
            var reportEntity =await _rptSchemeIBLL.GetEntity(reportId);
            dynamic paramJson = reportEntity.F_ParamJson.ToJson();
            var data = new
            {
                tempStyle = reportEntity.F_TempStyle,
                chartType = reportEntity.F_TempType,
                chartData =await _rptSchemeIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), paramJson.F_ChartSqlString.ToString()),
                listData = await _rptSchemeIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), paramJson.F_ListSqlString.ToString())
            };
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, RptSchemeEntity entity)
        {
            await _rptSchemeIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _rptSchemeIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion
    }
}