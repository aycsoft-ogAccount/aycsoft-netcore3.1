using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：即时通讯消息内容
    /// </summary>
    [Table("lr_im_msg")]
    public class IMMsgEntity
    {
        #region 实体成员
        /// <summary>
        /// 消息主键
        /// </summary>
        [Key]
        public string F_MsgId { get; set; }
        /// <summary>
        /// 发送者ID
        /// </summary>
        public string F_SendUserId { get; set; }
        /// <summary>
        /// 接收者ID
        /// </summary>
        public string F_RecvUserId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string F_Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 是否是系统消息
        /// </summary>
        public int? F_IsSystem { get; set; }
        #endregion
    }
}
