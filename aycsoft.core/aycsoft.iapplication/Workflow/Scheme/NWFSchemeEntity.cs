using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流模板(新)
    /// </summary>
    [Table("lr_nwf_scheme")]
    public class NWFSchemeEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程模板信息主键 
        /// </summary> 
        public string F_SchemeInfoId { get; set; }
        /// <summary> 
        /// 1.正式2.草稿 
        /// </summary> 
        public int? F_Type { get; set; }
        /// <summary> 
        /// 创建时间 
        /// </summary> 
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 创建人主键 
        /// </summary> 
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 创建人名字 
        /// </summary> 
        public string F_CreateUserName { get; set; }
        /// <summary> 
        /// 流程模板内容 
        /// </summary> 
        public string F_Content { get; set; }
        #endregion
    }
}
