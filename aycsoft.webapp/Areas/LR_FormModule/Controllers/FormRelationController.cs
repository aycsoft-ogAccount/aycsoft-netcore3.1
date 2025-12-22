using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public class FormRelationController : MvcControllerBase
    {
        private readonly FormRelationIBLL _formRelationIBLL;
        private readonly FormSchemeIBLL _formSchemeIBLL;

        private readonly ModuleIBLL _moduleIBLL;
        private readonly DataAuthorizeIBLL _dataAuthorizeIBLL;

        public FormRelationController(FormRelationIBLL formRelationIBLL, FormSchemeIBLL formSchemeIBLL, ModuleIBLL moduleIBLL, DataAuthorizeIBLL dataAuthorizeIBLL) {
            _formRelationIBLL = formRelationIBLL;
            _formSchemeIBLL = formSchemeIBLL;
            _moduleIBLL = moduleIBLL;
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
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 添加条件查询字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QueryFieldForm()
        {
            return View();
        }
        /// <summary>
        /// 列表设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ColFieldForm()
        {
            return View();
        }

        /// <summary>
        /// 发布的功能页面（主页面）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PreviewIndex(string id)
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
            var data = await _formRelationIBLL.GetPageList(paginationobj, keyword);
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
        /// 获取关系数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var relation =await _formRelationIBLL.GetEntity(keyValue);
            var module =await _moduleIBLL.GetModuleEntity(relation.F_ModuleId);

            var jsonData = new
            {
                relation,
                module
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTree()
        {
            var data = await _formRelationIBLL.GetTree();
            return this.Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="relationJson">对应关系</param>
        /// <param name="moduleJson">模块</param>
        /// <param name="moduleColumnJson">列</param>
        /// <param name="moduleFormJson">表单字段</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, string relationJson, string moduleJson, string moduleColumnJson, string moduleFormJson)
        {
            FormRelationEntity formRelationEntity = relationJson.ToObject<FormRelationEntity>();
            ModuleEntity moduleEntity = moduleJson.ToObject<ModuleEntity>();
            moduleEntity.F_IsMenu = 1;
            moduleEntity.F_EnabledMark = 1;

            var userInfo = await this.CurrentUser();

            if (string.IsNullOrEmpty(keyValue))// 新增
            {
                formRelationEntity.F_Id = Guid.NewGuid().ToString();
                formRelationEntity.F_CreateDate = DateTime.Now;
                formRelationEntity.F_CreateUserId = userInfo.F_UserId;
                formRelationEntity.F_CreateUserName = userInfo.F_RealName;

                moduleEntity.F_Target = "iframe";
                moduleEntity.F_UrlAddress = "/LR_FormModule/FormRelation/PreviewIndex?id=" + formRelationEntity.F_Id;
            }

            List<ModuleButtonEntity> moduleButtonList = new List<ModuleButtonEntity>();
            ModuleButtonEntity addButtonEntity = new ModuleButtonEntity();
            addButtonEntity.F_ModuleButtonId = Guid.NewGuid().ToString();
            addButtonEntity.F_EnCode = "lr_add";
            addButtonEntity.F_FullName = "新增";
            moduleButtonList.Add(addButtonEntity);
            ModuleButtonEntity editButtonEntity = new ModuleButtonEntity();
            editButtonEntity.F_ModuleButtonId = Guid.NewGuid().ToString();
            editButtonEntity.F_EnCode = "lr_edit";
            editButtonEntity.F_FullName = "编辑";
            moduleButtonList.Add(editButtonEntity);
            ModuleButtonEntity deleteButtonEntity = new ModuleButtonEntity();
            deleteButtonEntity.F_ModuleButtonId = Guid.NewGuid().ToString();
            deleteButtonEntity.F_EnCode = "lr_delete";
            deleteButtonEntity.F_FullName = "删除";
            moduleButtonList.Add(deleteButtonEntity);
            ModuleButtonEntity printButtonEntity = new ModuleButtonEntity();
            printButtonEntity.F_ModuleButtonId = Guid.NewGuid().ToString();
            printButtonEntity.F_EnCode = "lr_print";
            printButtonEntity.F_FullName = "打印";
            moduleButtonList.Add(printButtonEntity);

            List<ModuleColumnEntity> moduleColumnList = moduleColumnJson.ToObject<List<ModuleColumnEntity>>();
            List<ModuleFormEntity> moduleFormEntitys = moduleFormJson.ToObject<List<ModuleFormEntity>>();

            await _moduleIBLL.SaveEntity(formRelationEntity.F_ModuleId, moduleEntity, moduleButtonList, moduleColumnList, moduleFormEntitys);
            formRelationEntity.F_ModuleId = moduleEntity.F_ModuleId;
            await _formRelationIBLL.SaveEntity(keyValue, formRelationEntity);

            return Success("保存成功！");
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
            var relation =await _formRelationIBLL.GetEntity(keyValue);
            await _formRelationIBLL.DeleteEntity(keyValue);
            await _moduleIBLL.Delete(relation.F_ModuleId);
            return Success("删除成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取自定义表单设置内容和表单模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetCustmerFormData(string keyValue)
        {
            var relation = await _formRelationIBLL.GetEntity(keyValue);

            var schemeInfoEntity =await _formSchemeIBLL.GetSchemeInfoEntity(relation.F_FormId);
            var schemeEntity =await _formSchemeIBLL.GetSchemeEntity(schemeInfoEntity.F_SchemeId);

            var jsonData = new
            {
                relation,
                schemeInfo = schemeInfoEntity,
                scheme = schemeEntity
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyValue">主键</param>
        /// <param name="queryJson">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPreviewPageList(string pagination, string keyValue, string queryJson)
        {
            var relation =await _formRelationIBLL.GetEntity(keyValue);
            var sql = await _dataAuthorizeIBLL.GetWhereSql(keyValue);
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _formSchemeIBLL.GetFormPageList(relation.F_FormId, paginationobj, queryJson, sql);
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
        /// 获取数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPreviewList(string keyValue, string queryJson)
        {
            //dataAuthorizeIBLL.SetWhereSql(keyValue, true);
            var sql = await _dataAuthorizeIBLL.GetWhereSql(keyValue);
            var relation =await _formRelationIBLL.GetEntity(keyValue);
            var data =await _formSchemeIBLL.GetFormList(relation.F_FormId, queryJson, sql);
            return Success(data);
        }
        #endregion
    }
}