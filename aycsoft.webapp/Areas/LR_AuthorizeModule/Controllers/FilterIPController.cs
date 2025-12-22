using System.Threading.Tasks;
using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_AuthorizeModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.30
    /// 描 述：IP过滤
    /// </summary>
    [Area("LR_AuthorizeModule")]
    public class FilterIPController : MvcControllerBase
    {
        private readonly FilterIPIBLL _filterIPIBLL;

        public FilterIPController(FilterIPIBLL filterIPIBLL) {
            _filterIPIBLL = filterIPIBLL;
        }


        #region 视图功能
        /// <summary>
        /// 过滤IP管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 过滤IP表单
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
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string objectId, string visitType)
        {
            var data = await _filterIPIBLL.GetList(objectId, visitType);
            return Success(data);
        }
        /// <summary>
        /// 过滤IP实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var data = await _filterIPIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存过滤IP表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterIPEntity">过滤IP实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, FilterIPEntity filterIPEntity)
        {
            await _filterIPIBLL.SaveForm(keyValue, filterIPEntity);
            return SuccessInfo("操作成功。");
        }
        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _filterIPIBLL.DeleteEntiy(keyValue);
            return SuccessInfo("删除成功。");
        }
        #endregion
    }
}
