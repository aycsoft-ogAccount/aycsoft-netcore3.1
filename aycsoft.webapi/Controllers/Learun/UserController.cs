using aycsoft.iapplication;
using aycsoft.operat;
using aycsoft.util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace aycsoft.webapi.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.17
    /// 描 述：用户api
    /// </summary>
    public class UserController : MvcControllerBase
    {
        private readonly UserIBLL _userIBLL;
        private readonly PostIBLL _postIBLL;
        private readonly RoleIBLL _roleIBLL;
        private readonly UserRelationIBLL _userRelationIBLL;
        private readonly IOperator _operator;
        private readonly LogIBLL _logIBLL;
        private readonly ImgIBLL _imgIBLL;
        private readonly DepartmentIBLL _departmentIBLL;

        private readonly IHttpContextAccessor _iHttpContextAccessor;
        /// <summary>
        /// 构造方法，注入依赖项
        /// </summary>
        /// <param name="userIBLL">用户操作接口</param>
        /// <param name="postIBLL">岗位操作接口</param>
        /// <param name="roleIBLL">角色操作接口</param>
        /// <param name="userRelationIBLL">用户关系操作接口</param>
        /// <param name="logIBLL">日志接口</param>
        /// <param name="ioperator">会话操作接口</param>
        /// <param name="imgIBLL">图片操作接口</param>
        /// <param name="iHttpContextAccessor">请求上下文</param>
        /// <param name="departmentIBLL">请求上下文</param>
        public UserController(
            UserIBLL userIBLL,
            PostIBLL postIBLL,
            RoleIBLL roleIBLL,
            UserRelationIBLL userRelationIBLL,
            LogIBLL logIBLL,
            IOperator ioperator,
            ImgIBLL imgIBLL,
            IHttpContextAccessor iHttpContextAccessor,
            DepartmentIBLL departmentIBLL)
        {
            _userIBLL = userIBLL;
            _postIBLL = postIBLL;
            _roleIBLL = roleIBLL;
            _userRelationIBLL = userRelationIBLL;
            _logIBLL = logIBLL;
            _operator = ioperator;
            _imgIBLL = imgIBLL;
            _iHttpContextAccessor = iHttpContextAccessor;
            _departmentIBLL = departmentIBLL;
        }


        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns>登录令牌信息</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]string username, [FromForm]string password)
        {
            #region 写入日志
           LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 1;
            logEntity.F_OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.ApiLogin);
            logEntity.F_OperateAccount = username;
            logEntity.F_OperateUserId = username;
            logEntity.F_IPAddress = _iHttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            logEntity.F_Module = ConfigHelper.GetConfig().SoftName;
            #endregion

            #region 内部账户验证
            UserEntity userEntity = await _userIBLL.GetEntityByAccount(username);
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
                string token = _operator.EncodeToken(userEntity.F_UserId, userEntity.F_RealName, username);
                return Success(token);
            }
            #endregion
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newpassword">新密码</param>
        /// <param name="oldpassword">旧密码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Modifypw([FromForm]string newpassword, [FromForm]string oldpassword)
        {
            var userInfo = await CurrentUser();
            if (userInfo.F_SecurityLevel == 1)
            {
                return Fail("当前账户不能修改密码");
            }
            bool res = await _userIBLL.RevisePassword(newpassword, oldpassword);
            if (!res)
            {
                return Fail("原密码错误，请重新输入");
            }


            return SuccessInfo("密码修改成功，请牢记新密码。\r 将会自动安全退出。");
        }

        /// <summary>
        /// 获取登录者用户信息
        /// </summary>
        /// <returns>登录者用户信息</returns>
        [HttpGet]
        public async Task<IActionResult> Current()
        {
            var data = await CurrentUser();
            data.F_Password = null;
            data.F_Secretkey = null;
            var roleIds = await _userRelationIBLL.GetObjectIds(data.F_UserId, 1);
            var postIds = await _userRelationIBLL.GetObjectIds(data.F_UserId, 2);
            var jsonData = new
            {
                baseinfo = data,
                post = await _postIBLL.GetListByPostIds(postIds),
                role = await _roleIBLL.GetListByRoleIds(roleIds)
            };

            return Success(jsonData);
        }

        /// <summary>
        /// 根据用户 id 获取用户头像
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> HeadImg(string id)
        {
            ImgEntity imgEntity = await _imgIBLL.GetEntity(id);
            if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
            {
                string imgContent = imgEntity.F_Content.Replace("data:image/" + imgEntity.F_ExName.Replace(".", "") + ";base64,", "");
                byte[] arr = Convert.FromBase64String(imgContent);
                return File(arr, "application/octet-stream");
            }
            else
            {
                byte[] arr2 = FileHelper.ReadRoot("/img/header.png");
                return File(arr2, "application/octet-stream");
            }
        }

        /// <summary>
        /// 按 id 获取用户信息
        /// </summary>
        /// <returns>单个用户信息</returns>
        [HttpGet]
        public async Task<IActionResult> Info(string id)
        {
            var user = await _userIBLL.GetEntity(id);
            user.F_Password = null;
            user.F_Secretkey = null;
            return Success(user);
        }

        /// <summary>
        /// 根据传入的 id 获取多个用户信息
        /// </summary>
        /// <returns>多个用户信息的数组</returns>
        [HttpPost]
        public async Task<IActionResult> Infos([FromForm]string ids)
        {
            var users = await _userIBLL.GetListByKeyValues(ids);
            return Success(users);
        }

        /// <summary>
        /// 根据公司/部门/关键字搜索获取用户
        /// </summary>
        /// <param name="companyId">公司 id</param>
        /// <param name="departmentId">部门 id</param>
        /// <param name="keywords">搜索关键字</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ByOrganize(string companyId, string departmentId, string keywords = "")
        {
            var userList = await _userIBLL.GetList(companyId, departmentId, keywords);
            return Success(userList);
        }
    }
}