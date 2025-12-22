using ce.autofac.extension;
using aycsoft.iapplication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.02.11
    /// 描 述：流程任务
    /// </summary>
    public class NWFTaskBLL :BLLBase, NWFTaskIBLL,BLL
    {
        private NWFTaskSerivce nWFTaskSerivce = new NWFTaskSerivce();

        #region 获取数据
        /// <summary>
        /// 获取所有的任务
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskEntity>> GetALLTaskList(string processId)
        {
            return nWFTaskSerivce.GetALLTaskList(processId);
        }

        /// <summary>
        /// 获取未完成的任务
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskEntity>> GetUnFinishTaskList(string processId)
        {
            return nWFTaskSerivce.GetUnFinishTaskList(processId);
        }
        /// <summary>
        /// 获取所有未完成的任务
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskEntity>> GetUnFinishTaskList() {
            return nWFTaskSerivce.GetUnFinishTaskList();
        }
        /// <summary>
        /// 判断任务是否允许撤销
        /// </summary>
        /// <param name="processId">流程实例</param>
        /// <param name="preNodeId">上一个节点（撤销任务节点）</param>
        /// <returns></returns>
        public Task<bool> IsRevokeTask(string processId, string preNodeId) {
            return nWFTaskSerivce.IsRevokeTask(processId, preNodeId);
        }
        /// <summary>
        /// 获取流程任务实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<NWFTaskEntity> GetEntity(string keyValue)
        {
            return nWFTaskSerivce.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取任务执行日志实体
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="prcoessId">流程进程主键</param>
        /// <returns></returns>
        public Task<NWFTaskLogEntity> GetLogEntityByNodeId(string nodeId, string prcoessId)
        {
            return nWFTaskSerivce.GetLogEntityByNodeId(nodeId, prcoessId);
        }

        /// <summary>
        /// 获取流程进程的任务处理日志
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        public Task<NWFTaskLogEntity> GetLogEntity(string taskId, string userId)
        {
            return nWFTaskSerivce.GetLogEntity(taskId, userId);
        }


        /// <summary>
        /// 获取流程进程的任务处理日志
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskLogEntity>> GetLogList(string processId) {
            return nWFTaskSerivce.GetLogList(processId);
        }
        /// <summary>
        /// 获取当前任务节点ID
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetCurrentNodeIds(string processId)
        {
            return nWFTaskSerivce.GetCurrentNodeIds(processId);
        }

        /// <summary>
        /// 获取最近一次的任务信息（审批任务）
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public Task<NWFTaskEntity> GetEntityByNodeId(string nodeId, string processId)
        {
            return nWFTaskSerivce.GetEntityByNodeId(nodeId, processId);
        }
        /// <summary>
        /// 获取任务执行人列表
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskRelationEntity>> GetTaskUserList(string taskId)
        {
            return nWFTaskSerivce.GetTaskUserList(taskId);
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 更新审核人
        /// </summary>
        /// <param name="list">审核人列表</param>
        /// <param name="taskList">任务列表</param>
        /// <param name="nWFTaskLogEntity">任务日志</param>
        public async Task Save(IEnumerable<NWFTaskRelationEntity> list, IEnumerable<string> taskList, NWFTaskLogEntity nWFTaskLogEntity)
        {
            await nWFTaskSerivce.Save(list, taskList, nWFTaskLogEntity);
        }
        #endregion
    }
}
