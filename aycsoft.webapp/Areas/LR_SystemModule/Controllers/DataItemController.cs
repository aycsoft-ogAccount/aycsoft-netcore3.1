using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.27
    /// 描 述：数据字典
    /// </summary>
    [Area("LR_SystemModule")]
    public class DataItemController : MvcControllerBase
    {
        private readonly DataItemIBLL _dataItemIBLL;

        public DataItemController(DataItemIBLL dataItemIBLL)
        {
            _dataItemIBLL = dataItemIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 数据字典管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 明细管理(表单)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ClassifyIndex()
        {
            return View();
        }
        /// <summary>
        /// 分类管理(表单)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ClassifyForm()
        {
            return View();
        }
        /// <summary>
        /// 明细管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DetailIndex()
        {
            return View();
        }
        #endregion

        #region 字典分类
        /// <summary>
        /// 获取字典分类列表
        /// </summary>
        /// <param name="keyword">关键词（名称/编码）</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetClassifyList(string keyword)
        {
            var data = await _dataItemIBLL.GetClassifyList(keyword, false);
            return this.Success(data);
        }
        /// <summary>
        /// 获取字典分类列表(树结构)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetClassifyTree()
        {
            var data = await _dataItemIBLL.GetClassifyTree();
            return this.Success(data);
        }
        /// <summary>
        /// 保存分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveClassifyForm(string keyValue, DataItemEntity entity)
        {
            await _dataItemIBLL.SaveClassifyEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 删除分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteClassifyForm(string keyValue)
        {
            await _dataItemIBLL.DeleteClassify(keyValue);
            return SuccessInfo("删除成功！");
        }

        #endregion

        #region 字典明细
        /// <summary>
        /// 获取数据字典明显根据分类编号
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <param name="keyword">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetDetailList(string itemCode, string keyword)
        {
            var data = await _dataItemIBLL.GetDetailList(itemCode, keyword);
            return Success(data);
        }
        /// <summary>
        /// 获取数据字典明显树形数据
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetDetailTree(string itemCode)
        {
            var data = await _dataItemIBLL.GetDetailTree(itemCode);
            return Success(data);
        }
        /// <summary>
        /// 保存明细数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemCode">分类编码</param>
        /// <param name="entity">实体</param>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveDetailForm(string keyValue, string itemCode, DataItemDetailEntity entity)
        {
            var data = await _dataItemIBLL.GetClassifyEntityByCode(itemCode);
            entity.F_ItemId = data.F_ItemId;
            await _dataItemIBLL.SaveDetailEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 删除明细数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteDetailForm(string keyValue)
        {
            await _dataItemIBLL.DeleteDetail(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion

        #region 明细数据应用于下拉框
        /// <summary>
        /// 获取数据字典明显根据分类编号
        /// </summary>
        /// <param name="itemCode">分类编码</param>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetDetailListByParentId(string itemCode, string parentId)
        {
            var data = await _dataItemIBLL.GetDetailListByParentId(itemCode, parentId);
            return Success(data);
        }
        #endregion


        /// <summary>
        /// 获取数据字典明显根据分类编号
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCodeList(string itemCode)
        {
            var data = await _dataItemIBLL.GetDetailList(itemCode);
            return Success(data);
        }
    }
}