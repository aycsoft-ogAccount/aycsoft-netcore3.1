using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_OrganizationModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.27
    /// 描 述：角色模块控制器
    /// </summary>
    [Area("LR_OrganizationModule")]
    public class RoleController : MvcControllerBase
    {
        private readonly RoleIBLL _roleIBLL;

        public RoleController(RoleIBLL roleIBLL) {
            _roleIBLL = roleIBLL;
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
        /// 获取角色列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList()
        {
            var data = await _roleIBLL.GetList();
            return Success(data);
        }
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
            var data = await _roleIBLL.GetPageList(paginationobj, keyword);
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
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntity(string keyValue)
        {
            var data = await _roleIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, RoleEntity entity)
        {
            await _roleIBLL.SaveEntity(keyValue, entity);
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
            await _roleIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion
    }
}