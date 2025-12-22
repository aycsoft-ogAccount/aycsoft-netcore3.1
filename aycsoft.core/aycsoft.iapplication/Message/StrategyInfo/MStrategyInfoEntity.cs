using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：消息策略
    /// </summary>
    [Table("lr_ms_strategyinfo")]
    public class MStrategyInfoEntity
    {
        #region 实体成员
        /// <summary>
        /// 策略主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 策略名称
        /// </summary>
        public string F_StrategyName { get; set; }
        /// <summary>
        /// 策略编码
        /// </summary>
        public string F_StrategyCode { get; set; }
        /// <summary>
        /// 发送角色
        /// </summary>
        public string F_SendRole { get; set; }
        /// <summary>
        /// 消息类型1邮箱2微信3短信4系统IM
        /// </summary>
        public string F_MessageType { get; set; }
        /// <summary>
        /// 备注
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

