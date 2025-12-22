using aycsoft.database;
using aycsoft.iapplication;
using System;
using System.Collections.Generic;
using System.Text;
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
    public class NWFTaskSerivce : ServiceBase
    {

        #region 获取数据
        /// <summary>
        /// 获取所有的任务
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskEntity>> GetALLTaskList(string processId)
        {
            return this.BaseRepository().FindList<NWFTaskEntity>("select * from lr_nwf_task where F_ProcessId = @processId ",new { processId });
        }
        /// <summary>
        /// 获取未完成的任务
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskEntity>> GetUnFinishTaskList(string processId) {
            return this.BaseRepository().FindList<NWFTaskEntity>("select * from lr_nwf_task where F_IsFinished = 0 AND F_ProcessId = @processId ", new { processId });
        }
        /// <summary>
        /// 获取所有未完成的任务
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskEntity>> GetUnFinishTaskList()
        {
            return this.BaseRepository().FindList<NWFTaskEntity>("select * from lr_nwf_task where F_IsFinished = 0 AND (F_Type = 1 OR F_Type = 3) ");
        }

        /// <summary>
        /// 判断任务是否允许撤销
        /// </summary>
        /// <param name="processId">流程实例</param>
        /// <param name="preNodeId">上一个节点（撤销任务节点）</param>
        /// <returns></returns>
        public async Task<bool> IsRevokeTask(string processId,string preNodeId)
        {
            bool res = true;
            var list2 = (List<NWFTaskEntity>) await this.BaseRepository().FindList<NWFTaskEntity>("select * from lr_nwf_task where F_ProcessId = @processId AND F_PrevNodeId = @preNodeId AND  (F_Type = 1 OR F_Type = 3 OR F_Type = 5) AND F_IsFinished = 0 ", new { processId, preNodeId });
            if (list2.Count > 0)
            {
                return res;
            }

            var list = await this.BaseRepository().FindList<NWFTaskEntity>("select * from lr_nwf_task where F_ProcessId = @processId AND F_PrevNodeId = @preNodeId AND F_Type = 1 AND F_IsFinished = 1", new { processId, preNodeId });
            
            foreach (var item in list)
            {
                string nodeId = item.F_NodeId;
                var entity = await this.BaseRepository().FindEntity<NWFTaskEntity>("select * from lr_nwf_task where F_ProcessId = @processId AND F_NodeId = @nodeId AND F_IsFinished = 0 ", new { processId, nodeId });

                if (entity == null)
                {
                    res = false;
                    break;
                }
            }

            return res;
        }

        /// <summary>
        /// 获取流程任务实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<NWFTaskEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<NWFTaskEntity>(keyValue);
        }
        /// <summary>
        /// 获取最近一次的任务信息（审批任务）
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public Task<NWFTaskEntity> GetEntityByNodeId(string nodeId, string processId) {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM LR_NWF_Task WHERE F_ProcessId = @processId AND F_NodeId = @nodeId AND F_Type != 3 ORDER BY F_CreateDate DESC");
            return this.BaseRepository().FindEntity<NWFTaskEntity>(strSql.ToString(), new { processId, nodeId });
        }
        /// <summary>
        /// 获取任务执行人列表
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskRelationEntity>> GetTaskUserList(string taskId) {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM LR_NWF_TaskRelation WHERE F_TaskId = @taskId  ORDER BY F_Sort");
            return this.BaseRepository().FindList<NWFTaskRelationEntity>(strSql.ToString(), new { taskId });
        }
        /// <summary>
        /// 获取任务执行日志实体
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="prcoessId">流程进程主键</param>
        /// <returns></returns>
        public Task<NWFTaskLogEntity> GetLogEntityByNodeId(string nodeId,string prcoessId) {
            return this.BaseRepository().FindEntity<NWFTaskLogEntity>("select * from lr_nwf_tasklog where F_NodeId = @nodeId AND F_ProcessId = @prcoessId AND F_TaskType != 3 AND F_TaskType !=6  ", new { nodeId, prcoessId });
        }
        /// <summary>
        /// 获取任务执行日志列表
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="prcoessId">流程进程主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskLogEntity>> GetLogListByNodeId(string nodeId, string prcoessId)
        {
            return this.BaseRepository().FindList<NWFTaskLogEntity>("select * from lr_nwf_tasklog where F_NodeId = @nodeId AND F_ProcessId = @prcoessId AND F_TaskType = 1", new { nodeId, prcoessId });
        }
        /// <summary>
        /// 获取流程进程的任务处理日志
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFTaskLogEntity>> GetLogList(string processId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM LR_NWF_TaskLog WHERE F_ProcessId = @processId ORDER BY F_CreateDate DESC ");

            return this.BaseRepository().FindList<NWFTaskLogEntity>(strSql.ToString(), new { processId });
        }

        /// <summary>
        /// 获取流程进程的任务处理日志
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        public Task<NWFTaskLogEntity> GetLogEntity(string taskId,string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM LR_NWF_TaskLog WHERE F_TaskId = @taskId AND F_CreateUserId = @userId AND F_TaskType != 100");
            return this.BaseRepository().FindEntity<NWFTaskLogEntity>(strSql.ToString(),new { taskId, userId });
        }

        /// <summary>
        /// 获取当前任务节点ID
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetCurrentNodeIds(string processId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                                t.F_NodeId
                                FROM
	                                LR_NWF_Task t
                                WHERE
	                                t.F_IsFinished = 0 AND t.F_ProcessId = @processId
                                GROUP BY
	                                t.F_NodeId      
                ");
            var taskList = await this.BaseRepository().FindList<NWFTaskEntity>(strSql.ToString(), new { processId });
            List<string> list = new List<string>();
            foreach (var item in taskList)
            {
                list.Add(item.F_NodeId);
            }
            return list;
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
            var db = this.BaseRepository().BeginTrans();
            try
            {
                foreach (string taskId in taskList) {
                    await db.DeleteAny<NWFTaskRelationEntity>(new {F_TaskId = taskId , F_Result = 0 , F_Mark = 0 });
                }

                foreach (var taskUser in list) {
                    await db.Insert(taskUser);
                }

                await db.Insert(nWFTaskLogEntity);

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
