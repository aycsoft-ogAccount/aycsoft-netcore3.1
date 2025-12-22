using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流委托规则(新)
    /// </summary>
    [Table("lr_nwf_delegaterule")]
    public class NWFDelegateRuleEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 被委托人Id 
        /// </summary> 
        public string F_ToUserId { get; set; }
        /// <summary> 
        /// 被委托人名称 
        /// </summary> 
        public string F_ToUserName { get; set; }
        /// <summary> 
        /// 委托开始时间 
        /// </summary> 
        public DateTime? F_BeginDate { get; set; }
        /// <summary> 
        /// 委托结束时间 
        /// </summary> 
        public DateTime? F_EndDate { get; set; }
        /// <summary> 
        /// 委托人Id 
        /// </summary> 
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 委托人名称 
        /// </summary> 
        public string F_CreateUserName { get; set; }
        /// <summary> 
        /// 创建时间 
        /// </summary> 
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 有效标志1有效 0 无效 
        /// </summary> 
        public int? F_EnabledMark { get; set; }
        /// <summary> 
        /// 备注 
        /// </summary> 
        public string F_Description { get; set; }
        #endregion
    }
}
