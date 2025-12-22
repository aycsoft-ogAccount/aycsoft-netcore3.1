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
    /// 日 期：2022.09.27
    /// 描 述：日志控制器
    /// </summary>
    [Area("LR_SystemModule")]
    public class LogController : MvcControllerBase
    {
        private readonly LogIBLL _logIBLL;

        public LogController(LogIBLL logIBLL) {
            _logIBLL = logIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 日志管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 清空
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
        ///  分页查询
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件函数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson)
        {

            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _logIBLL.GetPageList(paginationobj, queryJson, "");
            var jsonData = new
            {
                rows = data,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 分页查询(本人数据)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageListByMy(string pagination, string queryJson)
        {

            Pagination paginationobj = pagination.ToObject<Pagination>();
            var userInfo = await this.CurrentUser();
            var data =await _logIBLL.GetPageList(paginationobj, queryJson, userInfo.F_UserId);
            var jsonData = new
            {
                rows = data,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records
            };
            return Success(jsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveRemoveLog(int categoryId, string keepTime)
        {
            await _logIBLL.Remove(categoryId, keepTime);
            return SuccessInfo("清空成功。");
        }
        #endregion
    }
}