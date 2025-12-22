using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流任务日志(新)
    /// </summary>
    [Table("lr_nwf_tasklog")]
    public class NWFTaskLogEntity
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
        /// 流程任务主键 
        /// </summary> 
        public string F_TaskId { get; set; }
        /// <summary> 
        /// 操作码create创建 agree 同意 disagree 不同意 lrtimeout 超时 
        /// </summary> 
        public string F_OperationCode { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string F_OperationName { get; set; }
        /// <summary> 
        /// 流程节点ID 
        /// </summary> 
        public string F_NodeId { get; set; }
        /// <summary> 
        /// 流程节点名称 
        /// </summary> 
        public string F_NodeName { get; set; }
        /// <summary> 
        /// 流程任务类型 0创建1审批2传阅3加签审核4子流程5重新创建6.超时流转7会签审核8加签9催办10超时提醒 100其他
        /// </summary> 
        public int? F_TaskType { get; set; }
        /// <summary> 
        /// 上一流程节点ID 
        /// </summary> 
        public string F_PrevNodeId { get; set; }
        /// <summary> 
        /// 上一流程节点名称 
        /// </summary> 
        public string F_PrevNodeName { get; set; }
        /// <summary> 
        /// 创建时间 
        /// </summary> 
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 创建人主键 
        /// </summary> 
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 创建人员名称 
        /// </summary> 
        public string F_CreateUserName { get; set; }

        /// <summary>
        /// 任务人Id
        /// </summary>
        public string F_TaskUserId { get; set; }
        /// <summary>
        /// 任务人名称
        /// </summary>
        public string F_TaskUserName { get; set; }
        /// <summary> 
        /// 备注信息 
        /// </summary> 
        public string F_Des { get; set; }

        /// <summary> 
        /// 签名图片 
        /// </summary> 
        public string F_SignImg { get; set; }
        /// <summary> 
        /// 盖章图片
        /// </summary> 
        public string F_StampImg { get; set; }
        #endregion
    }
}
