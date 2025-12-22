using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace aycsoft.webapi.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.22
    /// 描 述：即时通讯
    /// </summary>
    public class IMController : MvcControllerBase
    {
        private readonly IMSysUserIBLL _iMSysUserIBLL;
        private readonly IMMsgIBLL _iMMsgIBLL;
        private readonly IMContactsIBLL _iMContactsIBLL;

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="iMSysUserIBLL">即时通讯系统用户</param>
        /// <param name="iMMsgIBLL">消息方法</param>
        /// <param name="iMContactsIBLL">联系人方法</param>
        public IMController(IMSysUserIBLL iMSysUserIBLL, IMMsgIBLL iMMsgIBLL, IMContactsIBLL iMContactsIBLL)
        {

            _iMSysUserIBLL = iMSysUserIBLL;
            _iMMsgIBLL = iMMsgIBLL;
            _iMContactsIBLL = iMContactsIBLL;
        }

        /// <summary>
        /// 获取最近联系人列表
        /// </summary>
        /// <param name="time">开始时间</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> contacts(string time)
        {
            var sysUserList = await _iMSysUserIBLL.GetList("");
            DateTime beginTime = DateTime.Now;
            var data = await _iMContactsIBLL.GetList(this.GetUserId(), DateTime.Parse(time));
            var jsondata = new
            {
                data,
                sysUserList,
                time = beginTime
            };
            return Success(jsondata);
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="userId">接收人主键</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Send([FromForm]string userId, [FromForm]string content)
        {
            IMMsgEntity entity = new IMMsgEntity();
            entity.F_SendUserId = this.GetUserId();
            entity.F_RecvUserId = userId;
            entity.F_Content = content;
            await _iMMsgIBLL.SaveEntity(entity);
            // 向即时消息服务器发送一条信息
            await SendHubs.callMethod("sendMsg2", this.GetUserId(), userId, content, 0);

            var jsonData = new
            {
                time = entity.F_CreateDate,
                msgId = entity.F_MsgId
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 添加一条最近的联系人
        /// </summary>
        /// <param name="otherUserId">联系人</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddContact([FromForm]string otherUserId)
        {
            IMContactsEntity entity = new IMContactsEntity();
            entity.F_MyUserId = this.GetUserId();
            entity.F_OtherUserId = otherUserId;
            await _iMContactsIBLL.SaveEntity(entity);
            return SuccessInfo("添加成功！");
        }
        /// <summary>
        /// 更新消息读取状态
        /// </summary>
        /// <param name="otherUserId">消息发送者ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update([FromForm]string otherUserId)
        {
            await _iMContactsIBLL.UpdateState(this.GetUserId(), otherUserId);
            return SuccessInfo("更新成功！");
        }

        /// <summary>
        /// 获取最近10条聊天记录
        /// </summary>
        /// <param name="otherUserId">消息发送者ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LastMsg(string otherUserId)
        {
            var data = await _iMMsgIBLL.GetList(this.GetUserId(), otherUserId);
            return Success(data);
        }

        /// <summary>
        /// 获取小于某时间点的5条记录
        /// </summary>
        /// <param name="otherUserId">消息交谈方Id</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> MsgList(string otherUserId, DateTime time)
        {
            var data = await _iMMsgIBLL.GetListByTime(this.GetUserId(), otherUserId, time);
            return Success(data);
        }
        /// <summary>
        /// 获取大于某时间点的所有数据
        /// </summary>
        /// <param name="otherUserId">消息交谈方Id</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> MsgList2(string otherUserId, DateTime time)
        {
            var data = await _iMMsgIBLL.GetListByTime2(this.GetUserId(), otherUserId, time);
            return Success(data);
        }
    }
}