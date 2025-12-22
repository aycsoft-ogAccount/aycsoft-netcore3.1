using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.11
    /// 描 述：功能模块控制器
    /// </summary>
    [Area("LR_SystemModule")]
    public class ModuleController : MvcControllerBase
    {
        private readonly ModuleIBLL _moduleIBLL;

        public ModuleController(ModuleIBLL moduleIBLL)
        {
            _moduleIBLL = moduleIBLL;
        }

        #region 视图方法
        /// <summary>
        /// 主页
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

        #region 功能模块
        /// <summary>
        /// 获取功能模块数据列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetModuleList()
        {
            var data = await _moduleIBLL.GetModuleList();
            return this.Success(data);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetModuleTree()
        {
            var data = await _moduleIBLL.GetModuleTree();
            return this.Success(data);
        }
        /// <summary>
        /// 获取树形数据(带勾选框)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetModuleCheckTree()
        {
            var data = await _moduleIBLL.GetModuleCheckTree();
            return this.Success(data);
        }
        /// <summary>
        /// 获取功能列表的树形数据(只有展开项)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetExpendModuleTree()
        {
            var data = await _moduleIBLL.GetExpendModuleTree();
            return this.Success(data);
        }
        /// <summary>
        /// 获取列表数据根据父级id
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="parentId">功能类型</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetModuleListByParentId(string keyword, string parentId)
        {
            var jsondata = await _moduleIBLL.GetModuleListByParentId(keyword, parentId);
            return this.Success(jsondata);
        }

        /// <summary>
        /// 获取树形数据(带勾选框)
        /// </summary>
        /// <param name="type">1 模块 2 按钮 3 列 4 表单</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetCheckTree(string type, string objectId)
        {
            var list = type switch
            {
                "1" => await _moduleIBLL.GetModuleCheckTree(),
                "2" => await _moduleIBLL.GetButtonCheckTree(objectId),
                "3" => await _moduleIBLL.GetColumnCheckTree(objectId),
                "4" => await _moduleIBLL.GetFormCheckTree(objectId),
                _ => await _moduleIBLL.GetModuleCheckTree(),
            };
            return this.Success(list);
        }
        #endregion

        #region 模块按钮
        /// <summary>
        /// 获取功能模块按钮数据列表
        /// </summary>
        /// <param name="moduleId">模块主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetButtonListNoAuthorize(string moduleId)
        {
            var data = await _moduleIBLL.GetButtonListNoAuthorize(moduleId);
            return this.Success(data);
        }
        /// <summary>
        /// 获取功能模块按钮数据列表
        /// </summary>
        /// <param name="moduleId">模块主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetButtonList(string moduleId)
        {
            var data = await _moduleIBLL.GetButtonList(moduleId);
            return this.Success(data);
        }
        #endregion

        #region 模块视图
        /// <summary>
        /// 获取功能模块视图数据列表
        /// </summary>
        /// <param name="moduleId">模块主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetColumnList(string moduleId)
        {
            var data = await _moduleIBLL.GetColumnList(moduleId);
            return this.Success(data);
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var module = await _moduleIBLL.GetModuleEntity(keyValue);
            var btns = await _moduleIBLL.GetButtonList(keyValue);
            var cols = await _moduleIBLL.GetColumnList(keyValue);
            var fields = await _moduleIBLL.GetFormList(keyValue);
            var jsondata = new
            {
                moduleEntity = module,
                moduleButtons = btns,
                moduleColumns = cols,
                moduleFields = fields
            };
            return this.Success(jsondata);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntityJson">功能实体</param>
        /// <param name="moduleButtonListJson">按钮实体列表</param>
        /// <param name="moduleColumnListJson">视图实体列表</param>
        /// <param name="moduleFormListJson">表单字段列表</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, string moduleEntityJson, string moduleButtonListJson, string moduleColumnListJson, string moduleFormListJson)
        {
            var moduleButtonList = moduleButtonListJson.ToList<ModuleButtonEntity>();
            var moduleColumnList = moduleColumnListJson.ToList<ModuleColumnEntity>();
            var moduleFormList = moduleFormListJson.ToList<ModuleFormEntity>();
            var moduleEntity = moduleEntityJson.ToObject<ModuleEntity>();

            await _moduleIBLL.SaveEntity(keyValue, moduleEntity, moduleButtonList, moduleColumnList, moduleFormList);
            return SuccessInfo("保存成功。");
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
            bool res = await _moduleIBLL.Delete(keyValue);
            if (res)
            {
                return SuccessInfo("删除成功。");
            }
            else
            {
                return Fail("有子节点无法删除。");
            }
        }
        #endregion

        #region 权限数据
        /// <summary>
        /// 获取权限按钮和列表信息
        /// </summary>
        /// <param name="code">菜单对应编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetAuthorizeButtonColumnList(string code)
        {
            Dictionary<string, string> dicButton = new Dictionary<string, string>();
            Dictionary<string, string> dicColumn = new Dictionary<string, string>();
            Dictionary<string, string> dicForm = new Dictionary<string, string>();

            ModuleEntity moduleEntity = await _moduleIBLL.GetEntityByCode(code);

            if (moduleEntity != null)
            {
                List<ModuleButtonEntity> buttonList = (List<ModuleButtonEntity>)await _moduleIBLL.GetButtonList(moduleEntity.F_ModuleId);
                foreach (var item in buttonList)
                {
                    if (!dicButton.ContainsKey(item.F_EnCode))
                    {
                        dicButton.Add(item.F_EnCode, item.F_FullName);
                    }
                }
                List<ModuleColumnEntity> columnList = (List<ModuleColumnEntity>)await _moduleIBLL.GetColumnList(moduleEntity.F_ModuleId);
                foreach (var item in columnList)
                {
                    if (!dicColumn.ContainsKey(item.F_EnCode.ToLower()))
                    {
                        dicColumn.Add(item.F_EnCode.ToLower(), item.F_FullName);
                    }
                }
                List<ModuleFormEntity> formList = (List<ModuleFormEntity>)await _moduleIBLL.GetFormList(moduleEntity.F_ModuleId);
                foreach (var item in formList)
                {
                    if (item.F_EnCode != null && !dicForm.ContainsKey(item.F_EnCode))
                    {
                        dicForm.Add(item.F_EnCode, item.F_FullName);
                    }
                }
            }
            var jsonData = new
            {
                module = moduleEntity,
                btns = dicButton,
                cols = dicColumn,
                forms = dicForm
            };
            return Success(jsonData);
        }
        #endregion
    }
}