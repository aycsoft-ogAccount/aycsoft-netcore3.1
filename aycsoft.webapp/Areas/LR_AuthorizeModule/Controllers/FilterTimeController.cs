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
    /// 描 述：时间过滤
    /// </summary>
    [Area("LR_AuthorizeModule")]
    public class FilterTimeController : MvcControllerBase
    {
        private FilterTimeIBLL _filterTimeIBLL;

        public FilterTimeController(FilterTimeIBLL filterTimeIBLL) {
            _filterTimeIBLL = filterTimeIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 过滤时段表单
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
        /// 过滤时段实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var data = await _filterTimeIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存过滤时段表单（新增、修改）
        /// </summary>
        /// <param name="filterTimeEntity">过滤时段实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(FilterTimeEntity filterTimeEntity)
        {
            await _filterTimeIBLL.SaveForm(filterTimeEntity);
            return SuccessInfo("操作成功。");
        }
        /// <summary>
        /// 删除过滤时段
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _filterTimeIBLL.DeleteEntiy(keyValue);
            return Success("删除成功。");
        }
        #endregion
    }
}
