using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_OrganizationModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.27
    /// 描 述：公司模块控制器
    /// </summary>
    [Area("LR_OrganizationModule")]
    public class CompanyController : MvcControllerBase
    {
        private readonly CompanyIBLL _companyIBLL;

        public CompanyController(CompanyIBLL companyIBLL)
        {
            _companyIBLL = companyIBLL;
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
        /// 获取公司列表信息
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string keyword)
        {
            var data = await _companyIBLL.GetList(keyword);
            return Success(data);
        }

        /// <summary>
        /// 获取公司列表信息
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetListByPId(string pid)
        {
            var data = await _companyIBLL.GetListByPId(pid);
            return Success(data);
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTree(string parentId)
        {
            var data = await _companyIBLL.GetTree(parentId);
            return Success(data);
        }

        /// <summary>
        /// 获取公司实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntity(string keyValue)
        {
            var data = await _companyIBLL.GetEntity(keyValue);
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
        public async Task<IActionResult> SaveForm(string keyValue, CompanyEntity entity)
        {
            await _companyIBLL.SaveEntity(keyValue, entity);
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
            await _companyIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion
    }
}