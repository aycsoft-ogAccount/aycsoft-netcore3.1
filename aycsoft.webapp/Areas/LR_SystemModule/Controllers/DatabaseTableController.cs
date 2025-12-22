using System.Collections.Generic;
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
    /// 日 期：2022.10.23
    /// 描 述：数据表管理
    /// </summary>
    [Area("LR_SystemModule")]
    public class DatabaseTableController : MvcControllerBase
    {

        private readonly DatabaseTableIBLL _databaseTableIBLL;
        private readonly DbDraftIBLL _dbDraftIBLL;

        public DatabaseTableController(DatabaseTableIBLL databaseTableIBLL, DbDraftIBLL dbDraftIBLL)
        {
            _databaseTableIBLL = databaseTableIBLL;
            _dbDraftIBLL = dbDraftIBLL;
        }

        #region 获取视图
        /// <summary>
        /// 主页面管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表数据查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TableIndex()
        {
            return View();
        }

        /// <summary>
        /// 新增、编辑表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditTableForm()
        {
            return View();
        }

        /// <summary>
        /// 复制表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CopyTableForm()
        {
            return View();
        }

        /// <summary>
        /// 草稿管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DraftForm()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据表数据
        /// </summary>
        /// <param name="dbCode">连接串编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string dbCode, string tableName)
        {
            var data = await _databaseTableIBLL.GetTableList(dbCode, tableName);
            return Success(data);
        }
        /// <summary>
        /// 获取表的字段数据
        /// </summary>
        /// <param name="dbCode">连接串编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFieldList(string dbCode, string tableName)
        {
            var data = await _databaseTableIBLL.GetTableFiledList(dbCode, tableName);
            return Success(data);
        }
        /// <summary>
        /// 获取表数据
        /// </summary>
        /// <param name="dbCode">连接串编码</param>
        /// <param name="tableName">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="logic">逻辑</param>
        /// <param name="keyword">关键字</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTableDataList(string dbCode, string tableName, string field, string logic, string keyword, string pagination)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _databaseTableIBLL.GetTableDataList(dbCode, tableName, field, logic, keyword, paginationobj);
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
        /// 获取表数据
        /// </summary>
        /// <param name="dbCode">连接串编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTableDataAllList(string dbCode, string tableName)
        {
            var data = await _databaseTableIBLL.GetTableDataList(dbCode, tableName);
            return Success(data);
        }
        /// <summary>
        /// 获取表数据(树形数据)
        /// </summary>
        /// <param name="parentId">连接串主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTreeList(string parentId)
        {
            var data = await _databaseTableIBLL.GetTreeList(parentId);
            return Success(data);
        }
        /// <summary>
        /// 获取表字段树形数据
        /// </summary>
        /// <param name="dbCode">连接串编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public async Task<IActionResult> GetFieldTreeList(string dbCode, string tableName)
        {
            var data = await _databaseTableIBLL.GetFiledTreeList(dbCode, tableName);
            return Success(data);
        }
        /// <summary>
        /// 给定查询语句查询字段
        /// </summary>
        /// <param name="dbCode">连接串编码</param>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSqlColName(string dbCode, string strSql)
        {
            var data = await _databaseTableIBLL.GetSqlColName(dbCode, strSql);
            return Success(data);
        }

        /// <summary>
        /// 获取建表草稿
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetDraftList(string queryJson)
        {
            var data = await _dbDraftIBLL.GetList(queryJson);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存列数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveDraft(string keyValue, DbDraftEntity entity)
        {
            await _dbDraftIBLL.SaveEntity(keyValue, entity);
            return Success("保存草稿成功", entity.F_Id);
        }
        /// <summary>
        /// 保存列数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteDraft(string keyValue)
        {
            await _dbDraftIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除草稿成功");
        }


        /// <summary>
        /// 建表
        /// </summary>
        /// <param name="dbCode">数据库连接编码</param>
        /// <param name="draftId">草稿ID</param>
        /// <param name="tableName">表名</param>
        /// <param name="tableRemark">备注</param>
        /// <param name="strColList">字段列表</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveTable(string dbCode, string draftId, string tableName, string tableRemark, string strColList)
        {
            List<DatabaseTableFieldModel> colList = strColList.ToObject<List<DatabaseTableFieldModel>>();
            string res = await _databaseTableIBLL.CreateTable(dbCode, tableName, tableRemark, colList);
            if (res == "建表成功")
            {
                if (!string.IsNullOrEmpty(draftId))
                {
                    await _dbDraftIBLL.DeleteEntity(draftId);
                }
                return SuccessInfo("创建成功");
            }
            else
            {
                return Fail(res);
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 判断数据表字段重复
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名</param>
        /// <param name="filedsJson">数据字段</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> ExistFiled(string keyValue, string tableName, string keyName, string filedsJson)
        {
            var res = await _databaseTableIBLL.ExistFiled(keyValue, tableName, keyName, filedsJson);
            return Success(res);
        }
        #endregion
    }
}
