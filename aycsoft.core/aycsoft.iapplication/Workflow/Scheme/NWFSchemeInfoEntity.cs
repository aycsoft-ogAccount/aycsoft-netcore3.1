using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流模板信息(新)
    /// </summary>
    [Table("lr_nwf_schemeinfo")]
    public class NWFSchemeInfoEntity
    {
        #region 实体成员
        /// <summary> 
        /// 主键 
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程编码 
        /// </summary> 
        public string F_Code { get; set; }
        /// <summary> 
        /// 流程模板名称 
        /// </summary> 
        public string F_Name { get; set; }
        /// <summary> 
        /// 流程分类 
        /// </summary> 
        public string F_Category { get; set; }
        /// <summary> 
        /// 流程模板ID 
        /// </summary> 
        public string F_SchemeId { get; set; }
        /// <summary> 
        /// 是否有效 
        /// </summary> 
        public int? F_EnabledMark { get; set; }
        /// <summary> 
        /// 是否在我的任务允许发起 1允许 2不允许 
        /// </summary> 
        public int? F_Mark { get; set; }
        /// <summary> 
        /// 是否在App上允许发起 1允许 2不允许 
        /// </summary> 
        public int? F_IsInApp { get; set; }
        /// <summary> 
        /// 备注 
        /// </summary> 
        public string F_Description { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 1.正式（已发布）2.草稿
        /// </summary>
        [NotWrited]
        public int? F_Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [NotWrited]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [NotWrited]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [NotWrited]
        public string F_CreateUserName { get; set; }
        #endregion
    }
}
