using System.Threading.Tasks;
using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.3 Aycsoft敏捷开发框架
    /// Copyright (c) 2013-2018 上海Aycsoft信息技术有限公司
    /// 创建人：Aycsoft-框架开发组
    /// 日 期：2017.03.09
    /// 描 述：数据库创建常用字段
    /// </summary>
    [Area("LR_SystemModule")]
    public class DbFieldController : MvcControllerBase
    {
        private readonly DbFieldIBLL _dbFieldIBLL;

        public DbFieldController(DbFieldIBLL dbFieldIBLL)
        {
            _dbFieldIBLL = dbFieldIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
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
        /// 字段选择页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SelectForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string queryJson)
        {
            var data = await _dbFieldIBLL.GetList(queryJson);
            return this.Success(data);
        }
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
            var data = await _dbFieldIBLL.GetPageList(paginationobj, queryJson);
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
            var data = await _dbFieldIBLL.GetEntity(keyValue);
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
        public async Task<IActionResult> SaveForm(string keyValue, DbFieldEntity entity)
        {
            await _dbFieldIBLL.SaveEntity(keyValue, entity);
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
            await _dbFieldIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion
    }
}
