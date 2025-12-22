using System;
using cd.dapper.extension;
namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.14
    /// 描 述：看板信息
    /// </summary>
    [Table("lr_kbkanbaninfo")]
    public class LR_KBKanBanInfoEntity
    {
        #region 实体成员
        /// <summary>
        /// 看板主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 看板名称
        /// </summary>
        public string F_KanBanName { get; set; }
        /// <summary>
        /// 看板编号
        /// </summary>
        public string F_KanBanCode { get; set; }
        /// <summary>
        /// 刷新时间（分钟）
        /// </summary>
        public int? F_RefreshTime { get; set; }
        /// <summary>
        /// 模板id
        /// </summary>
        public string F_TemplateId { get; set; }
        /// <summary>
        /// 看板说明
        /// </summary>
        public string F_Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        public string F_ModifyUserName { get; set; }
        #endregion
    }
}

