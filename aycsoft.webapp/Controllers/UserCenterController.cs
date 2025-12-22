using System.Threading.Tasks;
using aycsoft.iapplication;
using aycsoft.operat;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.24
    /// 描 述：个人中心
    /// </summary>
    public class UserCenterController : MvcControllerBase
    {
        private readonly UserIBLL _userIBLL;
        private readonly PostIBLL _postIBLL;
        private readonly RoleIBLL _roleIBLL;
        private readonly UserRelationIBLL _userRelationIBLL;
        private readonly IOperator _operator;

        public UserCenterController(UserIBLL userIBLL, PostIBLL postIBLL, RoleIBLL roleIBLL, UserRelationIBLL userRelationIBLL,IOperator ioperator) {
            _userIBLL = userIBLL;
            _postIBLL = postIBLL;
            _roleIBLL = roleIBLL;
            _userRelationIBLL = userRelationIBLL;
            _operator = ioperator;
        }

        #region 视图功能
        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 联系方式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ContactForm()
        {
            return View();
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HeadForm()
        {
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PassWordForm()
        {
            return View();
        }
        /// <summary>
        /// 个人中心-日志管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LogIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetUserInfo()
        {
            var data =await CurrentUser();
            data.F_Password = null;
            data.F_Secretkey = null;
            var roleIds = await _userRelationIBLL.GetObjectIds(data.F_UserId, 1);
            var postIds = await _userRelationIBLL.GetObjectIds(data.F_UserId, 2);
            var jsonData = new
            {
                baseinfo = data,
                post =await _postIBLL.GetListByPostIds(postIds),
                role =await _roleIBLL.GetListByRoleIds(roleIds)
            };

            return Success(jsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 验证旧密码
        /// </summary>
        /// <param name="OldPassword"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> ValidationOldPassword(string OldPassword)
        {
            var userInfo = await CurrentUser();
            OldPassword = Md5Helper.Encrypt(DESEncrypt.Encrypt(OldPassword, userInfo.F_Secretkey).ToLower(), 32).ToLower();
            if (OldPassword != userInfo.F_Password)
            {
                return Fail("原密码错误，请重新输入");
            }
            else
            {
                return SuccessInfo("通过信息验证");
            }
        }
        /// <summary>
        /// 提交修改密码
        /// </summary>
        /// <param name="password">新密码</param>
        /// <param name="oldPassword">旧密码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SubmitResetPassword(string password, string oldPassword)
        {
            var userInfo = await CurrentUser();
            if (userInfo.F_SecurityLevel == 1)
            {
                return Fail("当前账户不能修改密码");
            }
            bool res =await _userIBLL.RevisePassword(password, oldPassword);
            if (!res)
            {
                return Fail("原密码错误，请重新输入");
            }

            return SuccessInfo("密码修改成功，请牢记新密码。\r 将会自动安全退出。");
        }
        #endregion
    }
}
