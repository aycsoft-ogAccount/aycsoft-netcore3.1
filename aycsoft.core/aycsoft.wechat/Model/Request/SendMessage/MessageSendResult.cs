using System;
namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：如果对应用或收件人、部门、标签任何一个无权限，则本次发送失败；如果收件人、部门或标签不存在，发送仍然执行，但返回无效的部分
    /// </summary>
    public class MessageSendResult : OperationResultsBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string invaliduser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invalidparty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invalidtag { get; set; }
    }
}
