using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_OrganizationModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.17
    /// 描 述：用户模块控制器
    /// </summary>
    [Area("LR_OrganizationModule")]
    public class UserController : MvcControllerBase
    {
        private readonly UserIBLL _userIBLL;
        private readonly ImgIBLL _imgIBLL;

        public UserController(UserIBLL userIBLL, ImgIBLL imgIBLL) {
            _userIBLL = userIBLL;
            _imgIBLL = imgIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 用户管理主页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 用户管理表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

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
        public IActionResult SelectOnlyForm()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string keyword, string companyId, string departmentId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await  _userIBLL.GetPageList(companyId, departmentId, paginationobj, keyword);
            var jsonData = new
            {
                rows = data,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string companyId, string departmentId, string keyword)
        {
            var data = await _userIBLL.GetList(companyId, departmentId, keyword);
            return Success(data);
        }

        /// <summary>
        /// 获取全部用户数据
        /// </summary>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetAllList(string keyword)
        {
            var data = await _userIBLL.GetAllList(keyword);
            return Success(data);
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="userIds">用户主键串</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntityListByUserIds(string userIds)
        {
            var list =await _userIBLL.GetListByKeyValues(userIds);
            return Success(list);
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="keyValue">用户主键串</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetListByUserIds(string keyValue)
        {
            var list = await _userIBLL.GetListByKeyValues(keyValue);
            string text = "";
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text += ",";
                }
                text += item.F_RealName;
            }
            return Success(text);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetUserEntity(string userId)
        {
            var data = await _userIBLL.GetEntity(userId);
            return Success(data);
        }

        
        /// <summary>
        /// 获取头像文件
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> HeadImg(string account)
        {

            ImgEntity imgEntity = await _imgIBLL.GetEntity(account);
            if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
            {
                string imgContent = imgEntity.F_Content.Replace("data:image/" + imgEntity.F_ExName.Replace(".", "") + ";base64,", "");
                byte[] arr = Convert.FromBase64String(imgContent);
                return File(arr, "application/octet-stream");
            }
            else {
                byte[] arr2 = FileHelper.ReadRoot("/img/header.png");
                return File(arr2, "application/octet-stream");
            }
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
        public async Task<IActionResult> SaveForm(string keyValue, UserEntity entity)
        {
            try
            {
                await _userIBLL.SaveEntity(keyValue, entity);
                return SuccessInfo("保存成功！");
            }
            catch (Exception)
            {
                return Fail("账号不能重复");
            }

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
            await _userIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 启用禁用账号
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpdateState(string keyValue, int state)
        {
            await _userIBLL.UpdateState(keyValue, state);
            return SuccessInfo("操作成功！");
        }
        /// <summary>
        /// 重置用户账号密码
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> ResetPassword(string keyValue)
        {
            await _userIBLL.ResetPassword(keyValue);
            return SuccessInfo("操作成功！");
        }
        #endregion

        #region 数据导出
        /// <summary>
        /// 导出用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExportUserList()
        {
            return  File(await _userIBLL.GetExportList(), "application/ms-excel","用户导出.xls");
        }
        #endregion
    }
}