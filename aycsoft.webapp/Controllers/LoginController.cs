using aycsoft.iapplication;
using aycsoft.operat;
using aycsoft.util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.11
    /// 描 述：登录模块控制器
    /// </summary>
    public class LoginController : MvcControllerBase
    {
        private readonly UserIBLL _userIBLL;
        private readonly IOperator _operator;
        private readonly LogIBLL _logIBLL;
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public LoginController(UserIBLL userIBLL, LogIBLL logIBLL, IOperator ioperator, IHttpContextAccessor iHttpContextAccessor)
        {
            _userIBLL = userIBLL;
            _logIBLL = logIBLL;
            _operator = ioperator;
            _iHttpContextAccessor = iHttpContextAccessor;
        }

        #region 视图功能
        /// <summary>
        /// 页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetUserInfo()
        {
            var entity = await _userIBLL.GetEntity();
            entity.F_Password = null;
            entity.F_Secretkey = null;
            return Success(entity);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [AllowAnonymous]
        public async Task<IActionResult> CheckLogin(string account, string password)
        {
            #region 写入日志
            LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 1;
            logEntity.F_OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.F_OperateAccount = account;
            logEntity.F_OperateUserId = account;
            logEntity.F_IPAddress = _iHttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            logEntity.F_Module = ConfigHelper.GetConfig().SoftName;
            #endregion

            #region 内部账户验证
            UserEntity userEntity = await _userIBLL.GetEntityByAccount(account);
            if (userEntity == null)
            {
                logEntity.F_ExecuteResult = 0;
                logEntity.F_ExecuteResultJson = "没有此账号!";
                await _logIBLL.Write(logEntity);
                return Fail("账号密码不匹配");
            }

            if (userEntity.F_EnabledMark != 1)
            {
                logEntity.F_ExecuteResult = 0;
                logEntity.F_ExecuteResultJson = "账户被系统锁定,请联系管理员!";
                await _logIBLL.Write(logEntity);
                return Fail("账户被系统锁定,请联系管理员!");
            }
            bool isOk = _userIBLL.IsPasswordOk(userEntity.F_Password, password, userEntity.F_Secretkey);


            if (!isOk)//登录失败
            {
                logEntity.F_ExecuteResult = 0;
                logEntity.F_ExecuteResultJson = "账号密码不匹配";
                await _logIBLL.Write(logEntity);
                return Fail("账号密码不匹配");
            }
            else
            {
                logEntity.F_ExecuteResult = 1;
                logEntity.F_ExecuteResultJson = "登录成功";
                await _logIBLL.Write(logEntity);
                string token = _operator.EncodeToken(userEntity.F_UserId, userEntity.F_RealName,account);
                return Success(token);
            }
            #endregion
        }
        #endregion
    }
}