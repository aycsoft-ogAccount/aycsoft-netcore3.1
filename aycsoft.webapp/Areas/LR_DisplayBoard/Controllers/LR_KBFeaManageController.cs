using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_DisplayBoard.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：看板发布
    /// </summary>
    [Area("LR_DisplayBoard")]
    public class LR_KBFeaManageController : MvcControllerBase
    {
        private readonly LR_KBFeaManageIBLL _lR_KBFeaManageIBLL;
        private readonly ModuleIBLL _moduleIBLL;

        public LR_KBFeaManageController(LR_KBFeaManageIBLL lR_KBFeaManageIBLL, ModuleIBLL moduleIBLL) {
            _lR_KBFeaManageIBLL = lR_KBFeaManageIBLL;
            _moduleIBLL = moduleIBLL;
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
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string queryJson)
        {
            var data = await _lR_KBFeaManageIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _lR_KBFeaManageIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var data = await _lR_KBFeaManageIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            string moduleId =(await _lR_KBFeaManageIBLL.GetEntity(keyValue)).F_ModuleId;
            await _lR_KBFeaManageIBLL.DeleteEntity(keyValue);
            await _moduleIBLL.Delete(moduleId);

            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, LR_KBFeaManageEntity entity)
        {
            ModuleEntity moduleEntity = new ModuleEntity();
            if (string.IsNullOrEmpty(keyValue))// 新增
            {
                moduleEntity.F_Target = "iframe";
            }
            moduleEntity.F_UrlAddress = "/LR_DisplayBoard/LR_KBKanBanInfo/PreviewForm?keyValue=" + entity.F_KanBanId;
            moduleEntity.F_ModuleId = entity.F_ModuleId;
            moduleEntity.F_ParentId = entity.F_ParentId;
            moduleEntity.F_Icon = entity.F_Icon;
            moduleEntity.F_FullName = entity.F_FullName;
            moduleEntity.F_EnCode = entity.F_EnCode;
            moduleEntity.F_SortCode = entity.F_SortCode;
            moduleEntity.F_IsMenu = 1;
            moduleEntity.F_EnabledMark = 1;
            List<ModuleButtonEntity> moduleButtonList = new List<ModuleButtonEntity>();
            ModuleButtonEntity addButtonEntity = new ModuleButtonEntity();
            List<ModuleColumnEntity> moduleColumnList = new List<ModuleColumnEntity>();
            List<ModuleFormEntity> moduleFormEntitys = new List<ModuleFormEntity>();
            await _moduleIBLL.SaveEntity(moduleEntity.F_ModuleId, moduleEntity, moduleButtonList, moduleColumnList, moduleFormEntitys);

            entity.F_ModuleId = moduleEntity.F_ModuleId;
            await _lR_KBFeaManageIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }
        #endregion

    }
}