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
    /// 描 述：部门模块控制器
    /// </summary>
    [Area("LR_OrganizationModule")]
    public class DepartmentController : MvcControllerBase
    {
        private readonly DepartmentIBLL _departmentIBLL;
        private readonly CompanyIBLL _companyIBLL;

        public DepartmentController(DepartmentIBLL departmentIBLL, CompanyIBLL companyIBLL) {
            _departmentIBLL = departmentIBLL;
            _companyIBLL = companyIBLL;
        }

        #region 获取视图
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
        /// 表单
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
        /// 获取部门列表信息(根据公司Id)
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string companyId, string keyword)
        {
            var data = await _departmentIBLL.GetList(companyId, keyword);
            return Success(data);
        }

        /// <summary>
        /// 获取部门列表信息(根据公司Id 和 父级 id)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="pid">父级 id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetListByPid(string companyId, string pid) {
            var data = await _departmentIBLL.GetListByPid(companyId, pid);
            return Success(data);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="companyId">公司id</param>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTree(string companyId, string parentId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                var companylist = await _companyIBLL.GetList();
                var data = await _departmentIBLL.GetTree(companylist);
                return Success(data);
            }
            else
            {
                var data = await _departmentIBLL.GetTree(companyId, parentId);
                return Success(data);
            }
        }
        /// <summary>
        /// 获取部门实体数据
        /// </summary>
        /// <param name="departmentId">部门主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntity(string departmentId)
        {
            var data =await _departmentIBLL.GetEntity(departmentId);
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
        public async Task<IActionResult> SaveForm(string keyValue, DepartmentEntity entity)
        {
            await _departmentIBLL.SaveEntity(keyValue, entity);
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
            await _departmentIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion
    }
}