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
    /// 描 述：岗位模块控制器
    /// </summary>
    [Area("LR_OrganizationModule")]
    public class PostController : MvcControllerBase
    {
        private readonly PostIBLL _postIBLL;

        public PostController(PostIBLL postIBLL) {
            _postIBLL = postIBLL;
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
        /// <summary>
        /// 岗位选择页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SelectForm()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取岗位列表信息
        /// </summary>
        /// <param name="companyId">公司</param>
        /// <param name="departmentId">部门</param>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string companyId, string departmentId, string keyword)
        {
            var data = await _postIBLL.GetList(companyId, departmentId, keyword);
            return Success(data);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTree(string companyId)
        {
            var data = await _postIBLL.GetTree(companyId);
            return Success(data);
        }
        /// <summary>
        /// 获取岗位名称
        /// </summary>
        /// <param name="keyValue">岗位主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntityName(string keyValue)
        {
            if (keyValue == "0")
            {
                return Success("");
            }
            var data = await _postIBLL.GetEntity(keyValue);
            return Success(data.F_Name);
        }
        /// <summary>
        /// 获取岗位实体数据
        /// </summary>
        /// <param name="keyValue">岗位主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntity(string keyValue)
        {
            var data =await _postIBLL.GetEntity(keyValue);
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
        public async Task<IActionResult> SaveForm(string keyValue, PostEntity entity)
        {
            await _postIBLL.SaveEntity(keyValue, entity);
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
            await _postIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion
    }
}