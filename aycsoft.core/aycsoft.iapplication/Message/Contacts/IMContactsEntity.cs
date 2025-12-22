using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：最近联系人列表
    /// </summary>
    [Table("lr_im_contacts")]
    public class IMContactsEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 发送者ID
        /// </summary>
        public string F_MyUserId { get; set; }
        /// <summary>
        /// 接收者ID
        /// </summary>
        public string F_OtherUserId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string F_Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? F_Time { get; set; }
        /// <summary>
        /// 是否已读1 未读 2 已读
        /// </summary>
        public int? F_IsRead { get; set; }
        #endregion
    }
}
