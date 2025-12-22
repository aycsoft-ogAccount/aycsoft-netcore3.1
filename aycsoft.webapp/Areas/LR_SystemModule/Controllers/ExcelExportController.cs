using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.25
    /// 描 述：Excel导出管理
    /// </summary>
    [Area("LR_SystemModule")]
    public class ExcelExportController : MvcControllerBase
    {
        private readonly ExcelExportIBLL _excelExportIBLL;

        public ExcelExportController(ExcelExportIBLL excelExportIBLL)
        {
            _excelExportIBLL = excelExportIBLL;
        }


        #region 视图功能
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _excelExportIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取列表数据
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string moduleId)
        {
            var data = await _excelExportIBLL.GetList(moduleId);
            return Success(data);
        }
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntity(string keyValue)
        {
            var data = await _excelExportIBLL.GetEntity(keyValue);
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
        public async Task<IActionResult> SaveForm(string keyValue, ExcelExportEntity entity)
        {
            await _excelExportIBLL.SaveEntity(keyValue, entity);
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
            await _excelExportIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 更新表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpdateState(string keyValue, int state)
        {
            ExcelExportEntity entity = new ExcelExportEntity()
            {
                F_EnabledMark = state
            };
            await _excelExportIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("操作成功！");
        }
        #endregion
    }
}