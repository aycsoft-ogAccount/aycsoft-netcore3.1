using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.18
    /// 描 述：行政区域
    /// </summary>
    [Area("LR_SystemModule")]
    public class AreaController : MvcControllerBase
    {
        private readonly AreaIBLL _areaIBLL;

        public AreaController(AreaIBLL areaIBLL)
        {
            _areaIBLL = areaIBLL;
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
        /// </summary>
        /// <param name="parentId">父级主键</param>
        /// <param name="keyword">关键字查询（名称/编号）</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Getlist(string parentId, string keyword)
        {
            var list = await _areaIBLL.GetList(parentId, keyword);
            return Success(list);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTree(string parentId = "0")
        {
            var data = await _areaIBLL.GetTree(parentId);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, AreaEntity entity)
        {

            await _areaIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _areaIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion       
    }
}