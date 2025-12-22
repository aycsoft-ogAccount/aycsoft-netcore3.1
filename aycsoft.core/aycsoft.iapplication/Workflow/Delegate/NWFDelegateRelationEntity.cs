using cd.dapper.extension;
namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流委托模板关系表(新)
    /// </summary>
    [Table("lr_nwf_delegaterelation")]
    public class NWFDelegateRelationEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 委托规则主键 
        /// </summary> 
        public string F_DelegateRuleId { get; set; }
        /// <summary> 
        /// 流程模板信息主键 
        /// </summary> 
        public string F_SchemeInfoId { get; set; }
        #endregion
    }
}
