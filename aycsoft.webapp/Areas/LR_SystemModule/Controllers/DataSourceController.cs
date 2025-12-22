using System.Threading.Tasks;
using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.31
    /// 描 述：数据源管理
    /// </summary>
    [Area("LR_SystemModule")]
    public class DataSourceController : MvcControllerBase
    {
        private readonly DataSourceIBLL _dataSourceIBLL;

        public DataSourceController(DataSourceIBLL dataSourceIBLL)
        {
            _dataSourceIBLL = dataSourceIBLL;
        }

        #region 获取视图
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
        /// 测试页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TestForm()
        {
            return View();
        }
        /// <summary>
        /// 选择页面
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
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _dataSourceIBLL.GetPageList(paginationobj, keyword);
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
        /// 获取所有数据源数据列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList()
        {
            var data = await _dataSourceIBLL.GetList();
            return Success(data);
        }
        /// <summary>
        /// 获取所有数据源实体根据编号
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntityByCode(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return Success("");
            }
            else
            {
                var data = await _dataSourceIBLL.GetEntityByCode(keyValue.Split(',')[0]);
                return Success(data);
            }

        }
        /// <summary>
        /// 获取所有数据源实体根据编号
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetNameByCode(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return Success("");
            }
            else
            {
                var data = await _dataSourceIBLL.GetEntityByCode(keyValue.Split(',')[0]);
                return Success(data.F_Name);
            }

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
        public async Task<IActionResult> SaveForm(string keyValue, DataSourceEntity entity)
        {
            bool res = await _dataSourceIBLL.SaveEntity(keyValue, entity);
            if (res)
            {
                return SuccessInfo("保存成功！");
            }
            else
            {
                return Fail("保存失败,编码重复！");
            }
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
            await _dataSourceIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取数据源数据
        /// </summary>
        /// <param name="code">数据源编号</param>
        /// <param name="queryJson">数据源请求条件字串</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetDataTable(string code, string queryJson)
        {
            var data = await _dataSourceIBLL.GetDataTable(code, queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取数据源数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="code">数据源编号</param>
        /// <param name="queryJson">数据源请求条件字串</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetDataTablePage(string pagination, string code, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _dataSourceIBLL.GetDataTable(code, paginationobj, queryJson);
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
        /// 获取表单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTree(string code, string parentId, string Id, string showId)
        {
            var data = await _dataSourceIBLL.GetTree(code, parentId, Id, showId);
            return Success(data);
        }

        /// <summary>
        /// 获取数据源列名
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetDataColName(string code)
        {
            var data = await _dataSourceIBLL.GetDataColName(code);
            return Success(data);
        }

        /// <summary>
        /// 获取数据源列名
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> GetDataTableBySql(string code, string sql)
        {
            sql = Str.SqlFilters(sql);
            var data = await _dataSourceIBLL.GetDataTableBySql(code, sql);
            return Success(data);
        }
        #endregion
    }
}
