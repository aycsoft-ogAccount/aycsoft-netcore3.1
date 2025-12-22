using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_FormModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.02.26
    /// 描 述：自定义表单
    /// </summary>
    [Area("LR_FormModule")]
    public class CustmerformController : MvcControllerBase
    {
        private readonly FormSchemeIBLL _formSchemeIBLL;

        public CustmerformController(FormSchemeIBLL formSchemeIBLL)
        {
            _formSchemeIBLL = formSchemeIBLL;
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
        /// 表单设计页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 表单预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PreviewForm()
        {
            return View();
        }
        /// <summary>
        /// 表单模板历史记录查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HistoryForm()
        {
            return View();
        }
        /// <summary>
        /// 数据库表增改
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DataTableForm()
        {
            return View();
        }

        /// <summary>
        /// 自定义表单弹层实例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LayerInstanceForm()
        {
            return View();
        }

        /// <summary>
        /// 自定义表单窗口页实例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TabInstanceForm()
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
        /// <param name="category">分类</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string keyword, string category)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _formSchemeIBLL.GetSchemeInfoPageList(paginationobj, keyword, category);
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
        /// 获取模板分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">自定义表单信息主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetSchemePageList(string pagination, string schemeInfoId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _formSchemeIBLL.GetSchemePageList(paginationobj, schemeInfoId);
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
        /// 获取设计表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var schemeInfoEntity =await _formSchemeIBLL.GetSchemeInfoEntity(keyValue);
            var schemeEntity =await _formSchemeIBLL.GetSchemeEntity(schemeInfoEntity.F_SchemeId);
            var jsonData = new
            {
                schemeInfoEntity,
                schemeEntity
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取自定义表单模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetSchemeEntity(string keyValue)
        {
            var data =await _formSchemeIBLL.GetSchemeEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 获取自定义表单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetSchemeInfoList()
        {
            var data =await _formSchemeIBLL.GetCustmerSchemeInfoList();
            return Success(data);
        }

        /// <summary>
        /// 获取自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="processIdName">流程关联字段名</param>
        /// <param name="keyValue">数据主键值</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetInstanceForm(string schemeInfoId, string processIdName, string keyValue)
        {
            if (string.IsNullOrEmpty(processIdName))
            {
                var data =await _formSchemeIBLL.GetInstanceForm(schemeInfoId, keyValue);
                return Success(data);
            }
            else
            {
                var data =await _formSchemeIBLL.GetInstanceForm(schemeInfoId, processIdName, keyValue);
                return Success(data);
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfo">表单设计模板信息</param>
        /// <param name="scheme">模板内容</param>
        /// <param name="type">类型1.正式2.草稿</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, string schemeInfo, string scheme, int type)
        {
            FormSchemeInfoEntity schemeInfoEntity = schemeInfo.ToObject<FormSchemeInfoEntity>();
            FormSchemeEntity schemeEntity = new FormSchemeEntity();
            schemeEntity.F_Scheme = scheme;
            schemeEntity.F_Type = type;

            await _formSchemeIBLL.SaveEntity(keyValue, schemeInfoEntity, schemeEntity);
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
            await _formSchemeIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 更新表单模板版本
        /// </summary>
        /// <param name="schemeInfoId">信息主键</param>
        /// <param name="schemeId">模板主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpdateScheme(string schemeInfoId, string schemeId)
        {
            await _formSchemeIBLL.UpdateScheme(schemeInfoId, schemeId);
            return SuccessInfo("更新成功！");
        }
        /// <summary>
        /// 启用/停用表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpDateSate(string keyValue, int state)
        {
            await _formSchemeIBLL.UpdateState(keyValue, state);
            return SuccessInfo((state == 1 ? "启用" : "禁用") + "成功！");
        }

        /// <summary>
        /// 保存自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="processIdName">流程关联字段名</param>
        /// <param name="keyValue">数据主键值</param>
        /// <param name="formData">自定义表单数据</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveInstanceForm(string schemeInfoId, string processIdName, string keyValue, string formData)
        {
            await _formSchemeIBLL.SaveInstanceForm(schemeInfoId, processIdName, keyValue, formData);
            return SuccessInfo("保存成功！");
        }

        /// <summary>
        /// 删除自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="keyValue">数据主键值</param>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteInstanceForm(string schemeInfoId, string keyValue)
        {
            await _formSchemeIBLL.DeleteInstanceForm(schemeInfoId, keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion
    }
}