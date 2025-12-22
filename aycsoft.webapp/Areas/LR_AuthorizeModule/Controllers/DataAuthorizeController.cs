using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_AuthorizeModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.25
    /// 描 述：数据权限
    /// </summary>
    [Area("LR_AuthorizeModule")]
    public class DataAuthorizeController : MvcControllerBase
    {
        private readonly DataAuthorizeIBLL _dataAuthorizeIBLL;

        public DataAuthorizeController(DataAuthorizeIBLL dataAuthorizeIBLL)
        {
            _dataAuthorizeIBLL = dataAuthorizeIBLL;
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
        /// 自定义表单数据权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CustomerFormIndex()
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


        /// <summary>
        /// 自定义表单权限设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CustomerForm()
        {
            return View();
        }

        /// <summary>
        /// 添加条件表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryForm()
        {
            return View();
        }
        #endregion


        #region 获取数据
        /// <summary>
        /// 获取数据权限对应关系数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <param name="objectId">用户或角色主键</param>
        /// <param name="type">对象主键</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPageList(string pagination, string keyword, string objectId, int type)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _dataAuthorizeIBLL.GetPageList(paginationobj, keyword, objectId, type);
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
        /// 获取条件列表数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntity(string keyValue)
        {
            var data = await _dataAuthorizeIBLL.GetEntity(keyValue);
            return Success(data);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, DataAuthorizeEntity entity)
        {

            await _dataAuthorizeIBLL.SaveEntity(keyValue, entity);
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
            await _dataAuthorizeIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion
    }
}
