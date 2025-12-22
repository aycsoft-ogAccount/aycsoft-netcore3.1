using cd.dapper.extension;
using aycsoft.workflow;
using System;
using System.Collections.Generic;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流任务(新)
    /// </summary>
    [Table("lr_nwf_task")]
    public class NWFTaskEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程实例主键 
        /// </summary> 
        public string F_ProcessId { get; set; }
        /// <summary> 
        /// 流程节点ID 
        /// </summary> 
        public string F_NodeId { get; set; }
        /// <summary> 
        /// 流程节点名称 
        /// </summary> 
        public string F_NodeName { get; set; }
        /// <summary> 
        /// 任务类型1审批2传阅3加签4子流程5重新创建 6子流程重新创建
        /// </summary> 
        public int? F_Type { get; set; }
        /// <summary> 
        /// 是否完成1完成2关闭0未完成3子流程处理中
        /// </summary> 
        public int? F_IsFinished { get; set; }
        /// <summary> 
        /// 任务超时流转到下一个节点时间 
        /// </summary> 
        public int? F_TimeoutAction { get; set; }
        /// <summary> 
        /// 任务超时提醒消息时间 
        /// </summary> 
        public int? F_TimeoutNotice { get; set; }
        /// <summary> 
        /// 任务超时消息提醒间隔时间 
        /// </summary> 
        public int? F_TimeoutInterval { get; set; }
        /// <summary> 
        /// 任务超时消息发送策略编码 
        /// </summary> 
        public string F_TimeoutStrategy { get; set; }
        /// <summary> 
        /// 上一个任务节点Id 
        /// </summary> 
        public string F_PrevNodeId { get; set; }
        /// <summary> 
        /// 上一个节点名称 
        /// </summary> 
        public string F_PrevNodeName { get; set; }
        /// <summary> 
        /// 任务创建时间 
        /// </summary> 
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 任务创建人员 
        /// </summary> 
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 任务创建人员名称 
        /// </summary> 
        public string F_CreateUserName { get; set; }
        /// <summary> 
        /// 任务变更时间 
        /// </summary> 
        public DateTime? F_ModifyDate { get; set; }
        /// <summary> 
        /// 任务变更人员信息 
        /// </summary> 
        public string F_ModifyUserId { get; set; }
        /// <summary> 
        /// 任务变更人员名称 
        /// </summary> 
        public string F_ModifyUserName { get; set; }
        /// <summary> 
        /// 是否被催办 1 被催办了
        /// </summary> 
        public int? F_IsUrge { get; set; }
        /// <summary> 
        /// 加签情况下最初的审核者 
        /// </summary> 
        public string F_FirstUserId { get; set; }
        /// <summary>
        /// 子流程进程主键
        /// </summary>
        public string F_ChildProcessId { get; set; }
        /// <summary>
        /// 批量审核 1是允许 其他值都不允许
        /// </summary>
        public int? F_IsBatchAudit { get; set; }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 流程审核人信息
        /// </summary>
        [NotWrited]
        public List<NWFUserInfo> nWFUserInfoList { get; set; }
        #endregion
    }
}
