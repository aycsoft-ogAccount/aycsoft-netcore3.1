using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流会签计算(新)
    /// </summary>
    [Table("lr_nwf_confluence")]
    public class NWFConfluenceEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键
        /// </summary> 
       [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程进程主键 
        /// </summary> 
        public string F_ProcessId { get; set; }
        /// <summary> 
        /// 会签节点ID 
        /// </summary> 
        public string F_NodeId { get; set; }
        /// <summary> 
        /// 上一节点ID  
        /// </summary> 
        public string F_FormNodeId { get; set; }
        /// <summary> 
        /// 状态1同意0不同意 
        /// </summary> 
        public int? F_State { get; set; }
        #endregion

        /// <summary>
        /// 是否清除之前审核信息
        /// </summary>
        [NotWrited]
        public bool isClear { get; set; }
        /// <summary>
        /// 会签审核结果 1 通过 -1 不通过
        /// </summary>
        [NotWrited]
        public int confluenceRes { get; set; }
    }
}
