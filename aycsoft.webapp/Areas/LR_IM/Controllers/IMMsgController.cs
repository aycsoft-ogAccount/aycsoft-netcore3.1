using System.Threading.Tasks;
using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_IM.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.07
    /// 描 述：即时通讯
    /// </summary>
    [Area("LR_IM")]
    public class IMMsgController : MvcControllerBase
    {
        private readonly IMMsgIBLL _iMMsgIBLL;
        private readonly IMSysUserIBLL _iMSysUserIBLL;
        private readonly IMContactsIBLL _iMContactsIBLL;

        public IMMsgController(IMMsgIBLL iMMsgIBL, IMSysUserIBLL iMSysUserIBLL, IMContactsIBLL iMContactsIBLL)
        {
            _iMMsgIBLL = iMMsgIBL;
            _iMSysUserIBLL = iMSysUserIBLL;
            _iMContactsIBLL = iMContactsIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 聊天记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据(消息的最近10条数据)
        /// </summary>
        /// <param name="userId">对方用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetMsgList(string userId)
        {
            var userInfo = await this.CurrentUser();
            var data = await _iMMsgIBLL.GetList(userInfo.F_UserId, userId);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据(消息列表)
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">对方用户ID</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetMsgPageList(string pagination, string userId, string keyWord)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var userInfo = await this.CurrentUser();
            var data = await _iMMsgIBLL.GetPageList(paginationobj, userInfo.F_UserId, userId, keyWord);
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
        /// 获取最近联系人列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetContactsList()
        {
            var sysUserList = await _iMSysUserIBLL.GetList("");
            var data = await _iMContactsIBLL.GetList(GetUserId());
            var jsonData = new
            {
                data,
                sysUserList
            };

            return Success(jsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="userId">接收方用户id</param>
        /// <param name="content">消息内容</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SendMsg(string userId, string content)
        {
            var userInfo = await this.CurrentUser();
            IMMsgEntity entity = new IMMsgEntity
            {
                F_SendUserId = userInfo.F_UserId,
                F_RecvUserId = userId,
                F_Content = content
            };
            await _iMMsgIBLL.SaveEntity(entity);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 添加一条最近的联系人
        /// </summary>
        /// <param name="otherUserId">对方用户Id</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> AddContact(string otherUserId)
        {
            var userInfo = await this.CurrentUser();
            IMContactsEntity entity = new IMContactsEntity
            {
                F_MyUserId = userInfo.F_UserId,
                F_OtherUserId = otherUserId
            };
            await _iMContactsIBLL.SaveEntity(entity);
            return SuccessInfo("保存成功！");
        }

        /// <summary>
        /// 移除一个最近的联系人
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> RemoveContact(string otherUserId)
        {
            var userInfo = await this.CurrentUser();
            await _iMContactsIBLL.DeleteEntity(userInfo.F_UserId, otherUserId);
            return SuccessInfo("移除成功！");
        }
        /// <summary>
        /// 更新联系人消息读取状态
        /// </summary>
        /// <param name="otherUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpdateContactState(string otherUserId)
        {
            var userInfo = await this.CurrentUser();
            await _iMContactsIBLL.UpdateState(userInfo.F_UserId, otherUserId);
            return SuccessInfo("保存成功！");
        }
        #endregion
    }
}
