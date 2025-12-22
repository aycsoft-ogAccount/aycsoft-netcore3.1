using aycsoft.iapplication;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.02.12
    /// 描 述：流程进程
    /// </summary>
    public class NWFProcessSericve: ServiceBase
    {
        #region 获取数据
        /// <summary>
        /// 获取流程进程实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<NWFProcessEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<NWFProcessEntity>(keyValue);
        }
        /// <summary>
        /// 获取流程进程实例
        /// </summary>
        /// <param name="processId">父流程进程主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public Task<NWFProcessEntity> GetEntityByProcessId(string processId, string nodeId)
        {
            return this.BaseRepository().FindEntity<NWFProcessEntity>(" select * from lr_nwf_process where  F_ParentProcessId =@processId AND F_ParentNodeId = @nodeId  ", new { processId, nodeId });
        }
        /// <summary>
        /// 获取子流程列表
        /// </summary>
        /// <param name="parentProcessId">父流程进程主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFProcessEntity>> GetChildProcessList(string parentProcessId)
        {
            return this.BaseRepository().FindList<NWFProcessEntity>("select * from lr_nwf_process where F_ParentProcessId =@parentProcessId ", new { parentProcessId });
        }

        /// <summary>
        /// 获取流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(" select * from lr_nwf_process where F_EnabledMark != 2 AND F_IsChild = 0 ");
            var queryParam = queryJson.ToJObject();
            // 分类
            if (!queryParam["categoryId"].IsEmpty()) // 1:未完成 2:已完成
            {
                if (queryParam["categoryId"].ToString() == "1")
                {
                    strSql.Append(" AND F_IsFinished = 0 ");
                }
                else
                {
                    strSql.Append(" AND F_IsFinished = 1 ");
                }
            }
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            // 操作时间
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                startTime = queryParam["StartTime"].ToDate();
                endTime = queryParam["EndTime"].ToDate();
                strSql.Append(" AND ( F_CreateDate  >= @startTime AND F_CreateDate <= @endTime )");
            }
            string keyword = "";
            // 关键字
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword ="%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" AND ( F_Title like @keyword OR F_SchemeName like @keyword OR F_CreateUserName like @keyword ) ");
            }

            var list =await this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(),new { startTime, endTime, keyword }, pagination);
            foreach (var item in list)
            {
                if (item.F_IsFinished != 1 && item.F_EnabledMark != 3 && item.F_EnabledMark != 2)
                {
                    var taskEntity = await this.BaseRepository().FindEntity<NWFTaskEntity>(" select * from lr_nwf_task where F_ProcessId = @processId  AND F_IsFinished = 0 ", new { processId = item.F_Id });
                    if (taskEntity == null)
                    {
                        item.F_EnabledMark = 500;
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFProcessEntity>> GetMyPageList(Pagination pagination, string queryJson, string schemeCode, string userId)
        {
            var strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            strSql.Append(" select * from lr_nwf_process where F_IsChild = 0 AND F_CreateUserId = @userId AND F_EnabledMark != 3 ");
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            // 操作时间
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                startTime = queryParam["StartTime"].ToDate();
                endTime = queryParam["EndTime"].ToDate();
                strSql.Append(" AND ( F_CreateDate  >= @startTime AND F_CreateDate <= @endTime )");
            }
            string keyword = "";
            // 关键字
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" AND ( F_Title like @keyword OR F_SchemeName like @keyword OR F_CreateUserName like @keyword ) ");
            }
            if (!string.IsNullOrEmpty(schemeCode))
            {
                strSql.Append(" AND F_SchemeCode = @schemeCode ");
            }
            return this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(),new { schemeCode, userId, keyword, startTime, endTime }, pagination);
        }
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="queryJson">分页参数</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFProcessEntity>> GetMyPageList(string queryJson, string schemeCode, string userId )
        {
            var strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            strSql.Append(" select * from lr_nwf_process where F_IsChild = 0 AND F_CreateUserId = @userId AND F_EnabledMark != 3 ");
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            // 操作时间
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                startTime = queryParam["StartTime"].ToDate();
                endTime = queryParam["EndTime"].ToDate();
                strSql.Append(" AND ( F_CreateDate  >= @startTime AND F_CreateDate <= @endTime )");
            }
            string keyword = "";
            // 关键字
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" AND ( F_Title like @keyword OR F_SchemeName like @keyword OR F_CreateUserName like @keyword ) ");
            }
            if (!string.IsNullOrEmpty(schemeCode))
            {
                strSql.Append(" AND F_SchemeCode = @schemeCode ");
            }
            return this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(), new { schemeCode, userId, keyword, startTime, endTime });
        }

        /// <summary>
        /// 获取我的代办任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(UserEntity userInfo, Pagination pagination, string queryJson, string schemeCode, bool isBatchAudit = false)
        {
            string userId = userInfo.F_UserId;
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                                t.F_Id AS F_TaskId,
	                                t.F_Type AS F_TaskType,
	                                t.F_NodeName AS F_TaskName,
                                    t.F_IsUrge,
                                    t.F_ModifyDate as F_CreateDate,
                                    p.F_Id,
                                    p.F_SchemeId,
                                    p.F_SchemeCode,
                                    p.F_SchemeName,
                                    p.F_Title,
                                    p.F_Level,
                                    p.F_EnabledMark,
                                    p.F_IsAgain,
                                    p.F_IsFinished,
                                    p.F_IsChild,
                                    p.F_ParentTaskId,
                                    p.F_ParentProcessId,
                                    p.F_CreateUserId,
                                    p.F_CreateUserName,
                                    p.F_IsStart
                                FROM
	                                (
		                                SELECT
			                                F_TaskId
		                                FROM
			                                LR_NWF_TaskRelation r1
                                        LEFT JOIN LR_NWF_Task t1 ON r1.F_TaskId = t1.F_Id 
                                        WHERE r1.F_Mark = 0 AND r1.F_Result = 0 AND (r1.F_UserId  = @userId 
		                               ");


            // 添加委托信息
            IEnumerable<UserEntity> delegateList = await GetDelegateProcess(userId);
            foreach (var item in delegateList)
            {
                string processId = "'" + item.F_Description.Replace(",", "','") + "'";
                string userI2 = "'" + item.F_UserId + "'";

                strSql.Append("  OR (r1.F_UserId =" + userI2 + " AND t1.F_ProcessId in (" + processId + ") AND t1.F_Type != 2 )");
            }
            strSql.Append(@") GROUP BY
			                                F_TaskId
	                                ) r
                                LEFT JOIN LR_NWF_Task t ON t.F_Id = r.F_TaskId
                                LEFT JOIN LR_NWF_Process p ON p.F_Id = t.F_ProcessId
                                WHERE
	                                t.F_IsFinished = 0  AND (p.F_IsFinished = 0 OR t.F_Type = 2 OR t.F_Type = 4 OR t.F_Type = 6) AND p.F_EnabledMark != 3 ");

            var queryParam = queryJson.ToJObject();
            DateTime startTime = DateTime.Now, endTime = DateTime.Now;

            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                startTime = queryParam["StartTime"].ToDate();
                endTime = queryParam["EndTime"].ToDate();
                strSql.Append(" AND ( t.F_ModifyDate >= @startTime AND  t.F_ModifyDate <= @endTime ) ");
            }
            string keyword = "";
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" AND ( p.F_Title like @keyword OR  p.F_SchemeName like @keyword ) ");
            }

            if (!string.IsNullOrEmpty(schemeCode))
            {
                strSql.Append(" AND p.F_SchemeCode = @schemeCode ");
            }

            if (isBatchAudit)
            {
                strSql.Append(" AND t.F_IsBatchAudit = 1 ");
            }

            return await this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(), new { userId, startTime, endTime, keyword, schemeCode }, pagination);
        }
        /// <summary>
        /// 获取我的代办任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(UserEntity userInfo, string queryJson, string schemeCode, bool isBatchAudit = false)
        {
            string userId = userInfo.F_UserId;
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                                t.F_Id AS F_TaskId,
	                                t.F_Type AS F_TaskType,
	                                t.F_NodeName AS F_TaskName,
                                    t.F_IsUrge,
                                    t.F_ModifyDate as F_CreateDate,
                                    p.F_Id,
                                    p.F_SchemeId,
                                    p.F_SchemeCode,
                                    p.F_SchemeName,
                                    p.F_Title,
                                    p.F_Level,
                                    p.F_EnabledMark,
                                    p.F_IsAgain,
                                    p.F_IsFinished,
                                    p.F_IsChild,
                                    p.F_ParentTaskId,
                                    p.F_ParentProcessId,
                                    p.F_CreateUserId,
                                    p.F_CreateUserName,
                                    p.F_IsStart
                                FROM
	                                (
		                                SELECT
			                                F_TaskId
		                                FROM
			                                LR_NWF_TaskRelation r1
                                        LEFT JOIN LR_NWF_Task t1 ON r1.F_TaskId = t1.F_Id 
                                        WHERE r1.F_Mark = 0 AND r1.F_Result = 0 AND (r1.F_UserId  = @userId 
		                               ");


            // 添加委托信息
            IEnumerable<UserEntity> delegateList = await GetDelegateProcess(userId);
            foreach (var item in delegateList)
            {
                string processId = "'" + item.F_Description.Replace(",", "','") + "'";
                string userI2 = "'" + item.F_UserId + "'";

                strSql.Append("  OR (r1.F_UserId =" + userI2 + " AND t1.F_ProcessId in (" + processId + ") AND t1.F_Type != 2 )");
            }
            strSql.Append(@") GROUP BY
			                                F_TaskId
	                                ) r
                                LEFT JOIN LR_NWF_Task t ON t.F_Id = r.F_TaskId
                                LEFT JOIN LR_NWF_Process p ON p.F_Id = t.F_ProcessId
                                WHERE
	                                t.F_IsFinished = 0  AND (p.F_IsFinished = 0 OR t.F_Type = 2 OR t.F_Type = 4 OR t.F_Type = 6) AND p.F_EnabledMark != 3 ");

            var queryParam = queryJson.ToJObject();
            DateTime startTime = DateTime.Now, endTime = DateTime.Now;

            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                startTime = queryParam["StartTime"].ToDate();
                endTime = queryParam["EndTime"].ToDate();
                strSql.Append(" AND ( t.F_ModifyDate >= @startTime AND  t.F_ModifyDate <= @endTime ) ");
            }
            string keyword = "";
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" AND ( p.F_Title like @keyword OR  p.F_SchemeName like @keyword ) ");
            }

            if (!string.IsNullOrEmpty(schemeCode))
            {
                strSql.Append(" AND p.F_SchemeCode = @schemeCode ");
            }

            if (isBatchAudit)
            {
                strSql.Append(" AND t.F_IsBatchAudit = 1 ");
            }

            return await this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(), new { userId, startTime, endTime, keyword, schemeCode });
        }
        /// <summary>
        /// 获取我的已办任务列表
        /// </summary>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="userInfo">登录者信息</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFProcessEntity>> GetMyFinishTaskPageList(Pagination pagination, string queryJson, string schemeCode, UserEntity userInfo)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                    t.F_TaskId,
	                    t.F_TaskType,
	                    t.F_TaskName,
	                    t.F_CreateDate,
	                    p.F_Title,
	                    p.F_Id,
	                    p.F_SchemeId,
	                    p.F_SchemeCode,
	                    p.F_SchemeName,
	                    p.F_Level,
	                    p.F_EnabledMark,
	                    p.F_IsAgain,
	                    p.F_IsFinished,
	                    p.F_IsChild,
	                    p.F_ParentTaskId,
	                    p.F_ParentProcessId,
	                    p.F_CreateUserId,
	                    p.F_CreateUserName,
	                    p.F_IsStart
                    FROM
	                    (
		                    SELECT
			                    MAX(t.F_Id) AS F_TaskId,
			                    MAX(t.F_Type) AS F_TaskType,
			                    MAX(t.F_NodeName) AS F_TaskName,
			                    MAX(r.F_Time) AS F_CreateDate,
			                    t.F_ProcessId
		                    FROM
			                    LR_NWF_Task t
		                    LEFT JOIN LR_NWF_TaskRelation r ON r.F_TaskId = t.F_Id
		                    WHERE
			                    (
				                    r.F_Result = 1
				                    OR r.F_Result = 2
				                    OR r.F_Result = 4
			                    )
		                    AND r.F_UserId = @userId
		                    GROUP BY
			                    t.F_NodeId,t.F_ProcessId
	                    ) t
                    LEFT JOIN LR_NWF_Process p ON t.F_ProcessId = p.F_Id where 1 = 1 AND p.F_EnabledMark != 3
                ");


            var queryParam = queryJson.ToJObject();
            DateTime startTime = DateTime.Now, endTime = DateTime.Now;

            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                startTime = queryParam["StartTime"].ToDate();
                endTime = queryParam["EndTime"].ToDate();
                strSql.Append(" AND ( t.F_CreateDate >= @startTime AND  t.F_CreateDate <= @endTime ) ");
            }
            string keyword = "";
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" AND ( p.F_Title like @keyword OR  p.F_SchemeName like @keyword ) ");
            }
            if (!string.IsNullOrEmpty(schemeCode))
            {
                strSql.Append(" AND p.F_SchemeCode = @schemeCode ");
            }

            return this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(), new { userId = userInfo.F_UserId, startTime, endTime, keyword, schemeCode }, pagination);
        }

        /// <summary>
        /// 获取我的已办任务列表
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="userInfo">登录者信息</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFProcessEntity>> GetMyFinishTaskPageList(string queryJson, string schemeCode, UserEntity userInfo)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                    t.F_TaskId,
	                    t.F_TaskType,
	                    t.F_TaskName,
	                    t.F_CreateDate,
	                    p.F_Title,
	                    p.F_Id,
	                    p.F_SchemeId,
	                    p.F_SchemeCode,
	                    p.F_SchemeName,
	                    p.F_Level,
	                    p.F_EnabledMark,
	                    p.F_IsAgain,
	                    p.F_IsFinished,
	                    p.F_IsChild,
	                    p.F_ParentTaskId,
	                    p.F_ParentProcessId,
	                    p.F_CreateUserId,
	                    p.F_CreateUserName,
	                    p.F_IsStart
                    FROM
	                    (
		                    SELECT
			                    MAX(t.F_Id) AS F_TaskId,
			                    MAX(t.F_Type) AS F_TaskType,
			                    MAX(t.F_NodeName) AS F_TaskName,
			                    MAX(r.F_Time) AS F_CreateDate,
			                    t.F_ProcessId
		                    FROM
			                    LR_NWF_Task t
		                    LEFT JOIN LR_NWF_TaskRelation r ON r.F_TaskId = t.F_Id
		                    WHERE
			                    (
				                    r.F_Result = 1
				                    OR r.F_Result = 2
				                    OR r.F_Result = 4
			                    )
		                    AND r.F_UserId = @userId
		                    GROUP BY
			                    t.F_NodeId,F_ProcessId
	                    ) t
                    LEFT JOIN LR_NWF_Process p ON t.F_ProcessId = p.F_Id where 1 = 1 AND p.F_EnabledMark != 3
                ");


            var queryParam = queryJson.ToJObject();
            DateTime startTime = DateTime.Now, endTime = DateTime.Now;

            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                startTime = queryParam["StartTime"].ToDate();
                endTime = queryParam["EndTime"].ToDate();
                strSql.Append(" AND ( t.F_CreateDate >= @startTime AND  t.F_CreateDate <= @endTime ) ");
            }
            string keyword = "";
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" AND ( p.F_Title like @keyword OR  p.F_SchemeName like @keyword ) ");
            }
            if (!string.IsNullOrEmpty(schemeCode))
            {
                strSql.Append(" AND p.F_SchemeCode = @schemeCode ");
            }

            return this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(), new { userId = userInfo.F_UserId, startTime, endTime, keyword, schemeCode });
        }
        /// <summary>
        /// 获取委托人关联的流程进程列表
        /// </summary>
        /// <param name="userId">当前用户主键</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserEntity>> GetDelegateProcess(string userId)
        {
            List<UserEntity> delegateUserlist = new List<UserEntity>();
            DateTime datetime = DateTime.Now;
            var wfDelegateRuleList =await this.BaseRepository().FindList<NWFDelegateRuleEntity>(" select * from lr_nwf_delegaterule where F_ToUserId = @userId AND F_BeginDate <= @datetime AND F_EndDate <= @datetime   ", new { userId, datetime });
            foreach (var item in wfDelegateRuleList)
            {
                UserEntity userinfo = new UserEntity();
                userinfo.F_UserId = item.F_CreateUserId;

                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                    p.F_Id
                                    FROM
	                                    LR_NWF_DelegateRelation d
                                    LEFT JOIN LR_NWF_SchemeInfo s ON s.F_Id = d.F_SchemeInfoId
                                    LEFT JOIN LR_NWF_Process p ON p.F_SchemeCode = s.F_Code
                                    WHERE
	                                    p.F_Id IS NOT NULL
                                    AND p.F_IsFinished = 0
                                    AND d.F_DelegateRuleId = @DelegateRuleId ");

                DataTable dt = await this.BaseRepository().FindTable(strSql.ToString(), new { DelegateRuleId = item.F_Id });
                userinfo.F_Description = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr[0].ToString()))
                    {
                        if (!string.IsNullOrEmpty(userinfo.F_Description))
                        {
                            userinfo.F_Description += ",";
                        }
                        userinfo.F_Description += dr[0].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(userinfo.F_Description))
                {
                    delegateUserlist.Add(userinfo);
                }
            }
            return delegateUserlist;

        }


        #region 获取sql语句
        /// <summary>
        /// 获取我的流程信息列表SQL语句
        /// </summary>
        /// <returns></returns>
        public string GetMySql()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                            p.F_CreateDate,
                                p.F_Id,
                                p.F_SchemeId,
                                p.F_SchemeCode,
                                p.F_SchemeName,
                                p.F_Title,
                                p.F_Level,
                                p.F_EnabledMark,
                                p.F_IsAgain,
                                p.F_IsFinished,
                                p.F_IsChild,
                                p.F_ParentTaskId,
                                p.F_ParentProcessId,
                                p.F_CreateUserId,
                                p.F_CreateUserName,
                                p.F_IsStart
                            FROM
	                            LR_NWF_Process p
                            WHERE
	                            p.F_CreateUserId = @userId AND p.F_IsChild = 0 AND p.F_EnabledMark != 3
            ");

            return strSql.ToString();
        }
        /// <summary>
        /// 获取我的代办任务列表SQL语句
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        public async Task<string> GetMyTaskSql(UserEntity userInfo, bool isBatchAudit = false)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                                t.F_Id AS F_TaskId,
	                                t.F_Type AS F_TaskType,
	                                t.F_NodeName AS F_TaskName,
                                    t.F_IsUrge,
                                    t.F_ModifyDate as F_CreateDate,
                                    p.F_Id,
                                    p.F_SchemeId,
                                    p.F_SchemeCode,
                                    p.F_SchemeName,
                                    p.F_Title,
                                    p.F_Level,
                                    p.F_EnabledMark,
                                    p.F_IsAgain,
                                    p.F_IsFinished,
                                    p.F_IsChild,
                                    p.F_ParentTaskId,
                                    p.F_ParentProcessId,
                                    p.F_CreateUserId,
                                    p.F_CreateUserName,
                                    p.F_IsStart
                                FROM
	                                (
		                                SELECT
			                                F_TaskId
		                                FROM
			                                LR_NWF_TaskRelation r1
                                        LEFT JOIN LR_NWF_Task t1 ON r1.F_TaskId = t1.F_Id 
                                        WHERE r1.F_Mark = 0 AND r1.F_Result = 0 AND (r1.F_UserId  = @userId 
		                               ");


            // 添加委托信息
            IEnumerable<UserEntity> delegateList = await GetDelegateProcess(userInfo.F_UserId);
            foreach (var item in delegateList)
            {
                string processId = "'" + item.F_Description.Replace(",", "','") + "'";
                string userI2 = "'" + item.F_UserId + "'";

                strSql.Append("  OR (r1.F_UserId =" + userI2 + " AND t1.F_ProcessId in (" + processId + ") AND t1.F_Type != 2 )");
            }
            strSql.Append(@") GROUP BY
			                                F_TaskId
	                                ) r
                                LEFT JOIN LR_NWF_Task t ON t.F_Id = r.F_TaskId
                                LEFT JOIN LR_NWF_Process p ON p.F_Id = t.F_ProcessId
                                WHERE
	                                t.F_IsFinished = 0  AND (p.F_IsFinished = 0 OR t.F_Type = 2) AND p.F_EnabledMark != 3 ");

            if (isBatchAudit)
            {
                strSql.Append(" AND t.F_IsBatchAudit = 1 ");
            }

            return strSql.ToString();
        }
        /// <summary>
        /// 获取我的已办任务列表SQL语句
        /// </summary>
        /// <returns></returns>
        public string GetMyFinishTaskSql()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                    t.F_TaskId,
	                    t.F_TaskType,
	                    t.F_TaskName,
	                    t.F_CreateDate,
	                    p.F_Title,
	                    p.F_Id,
	                    p.F_SchemeId,
	                    p.F_SchemeCode,
	                    p.F_SchemeName,
	                    p.F_Level,
	                    p.F_EnabledMark,
	                    p.F_IsAgain,
	                    p.F_IsFinished,
	                    p.F_IsChild,
	                    p.F_ParentTaskId,
	                    p.F_ParentProcessId,
	                    p.F_CreateUserId,
	                    p.F_CreateUserName,
	                    p.F_IsStart
                    FROM
	                    (
		                    SELECT
			                    MAX(t.F_Id) AS F_TaskId,
			                    MAX(t.F_Type) AS F_TaskType,
			                    MAX(t.F_NodeName) AS F_TaskName,
			                    MAX(r.F_Time) AS F_CreateDate,
			                    t.F_ProcessId
		                    FROM
			                    LR_NWF_Task t
		                    LEFT JOIN LR_NWF_TaskRelation r ON r.F_TaskId = t.F_Id
		                    WHERE
			                    (
				                    r.F_Result = 1
				                    OR r.F_Result = 2
				                    OR r.F_Result = 4
			                    )
		                    AND r.F_UserId = @userId
		                    GROUP BY
			                    t.F_NodeId,F_ProcessId
	                    ) t
                    LEFT JOIN LR_NWF_Process p ON t.F_ProcessId = p.F_Id where F_EnabledMark != 3
                ");

            return strSql.ToString();
        }
        #endregion

        #endregion

        #region 保存信息
        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFProcessEntity">流程进程</param>
        /// <param name="taskList">流程任务列表</param>
        /// <param name="taskMsgList">流程消息列表</param>
        /// <param name="taskLogEntity">任务日志</param>
        /// <returns></returns>
        public async Task Save(NWFProcessEntity nWFProcessEntity, IEnumerable<NWFTaskEntity> taskList, IEnumerable<NWFTaskMsgEntity> taskMsgList, NWFTaskLogEntity taskLogEntity)
        {
            NWFProcessEntity nWFProcessEntityTmp = await this.BaseRepository().FindEntityByKey<NWFProcessEntity>(nWFProcessEntity.F_Id);
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (nWFProcessEntityTmp == null)
                {
                    await db.Insert(nWFProcessEntity);
                }
                else
                {
                    await db.Update(nWFProcessEntity);
                }
                foreach (var task in taskList)
                {
                    task.F_ModifyDate = DateTime.Now;
                    await db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null)
                    {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.F_Id = Guid.NewGuid().ToString();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            await db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }
                foreach (var taskMsg in taskMsgList)
                {
                    await db.Insert(taskMsg);
                }

                await db.Insert(taskLogEntity);

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存流程进程信息
        /// </summary>
        /// <param name="taskLogEntity">任务日志</param>
        /// <param name="taskRelationEntity">任务执行人状态更新</param>
        /// <param name="taskEntityUpdate">任务状态更新</param>
        /// <param name="processEntity">流程进程状态更新</param>
        /// <param name="confluenceList">会签信息</param>
        /// <param name="closeTaskList">会签需要关闭的任务</param>
        /// <param name="taskList">新的任务列表</param>
        /// <param name="taskMsgList">新的任务消息列表</param>
        /// <param name="pProcessEntity">父流程实例</param>
        /// <returns></returns>
        public async Task Save(NWFTaskLogEntity taskLogEntity, NWFTaskRelationEntity taskRelationEntity, NWFTaskEntity taskEntityUpdate, NWFProcessEntity processEntity, IEnumerable<NWFConfluenceEntity> confluenceList, IEnumerable<NWFTaskEntity> closeTaskList, IEnumerable<NWFTaskEntity> taskList, IEnumerable<NWFTaskMsgEntity> taskMsgList, NWFProcessEntity pProcessEntity = null)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (taskLogEntity.F_Des == "无审核人跳过" || taskLogEntity.F_Des == "系统自动审核")
                {
                    taskLogEntity.F_CreateDate = taskLogEntity.F_CreateDate.Value.AddMilliseconds(10);
                }

                await db.Insert(taskLogEntity);
                if (taskRelationEntity != null) {
                    await db.Update(taskRelationEntity);
                }
               
                await db.Update(taskEntityUpdate);

                if (processEntity != null)
                {
                    await db.Update(processEntity);
                }

                if (pProcessEntity != null)
                {
                    await db.Update(pProcessEntity);
                }

                if (confluenceList != null)
                {
                    foreach (var item in confluenceList)
                    {
                        if (item.isClear)
                        {
                            string processId = item.F_ProcessId;
                            string nodeId = item.F_NodeId;
                            await db.DeleteAny<NWFConfluenceEntity>(new { F_ProcessId = processId, F_NodeId = nodeId });
                            // 增加一条会签审核记录
                            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
                            {
                                F_Id = Guid.NewGuid().ToString(),
                                F_ProcessId = processId,
                                F_OperationCode = "confluence",
                                F_OperationName = "会签" + (item.confluenceRes == 1 ? "通过" : "不通过"),
                                F_NodeId = item.F_NodeId,
                                F_TaskType = 7,
                                F_CreateDate = DateTime.Now
                            };
                            await db.Insert(nWFTaskLogEntity);
                        }
                        else
                        {
                            await db.Insert(item);
                        }
                    }
                }

                if (closeTaskList != null)
                {
                    foreach (var item in closeTaskList)
                    {
                        await db.Update(item);
                    }
                }

                foreach (var task in taskList)
                {
                    task.F_ModifyDate = DateTime.Now;
                    await db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null)
                    {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.F_Id = Guid.NewGuid().ToString();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            await db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }
                foreach (var taskMsg in taskMsgList)
                {
                    await db.Insert(taskMsg);
                }



                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFProcessEntity">流程进程</param>
        /// <returns></returns>
        public async Task Save(NWFProcessEntity nWFProcessEntity)
        {
            await this.BaseRepository().Insert(nWFProcessEntity);
        }
        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFTaskLogEntity">任务日志数据</param>
        /// <param name="taskUserUpdateList">任务执行人需要更新状态数据</param>
        /// <param name="nWFTaskMsgEntity">任务消息</param>
        public async Task Save(NWFTaskLogEntity nWFTaskLogEntity, IEnumerable<NWFTaskRelationEntity> taskUserUpdateList, NWFTaskMsgEntity nWFTaskMsgEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.Insert(nWFTaskLogEntity);

                foreach (var item in taskUserUpdateList)
                {
                    await db.Update(item);
                }
                await db.Insert(nWFTaskMsgEntity);
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFTaskLogEntity">任务日志数据</param>
        /// <param name="nWFTaskRelationEntity">任务执行人需要更新状态数据</param>
        /// <param name="taskEntity">任务</param>
        public async Task Save(NWFTaskLogEntity nWFTaskLogEntity, NWFTaskRelationEntity nWFTaskRelationEntity, NWFTaskEntity taskEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.Insert(nWFTaskLogEntity);
                await db.Update(nWFTaskRelationEntity);
                if (taskEntity != null)
                {
                    taskEntity.F_ModifyDate = DateTime.Now;
                    await db.Update(taskEntity);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFTaskLogEntity">任务日志数据</param>
        /// <param name="taskList">需要更新的任务列表</param>
        /// <param name="taskMsgList">任务消息列表</param>
        public async Task Save(NWFTaskLogEntity nWFTaskLogEntity, IEnumerable<NWFTaskEntity> taskList, IEnumerable<NWFTaskMsgEntity> taskMsgList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.Insert(nWFTaskLogEntity);
                foreach (var item in taskList)
                {
                    item.F_ModifyDate = DateTime.Now;
                    await db.Update(item);
                }
                foreach (var item in taskMsgList)
                {
                    await db.Insert(item);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFTaskLogEntity">任务日志数据</param>
        /// <param name="task">需要更新的任务</param>
        /// <param name="taskMsgList">任务消息列表</param>
        public async Task Save(NWFTaskLogEntity nWFTaskLogEntity, NWFTaskEntity task, IEnumerable<NWFTaskMsgEntity> taskMsgList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.Insert(nWFTaskLogEntity);
                task.F_ModifyDate = DateTime.Now;
                await db.Update(task);
                foreach (var item in taskMsgList)
                {
                    await db.Insert(item);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 保存流程进程信息
        /// </summary>
        /// <param name="pTaskLogEntity"></param>
        /// <param name="pTaskRelationEntity"></param>
        /// <param name="pTaskEntityUpdate"></param>
        /// <param name="pProcessEntity"></param>
        /// <param name="pTaskList"></param>
        /// <param name="pTaskMsgList"></param>
        /// <param name="nWFProcessEntity"></param>
        /// <param name="taskList"></param>
        /// <param name="taskMsgList"></param>
        /// <param name="taskLogEntity"></param>
        /// <returns></returns>
        public async Task Save(NWFTaskLogEntity pTaskLogEntity, NWFTaskRelationEntity pTaskRelationEntity, NWFTaskEntity pTaskEntityUpdate, NWFProcessEntity pProcessEntity, IEnumerable<NWFTaskEntity> pTaskList, IEnumerable<NWFTaskMsgEntity> pTaskMsgList, NWFProcessEntity nWFProcessEntity, IEnumerable<NWFTaskEntity> taskList, IEnumerable<NWFTaskMsgEntity> taskMsgList, NWFTaskLogEntity taskLogEntity)
        {
            NWFProcessEntity nWFProcessEntityTmp = await this.BaseRepository().FindEntityByKey<NWFProcessEntity>(nWFProcessEntity.F_Id);
            IEnumerable<NWFTaskEntity> uTaskList = await this.BaseRepository().FindList<NWFTaskEntity>(" select * from lr_nwf_task where F_ProcessId =@processId AND F_NodeId =@nodeId AND F_IsFinished = 0  ",new { processId = nWFProcessEntity.F_Id, nodeId = taskLogEntity.F_NodeId });
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (nWFProcessEntityTmp == null)
                {
                    await db.Insert(nWFProcessEntity);
                }
                else
                {
                    await db.Update(nWFProcessEntity);
                }
                foreach (var task in taskList)
                {
                    task.F_ModifyDate = DateTime.Now;
                    await db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null)
                    {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.F_Id = Guid.NewGuid().ToString();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            await db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }
                foreach (var taskMsg in taskMsgList)
                {
                    await db.Insert(taskMsg);
                }

                await db.Insert(taskLogEntity);
                foreach (var item in uTaskList)
                {
                    item.F_IsFinished = 1;
                    await db.Update(item);
                }


                // 父流程
                await db.Insert(pTaskLogEntity);
                await db.Update(pTaskRelationEntity);
                await db.Update(pTaskEntityUpdate);
                if (pProcessEntity != null)
                {
                    await db.Update(pProcessEntity);
                }

                foreach (var task in pTaskList)
                {
                    task.F_ModifyDate = DateTime.Now;
                    await db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null)
                    {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.F_Id = Guid.NewGuid().ToString();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            await db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }
                foreach (var taskMsg in pTaskMsgList)
                {
                    await db.Insert(taskMsg);
                }

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// （流程撤销）
        /// </summary>
        /// <param name="processId">流程进程实例</param>
        /// <param name="taskList">流程任务列表</param>
        /// <param name="EnabledMark">2草稿3作废</param>
        /// <param name="nWFTaskLogEntity">流程任务日志实体</param>
        public async Task Save(string processId, IEnumerable<NWFTaskEntity> taskList, int EnabledMark, NWFTaskLogEntity nWFTaskLogEntity = null)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                NWFProcessEntity nWFProcessEntity = new NWFProcessEntity();
                nWFProcessEntity.F_Id = processId;
                nWFProcessEntity.F_EnabledMark = EnabledMark;
                await db.Update(nWFProcessEntity);
                if (EnabledMark == 2)
                {
                    await db.DeleteAny<NWFTaskLogEntity>(new { F_ProcessId = processId });
                }
                foreach (var task in taskList)
                {
                    await db.Delete(task);
                    string taskId = task.F_Id;
                    await db.DeleteAny<NWFTaskMsgEntity>(new { F_TaskId = taskId });
                    await db.DeleteAny<NWFTaskRelationEntity>(new { F_TaskId = taskId });
                }
                if (nWFTaskLogEntity != null)
                {
                    await db.Insert(nWFTaskLogEntity);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 删除流程进程实体
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        public async Task DeleteEntity(string processId)
        {
            await this.BaseRepository().DeleteAny<NWFProcessEntity>( new { F_Id = processId });
        }

        /// <summary>
        /// 删除流程进程所有信息（流程撤销）
        /// </summary>
        /// <param name="processId">流程进程实例</param>
        /// <param name="taskList">流程任务列表</param>
        public async Task Delete(string processId, IEnumerable<NWFTaskEntity> taskList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<NWFProcessEntity>(new { F_Id = processId });
                await db.DeleteAny<NWFTaskLogEntity>(new { F_ProcessId = processId });
                foreach (var task in taskList)
                {
                    await db.Delete(task);
                    string taskId = task.F_Id;
                    await db.DeleteAny<NWFTaskMsgEntity>(new { F_TaskId = taskId });
                    await db.DeleteAny<NWFTaskRelationEntity>(new { F_TaskId = taskId });
                }

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 撤销审核
        /// </summary>
        /// <param name="taskList">需要撤回的任务节点</param>
        /// <param name="taskUser">当前处理人</param>
        /// <param name="taskEntity">当前任务</param>
        /// <param name="taskLogEntity">日志信息</param>
        /// <param name="taskUserNew">当前任务节点的处理人（串行多人审核）</param>
        public async Task RevokeAudit(IEnumerable<string> taskList, NWFTaskRelationEntity taskUser, NWFTaskEntity taskEntity, NWFTaskLogEntity taskLogEntity, NWFTaskRelationEntity taskUserNew = null)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (taskList != null)
                {
                    foreach (var taskId in taskList)
                    {
                        await db.DeleteAny<NWFTaskEntity>(new { F_Id = taskId });
                        await db.DeleteAny<NWFTaskRelationEntity>(new { F_TaskId = taskId });
                        await db.DeleteAny<NWFTaskMsgEntity>(new { F_TaskId = taskId });
                    }

                }

                if (taskEntity != null)
                {
                    await db.Update(taskEntity);
                }

                taskUser.F_Mark = 0;
                taskUser.F_Result = 0;
                await db.Update(taskUser);

                await db.Insert(taskLogEntity);

                if (taskUserNew != null)
                {
                    taskUserNew.F_Mark = 1;
                    taskUserNew.F_Result = 0;
                    await db.Update(taskUserNew);
                }


                // 更新下流程实例（处理重新发起状态）
                NWFProcessEntity nWFProcessEntity = new NWFProcessEntity();
                nWFProcessEntity.F_Id = taskLogEntity.F_ProcessId;
                nWFProcessEntity.F_IsAgain = 0;
                await db.Update(nWFProcessEntity);

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 保存任务
        /// </summary>
        /// <param name="taskList">任务列表</param>
        public async Task SaveTask(List<NWFTaskEntity> taskList)
        {
            var db = this.BaseRepository().BeginTrans();

            try
            {
                foreach (var task in taskList)
                {
                    task.F_ModifyDate = DateTime.Now;
                    await db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null)
                    {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.F_Id = Guid.NewGuid().ToString();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            await db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }

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
