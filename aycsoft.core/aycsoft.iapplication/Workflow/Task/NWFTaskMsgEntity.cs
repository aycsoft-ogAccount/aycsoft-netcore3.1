using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流任务消息(新)
    /// </summary>
    [Table("lr_nwf_taskmsg")]
    public class NWFTaskMsgEntity
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
        /// 任务发送人主键 
        /// </summary> 
        public string F_FromUserId { get; set; }
        /// <summary> 
        /// 任务发送人账号 
        /// </summary> 
        public string F_FromUserAccount { get; set; }
        /// <summary> 
        /// 任务发送人名称 
        /// </summary> 
        public string F_FromUserName { get; set; }
        /// <summary> 
        /// 任务接收人主键 
        /// </summary> 
        public string F_ToUserId { get; set; }
        /// <summary> 
        /// 任务接收人账号 
        /// </summary> 
        public string F_ToAccount { get; set; }
        /// <summary> 
        /// 任务接收人名称 
        /// </summary> 
        public string F_ToName { get; set; }
        /// <summary> 
        /// 任务标题 
        /// </summary> 
        public string F_Title { get; set; }
        /// <summary> 
        /// 任务内容 
        /// </summary> 
        public string F_Content { get; set; }
        /// <summary> 
        /// 任务创建时间 
        /// </summary> 
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 是否结束1结束0未结束 
        /// </summary> 
        public int? F_IsFinished { get; set; }
        #endregion

        #region 扩展属性
        /// <summary> 
        /// 节点Id 
        /// </summary>
        [NotWrited]
        public string NodeId { get; set; }
        #endregion
    }
}
