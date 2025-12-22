using System.Threading.Tasks;
using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_AuthorizeModule
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.25
    /// 描 述：用户对应对象
    /// </summary>
    [Area("LR_AuthorizeModule")]
    public class UserRelationController : MvcControllerBase
    {
        private readonly UserRelationIBLL _userRelationIBLL;
        private readonly UserIBLL _userIBLL;

        public UserRelationController(UserRelationIBLL userRelationIBLL, UserIBLL userIBLL) {
            _userRelationIBLL = userRelationIBLL;
            _userIBLL = userIBLL;
        }

        #region 获取视图
        /// <summary>
        /// 人员选择
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SelectForm()
        {
            return View();
        }
        /// <summary>
        /// 人员选择
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LookForm()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取用户主键列表信息
        /// </summary>
        /// <param name="objectId">用户主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetUserIdList(string objectId)
        {
            var data =await _userRelationIBLL.GetUserIdList(objectId);
            string userIds = "";
            foreach (var item in data)
            {
                if (userIds != "")
                {
                    userIds += ",";
                }
                userIds += item.F_UserId;
            }
            var userList = await _userIBLL.GetListByKeyValues(userIds);
            var datajson = new
            {
                userIds,
                userInfoList = userList
            };
            return Success(datajson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="objectId">对象主键</param>
        /// <param name="category">分类:1-角色2-岗位</param>
        /// <param name="userIds">对用户主键列表</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string objectId, int category, string userIds)
        {
            await _userRelationIBLL.SaveEntityList(objectId, category, userIds);
            return SuccessInfo("保存成功！");
        }
        #endregion
    }
}
