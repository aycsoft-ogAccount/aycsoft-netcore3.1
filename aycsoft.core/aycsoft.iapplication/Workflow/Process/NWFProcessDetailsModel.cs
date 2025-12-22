using System.Collections.Generic;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流进程详情数据模型
    /// </summary>
    public class NWFProcessDetailsModel
    {

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string CurrentNodeId { get; set; }
        /// <summary>
        /// 当前正在执行的任务节点ID数据
        /// </summary>
        public IEnumerable<string> CurrentNodeIds { get; set; }
        /// <summary>
        /// 流程模板信息
        /// </summary>
        public string Scheme { get; set; }
        /// <summary>
        /// 任务执行记录
        /// </summary>
        public IEnumerable<NWFTaskLogEntity> TaskLogList { get; set; }
        /// <summary>
        /// 子流程进程主键
        /// </summary>
        public string childProcessId { get; set; }
        /// <summary>
        /// 父流程进程主键
        /// </summary>
        public string parentProcessId { get; set; }
        /// <summary>
        /// 流程是否结束 0 不是 1 是
        /// </summary>
        public int isFinished { get; set; }
    }
}
