using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流进程(新)
    /// </summary>
    [Table("lr_nwf_process")]
    public class NWFProcessEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程模板主键 
        /// </summary> 
        public string F_SchemeId { get; set; }
        /// <summary> 
        /// 流程模板编码 
        /// </summary> 
        public string F_SchemeCode { get; set; }
        /// <summary> 
        /// 流程模板名称 
        /// </summary> 
        public string F_SchemeName { get; set; }
        /// <summary> 
        /// 流程进程自定义标题 
        /// </summary> 
        public string F_Title { get; set; }
        /// <summary> 
        /// 流程进程等级 
        /// </summary> 
        public int? F_Level { get; set; }
        /// <summary> 
        /// 流程进程有效标志 1正常2草稿3作废
        /// </summary> 
        public int? F_EnabledMark { get; set; }
        /// <summary> 
        /// 是否重新发起1是0不是 
        /// </summary> 
        public int? F_IsAgain { get; set; }
        /// <summary> 
        /// 流程进程是否结束1是0不是 
        /// </summary> 
        public int? F_IsFinished { get; set; }
        /// <summary> 
        /// 是否是子流程进程1是0不是 
        /// </summary> 
        public int? F_IsChild { get; set; }

        /// <summary> 
        /// 子流程执行方式1异步0同步
        /// </summary> 
        public int? F_IsAsyn { get; set; }
        /// <summary>
        /// 父流程的发起子流程的节点Id
        /// </summary>
        public string F_ParentNodeId { get; set; }
        /// <summary> 
        /// 流程进程父进程任务主键 
        /// </summary> 
        public string F_ParentTaskId { get; set; }
        /// <summary> 
        /// 流程进程父进程主键 
        /// </summary> 
        public string F_ParentProcessId { get; set; }
        /// <summary>
        /// 1表示开始处理过了 0 还没人处理过
        /// </summary>
        public int? F_IsStart { get; set; }
        /// <summary> 
        /// 创建时间 
        /// </summary> 
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 创建人主键 
        /// </summary> 
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 创建人名称 
        /// </summary> 
        public string F_CreateUserName { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 任务名称
        /// </summary>
        [NotWrited]
        public string F_TaskName { get; set; }
        /// <summary>
        /// 任务主键
        /// </summary>
        [NotWrited]
        public string F_TaskId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [NotWrited]
        public int? F_TaskType { get; set; }

        /// <summary> 
        /// 是否被催办 1 被催办了
        /// </summary> 
        [NotWrited]
        public int? F_IsUrge { get; set; }
        #endregion
    }
}
