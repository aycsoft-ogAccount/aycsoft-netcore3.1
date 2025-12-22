using ce.autofac.extension;
using aycsoft.util;
using aycsoft.workflow;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：流程进程
    /// </summary>
    public interface NWFProcessIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取流程进程实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<NWFProcessEntity> GetEntity(string keyValue);
        /// <summary>
        /// 获取流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        Task<IEnumerable<NWFProcessEntity>> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        Task<IEnumerable<NWFProcessEntity>> GetMyPageList(Pagination pagination, string queryJson, string schemeCode = null);
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        Task<IEnumerable<NWFProcessEntity>> GetMyPageList(string queryJson, string schemeCode = null);
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(Pagination pagination, string queryJson, string schemeCode = null);
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(string queryJson, string schemeCode = null);

        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(Pagination pagination, string queryJson, bool isBatchAudit, string schemeCode = null);
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(string queryJson, bool isBatchAudit, string schemeCode = null);
        /// <summary>
        /// 获取已处理任务列表
        /// </summary>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        Task<IEnumerable<NWFProcessEntity>> GetMyFinishTaskPageList(Pagination pagination, string queryJson, string schemeCode = null);
        /// <summary>
        /// 获取已处理任务列表
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        Task<IEnumerable<NWFProcessEntity>> GetMyFinishTaskPageList(string queryJson, string schemeCode = null);
        #endregion

        #region 保存更新删除
        /// <summary>
        /// 删除流程进程实体
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        Task DeleteEntity(string processId);
        #endregion

        #region 流程API

        /// <summary>
        /// 获取下一节点审核人
        /// </summary>
        /// <param name="code">流程模板code</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="nodeId">流程节点Id</param>
        /// <param name="operationCode">流程操作代码</param>
        /// <returns></returns>
        Task<Dictionary<string, IEnumerable<NWFUserInfo>>> GetNextAuditors(string code, string processId, string taskId, string nodeId, string operationCode);
        /// <summary>
        /// 获取流程进程信息
        /// </summary>
        /// <param name="processId">进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        Task<NWFProcessDetailsModel> GetProcessDetails(string processId, string taskId);
        /// <summary>
        /// 获取子流程详细信息
        /// </summary>
        /// <param name="processId">父流程进程主键</param>
        /// <param name="taskId">父流程子流程发起主键</param>
        /// <param name="schemeCode">子流程流程模板编码</param>
        /// <param name="nodeId">父流程发起子流程节点Id</param>
        /// <returns></returns>
        Task<NWFProcessDetailsModel> GetChildProcessDetails(string processId, string taskId, string schemeCode, string nodeId);
        /// <summary>
        /// 保存草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="userId">创建人</param>
        /// <returns></returns>
        Task SaveDraft(string processId, string schemeCode, string userId);
        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        Task DeleteDraft(string processId);
        /// <summary>
        /// 创建流程
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="title">标题</param>
        /// <param name="level">流程等级</param>
        /// <param name="auditors">下一节点审核人</param>
        /// <param name="userId">创建人主键</param>
        Task CreateFlow(string schemeCode, string processId, string title, int level, string auditors, string userId);
        /// <summary>
        /// 创建流程(子流程)
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="parentProcessId">父级流程实例主键</param>
        /// <param name="parentTaskId">父级流程任务主键</param>
        Task CreateChildFlow(string schemeCode, string processId, string parentProcessId, string parentTaskId);
        /// <summary>
        /// 重新创建流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        Task AgainCreateFlow(string processId);
        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="operationCode">流程审批操作码agree 同意 disagree 不同意 lrtimeout 超时</param>
        /// <param name="operationName">流程审批操名称</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="des">审批意见</param>
        /// <param name="auditors">下一节点指定审核人</param>
        /// <param name="stamp">盖章信息</param>
        /// <param name="signUrl">签名信息</param>
        Task AuditFlow(string operationCode, string operationName, string processId, string taskId, string des, string auditors, string stamp, string signUrl);
        /// <summary>
        /// 批量审核（只有同意和不同意）
        /// </summary>
        /// <param name="operationCode">操作码</param>
        /// <param name="taskIds">任务id串</param>
        Task AuditFlows(string operationCode, string taskIds);
        /// <summary>
        /// 流程加签
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="userId">加签人员</param>
        /// <param name="des">加签说明</param>
        Task SignFlow(string processId, string taskId, string userId, string des);
        /// <summary>
        /// 流程加签审核
        /// </summary>
        /// <param name="operationCode">审核操作码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="des">加签说明</param>
        Task SignAuditFlow(string operationCode, string processId, string taskId, string des);

        /// <summary>
        /// 确认阅读
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        Task ReferFlow(string processId, string taskId);

        /// <summary>
        /// 催办流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        Task UrgeFlow(string processId);
        /// <summary>
        /// 撤销流程（只有在该流程未被处理的情况下）
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        Task RevokeFlow(string processId);
        /// <summary>
        /// 撤销审核（只有在下一个节点未被处理的情况下才能执行）
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">任务主键</param>
        Task<bool> RevokeAudit(string processId, string taskId);

        /// <summary>
        /// 流程任务超时处理
        /// </summary>
        Task MakeTaskTimeout();
        /// <summary>
        /// 获取流程当前任务需要处理的人
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        Task<IEnumerable<NWFTaskEntity>> GetTaskUserList(string processId);
        /// <summary>
        /// 指派流程审核人
        /// </summary>
        /// <param name="list">任务列表</param>
        Task AppointUser(IEnumerable<NWFTaskEntity> list);

        /// <summary>
        /// 作废流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        Task DeleteFlow(string processId);


        /// <summary>
        /// 给指定的流程添加审核节点
        /// </summary>
        /// <param name="processId">流程实例ID</param>
        /// <param name="bNodeId">开始节点</param>
        /// <param name="eNodeId">结束节点（审核任务的节点）</param>
        Task AddTask(string processId, string bNodeId, string eNodeId);
        #endregion

        #region 获取sql语句
        /// <summary>
        /// 获取我的流程信息列表SQL语句
        /// </summary>
        /// <returns></returns>
        string GetMySql();
        /// <summary>
        /// 获取我的代办任务列表SQL语句
        /// </summary>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        Task<string> GetMyTaskSql(bool isBatchAudit = false);
        /// <summary>
        /// 获取我的已办任务列表SQL语句
        /// </summary>
        /// <returns></returns>
        string GetMyFinishTaskSql();
        #endregion
    }
}
