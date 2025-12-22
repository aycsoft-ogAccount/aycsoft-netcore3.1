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
    /// 日 期：2022.10.25
    /// 描 述：功能权限设置
    /// </summary>
    [Area("LR_AuthorizeModule")]
    public class AuthorizeController : MvcControllerBase
    {
        private AuthorizeIBLL _authorizeIBLL;

        public AuthorizeController(AuthorizeIBLL authorizeIBLL) {
            _authorizeIBLL = authorizeIBLL;
        }

        #region 获取视图
        /// <summary>
        /// 功能权限设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 移动功能权限设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AppForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取设置信息
        /// </summary>
        /// <param name="objectId">设置对象:角色或人员</param>
        /// <param name="type">类型 1 模块 2 按钮 3 列 4 表单 5 移动端功能</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetData(string objectId,int type)
        {
            var data = await _authorizeIBLL.GetItemIdListByobjectIds(objectId, type);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="objectType">权限分类-1角色2用户</param>
        /// <param name="strFormId">对用功能id</param>
        /// <param name="type">类型 1 模块 2 按钮 3 列 4 表单 5 移动端功能</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string objectId, int objectType, string strFormId,int type)
        {
            if (string.IsNullOrEmpty(strFormId)) {
                strFormId = "";
            }
            string[] formIds = strFormId.Split(',');
            await _authorizeIBLL.SaveAppAuthorize(objectType, objectId, formIds, type);
            return Success("保存成功！");
        }
        #endregion
    }
}
