using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using aycsoft.workflow;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class NWFProcessBLL :BLLBase, NWFProcessIBLL, BLL
    {
        private NWFProcessSericve nWFProcessSerive = new NWFProcessSericve();

        private readonly NWFSchemeIBLL _nWFSchemeIBLL;
        private readonly NWFTaskIBLL _nWFTaskIBLL;
        private readonly NWFConfluenceIBLL _nWFConfluenceIBLL;
        // 消息模块
        private readonly MStrategyInfoIBLL _mStrategyInfoIBLL;
        private readonly ImgIBLL _imgIBLL;

        private readonly UserIBLL _userIBLL;
        private readonly UserRelationIBLL _userRelationIBLL;
        private readonly PostIBLL _postIBLL;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="nWFSchemeIBLL"></param>
        /// <param name="nWFTaskIBLL"></param>
        /// <param name="nWFConfluenceIBLL"></param>
        /// <param name="mStrategyInfoIBLL"></param>
        /// <param name="imgIBLL"></param>
        /// <param name="userIBLL"></param>
        /// <param name="userRelationIBLL"></param>
        /// <param name="postIBLL"></param>
        public NWFProcessBLL(NWFSchemeIBLL nWFSchemeIBLL, NWFTaskIBLL nWFTaskIBLL, NWFConfluenceIBLL nWFConfluenceIBLL, MStrategyInfoIBLL mStrategyInfoIBLL, ImgIBLL imgIBLL, UserIBLL userIBLL, UserRelationIBLL userRelationIBLL, PostIBLL postIBLL) {
            _nWFSchemeIBLL = nWFSchemeIBLL;
            _nWFTaskIBLL = nWFTaskIBLL;
            _nWFConfluenceIBLL = nWFConfluenceIBLL;
            _mStrategyInfoIBLL = mStrategyInfoIBLL;
            _imgIBLL = imgIBLL;
            _userIBLL = userIBLL;
            _userRelationIBLL = userRelationIBLL;
            _postIBLL = postIBLL;
        }

        #region 获取数据
        /// <summary>
        /// 获取流程进程实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<NWFProcessEntity> GetEntity(string keyValue)
        {
            return nWFProcessSerive.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFProcessEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return nWFProcessSerive.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyPageList(Pagination pagination, string queryJson, string schemeCode = null)
        {
            var userInfo = await this.CurrentUser();
            return await nWFProcessSerive.GetMyPageList(pagination, queryJson, schemeCode, userInfo.F_UserId);
        }
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyPageList(string queryJson, string schemeCode = null)
        {
            var userInfo = await this.CurrentUser();
            return await nWFProcessSerive.GetMyPageList(queryJson, schemeCode, userInfo.F_UserId);
        }
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(Pagination pagination, string queryJson, string schemeCode = null)
        {
            var userInfo = await this.CurrentUser();
            return await nWFProcessSerive.GetMyTaskPageList(userInfo, pagination, queryJson, schemeCode);
        }
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(string queryJson, string schemeCode = null)
        {
            var userInfo = await this.CurrentUser();
            return await nWFProcessSerive.GetMyTaskPageList(userInfo, queryJson, schemeCode);
        }
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(Pagination pagination, string queryJson, bool isBatchAudit, string schemeCode = null)
        {
            var userInfo = await this.CurrentUser();
            return await nWFProcessSerive.GetMyTaskPageList(userInfo, pagination, queryJson, schemeCode, isBatchAudit);
        }
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyTaskPageList(string queryJson, bool isBatchAudit, string schemeCode = null)
        {
            var userInfo = await this.CurrentUser();
            return await nWFProcessSerive.GetMyTaskPageList(userInfo, queryJson, schemeCode, isBatchAudit);
        }
        /// <summary>
        /// 获取已处理任务列表
        /// </summary>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyFinishTaskPageList(Pagination pagination, string queryJson, string schemeCode = null)
        {
            var userInfo = await this.CurrentUser();
            return await nWFProcessSerive.GetMyFinishTaskPageList(pagination, queryJson, schemeCode, userInfo);
        }
        /// <summary>
        /// 获取已处理任务列表
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFProcessEntity>> GetMyFinishTaskPageList(string queryJson, string schemeCode = null)
        {
            var userInfo = await this.CurrentUser();
            return await nWFProcessSerive.GetMyFinishTaskPageList(queryJson, schemeCode, userInfo);
        }
        #endregion

        #region 保存更新删除
        /// <summary>
        /// 删除流程进程实体
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        public async Task DeleteEntity(string processId)
        {
            await nWFProcessSerive.DeleteEntity(processId);
        }

        #endregion

        #region 流程API

        #region 委托方法
        /// <summary>
        /// 获取审核通过数
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public async Task<int> GetAgreeNum(string processId, string nodeId)
        {
            List<NWFConfluenceEntity> list = (List<NWFConfluenceEntity>)await _nWFConfluenceIBLL.GetList(processId, nodeId);
            return list.FindAll(t => t.F_State == 1).Count;
        }
        /// <summary>
        /// 获取审核不通过数
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public async Task<int> GetDisAgreeNum(string processId, string nodeId)
        {
            List<NWFConfluenceEntity> list = (List<NWFConfluenceEntity>)await _nWFConfluenceIBLL.GetList(processId, nodeId);
            return list.FindAll(t => t.F_State == 0).Count;
        }
        /// <summary>
        /// 查询sql方法
        /// </summary>
        /// <param name="dbCode">数据库编码</param>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<DataTable> FindDbTable(string dbCode, string sql, object param)
        {
            return nWFProcessSerive.BaseRepository(dbCode).FindTable(sql, param);
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 流程模板初始化
        /// </summary>
        /// <param name="code"></param>
        /// <param name="processId"></param>
        /// <param name="taskId"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private async Task<NWFIEngine> _Bootstraper(string code, string processId, string taskId, UserEntity userInfo)
        {
            NWFIEngine wfIEngine = null;
            NWFEngineConfig nWFEngineConfig = new NWFEngineConfig();
            NWFEngineParamConfig nWFEngineParamConfig = new NWFEngineParamConfig();
            nWFEngineConfig.ParamConfig = nWFEngineParamConfig;

            if (userInfo != null)
            {
                nWFEngineParamConfig.CurrentUser = new NWFUserInfo()
                {
                    Id = userInfo.F_UserId,
                    Account = userInfo.F_Account,
                    Name = userInfo.F_RealName,
                    CompanyId = userInfo.F_CompanyId,
                    DepartmentId = userInfo.F_DepartmentId
                };
            }

            if (!string.IsNullOrEmpty(code))
            {
                var schemeInfo =await _nWFSchemeIBLL.GetInfoEntityByCode(code);
                if (schemeInfo != null)
                {
                    var data =await _nWFSchemeIBLL.GetSchemeEntity(schemeInfo.F_SchemeId);
                    if (data != null)
                    {
                        nWFEngineParamConfig.Scheme = data.F_Content;
                        nWFEngineParamConfig.SchemeCode = code;
                        nWFEngineParamConfig.SchemeId = schemeInfo.F_SchemeId;
                        nWFEngineParamConfig.SchemeName = schemeInfo.F_Name;
                        nWFEngineParamConfig.ProcessId = processId;
                        nWFEngineParamConfig.HasInstance = false;

                        nWFEngineParamConfig.CreateUser = nWFEngineParamConfig.CurrentUser;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(processId))
            {
                var processEntity =await GetEntity(processId);
                if (processEntity != null)
                {
                    if (string.IsNullOrEmpty(processEntity.F_SchemeId))// 这种情况出现在草稿下
                    {
                        var schemeInfo =await _nWFSchemeIBLL.GetInfoEntityByCode(processEntity.F_SchemeCode);
                        if (schemeInfo != null)
                        {
                            var data =await _nWFSchemeIBLL.GetSchemeEntity(schemeInfo.F_SchemeId);
                            if (data != null)
                            {
                                nWFEngineParamConfig.Scheme = data.F_Content;
                                nWFEngineParamConfig.SchemeCode = processEntity.F_SchemeCode;
                                nWFEngineParamConfig.SchemeId = schemeInfo.F_SchemeId;
                                nWFEngineParamConfig.SchemeName = schemeInfo.F_Name;
                                nWFEngineParamConfig.ProcessId = processId;
                                nWFEngineParamConfig.HasInstance = true;

                                nWFEngineParamConfig.CreateUser = nWFEngineParamConfig.CurrentUser;
                            }
                        }
                    }
                    else
                    {
                        var data =await _nWFSchemeIBLL.GetSchemeEntity(processEntity.F_SchemeId);
                        if (data != null)
                        {
                            nWFEngineParamConfig.Scheme = data.F_Content;
                            nWFEngineParamConfig.SchemeCode = processEntity.F_SchemeCode;
                            nWFEngineParamConfig.SchemeId = processEntity.F_SchemeId;
                            nWFEngineParamConfig.SchemeName = processEntity.F_SchemeName;
                            nWFEngineParamConfig.IsChild = (int)processEntity.F_IsChild;
                            nWFEngineParamConfig.ParentProcessId = processEntity.F_ParentProcessId;
                            nWFEngineParamConfig.ParentTaskId = processEntity.F_ParentTaskId;
                            nWFEngineParamConfig.ProcessId = processId;
                            nWFEngineParamConfig.HasInstance = true;

                            UserEntity userEntity =await _userIBLL.GetEntity(processEntity.F_CreateUserId);

                            nWFEngineParamConfig.CreateUser = new NWFUserInfo()
                            {
                                Id = processEntity.F_CreateUserId,
                                Account = userEntity.F_Account,
                                Name = userEntity.F_RealName,
                                CompanyId = userEntity.F_CompanyId,
                                DepartmentId = userEntity.F_DepartmentId
                            };
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(taskId))
            {
                var taskEntiy =await _nWFTaskIBLL.GetEntity(taskId);
                if (taskEntiy != null)
                {
                    var processEntity =await GetEntity(taskEntiy.F_ProcessId);
                    if (processEntity != null)
                    {
                        var data =await _nWFSchemeIBLL.GetSchemeEntity(processEntity.F_SchemeId);
                        if (data != null)
                        {
                            nWFEngineParamConfig.Scheme = data.F_Content;
                            nWFEngineParamConfig.SchemeCode = processEntity.F_SchemeCode;
                            nWFEngineParamConfig.SchemeId = processEntity.F_SchemeId;
                            nWFEngineParamConfig.SchemeName = processEntity.F_SchemeName;
                            nWFEngineParamConfig.IsChild = (int)processEntity.F_IsChild;
                            nWFEngineParamConfig.ParentProcessId = processEntity.F_ParentProcessId;
                            nWFEngineParamConfig.ParentTaskId = processEntity.F_ParentTaskId;
                            nWFEngineParamConfig.ProcessId = processEntity.F_Id;
                            nWFEngineParamConfig.HasInstance = true;

                            UserEntity userEntity =await _userIBLL.GetEntity(processEntity.F_CreateUserId);

                            nWFEngineParamConfig.CreateUser = new NWFUserInfo()
                            {
                                Id = processEntity.F_CreateUserId,
                                Account = userEntity.F_Account,
                                Name = userEntity.F_RealName,
                                CompanyId = userEntity.F_CompanyId,
                                DepartmentId = userEntity.F_DepartmentId
                            };
                        }
                    }
                }
            }



            // 注册委托方法
            nWFEngineConfig.DbFindTable = FindDbTable;
            nWFEngineConfig.GetAgreeNum = GetAgreeNum;
            nWFEngineConfig.GetDisAgreeNum = GetDisAgreeNum;

            wfIEngine = new NWFEngine(nWFEngineConfig);
            return wfIEngine;
        }
        /// <summary>
        /// 获取节点处理人列表
        /// </summary>
        /// <param name="nodeAuditorList"></param>
        /// <param name="nodeInfo"></param>
        /// <param name="paramConfig"></param>
        /// <returns></returns>
        private async Task<IEnumerable<NWFUserInfo>> _GetNodeAuditors(List<NWFAuditor> nodeAuditorList, NWFNodeInfo nodeInfo, NWFEngineParamConfig paramConfig)
        {
            List<NWFUserInfo> list = new List<NWFUserInfo>();

            List<NWFUserInfo> list2 =(List<NWFUserInfo>)await _GetNodeAuditors2(nodeAuditorList, nodeInfo, paramConfig);

            // 判断当前节点之前是否有过审核，如果有就用之前的审核人
            NWFTaskEntity taskEntiy = null;

            if (!string.IsNullOrEmpty(paramConfig.ProcessId))
            {
                taskEntiy =await _nWFTaskIBLL.GetEntityByNodeId(nodeInfo.id, paramConfig.ProcessId);
                if (taskEntiy != null)
                {
                    if (taskEntiy.F_IsFinished == 0)
                    {
                        return list;
                    }

                    List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(taskEntiy.F_Id);

                    if (nodeInfo.type == "stepnode" && nodeInfo.isAllAuditor == "2")
                    {
                        List<NWFTaskRelationEntity> taskUserList2;
                        // 如果普通节点设置了所有人都需要审核的情况
                        if (nodeInfo.auditorAgainType == "1")
                        {
                            //审核通过的人不需要再审核:获取未审核通过的人。但是在大家都通过的情况下就获取所有人
                            taskUserList2 = taskUserList.FindAll(t => t.F_Result != 1);
                            if (taskUserList2.Count == 0)
                            {
                                taskUserList2 = taskUserList;
                            }
                        }
                        else
                        {
                            taskUserList2 = taskUserList;
                        }
                        if (taskUserList2.Count > 0)
                        {
                            foreach (var item in taskUserList2)
                            {
                                UserEntity taskUserEntity =await _userIBLL.GetEntity(item.F_UserId);
                                if (taskUserEntity != null)
                                {
                                    list.Add(new NWFUserInfo()
                                    {
                                        Id = taskUserEntity.F_UserId,
                                        Account = taskUserEntity.F_Account,
                                        Name = taskUserEntity.F_RealName
                                    });
                                }
                            }
                            return list;
                        }
                    }
                    else
                    {
                        NWFTaskRelationEntity taskUser = taskUserList.Find(t => t.F_Result != 0 && t.F_Result != 3);
                        if (taskUser != null)
                        {

                            UserEntity taskUserEntity =await _userIBLL.GetEntity(taskUser.F_UserId);
                            if (taskUserEntity != null)
                            {
                                list.Add(new NWFUserInfo()
                                {
                                    Id = taskUserEntity.F_UserId,
                                    Account = taskUserEntity.F_Account,
                                    Name = taskUserEntity.F_RealName
                                });
                                string _userId = taskUserEntity.F_UserId;

                                if (list2.Find(t => t.Id == _userId) == null)
                                {
                                    if (list2.Count == 0)
                                    {// 未找到审核人，默认成系统管理员
                                        if (nodeInfo.noPeopleGz == 3)
                                        {
                                            throw (new Exception("下一节点没有审核人,无法提交！"));
                                        }
                                        // 如果找不到审核人就默认超级管理员才能审核
                                        var adminEntityList =await _userIBLL.GetAdminList();
                                        foreach (var item in adminEntityList)
                                        {
                                            list2.Add(new NWFUserInfo()
                                            {
                                                Id = item.F_UserId,
                                                Account = item.F_Account,
                                                Name = item.F_RealName,
                                                noPeople = true
                                            });
                                        }
                                    }
                                    return list2;
                                }
                                return list;
                            }
                        }
                    }
                }
            }

            list.AddRange(list2);

            if (list.Count == 0)
            {// 未找到审核人，默认成系统管理员
                if (nodeInfo.noPeopleGz == 3)
                {
                    throw (new Exception("下一节点没有审核人,无法提交！"));
                }
                // 如果找不到审核人就默认超级管理员才能审核
                var adminEntityList =await _userIBLL.GetAdminList();
                foreach (var item in adminEntityList)
                {
                    list.Add(new NWFUserInfo()
                    {
                        Id = item.F_UserId,
                        Account = item.F_Account,
                        Name = item.F_RealName,
                        noPeople = true
                    });
                }
            }

            return list;
        }

        private async Task<IEnumerable<NWFUserInfo>> _GetNodeAuditors2(List<NWFAuditor> nodeAuditorList, NWFNodeInfo nodeInfo, NWFEngineParamConfig paramConfig)
        {
            List<NWFUserInfo> list = new List<NWFUserInfo>();

            if (nodeAuditorList == null)
            {
                // 开始节点的情况
                list.Add(new NWFUserInfo()
                {
                    Id = paramConfig.CreateUser.Id,
                    Account = paramConfig.CreateUser.Account,
                    Name = paramConfig.CreateUser.Name
                });

                return list;
            }

            if (nodeAuditorList.Count == 0)
            {
                // 如果找不到审核人就默认超级管理员才能审核
                var adminEntityList =await _userIBLL.GetAdminList();
                foreach (var item in adminEntityList)
                {
                    list.Add(new NWFUserInfo()
                    {
                        Id = item.F_UserId,
                        Account = item.F_Account,
                        Name = item.F_RealName
                    });
                }
            }
            else
            {
                foreach (var item in nodeAuditorList)
                {
                    switch (item.type)//1.岗位2.角色3.用户4.上下级5.表单指定字段6.某一个节点执行人
                    {
                        case 1:// 岗位
                            var userRelationList =await _userRelationIBLL.GetUserIdList(item.auditorId);
                            foreach (var userRelation in userRelationList)
                            {
                                var userEntity =await _userIBLL.GetEntity(userRelation.F_UserId);
                                if (userEntity != null)
                                {
                                    if (item.condition == 1)
                                    {
                                        if (userEntity.F_DepartmentId == paramConfig.CreateUser.DepartmentId)
                                        {
                                            list.Add(new NWFUserInfo()
                                            {
                                                Id = userEntity.F_UserId,
                                                Account = userEntity.F_Account,
                                                Name = userEntity.F_RealName
                                            });
                                        }
                                    }
                                    else if (item.condition == 2)
                                    {
                                        if (userEntity.F_CompanyId == paramConfig.CreateUser.CompanyId)
                                        {
                                            list.Add(new NWFUserInfo()
                                            {
                                                Id = userEntity.F_UserId,
                                                Account = userEntity.F_Account,
                                                Name = userEntity.F_RealName
                                            });
                                        }
                                    }
                                    else
                                    {
                                        list.Add(new NWFUserInfo()
                                        {
                                            Id = userEntity.F_UserId,
                                            Account = userEntity.F_Account,
                                            Name = userEntity.F_RealName
                                        });
                                    }
                                }
                            }
                            break;
                        case 2:// 角色
                            var userRelationList2 =await _userRelationIBLL.GetUserIdList(item.auditorId);
                            foreach (var userRelation in userRelationList2)
                            {
                                NWFAuditor wfAuditor = new NWFAuditor();
                                var userEntity =await _userIBLL.GetEntity(userRelation.F_UserId);
                                if (userEntity != null)
                                {
                                    if (item.condition == 1)
                                    {
                                        if (userEntity.F_DepartmentId == paramConfig.CreateUser.DepartmentId)
                                        {
                                            list.Add(new NWFUserInfo()
                                            {
                                                Id = userEntity.F_UserId,
                                                Account = userEntity.F_Account,
                                                Name = userEntity.F_RealName
                                            });
                                        }
                                    }
                                    else if (item.condition == 2)
                                    {
                                        if (userEntity.F_CompanyId == paramConfig.CreateUser.CompanyId)
                                        {
                                            list.Add(new NWFUserInfo()
                                            {
                                                Id = userEntity.F_UserId,
                                                Account = userEntity.F_Account,
                                                Name = userEntity.F_RealName
                                            });
                                        }
                                    }
                                    else if (item.condition == 3) // 需要存在上级关系
                                    {
                                        // 获取当前用户的岗位
                                        var postList1 = await _userRelationIBLL.GetObjectIds(paramConfig.CreateUser.Id, 2);// 发起人岗位
                                        var postList2 = await _userRelationIBLL.GetObjectIds(userEntity.F_UserId, 2);// 节点审核人岗位


                                        if (await _postIBLL.IsUp(postList1, postList2))
                                        {
                                            list.Add(new NWFUserInfo()
                                            {
                                                Id = userEntity.F_UserId,
                                                Account = userEntity.F_Account,
                                                Name = userEntity.F_RealName
                                            });
                                        }
                                    }
                                    else if (item.condition == 4) // 需要存在下级关系
                                    {
                                        // 获取当前用户的岗位
                                        var postList1 = await _userRelationIBLL.GetObjectIds(paramConfig.CreateUser.Id, 2);// 发起人岗位
                                        var postList2 = await _userRelationIBLL.GetObjectIds(userEntity.F_UserId, 2);// 节点审核人岗位

                                        if (await _postIBLL.IsDown(postList1, postList2))
                                        {
                                            list.Add(new NWFUserInfo()
                                            {
                                                Id = userEntity.F_UserId,
                                                Account = userEntity.F_Account,
                                                Name = userEntity.F_RealName
                                            });
                                        }
                                    }
                                    else
                                    {
                                        list.Add(new NWFUserInfo()
                                        {
                                            Id = userEntity.F_UserId,
                                            Account = userEntity.F_Account,
                                            Name = userEntity.F_RealName
                                        });
                                    }
                                }
                            }
                            break;
                        case 3:// 用户
                            if (!string.IsNullOrEmpty(item.auditorId))
                            {
                                string[] userList = item.auditorId.Split(',');
                                foreach (var userItem in userList)
                                {
                                    var userEntity3 = await _userIBLL.GetEntity(userItem);
                                    if (userEntity3 != null)
                                    {
                                        list.Add(new NWFUserInfo()
                                        {
                                            Id = userEntity3.F_UserId,
                                            Account = userEntity3.F_Account,
                                            Name = userEntity3.F_RealName
                                        });
                                    }
                                }
                            }

                            break;
                        case 4:// 上下级 上1-5；下6-10
                            var postId =await _userRelationIBLL.GetObjectIds(paramConfig.CreateUser.Id, 2);// 发起人岗位
                            int level = Convert.ToInt32(item.auditorId);
                            IEnumerable<string> postList;
                            if (level < 6)
                            {
                                postList = await _postIBLL.GetUpIdList(postId, level);
                            }
                            else
                            {
                                level = level - 5;
                                postList = await _postIBLL.GetDownIdList(postId, level);
                            }
                            var userRelationList4 =await _userRelationIBLL.GetUserIdList(postList);
                            foreach (var userRelationItem in userRelationList4)
                            {
                                NWFAuditor wfAuditor = new NWFAuditor();
                                var userEntity = await _userIBLL.GetEntity(userRelationItem.F_UserId);
                                if (userEntity != null)
                                {
                                    list.Add(new NWFUserInfo()
                                    {
                                        Id = userEntity.F_UserId,
                                        Account = userEntity.F_Account,
                                        Name = userEntity.F_RealName
                                    });
                                }
                            }
                            break;
                        case 5:// 表单指定字段
                               // 获取对应的表单数据dbId,table,relationId,id 数据库主键/表/关联字段/审核人字段
                            if (!string.IsNullOrEmpty(item.auditorId))
                            {
                                string[] idList = item.auditorId.Split('|');
                                if (idList.Length == 4)
                                {
                                    string dbId = idList[0];
                                    string table = idList[1];
                                    string relationId = idList[2];
                                    string id = idList[3];

                                    string sql = "select " + id + " from " + table + " where " + relationId + " ='" + paramConfig.ProcessId + "'";
                                    DataTable dt =await nWFProcessSerive.BaseRepository(dbId).FindTable(sql);
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        var userEntity5 =await _userIBLL.GetEntity(row[0].ToString());
                                        if (userEntity5 != null)
                                        {
                                            list.Add(new NWFUserInfo()
                                            {
                                                Id = userEntity5.F_UserId,
                                                Account = userEntity5.F_Account,
                                                Name = userEntity5.F_RealName
                                            });
                                        }
                                    }
                                }
                            }
                            break;
                        case 6:// 某一个节点执行人
                            var task = await _nWFTaskIBLL.GetLogEntityByNodeId(item.auditorId, paramConfig.ProcessId);
                            if (task != null && !string.IsNullOrEmpty(task.F_CreateUserId))
                            {
                                var userEntity6 =await _userIBLL.GetEntity(task.F_CreateUserId);
                                if (userEntity6 != null)
                                {
                                    list.Add(new NWFUserInfo()
                                    {
                                        Id = userEntity6.F_UserId,
                                        Account = userEntity6.F_Account,
                                        Name = userEntity6.F_RealName
                                    });
                                }
                            }
                            break;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 创建流程任务
        /// </summary>
        /// <param name="nodeList">节点信息</param>
        /// <param name="currentNodeInfo">当前节点信息</param>
        /// <param name="paramConfig">流程配置信息</param>
        /// <returns></returns>
        private async Task<IEnumerable<NWFTaskEntity>> _CreateTask(IEnumerable<NWFNodeInfo> nodeList, NWFNodeInfo currentNodeInfo, NWFEngineParamConfig paramConfig)
        {
            List<NWFTaskEntity> list = new List<NWFTaskEntity>();
            foreach (var node in nodeList)
            {
                NWFTaskEntity nWFTaskEntity = new NWFTaskEntity();
                nWFTaskEntity.F_Id = Guid.NewGuid().ToString();
                nWFTaskEntity.F_IsFinished = 0;
                nWFTaskEntity.F_IsUrge = 0;
                nWFTaskEntity.F_CreateDate = DateTime.Now;

                nWFTaskEntity.F_ProcessId = paramConfig.ProcessId;
                nWFTaskEntity.F_NodeId = node.id;
                nWFTaskEntity.F_NodeName = node.name;
                nWFTaskEntity.F_PrevNodeId = currentNodeInfo.id;
                nWFTaskEntity.F_PrevNodeName = currentNodeInfo.name;

                nWFTaskEntity.F_CreateUserId = paramConfig.CurrentUser.Id;
                nWFTaskEntity.F_CreateUserName = paramConfig.CurrentUser.Name;

                if (!string.IsNullOrEmpty(node.timeoutAction))
                {
                    nWFTaskEntity.F_TimeoutAction = Convert.ToInt32(node.timeoutAction);
                }
                if (!string.IsNullOrEmpty(node.timeoutInterval))
                {
                    nWFTaskEntity.F_TimeoutInterval = Convert.ToInt32(node.timeoutInterval);
                }
                if (!string.IsNullOrEmpty(node.timeoutNotice))
                {
                    nWFTaskEntity.F_TimeoutNotice = Convert.ToInt32(node.timeoutNotice);
                }
                if (!string.IsNullOrEmpty(node.timeoutStrategy))
                {
                    nWFTaskEntity.F_TimeoutStrategy = node.timeoutStrategy;
                }
                nWFTaskEntity.nWFUserInfoList =(List<NWFUserInfo>)await _GetNodeAuditors(node.auditors, node, paramConfig);

                switch (node.type)
                {
                    case "stepnode":// 审核节点
                        nWFTaskEntity.F_Type = 1;
                        nWFTaskEntity.F_IsBatchAudit = node.isBatchAudit;
                        if (node.isAllAuditor == "2" && node.auditorType == "2")
                        {// 当前节点人员需要都审核
                            foreach (var item in nWFTaskEntity.nWFUserInfoList)
                            {
                                item.Mark = 1;
                            }
                            nWFTaskEntity.nWFUserInfoList[0].Mark = 0;
                        }
                        break;
                    case "auditornode":// 查阅节点
                        nWFTaskEntity.F_Type = 2;
                        nWFTaskEntity.F_IsBatchAudit = node.isBatchAudit;
                        break;
                    case "childwfnode":// 子流程节点
                        NWFTaskEntity taskEntiy =await _nWFTaskIBLL.GetEntityByNodeId(node.id, paramConfig.ProcessId);
                        if (taskEntiy == null)
                        {
                            nWFTaskEntity.F_ChildProcessId = Guid.NewGuid().ToString();
                            nWFTaskEntity.F_Type = 4;
                        }
                        else
                        {
                            NWFProcessEntity cNWFProcessEntity =await nWFProcessSerive.GetEntity(taskEntiy.F_ChildProcessId);
                            if (cNWFProcessEntity.F_IsFinished == 1)
                            {// 如果子流程已经结束
                                nWFTaskEntity.F_ChildProcessId = taskEntiy.F_ChildProcessId;
                                nWFTaskEntity.F_Type = 6;


                                NWFIEngine nWFIEngine =await _Bootstraper("", taskEntiy.F_ChildProcessId, "", null);
                                NWFTaskEntity nWFTaskEntity2 = new NWFTaskEntity();

                                nWFTaskEntity2.F_Id = Guid.NewGuid().ToString();
                                nWFTaskEntity2.F_IsFinished = 0;
                                nWFTaskEntity2.F_IsUrge = 0;
                                nWFTaskEntity2.F_CreateDate = DateTime.Now;

                                nWFTaskEntity2.F_ProcessId = cNWFProcessEntity.F_Id;
                                nWFTaskEntity2.F_NodeId = nWFIEngine.GetStartNode().id;
                                nWFTaskEntity2.F_NodeName = nWFIEngine.GetStartNode().name;
                                nWFTaskEntity2.F_PrevNodeId = currentNodeInfo.id;
                                nWFTaskEntity2.F_PrevNodeName = currentNodeInfo.name;

                                nWFTaskEntity2.F_CreateUserId = paramConfig.CurrentUser.Id;
                                nWFTaskEntity2.F_CreateUserName = paramConfig.CurrentUser.Name;
                                nWFTaskEntity2.F_Type = 5;
                                list.Add(nWFTaskEntity2);

                            }
                            else
                            {
                                nWFTaskEntity.F_Type = null;
                            }
                        }
                        break;
                    case "startround":// 开始节点
                        if (paramConfig.IsChild == 1)
                        {
                            NWFTaskEntity pNWFTaskEntity = await _nWFTaskIBLL.GetEntity(paramConfig.ParentTaskId);

                            nWFTaskEntity.F_ProcessId = paramConfig.ParentProcessId;
                            nWFTaskEntity.F_NodeId = pNWFTaskEntity.F_NodeId;
                            nWFTaskEntity.F_NodeName = pNWFTaskEntity.F_NodeName;
                            nWFTaskEntity.F_PrevNodeId = pNWFTaskEntity.F_NodeId;
                            nWFTaskEntity.F_PrevNodeName = pNWFTaskEntity.F_NodeName;
                            nWFTaskEntity.F_Type = 6;
                            nWFTaskEntity.F_ChildProcessId = paramConfig.ProcessId;


                            NWFTaskEntity nWFTaskEntity2 = new NWFTaskEntity();
                            nWFTaskEntity2.F_Id = Guid.NewGuid().ToString();
                            nWFTaskEntity2.F_IsFinished = 0;
                            nWFTaskEntity2.F_IsUrge = 0;
                            nWFTaskEntity2.F_CreateDate = DateTime.Now;

                            nWFTaskEntity2.F_ProcessId = paramConfig.ProcessId;
                            nWFTaskEntity2.F_NodeId = node.id;
                            nWFTaskEntity2.F_NodeName = node.name;
                            nWFTaskEntity2.F_PrevNodeId = currentNodeInfo.id;
                            nWFTaskEntity2.F_PrevNodeName = currentNodeInfo.name;

                            nWFTaskEntity2.F_CreateUserId = paramConfig.CurrentUser.Id;
                            nWFTaskEntity2.F_CreateUserName = paramConfig.CurrentUser.Name;
                            nWFTaskEntity2.F_Type = 5;
                            list.Add(nWFTaskEntity2);
                        }
                        else
                        {
                            nWFTaskEntity.F_Type = 5;
                        }
                        break;
                }
                if (nWFTaskEntity.nWFUserInfoList.Count > 0 && nWFTaskEntity.F_Type != null)
                {
                    list.Add(nWFTaskEntity);
                }
            }
            return list;
        }

        /// <summary>
        /// 创建流程任务
        /// </summary>
        /// <param name="taskList">任务信息列表</param>
        /// <param name="paramConfig">流程配置信息</param>
        /// <returns></returns>
        private IEnumerable<NWFTaskMsgEntity> _CreateTaskMsg(IEnumerable<NWFTaskEntity> taskList, NWFEngineParamConfig paramConfig)
        {
            List<NWFTaskMsgEntity> list = new List<NWFTaskMsgEntity>();
            foreach (var task in taskList)
            {
                if (task.nWFUserInfoList != null)
                {
                    foreach (var item in task.nWFUserInfoList)
                    {
                        NWFTaskMsgEntity nWFTaskMsgEntity = new NWFTaskMsgEntity();
                        nWFTaskMsgEntity.F_Id = Guid.NewGuid().ToString();
                        nWFTaskMsgEntity.F_IsFinished = 0;
                        nWFTaskMsgEntity.F_CreateDate = DateTime.Now;

                        nWFTaskMsgEntity.F_ProcessId = task.F_ProcessId;
                        nWFTaskMsgEntity.F_TaskId = task.F_Id;
                        nWFTaskMsgEntity.F_FromUserId = paramConfig.CurrentUser.Id;
                        nWFTaskMsgEntity.F_FromUserName = paramConfig.CreateUser.Name;
                        nWFTaskMsgEntity.F_FromUserAccount = paramConfig.CreateUser.Account;
                        nWFTaskMsgEntity.F_ToUserId = item.Id;
                        nWFTaskMsgEntity.F_ToName = item.Name;
                        nWFTaskMsgEntity.F_ToAccount = item.Account;
                        nWFTaskMsgEntity.F_Title = paramConfig.SchemeName;
                        nWFTaskMsgEntity.F_Content = "你有新的" + paramConfig.SchemeName + "";
                        nWFTaskMsgEntity.NodeId = task.F_NodeId;
                        switch (task.F_Type)
                        {
                            case 1:// 审核节点
                                nWFTaskMsgEntity.F_Content += "需要审核，发起人" + paramConfig.CreateUser.Name + "。";
                                break;
                            case 2:// 查阅节点
                                nWFTaskMsgEntity.F_Content += "需要查阅，发起人" + paramConfig.CreateUser.Name + "。";
                                break;
                            case 3:// 加签
                                nWFTaskMsgEntity.F_Content += "需要审核，来自加签，发起人" + paramConfig.CreateUser.Name + "。";
                                break;
                            case 4:// 子流程节点
                                nWFTaskMsgEntity.F_Content += "的" + task.F_NodeName + "需要创建，发起人" + paramConfig.CreateUser.Name + "。";
                                break;
                            case 5:// 开始节点
                                nWFTaskMsgEntity.F_Content += "需要重新创建。";
                                break;
                            case 6:// 子流程重新创建
                                nWFTaskMsgEntity.F_Content += "需要重新创建。";
                                break;
                        }
                        list.Add(nWFTaskMsgEntity);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 触发流程绑定的方法
        /// </summary>
        /// <param name="line">线条信息</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="code">操作码</param>
        /// <param name="paramConfig">配置方法</param>
        private async Task _TriggerMethod(NWFLineInfo line, string taskId, string nodeName, string code, NWFEngineParamConfig paramConfig)
        {
            switch (line.operationType)
            {
                case "sql":
                    if (!string.IsNullOrEmpty(line.dbId) && !string.IsNullOrEmpty(line.strSql))
                    {
                        string strSql = line.strSql.Replace("{processId}", "@processId");
                        // 流程当前执行人
                        strSql = strSql.Replace("{userId}", "@userId");
                        strSql = strSql.Replace("{userAccount}", "@userAccount");
                        strSql = strSql.Replace("{companyId}", "@companyId");
                        strSql = strSql.Replace("{departmentId}", "@departmentId");
                        strSql = strSql.Replace("{code}", "@code");
                        var param = new
                        {
                            processId = paramConfig.ProcessId,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId,
                            code = code
                        };

                        await nWFProcessSerive.BaseRepository(line.dbId).ExecuteSql(strSql, param);
                    }
                    break;
                case "interface":
                    if (!string.IsNullOrEmpty(line.strInterface))
                    {
                        var postData = new
                        {
                            processId = paramConfig.ProcessId,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId,
                            code
                        };
                        await HttpMethods.Post(line.strInterface, postData.ToJson());
                    }
                    break;
                case "ioc":
                    if (!string.IsNullOrEmpty(line.iocName) && IocManager.Instance.IsRegistered<IWorkFlowMethod>(line.iocName))
                    {
                        IWorkFlowMethod iWorkFlowMethod = IocManager.Instance.GetService<IWorkFlowMethod>(line.iocName);
                        WfMethodParameter wfMethodParameter = new WfMethodParameter()
                        {
                            processId = paramConfig.ProcessId,
                            taskId = taskId,
                            nodeName = nodeName,
                            code = code,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId
                        };
                        iWorkFlowMethod.Execute(wfMethodParameter);
                    }
                    break;
            }
        }




        /// <summary>
        /// 触发流程绑定的方法
        /// </summary>
        /// <param name="node">节点信息</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="childProcessId">子流程进程主键</param>
        /// <param name="paramConfig">配置方法</param>
        private async Task _TriggerMethod(NWFNodeInfo node, string taskId, string nodeName, string childProcessId, NWFEngineParamConfig paramConfig)
        {
            switch (node.operationType)
            {
                case "sql":
                    if (!string.IsNullOrEmpty(node.dbId) && !string.IsNullOrEmpty(node.strSql))
                    {
                        string strSql = node.strSql.Replace("{processId}", "@processId");
                        // 流程当前执行人
                        strSql = strSql.Replace("{userId}", "@userId");
                        strSql = strSql.Replace("{userAccount}", "@userAccount");
                        strSql = strSql.Replace("{companyId}", "@companyId");
                        strSql = strSql.Replace("{departmentId}", "@departmentId");
                        strSql = strSql.Replace("{childProcessId}", "@childProcessId");
                        var param = new
                        {
                            processId = paramConfig.ProcessId,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId,
                            childProcessId
                        };

                        await nWFProcessSerive.BaseRepository(node.dbId).ExecuteSql(strSql, param);
                    }
                    break;
                case "interface":
                    if (!string.IsNullOrEmpty(node.strInterface))
                    {
                        var postData = new
                        {
                            processId = paramConfig.ProcessId,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId,
                            childProcessId = childProcessId
                        };
                        await HttpMethods.Post(node.strInterface, postData.ToJson());
                    }
                    break;
                case "ioc":
                    if (!string.IsNullOrEmpty(node.iocName) && IocManager.Instance.IsRegistered<IWorkFlowMethod>(node.iocName))
                    {
                        IWorkFlowMethod iWorkFlowMethod = IocManager.Instance.GetService<IWorkFlowMethod>(node.iocName);
                        WfMethodParameter wfMethodParameter = new WfMethodParameter()
                        {
                            processId = paramConfig.ProcessId,
                            taskId = taskId,
                            nodeName = nodeName,
                            childProcessId = childProcessId,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId
                        };
                        iWorkFlowMethod.Execute(wfMethodParameter);
                    }
                    break;
            }
        }
        /// <summary>
        /// 触发流程绑定的方法
        /// </summary>
        /// <param name="closeDo">撤销，作废，删除</param>
        /// <param name="code">1撤销，2作废，3删除草稿</param>
        /// <param name="paramConfig">配置方法</param>
        private async Task _TriggerMethod(NWFCloseDo closeDo, string code, NWFEngineParamConfig paramConfig)
        {
            switch (closeDo.F_CloseDoType)
            {
                case "sql":
                    if (!string.IsNullOrEmpty(closeDo.F_CloseDoDbId) && !string.IsNullOrEmpty(closeDo.F_CloseDoSql))
                    {
                        string strSql = closeDo.F_CloseDoSql.Replace("{processId}", "@processId");
                        // 流程当前执行人
                        strSql = strSql.Replace("{userId}", "@userId");
                        strSql = strSql.Replace("{userAccount}", "@userAccount");
                        strSql = strSql.Replace("{companyId}", "@companyId");
                        strSql = strSql.Replace("{departmentId}", "@departmentId");
                        strSql = strSql.Replace("{code}", "@code");
                        var param = new
                        {
                            processId = paramConfig.ProcessId,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId,
                            code
                        };

                        await nWFProcessSerive.BaseRepository(closeDo.F_CloseDoDbId).ExecuteSql(strSql, param);
                    }
                    break;
                case "ioc":
                    if (!string.IsNullOrEmpty(closeDo.F_CloseDoIocName) && IocManager.Instance.IsRegistered<IWorkFlowMethod>(closeDo.F_CloseDoIocName))
                    {
                        IWorkFlowMethod iWorkFlowMethod = IocManager.Instance.GetService<IWorkFlowMethod>(closeDo.F_CloseDoIocName);
                        WfMethodParameter wfMethodParameter = new WfMethodParameter()
                        {
                            processId = paramConfig.ProcessId,
                            code = code,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId
                        };
                        iWorkFlowMethod.Execute(wfMethodParameter);
                    }
                    break;
                case "interface":
                    if (!string.IsNullOrEmpty(closeDo.F_CloseDoInterface))
                    {
                        var postData = new
                        {
                            processId = paramConfig.ProcessId,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId,
                            code = code
                        };
                        await HttpMethods.Post(closeDo.F_CloseDoInterface, postData.ToJson());
                    }
                    break;
            }
        }



        /// <summary>
        /// 触发流程绑定的方法
        /// </summary>
        /// <param name="line">线条信息</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="paramConfig">配置方法</param>
        private async Task _TriggerMethodR(NWFLineInfo line, string taskId, string nodeName, NWFEngineParamConfig paramConfig)
        {
            switch (line.operationType)
            {
                case "sql":
                    if (!string.IsNullOrEmpty(line.dbId) && !string.IsNullOrEmpty(line.strSqlR))
                    {
                        string strSql = line.strSqlR.Replace("{processId}", "@processId");
                        // 流程当前执行人
                        strSql = strSql.Replace("{userId}", "@userId");
                        strSql = strSql.Replace("{userAccount}", "@userAccount");
                        strSql = strSql.Replace("{companyId}", "@companyId");
                        strSql = strSql.Replace("{departmentId}", "@departmentId");
                        strSql = strSql.Replace("{code}", "@code");
                        var param = new
                        {
                            processId = paramConfig.ProcessId,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId,
                            code = "RevokeAudit"
                        };

                        await nWFProcessSerive.BaseRepository(line.dbId).ExecuteSql(strSql, param);
                    }
                    break;
                case "interface":
                    if (!string.IsNullOrEmpty(line.strInterfaceR))
                    {
                        var postData = new
                        {
                            processId = paramConfig.ProcessId,
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId,
                            code = "RevokeAudit"
                        };
                        await HttpMethods.Post(line.strInterfaceR, postData.ToJson());
                    }
                    break;
                case "ioc":
                    if (!string.IsNullOrEmpty(line.iocNameR) && IocManager.Instance.IsRegistered<IWorkFlowMethod>(line.iocNameR))
                    {
                        IWorkFlowMethod iWorkFlowMethod = IocManager.Instance.GetService<IWorkFlowMethod>(line.iocNameR);
                        WfMethodParameter wfMethodParameter = new WfMethodParameter()
                        {
                            processId = paramConfig.ProcessId,
                            taskId = taskId,
                            nodeName = nodeName,
                            code = "RevokeAudit",
                            userId = paramConfig.CurrentUser.Id,
                            userAccount = paramConfig.CurrentUser.Account,
                            companyId = paramConfig.CurrentUser.CompanyId,
                            departmentId = paramConfig.CurrentUser.DepartmentId
                        };
                        iWorkFlowMethod.Execute(wfMethodParameter);
                    }
                    break;
            }
        }


        /// <summary>
        /// 会签节点处理
        /// </summary>
        /// <param name="nodeList">下一节点信息</param>
        /// <param name="closeTaskList">需要关闭的任务</param>
        /// <param name="nodeId">当前节点id</param>
        /// <param name="processId">当前流程进程主键</param>
        /// <param name="state">审批状态</param>
        /// <param name="nWFIEngine">流程引擎</param>
        /// <returns></returns>
        private async Task<IEnumerable<NWFConfluenceEntity>> _ClearConfluence(IEnumerable<NWFNodeInfo> nodeList, List<NWFTaskEntity> closeTaskList, string nodeId, string processId, int state, NWFIEngine nWFIEngine)
        {
            List<NWFConfluenceEntity> list = new List<NWFConfluenceEntity>();
            foreach (var node in nodeList)
            {
                if (node.type == "confluencenode")
                {
                    NWFConfluenceEntity entity = new NWFConfluenceEntity()
                    {
                        F_FormNodeId = nodeId,
                        F_ProcessId = processId,
                        F_NodeId = node.id,
                        F_State = state,
                        isClear = false
                    };
                    if (node.confluenceRes != 0)
                    {
                        entity.confluenceRes = node.confluenceRes;
                        entity.isClear = true;

                        // 需要关闭还没处理任务的节点
                        Dictionary<string, string> hasMap = new Dictionary<string, string>();// 记录已经处理的节点ID
                        var taskList =await _nWFTaskIBLL.GetUnFinishTaskList(processId);
                        foreach (var task in taskList)
                        {
                            if (task.F_NodeId != nodeId)
                            {
                                if (hasMap.ContainsKey(task.F_NodeId))
                                {
                                    task.F_IsFinished = 2;
                                    closeTaskList.Add(task);
                                }
                                else
                                {
                                    if (nWFIEngine.IsToNode(task.F_NodeId, node.id))
                                    {
                                        task.F_IsFinished = 2;
                                        closeTaskList.Add(task);
                                    }
                                }
                            }

                        }

                    }
                    entity.F_Id = Guid.NewGuid().ToString();
                    list.Add(entity);
                }
            }
            return list;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="taskMsgList">消息列表</param>
        /// <param name="nWFIEngine">流程引擎</param>
        private async Task _SendMsg(List<NWFTaskMsgEntity> taskMsgList, NWFIEngine nWFIEngine)
        {
            try
            {
                if (nWFIEngine != null)
                {
                    foreach (var taskMsg in taskMsgList)
                    {
                        NWFNodeInfo nodeInfo = nWFIEngine.GetNode(taskMsg.NodeId);
                        if (!string.IsNullOrEmpty(nodeInfo.notice))
                        {
                            UserEntity userEntity =await _userIBLL.GetEntity(taskMsg.F_ToUserId);
                            List<UserEntity> msgUserList = new List<UserEntity>();
                            msgUserList.Add(userEntity);
                            await _mStrategyInfoIBLL.SendMessage(nodeInfo.notice, taskMsg.F_Content, msgUserList.ToJson());
                        }

                    }
                }
            }
            catch
            {
            }
        }


        private async Task _AutoAuditFlow(List<NWFTaskEntity> taskList, NWFIEngine nWFIEngine, UserEntity userInfo)
        {

            foreach (var task in taskList)
            {
                var node = nWFIEngine.GetNode(task.F_NodeId);
                NWFUserInfo user = null;
                if (task.nWFUserInfoList != null && task.nWFUserInfoList.FindAll(t => t.noPeople == true).Count > 0 && node.noPeopleGz == 2)
                {
                   await AuditFlow("agree", "同意", nWFIEngine.GetConfig().ProcessId, task.F_Id, "无审核人跳过", null, "", "");
                }
                else if (node != null && node.type == "stepnode" && !string.IsNullOrEmpty(node.agreeGz))
                { // 普通审核节点
                    string[] agreeGzList = node.agreeGz.Split(',');
                    bool flag = false;
                    foreach (var item in agreeGzList)
                    {
                        switch (item)
                        {
                            case "1":// 处理人就是提交人
                                string createUserId = nWFIEngine.GetConfig().CreateUser.Id;
                                user = task.nWFUserInfoList.Find(t => t.Id == createUserId && t.Mark == 0);
                                if (user != null)
                                {
                                    flag = true;
                                }
                                break;
                            case "2":// 处理人和上一步处理人相同
                                user = task.nWFUserInfoList.Find(t => t.Id == userInfo.F_UserId && t.Mark == 0);
                                if (user != null)
                                {
                                    flag = true;
                                }
                                break;
                            case "3":// 处理人审批过(同意)
                                var logList = (List<NWFTaskLogEntity>)await _nWFTaskIBLL.GetLogList(nWFIEngine.GetConfig().ProcessId);
                                if (logList.Count > 0)
                                {
                                    foreach (var taskUserItem in task.nWFUserInfoList)
                                    {
                                        var logItem = logList.Find(t => t.F_CreateUserId == taskUserItem.Id && t.F_OperationCode == "agree");
                                        if (logItem != null)
                                        {
                                            UserEntity taskUserEntity =await _userIBLL.GetEntity(taskUserItem.Id);
                                            user = new NWFUserInfo
                                            {
                                                Id = taskUserEntity.F_UserId,
                                                Account = taskUserEntity.F_Account,
                                                Name = taskUserEntity.F_RealName
                                            };
                                            flag = true;
                                            break;
                                        }
                                    }
                                }
                                break;
                        }


                        if (flag)
                        {
                            await AuditFlow("agree", "同意", nWFIEngine.GetConfig().ProcessId, task.F_Id, "系统自动审核", null, "", "");
                            break;
                        }
                    }

                    user = null;
                }
            }
        }


        #endregion

        /// <summary>
        /// 获取下一节点审核人
        /// </summary>
        /// <param name="code">流程模板code</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="nodeId">流程节点Id</param>
        /// <param name="operationCode">流程操作代码</param>
        /// <returns></returns>
        public async Task<Dictionary<string, IEnumerable<NWFUserInfo>>> GetNextAuditors(string code, string processId, string taskId, string nodeId, string operationCode)
        {
            Dictionary<string, IEnumerable<NWFUserInfo>> res = new Dictionary<string, IEnumerable<NWFUserInfo>>();

            UserEntity userInfo = await this.CurrentUser();

            NWFIEngine nWFIEngine =await _Bootstraper(code, processId, taskId, userInfo);
            NWFNodeInfo nodeInfo = nWFIEngine.GetNode(nodeId);

            List<NWFNodeInfo> list =await nWFIEngine.GetNextTaskNode(nodeInfo, operationCode, true, new List<NWFLineInfo>());

            foreach (var item in list)
            {
                if (item.type == "stepnode" || item.type == "auditornode" || item.type == "childwfnode")//&& item.isAllAuditor == "1"暂时去掉多人审核的限制
                {
                    if (!res.ContainsKey(item.id))
                    {
                        res.Add(item.id, await _GetNodeAuditors(item.auditors, item, nWFIEngine.GetConfig()));
                    }
                }
            }

            return res;
        }
        /// <summary>
        /// 获取流程进程信息
        /// </summary>
        /// <param name="processId">进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        public async Task<NWFProcessDetailsModel> GetProcessDetails(string processId, string taskId)
        {
            var userInfo = await this.CurrentUser();
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, taskId, userInfo);
            NWFProcessDetailsModel nWFProcessDetailsModel = new NWFProcessDetailsModel();

            nWFProcessDetailsModel.Scheme = nWFIEngine.GetScheme();
            nWFProcessDetailsModel.CurrentNodeIds =await _nWFTaskIBLL.GetCurrentNodeIds(processId);
            nWFProcessDetailsModel.TaskLogList =await _nWFTaskIBLL.GetLogList(processId);
            nWFProcessDetailsModel.parentProcessId = nWFIEngine.GetConfig().ParentProcessId;

            nWFProcessDetailsModel.isFinished = (int)((await nWFProcessSerive.GetEntity(processId)).F_IsFinished);

            if (string.IsNullOrEmpty(taskId))
            {
                nWFProcessDetailsModel.CurrentNodeId = nWFIEngine.GetStartNode().id;
            }
            else
            {
                NWFTaskEntity nWFTaskEntity = await _nWFTaskIBLL.GetEntity(taskId);
                if (nWFTaskEntity != null)
                {
                    if (!string.IsNullOrEmpty(nWFTaskEntity.F_ChildProcessId))
                    {
                        nWFProcessDetailsModel.childProcessId = nWFTaskEntity.F_ChildProcessId;
                        nWFProcessDetailsModel.CurrentNodeIds =await _nWFTaskIBLL.GetCurrentNodeIds(nWFTaskEntity.F_ChildProcessId);
                        nWFProcessDetailsModel.TaskLogList =await _nWFTaskIBLL.GetLogList(nWFTaskEntity.F_ChildProcessId);
                    }
                    nWFProcessDetailsModel.CurrentNodeId = nWFTaskEntity.F_NodeId;
                }
            }
            return nWFProcessDetailsModel;
        }
        /// <summary>
        /// 获取子流程详细信息
        /// </summary>
        /// <param name="processId">父流程进程主键</param>
        /// <param name="taskId">父流程子流程发起主键</param>
        /// <param name="schemeCode">子流程流程模板编码</param>
        /// <param name="nodeId">父流程发起子流程节点Id</param>
        /// <returns></returns>
        public async Task<NWFProcessDetailsModel> GetChildProcessDetails(string processId, string taskId, string schemeCode, string nodeId)
        {
            var userInfo = await this.CurrentUser();
            NWFProcessEntity entity =await nWFProcessSerive.GetEntityByProcessId(processId, nodeId);
            NWFProcessDetailsModel nWFProcessDetailsModel = new NWFProcessDetailsModel();
            if (entity == null)
            {
                NWFIEngine nWFIEngine = await _Bootstraper(schemeCode, "", "", userInfo);
                nWFProcessDetailsModel.Scheme = nWFIEngine.GetScheme();
            }
            else
            {
                NWFIEngine nWFIEngine = await _Bootstraper("", entity.F_Id, "", userInfo);
                nWFProcessDetailsModel.Scheme = nWFIEngine.GetScheme();
                nWFProcessDetailsModel.CurrentNodeIds =await _nWFTaskIBLL.GetCurrentNodeIds(entity.F_Id);
                nWFProcessDetailsModel.TaskLogList =await _nWFTaskIBLL.GetLogList(entity.F_Id);
                nWFProcessDetailsModel.childProcessId = entity.F_Id;
                nWFProcessDetailsModel.isFinished = (int)(entity.F_IsFinished);
            }
            return nWFProcessDetailsModel;
        }
        /// <summary>
        /// 保存草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="userId">创建人</param>
        /// <returns></returns>
        public async Task SaveDraft(string processId, string schemeCode,string userId)
        {
            var userInfo = await this.CurrentUser(userId);
            // 判断当前流程进程是否有保存过
            var processEntity =await GetEntity(processId);
            if (processEntity == null)
            {// 创建草稿，已经存在不做处理
                var schemeInfo = await _nWFSchemeIBLL.GetInfoEntityByCode(schemeCode);
                NWFProcessEntity nWFProcessEntity = new NWFProcessEntity()
                {
                    F_Id = processId,
                    F_SchemeCode = schemeCode,
                    F_SchemeName = schemeInfo.F_Name,
                    F_EnabledMark = 2,
                    F_IsAgain = 0,
                    F_IsFinished = 0,
                    F_IsChild = 0,
                    F_IsStart = 0,
                    F_CreateUserId = userInfo.F_UserId,
                    F_CreateUserName = userInfo.F_RealName,
                    F_CreateDate = DateTime.Now
                };

                await nWFProcessSerive.Save(nWFProcessEntity);
            }
        }
        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        public async Task DeleteDraft(string processId)
        {
            var userInfo =await this.CurrentUser();
            // 执行
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, "", userInfo);
            NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
            var scheme = nWFIEngine.GetSchemeObj();
            await DeleteEntity(processId);
            await _TriggerMethod(scheme.closeDo, "3", nWFEngineParamConfig);

        }
        /// <summary>
        /// 创建流程
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="title">标题</param>
        /// <param name="level">流程等级</param>
        /// <param name="auditors">下一节点审核人</param>
        /// <param name="userId">发起人</param>
        public async Task CreateFlow(string schemeCode, string processId, string title, int level, string auditors,string userId)
        {
            var userInfo =await this.CurrentUser();
            if (!string.IsNullOrEmpty(userId) && userId != userInfo.F_UserId) {
                title += "【" + userInfo.F_RealName + "代发】";
                userInfo = await this.CurrentUser(userId);
            }

            // 初始化流程引擎
            NWFIEngine nWFIEngine =await _Bootstraper(schemeCode, processId, "", userInfo);
            NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
            nWFEngineParamConfig.Auditers = auditors;

            NWFNodeInfo nodeInfo = nWFIEngine.GetStartNode();
            // 获取下一节点信息
            List<NWFLineInfo> lineList = new List<NWFLineInfo>();
            List<NWFNodeInfo> list =await nWFIEngine.GetNextTaskNode(nodeInfo, "agree", false, lineList);
            // 创建任务
            List<NWFTaskEntity> taskList =(List<NWFTaskEntity>)await _CreateTask(list, nodeInfo, nWFEngineParamConfig);
            // 创建任务消息
            List<NWFTaskMsgEntity> taskMsgList =(List<NWFTaskMsgEntity>)_CreateTaskMsg(taskList, nWFEngineParamConfig);
            // 保存流程进程信息
            NWFProcessEntity nWFProcessEntity = new NWFProcessEntity()
            {
                F_Id = nWFEngineParamConfig.ProcessId,
                F_SchemeId = nWFEngineParamConfig.SchemeId,
                F_SchemeCode = nWFEngineParamConfig.SchemeCode,
                F_SchemeName = nWFEngineParamConfig.SchemeName,
                F_Level = level,
                F_EnabledMark = 1,
                F_IsAgain = 0,
                F_IsFinished = 0,
                F_IsChild = 0,
                F_IsStart = 0,
                F_CreateUserId = nWFEngineParamConfig.CurrentUser.Id,
                F_CreateUserName = nWFEngineParamConfig.CurrentUser.Name,
                F_CreateDate = DateTime.Now
        };
            if (!string.IsNullOrEmpty(title))
            {
                nWFProcessEntity.F_Title = title;
            }
            else
            {
                nWFProcessEntity.F_Title = nWFEngineParamConfig.SchemeName;
            }
            if (nWFEngineParamConfig.State == 1)
            {
                nWFProcessEntity.F_IsAgain = 1;
            }
            else if (nWFEngineParamConfig.State == 2)
            {
                nWFProcessEntity.F_IsFinished = 1;
            }

            // 创建任务日志信息
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = "create",
                F_OperationName = "创建流程",
                F_NodeId = nodeInfo.id,
                F_NodeName = nodeInfo.name,
                F_TaskType = 0,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };
            // 保存信息
            await nWFProcessSerive.Save(nWFProcessEntity, taskList, taskMsgList, nWFTaskLogEntity);
            // 触发流程绑定方法
            foreach (var line in lineList)
            {
                await _TriggerMethod(line, "", nodeInfo.name, "create", nWFEngineParamConfig);
            }
            // 触发消息
            await _SendMsg(taskMsgList, nWFIEngine);
            // 触发子流程节点方法
            foreach (var taskItem in taskList)
            {
                if (taskItem.F_Type == 4)
                {
                    NWFNodeInfo cNodeInfo = nWFIEngine.GetNode(taskItem.F_NodeId);
                    await _TriggerMethod(cNodeInfo, taskItem.F_Id, cNodeInfo.name, taskItem.F_ChildProcessId, nWFEngineParamConfig);
                }
            }


            // 触发自动跳过规则
            await _AutoAuditFlow(taskList, nWFIEngine, userInfo);

        }
        /// <summary>
        /// 创建流程(子流程)
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="parentProcessId">父级流程实例ID</param>
        /// <param name="parentTaskId">父级任务ID</param>
        public async Task CreateChildFlow(string schemeCode, string processId, string parentProcessId, string parentTaskId)
        {
            var userInfo = await this.CurrentUser();

            // 父节点信息
            NWFTaskEntity pTaskEntity = await _nWFTaskIBLL.GetEntity(parentTaskId);
            NWFIEngine pNWFIEngine =await _Bootstraper("", parentProcessId, parentTaskId, userInfo);
            NWFEngineParamConfig pNWFEngineParamConfig = pNWFIEngine.GetConfig();
            NWFNodeInfo pNodeInfo = pNWFIEngine.GetNode(pTaskEntity.F_NodeId);

            // 初始化流程引擎
            NWFProcessEntity cNWFProcessEntity =await nWFProcessSerive.GetEntity(processId);
            if (cNWFProcessEntity != null)
            {
                schemeCode = null;
            }

            NWFIEngine nWFIEngine =await _Bootstraper(schemeCode, processId, "", userInfo);
            NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();

            NWFNodeInfo nodeInfo = nWFIEngine.GetStartNode();
            // 获取下一节点信息
            List<NWFLineInfo> lineList = new List<NWFLineInfo>();
            List<NWFNodeInfo> list =await nWFIEngine.GetNextTaskNode(nodeInfo, "agree", false, lineList);
            // 创建任务
            List<NWFTaskEntity> taskList =(List<NWFTaskEntity>)await _CreateTask(list, nodeInfo, nWFEngineParamConfig);
            // 创建任务消息
            List<NWFTaskMsgEntity> taskMsgList = (List<NWFTaskMsgEntity>)_CreateTaskMsg(taskList, nWFEngineParamConfig);
            // 保存流程进程信息
            NWFProcessEntity nWFProcessEntity = new NWFProcessEntity()
            {
                F_Id = nWFEngineParamConfig.ProcessId,
                F_SchemeId = nWFEngineParamConfig.SchemeId,
                F_SchemeCode = nWFEngineParamConfig.SchemeCode,
                F_SchemeName = nWFEngineParamConfig.SchemeName,
                F_Title = pNWFEngineParamConfig.SchemeName + "【子流程】",
                F_EnabledMark = 1,
                F_IsAgain = 0,
                F_IsFinished = 0,
                F_IsChild = 1,
                F_IsAsyn = pNodeInfo.childType == "1" ? 0 : 1,
                F_IsStart = 0,
                F_CreateUserId = nWFEngineParamConfig.CurrentUser.Id,
                F_CreateUserName = nWFEngineParamConfig.CurrentUser.Name,

                F_ParentProcessId = parentProcessId,
                F_ParentTaskId = parentTaskId,
                F_ParentNodeId = pTaskEntity.F_NodeId,
                F_CreateDate = DateTime.Now
            };
            if (nWFEngineParamConfig.State == 1)
            {
                nWFProcessEntity.F_IsAgain = 1;
            }
            else if (nWFEngineParamConfig.State == 2)
            {
                nWFProcessEntity.F_IsFinished = 1;
            }

            // 创建任务日志信息
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = "create",
                F_OperationName = "创建流程",
                F_NodeId = nodeInfo.id,
                F_NodeName = nodeInfo.name,
                F_TaskType = 0,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };

            #region 对父流程的操作
            // 获取当前任务的执行人列表
            List<NWFTaskRelationEntity> pTaskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(parentTaskId);
            bool isMyPTask = false;
            string pTaskUserId = userInfo.F_UserId;
            Dictionary<string, string> pTaskUserMap = new Dictionary<string, string>();
            foreach (var item in pTaskUserList)
            {
                if (item.F_UserId == userInfo.F_UserId)
                {
                    isMyPTask = true;
                }
                if (!pTaskUserMap.ContainsKey(userInfo.F_UserId))
                {
                    pTaskUserMap.Add(userInfo.F_UserId, "1");
                }
            }
            if (!isMyPTask)
            {
                // 如果是委托任务
                var delegateList =await nWFProcessSerive.GetDelegateProcess(userInfo.F_UserId);
                foreach (var item in delegateList)
                {
                    if (pTaskUserMap.ContainsKey(item.F_UserId))
                    {
                        pTaskUserId = item.F_UserId;
                    }
                }
            }
            // 创建任务日志信息
            NWFTaskLogEntity pNWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = parentProcessId,
                F_OperationCode = schemeCode == null ? "againCreateChild" : "createChild",
                F_OperationName = schemeCode == null ? "重新创建" : "创建子流程",
                F_NodeId = pTaskEntity.F_NodeId,
                F_NodeName = pTaskEntity.F_NodeName,
                F_PrevNodeId = pTaskEntity.F_PrevNodeId,
                F_PrevNodeName = pTaskEntity.F_PrevNodeName,
                F_TaskId = parentTaskId,
                F_TaskType = 4,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };
            if (userInfo.F_UserId != pTaskUserId)
            {
                // 说明是委托任务
                nWFTaskLogEntity.F_TaskUserId = pTaskUserId;
                nWFTaskLogEntity.F_TaskUserName =(await _userIBLL.GetEntity(pTaskUserId)).F_RealName;
            }
            NWFTaskRelationEntity nWFTaskRelationEntity = pTaskUserList.Find(t => t.F_UserId == pTaskUserId);
            nWFTaskRelationEntity.F_Time = DateTime.Now;
            nWFTaskRelationEntity.F_Result = 4;
            NWFProcessEntity pNWFProcessEntity = new NWFProcessEntity()
            {
                F_Id = pNWFEngineParamConfig.ProcessId,
                F_IsStart = 1
            };

            List<NWFLineInfo> pLineList = new List<NWFLineInfo>();
            List<NWFTaskEntity> pTaskList = new List<NWFTaskEntity>();
            List<NWFTaskMsgEntity> pTaskMsgList = new List<NWFTaskMsgEntity>();
            if (pNodeInfo.childType == "1")
            {
                if (nWFProcessEntity.F_IsFinished == 1)
                {
                    // 如果是同步需要推动父流程运行
                    // 获取下一节点信息
                    List<NWFNodeInfo> pList =await pNWFIEngine.GetNextTaskNode(pNodeInfo, "agree", false, pLineList);
                    // 创建任务
                    pTaskList = (List<NWFTaskEntity>)await _CreateTask(pList, pNodeInfo, pNWFEngineParamConfig);
                    // 创建任务消息
                    pTaskMsgList = (List<NWFTaskMsgEntity>)_CreateTaskMsg(pTaskList, pNWFEngineParamConfig);
                    // 给流程发起者一条通知信息
                    NWFTaskMsgEntity pNWFTaskMsgEntity = new NWFTaskMsgEntity()
                    {
                        F_ProcessId = pNWFEngineParamConfig.ProcessId,
                        F_FromUserId = pNWFEngineParamConfig.CurrentUser.Id,
                        F_FromUserAccount = pNWFEngineParamConfig.CurrentUser.Account,
                        F_FromUserName = pNWFEngineParamConfig.CurrentUser.Name,
                        F_ToUserId = pNWFEngineParamConfig.CreateUser.Id,
                        F_ToAccount = pNWFEngineParamConfig.CreateUser.Account,
                        F_ToName = pNWFEngineParamConfig.CreateUser.Name,
                        F_Title = pNWFEngineParamConfig.SchemeName,
                        F_Content = "你的流程有状态的更新：" + pNWFEngineParamConfig.CurrentUser.Name + "发起子流程【" + nWFEngineParamConfig.SchemeName + "】",
                        NodeId = pNWFIEngine.GetStartNode().id,
                        F_Id = Guid.NewGuid().ToString(),
                        F_IsFinished = 0,
                        F_CreateDate = DateTime.Now
                    };
                    pTaskMsgList.Add(pNWFTaskMsgEntity);
                }
            }
            // 保存信息 // 父流程 任务日志 任务更新 任务执行人 父流程进程 任务 任务消息 
            pTaskEntity.F_IsFinished = 1;
            pTaskEntity.F_ModifyDate = DateTime.Now;
            pTaskEntity.F_CreateUserId = userInfo.F_UserId;
            pTaskEntity.F_CreateUserName = userInfo.F_RealName;

            await nWFProcessSerive.Save(pNWFTaskLogEntity, nWFTaskRelationEntity, pTaskEntity, pNWFProcessEntity, pTaskList, pTaskMsgList, nWFProcessEntity, taskList, taskMsgList, nWFTaskLogEntity);
            // 触发流程绑定方法
            foreach (var line in pLineList)
            {
                await _TriggerMethod(line, "", pNodeInfo.name, "create", pNWFEngineParamConfig);
            }
            // 触发消息
            await _SendMsg(pTaskMsgList, pNWFIEngine);

            #endregion
            // 触发流程绑定方法
            foreach (var line in lineList)
            {
                await _TriggerMethod(line, "", nodeInfo.name, "create", nWFEngineParamConfig);
            }

            // 触发消息
            await _SendMsg(taskMsgList, nWFIEngine);

            // 触发自动跳过规则
            await _AutoAuditFlow(taskList, nWFIEngine, userInfo);
        }
        /// <summary>
        /// 重新创建流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        public async Task AgainCreateFlow(string processId)
        {
            var userInfo = await this.CurrentUser();
            // 初始化流程引擎
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, "", userInfo);
            NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
            // 获取开始节点
            NWFNodeInfo nodeInfo = nWFIEngine.GetStartNode();
            // 获取任务实体
            var taskEntiy =await _nWFTaskIBLL.GetEntityByNodeId(nodeInfo.id, processId);
            if (taskEntiy == null)
            {
                throw (new Exception("找不到对应流程任务！"));
            }
            if (taskEntiy.F_IsFinished != 0)
            {
                throw (new Exception("该任务已经结束！"));
            }
            taskEntiy.F_ModifyDate = DateTime.Now;
            taskEntiy.F_ModifyUserId = userInfo.F_UserId;
            taskEntiy.F_ModifyUserName = userInfo.F_RealName;
            taskEntiy.F_IsFinished = 1;

            string taskUserId = userInfo.F_UserId;
            // 获取当前任务的执行人列表
            List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(taskEntiy.F_Id);
            bool isMyTask = false;
            Dictionary<string, string> taskUserMap = new Dictionary<string, string>();
            foreach (var item in taskUserList)
            {
                if (item.F_UserId == userInfo.F_UserId)
                {
                    isMyTask = true;
                }
                if (!taskUserMap.ContainsKey(userInfo.F_UserId))
                {
                    taskUserMap.Add(userInfo.F_UserId, "1");
                }
            }
            if (!isMyTask)
            {
                // 如果是委托任务
                var delegateList =await nWFProcessSerive.GetDelegateProcess(userInfo.F_UserId);
                foreach (var item in delegateList)
                {
                    if (taskUserMap.ContainsKey(item.F_UserId))
                    {
                        taskUserId = item.F_UserId;
                    }
                }
            }

            // 创建任务日志信息
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = "create",
                F_OperationName = "重新发起",
                F_NodeId = nodeInfo.id,
                F_NodeName = nodeInfo.name,
                F_PrevNodeId = taskEntiy.F_PrevNodeId,
                F_PrevNodeName = taskEntiy.F_PrevNodeName,
                F_TaskId = taskEntiy.F_Id,
                F_TaskType = 5,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_TaskUserId = userInfo.F_UserId,
                F_TaskUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };
            if (userInfo.F_UserId != taskUserId)
            {
                // 说明是委托任务
                nWFTaskLogEntity.F_TaskUserId = taskUserId;
                nWFTaskLogEntity.F_TaskUserName =(await _userIBLL.GetEntity(taskUserId)).F_RealName;
            }

            NWFTaskRelationEntity nWFTaskRelationEntity = taskUserList.Find(t => t.F_UserId == taskUserId);
            nWFTaskRelationEntity.F_Result = 1;

            // 获取下一节点信息
            List<NWFLineInfo> lineList = new List<NWFLineInfo>();
            List<NWFNodeInfo> list =await nWFIEngine.GetNextTaskNode(nodeInfo, "agree", false, lineList);

            // 创建任务
            List<NWFTaskEntity> taskList =(List<NWFTaskEntity>)await _CreateTask(list, nodeInfo, nWFEngineParamConfig);
            // 创建任务消息
            List<NWFTaskMsgEntity> taskMsgList = (List<NWFTaskMsgEntity>)_CreateTaskMsg(taskList, nWFEngineParamConfig);

            // 保存流程进程信息
            NWFProcessEntity nWFProcessEntity = new NWFProcessEntity()
            {
                F_Id = nWFEngineParamConfig.ProcessId
            };
            nWFProcessEntity.F_IsAgain = 0;
            if (nWFEngineParamConfig.State == 1)
            {
                nWFProcessEntity.F_IsAgain = 1;
            }
            else if (nWFEngineParamConfig.State == 2)
            {
                nWFProcessEntity.F_IsFinished = 1;
            }
            // 保存信息 任务日志 任务执行人状态更新 任务状态更新 流程进程状态更新 会签信息更新 新的任务列表 新的任务消息列表
            await nWFProcessSerive.Save(nWFTaskLogEntity, nWFTaskRelationEntity, taskEntiy, nWFProcessEntity, null, null, taskList, taskMsgList);

            // 触发流程绑定方法
            foreach (var line in lineList)
            {
                await _TriggerMethod(line, taskEntiy.F_Id, nodeInfo.name, "", nWFEngineParamConfig);
            }
            // 触发消息
            await _SendMsg(taskMsgList, nWFIEngine);

            // 触发子流程节点方法
            foreach (var taskItem in taskList)
            {
                if (taskItem.F_Type == 4)
                {
                    NWFNodeInfo cNodeInfo = nWFIEngine.GetNode(taskItem.F_NodeId);
                    await _TriggerMethod(cNodeInfo, taskItem.F_Id, cNodeInfo.name, taskItem.F_ChildProcessId, nWFEngineParamConfig);
                }
            }

            // 触发自动跳过规则
            await _AutoAuditFlow(taskList, nWFIEngine, userInfo);
        }
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
        public async Task AuditFlow(string operationCode, string operationName, string processId, string taskId, string des, string auditors, string stamp, string signUrl)
        {
            var userInfo = await this.CurrentUser();

            // 初始化流程引擎
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, taskId, userInfo);
            NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
            nWFEngineParamConfig.Auditers = auditors;

            // 获取任务实体
            var taskEntiy = await _nWFTaskIBLL.GetEntity(taskId);
            if (taskEntiy == null)
            {
                throw (new Exception("找不到对应流程任务！"));
            }
            if (taskEntiy.F_IsFinished != 0)
            {
                throw (new Exception("该任务已经结束！"));
            }

            taskEntiy.F_ModifyDate = DateTime.Now;
            taskEntiy.F_ModifyUserId = userInfo.F_UserId;
            taskEntiy.F_ModifyUserName = userInfo.F_RealName;
            taskEntiy.F_IsFinished = 1;
            NWFNodeInfo nodeInfo = nWFIEngine.GetNode(taskEntiy.F_NodeId);
            string taskUserId = userInfo.F_UserId;



            // 获取当前任务的执行人列表
            List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(taskId);
            bool isMyTask = false;
            Dictionary<string, string> taskUserMap = new Dictionary<string, string>();
            foreach (var item in taskUserList)
            {
                if (item.F_UserId == userInfo.F_UserId)
                {
                    isMyTask = true;
                }
                if (!taskUserMap.ContainsKey(userInfo.F_UserId))
                {
                    taskUserMap.Add(userInfo.F_UserId, "1");
                }
            }
            if (!isMyTask)
            {
                // 如果是委托任务
                var delegateList =await nWFProcessSerive.GetDelegateProcess(userInfo.F_UserId);
                foreach (var item in delegateList)
                {
                    //如果当前用户是处理委托任务时，找到原本任务处理人
                    if (!taskUserMap.ContainsKey(item.F_UserId))
                    {
                        taskUserId = item.F_UserId;
                    }
                }
            }
            // 创建任务日志信息
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = operationCode,
                F_OperationName = operationName,
                F_NodeId = nodeInfo.id,
                F_NodeName = nodeInfo.name,
                F_PrevNodeId = taskEntiy.F_PrevNodeId,
                F_PrevNodeName = taskEntiy.F_PrevNodeName,
                F_Des = des,
                F_TaskId = taskId,
                F_TaskType = 1,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_TaskUserId = userInfo.F_UserId,
                F_TaskUserName = userInfo.F_RealName,
                F_StampImg = stamp,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };

            //  保存签字图片
            if (!string.IsNullOrEmpty(signUrl))
            {
                ImgEntity imgEntity = new ImgEntity();
                imgEntity.F_Name = "sign";
                imgEntity.F_ExName = ".png";
                imgEntity.F_Content = signUrl;
                await _imgIBLL.SaveEntity("", imgEntity);
                nWFTaskLogEntity.F_SignImg = imgEntity.F_Id;

            }

            if (userInfo.F_UserId != taskUserId)
            {
                // 说明是委托任务
                nWFTaskLogEntity.F_TaskUserId = taskUserId;
                nWFTaskLogEntity.F_TaskUserName =(await _userIBLL.GetEntity(taskUserId)).F_RealName;
            }

            // 给流程发起者一条通知信息
            NWFTaskMsgEntity nWFTaskMsgEntity = new NWFTaskMsgEntity()
            {
                F_ProcessId = nWFEngineParamConfig.ProcessId,
                F_FromUserId = nWFEngineParamConfig.CurrentUser.Id,
                F_FromUserAccount = nWFEngineParamConfig.CurrentUser.Account,
                F_FromUserName = nWFEngineParamConfig.CurrentUser.Name,
                F_ToUserId = nWFEngineParamConfig.CreateUser.Id,
                F_ToAccount = nWFEngineParamConfig.CreateUser.Account,
                F_ToName = nWFEngineParamConfig.CreateUser.Name,
                F_Title = nWFEngineParamConfig.SchemeName,
                F_Content = "你的流程有状态的更新：" + nWFEngineParamConfig.CurrentUser.Name + operationName,
                NodeId = nWFIEngine.GetStartNode().id,
                F_Id = Guid.NewGuid().ToString(),
                F_IsFinished = 0,
                F_CreateDate = DateTime.Now
            };

            NWFTaskRelationEntity nWFTaskRelationEntity = taskUserList.Find(t => t.F_UserId == taskUserId);
            if (nWFTaskRelationEntity == null)
            {
                nWFTaskRelationEntity = taskUserList[0];
            }
            nWFTaskRelationEntity.F_Time = DateTime.Now;
            // 如果是一般审核节点
            if (nodeInfo.isAllAuditor == "2")
            {
                // 需要所有人都审核，有一人不同意或者所有人都同意
                if (operationCode == "agree")
                {
                    nWFTaskRelationEntity.F_Result = 1;
                    if (taskUserList.FindAll(t => t.F_Result == 0).Count > 0)
                    {
                        List<NWFTaskRelationEntity> taskUserUpdateList = new List<NWFTaskRelationEntity>();
                        taskUserUpdateList.Add(nWFTaskRelationEntity);
                        if (nodeInfo.auditorType == "2")
                        {
                            // 串行
                            NWFTaskRelationEntity nWFTaskRelationEntity2 = taskUserList[(int)nWFTaskRelationEntity.F_Sort];
                            nWFTaskRelationEntity2.F_Mark = 0;
                            taskUserUpdateList.Add(nWFTaskRelationEntity2);
                        }
                        await nWFProcessSerive.Save(nWFTaskLogEntity, taskUserUpdateList, nWFTaskMsgEntity);
                        return;
                    }
                }
                else if (nodeInfo.auditExecutType == "2")
                {// 需要所有人执行完才往下走
                    if (operationCode == "disagree")
                    {
                        nWFTaskRelationEntity.F_Result = 2;
                    }
                    else
                    {
                        nWFTaskRelationEntity.F_Result = 4;
                    }
                    if (taskUserList.FindAll(t => t.F_Result == 0).Count > 0)
                    {
                        List<NWFTaskRelationEntity> taskUserUpdateList = new List<NWFTaskRelationEntity>();
                        taskUserUpdateList.Add(nWFTaskRelationEntity);
                        if (nodeInfo.auditorType == "2")
                        {
                            // 串行
                            NWFTaskRelationEntity nWFTaskRelationEntity2 = taskUserList[(int)nWFTaskRelationEntity.F_Sort];
                            nWFTaskRelationEntity2.F_Mark = 0;
                            taskUserUpdateList.Add(nWFTaskRelationEntity2);
                        }
                        await nWFProcessSerive.Save(nWFTaskLogEntity, taskUserUpdateList, nWFTaskMsgEntity);
                        return;
                    }
                }
                else
                {
                    operationCode = "disagree";
                    nWFTaskRelationEntity.F_Result = 2;
                }
            }
            else
            {
                if (operationCode == "agree")
                {
                    nWFTaskRelationEntity.F_Result = 1;
                }
                else if (operationCode == "disagree")
                {
                    nWFTaskRelationEntity.F_Result = 2;
                }
                else
                {
                    nWFTaskRelationEntity.F_Result = 4;
                }
            }

            // 获取下一节点信息
            List<NWFLineInfo> lineList = new List<NWFLineInfo>();
            List<NWFNodeInfo> list =await nWFIEngine.GetNextTaskNode(nodeInfo, operationCode, false, lineList);

            // 会签处理
            int state = 0;
            if (operationCode == "agree")
            {
                state = 1;
            }
            List<NWFTaskEntity> closeTaskList = new List<NWFTaskEntity>();
            List<NWFConfluenceEntity> confluenceList =(List<NWFConfluenceEntity>)await _ClearConfluence(list, closeTaskList, nodeInfo.id, nWFEngineParamConfig.ProcessId, state, nWFIEngine);

            // 创建任务
            List<NWFTaskEntity> taskList = (List<NWFTaskEntity>)await _CreateTask(list, nodeInfo, nWFEngineParamConfig);
            // 创建任务消息
            List<NWFTaskMsgEntity> taskMsgList =(List<NWFTaskMsgEntity>)_CreateTaskMsg(taskList, nWFEngineParamConfig);

            // 保存流程进程信息
            NWFProcessEntity nWFProcessEntity = new NWFProcessEntity()
            {
                F_Id = nWFEngineParamConfig.ProcessId,
                F_IsStart = 1
            };
            if (nWFEngineParamConfig.State == 1)
            {
                nWFProcessEntity.F_IsAgain = 1;
            }
            else if (nWFEngineParamConfig.State == 2)
            {
                nWFProcessEntity.F_IsFinished = 1;
            }

            List<NWFLineInfo> pLineList = new List<NWFLineInfo>();
            List<NWFTaskEntity> pTaskList = new List<NWFTaskEntity>();
            List<NWFTaskMsgEntity> pTaskMsgList = new List<NWFTaskMsgEntity>();
            NWFEngineParamConfig pNWFEngineParamConfig = null;
            NWFNodeInfo pNodeInfo = null;
            NWFIEngine pNWFIEngine = null;

            NWFProcessEntity pNWFProcessEntity = null;
            if (nWFEngineParamConfig.IsChild == 1)
            {
                pNWFIEngine = await _Bootstraper("", nWFEngineParamConfig.ParentProcessId, nWFEngineParamConfig.ParentTaskId, userInfo);
                pNWFEngineParamConfig = pNWFIEngine.GetConfig();
                // 获取父级流程
                nWFTaskMsgEntity.F_ToUserId = pNWFEngineParamConfig.CreateUser.Id;
                nWFTaskMsgEntity.F_ToName = pNWFEngineParamConfig.CreateUser.Name;
                nWFTaskMsgEntity.F_ToAccount = pNWFEngineParamConfig.CreateUser.Account;
                nWFTaskMsgEntity.F_Title = pNWFEngineParamConfig.SchemeName;
                nWFTaskMsgEntity.F_Content = "你的流程【子流程:" + nWFEngineParamConfig.SchemeName + "】有状态的更新：" + nWFEngineParamConfig.CurrentUser.Name + operationName;
                nWFTaskMsgEntity.NodeId = pNWFIEngine.GetStartNode().id;

                // 获取子流程
                NWFProcessEntity cNWFProcessEntity =await nWFProcessSerive.GetEntity(nWFEngineParamConfig.ProcessId);
                if (cNWFProcessEntity.F_IsAsyn == 0)
                {
                    if (nWFEngineParamConfig.State == 2)
                    {
                        // 父节点信息
                        NWFTaskEntity pTaskEntity =await _nWFTaskIBLL.GetEntity(nWFEngineParamConfig.ParentTaskId);
                        pNodeInfo = pNWFIEngine.GetNode(pTaskEntity.F_NodeId);

                        // 获取下一节点信息
                        List<NWFNodeInfo> pList =await pNWFIEngine.GetNextTaskNode(pNodeInfo, "agree", false, pLineList);
                        // 创建任务
                        pTaskList = (List<NWFTaskEntity>)await _CreateTask(pList, pNodeInfo, pNWFEngineParamConfig);
                        // 创建任务消息
                        pTaskMsgList = (List<NWFTaskMsgEntity>)_CreateTaskMsg(pTaskList, pNWFEngineParamConfig);

                        if (pNWFEngineParamConfig.State == 1)
                        {
                            pNWFProcessEntity = new NWFProcessEntity();
                            pNWFProcessEntity.F_Id = pNWFEngineParamConfig.ProcessId;
                            pNWFProcessEntity.F_IsAgain = 1;
                        }
                        else if (pNWFEngineParamConfig.State == 2)
                        {
                            pNWFProcessEntity = new NWFProcessEntity();
                            pNWFProcessEntity.F_Id = pNWFEngineParamConfig.ProcessId;
                            pNWFProcessEntity.F_IsFinished = 1;
                        }
                    }
                }
                pTaskMsgList.Add(nWFTaskMsgEntity);
            }
            else
            {
                taskMsgList.Add(nWFTaskMsgEntity);
            }

            // 触发消息
            await _SendMsg(pTaskMsgList, pNWFIEngine);
            // 触发消息
            await _SendMsg(taskMsgList, nWFIEngine);

            List<NWFTaskEntity> nTaskList = new List<NWFTaskEntity>();
            nTaskList.AddRange(taskList);

            taskList.AddRange(pTaskList);
            taskMsgList.AddRange(pTaskMsgList);
            // 保存信息 任务日志 任务执行人状态更新 任务状态更新 流程进程状态更新 会签信息更新 新的任务列表 新的任务消息列表
            await nWFProcessSerive.Save(nWFTaskLogEntity, nWFTaskRelationEntity, taskEntiy, nWFProcessEntity, confluenceList, closeTaskList, taskList, taskMsgList, pNWFProcessEntity);

            // 触发流程绑定方法(父级点事件)
            foreach (var line in pLineList)
            {
                await _TriggerMethod(line, "", pNodeInfo.name, "create", pNWFEngineParamConfig);
            }

            // 触发流程绑定方法
            foreach (var line in lineList)
            {
                await _TriggerMethod(line, taskId, nodeInfo.name, operationCode, nWFEngineParamConfig);
            }

            // 触发子流程节点方法
            foreach (var taskItem in taskList)
            {
                if (taskItem.F_Type == 4)
                {
                    NWFNodeInfo cNodeInfo = nWFIEngine.GetNode(taskItem.F_NodeId);
                    if (cNodeInfo == null)
                    {
                        cNodeInfo = pNWFIEngine.GetNode(taskItem.F_NodeId);
                        await _TriggerMethod(cNodeInfo, taskItem.F_Id, cNodeInfo.name, taskItem.F_ChildProcessId, pNWFEngineParamConfig);

                    }
                    else
                    {
                        await _TriggerMethod(cNodeInfo, taskItem.F_Id, cNodeInfo.name, taskItem.F_ChildProcessId, nWFEngineParamConfig);
                    }
                }
            }


            // 触发自动跳过规则
            await _AutoAuditFlow(nTaskList, nWFIEngine, userInfo);
            await _AutoAuditFlow(pTaskList, pNWFIEngine, userInfo);

        }
        /// <summary>
        /// 批量审核（只有同意和不同意）
        /// </summary>
        /// <param name="operationCode">操作码</param>
        /// <param name="taskIds">任务id串</param>
        public async Task AuditFlows(string operationCode, string taskIds)
        {
            string[] taskIdList = taskIds.Split(',');
            foreach (var taskId in taskIdList)
            {
                string operationName = operationCode == "agree" ? "同意" : "不同意";
                NWFTaskEntity taskEntity = await _nWFTaskIBLL.GetEntity(taskId);
                if (taskEntity != null && taskEntity.F_IsFinished == 0 && taskEntity.F_IsBatchAudit == 1)
                {
                    await AuditFlow(operationCode, operationName, taskEntity.F_ProcessId, taskId, "批量审核", "{}", "", "");
                }
            }
        }
        /// <summary>
        /// 流程加签
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="userId">加签人员</param>
        /// <param name="des">加签说明</param>
        public async Task SignFlow(string processId, string taskId, string userId, string des)
        {
            var userInfo = await this.CurrentUser();
            // 初始化流程引擎
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, taskId, userInfo);
            NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
            // 获取任务实体
            var taskEntiy =await _nWFTaskIBLL.GetEntity(taskId);
            if (taskEntiy == null)
            {
                throw (new Exception("找不到对应流程任务！"));
            }
            if (taskEntiy.F_IsFinished != 0)
            {
                throw (new Exception("该任务已经结束！"));
            }

            taskEntiy.F_ModifyDate = DateTime.Now;
            taskEntiy.F_ModifyUserId = userInfo.F_UserId;
            taskEntiy.F_ModifyUserName = userInfo.F_RealName;
            taskEntiy.F_IsFinished = 1;
            string taskUserId = userInfo.F_UserId;
            // 获取当前任务的执行人列表
            List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(taskId);
            bool isMyTask = false;
            Dictionary<string, string> taskUserMap = new Dictionary<string, string>();
            foreach (var item in taskUserList)
            {
                if (item.F_UserId == userInfo.F_UserId)
                {
                    isMyTask = true;
                }
                if (!taskUserMap.ContainsKey(userInfo.F_UserId))
                {
                    taskUserMap.Add(userInfo.F_UserId, "1");
                }
            }
            if (!isMyTask)
            {
                // 如果是委托任务
                var delegateList =await nWFProcessSerive.GetDelegateProcess(userInfo.F_UserId);
                foreach (var item in delegateList)
                {
                    if (taskUserMap.ContainsKey(item.F_UserId))
                    {
                        taskUserId = item.F_UserId;
                    }
                }
            }

            UserEntity userEntity =await _userIBLL.GetEntity(userId);
            // 创建任务日志信息
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = "sign",
                F_OperationName = "请求【" + userEntity.F_RealName + "】加签",
                F_NodeId = taskEntiy.F_NodeId,
                F_NodeName = taskEntiy.F_NodeName,
                F_PrevNodeId = taskEntiy.F_PrevNodeId,
                F_PrevNodeName = taskEntiy.F_PrevNodeName,
                F_Des = des,
                F_TaskId = taskId,
                F_TaskType = 8,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_TaskUserId = userInfo.F_UserId,
                F_TaskUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };
            if (userInfo.F_UserId != taskUserId)
            {
                // 说明是委托任务
                nWFTaskLogEntity.F_TaskUserId = taskUserId;
                nWFTaskLogEntity.F_TaskUserName =(await _userIBLL.GetEntity(taskUserId)).F_RealName;
            }
            // 给流程发起者一条通知信息
            NWFTaskMsgEntity nWFTaskMsgEntity = new NWFTaskMsgEntity()
            {
                F_ProcessId = nWFEngineParamConfig.ProcessId,
                F_FromUserId = nWFEngineParamConfig.CurrentUser.Id,
                F_FromUserAccount = nWFEngineParamConfig.CurrentUser.Account,
                F_FromUserName = nWFEngineParamConfig.CurrentUser.Name,
                F_ToUserId = nWFEngineParamConfig.CreateUser.Id,
                F_ToAccount = nWFEngineParamConfig.CreateUser.Account,
                F_ToName = nWFEngineParamConfig.CreateUser.Name,
                F_Title = nWFEngineParamConfig.SchemeName,
                F_Content = "你的流程有状态的更新：" + nWFEngineParamConfig.CurrentUser.Name + "加签",
                NodeId = nWFIEngine.GetStartNode().id,
                F_Id = Guid.NewGuid().ToString(),
                F_IsFinished = 0,
                F_CreateDate = DateTime.Now
            };

            NWFTaskRelationEntity nWFTaskRelationEntity = taskUserList.Find(t => t.F_UserId == taskUserId);
            nWFTaskRelationEntity.F_Time = DateTime.Now;
            nWFTaskRelationEntity.F_Result = 4;

            // 创建任务
            List<NWFTaskEntity> taskList = new List<NWFTaskEntity>();

            NWFNodeInfo nodeInfo = nWFIEngine.GetNode(taskEntiy.F_NodeId);
            NWFTaskEntity nWFTaskEntity = new NWFTaskEntity();
            nWFTaskEntity.F_Id = Guid.NewGuid().ToString();
            nWFTaskEntity.F_IsFinished = 0;
            nWFTaskEntity.F_IsUrge = 0;
            nWFTaskEntity.F_CreateDate = DateTime.Now;
            nWFTaskEntity.F_ProcessId = nWFEngineParamConfig.ProcessId;
            nWFTaskEntity.F_NodeId = taskEntiy.F_NodeId;
            nWFTaskEntity.F_NodeName = taskEntiy.F_NodeName;
            nWFTaskEntity.F_PrevNodeId = taskEntiy.F_NodeId;
            nWFTaskEntity.F_PrevNodeName = taskEntiy.F_NodeName;

            nWFTaskEntity.F_CreateUserId = userInfo.F_UserId;
            nWFTaskEntity.F_CreateUserName = userInfo.F_RealName;
            nWFTaskEntity.F_TimeoutAction = taskEntiy.F_TimeoutAction;
            nWFTaskEntity.F_TimeoutInterval = taskEntiy.F_TimeoutInterval;
            nWFTaskEntity.F_TimeoutNotice = taskEntiy.F_TimeoutNotice;
            nWFTaskEntity.F_TimeoutStrategy = taskEntiy.F_TimeoutStrategy;
            nWFTaskEntity.nWFUserInfoList = new List<NWFUserInfo>();

            nWFTaskEntity.nWFUserInfoList.Add(new NWFUserInfo()
            {
                Id = userId,
                Account = userEntity.F_Account,
                Name = userEntity.F_RealName
            });
            nWFTaskEntity.F_Type = 3;

            if (string.IsNullOrEmpty(taskEntiy.F_FirstUserId))
            {
                nWFTaskEntity.F_FirstUserId = taskUserId;
            }
            else
            {
                nWFTaskEntity.F_FirstUserId = taskEntiy.F_FirstUserId;
            }

            taskList.Add(nWFTaskEntity);
            // 创建任务消息
            List<NWFTaskMsgEntity> taskMsgList =(List<NWFTaskMsgEntity>)_CreateTaskMsg(taskList, nWFEngineParamConfig);

            List<NWFTaskMsgEntity> pTaskMsgList = new List<NWFTaskMsgEntity>();
            NWFEngineParamConfig pNWFEngineParamConfig = null;
            NWFIEngine pNWFIEngine = null;

            if (nWFEngineParamConfig.IsChild == 1)
            {
                pNWFIEngine =await _Bootstraper("", nWFEngineParamConfig.ParentProcessId, nWFEngineParamConfig.ParentTaskId, userInfo);
                pNWFEngineParamConfig = pNWFIEngine.GetConfig();
                // 获取父级流程
                nWFTaskMsgEntity.F_ToUserId = pNWFEngineParamConfig.CreateUser.Id;
                nWFTaskMsgEntity.F_ToName = pNWFEngineParamConfig.CreateUser.Name;
                nWFTaskMsgEntity.F_ToAccount = pNWFEngineParamConfig.CreateUser.Account;
                nWFTaskMsgEntity.F_Title = pNWFEngineParamConfig.SchemeName;
                nWFTaskMsgEntity.F_Content = "你的流程【子流程:" + nWFEngineParamConfig.SchemeName + "】有状态的更新：" + nWFEngineParamConfig.CurrentUser.Name + "加签";
                nWFTaskMsgEntity.NodeId = pNWFIEngine.GetStartNode().id;

                pTaskMsgList.Add(nWFTaskMsgEntity);
            }
            else
            {
                taskMsgList.Add(nWFTaskMsgEntity);
            }
            // 保存流程进程信息
            NWFProcessEntity nWFProcessEntity = new NWFProcessEntity()
            {
                F_Id = nWFEngineParamConfig.ProcessId,
                F_IsStart = 1
            };
            await nWFProcessSerive.Save(nWFTaskLogEntity, nWFTaskRelationEntity, taskEntiy, nWFProcessEntity, null, null, taskList, taskMsgList);

            // 触发消息
            await _SendMsg(pTaskMsgList, pNWFIEngine);
            await _SendMsg(taskMsgList, nWFIEngine);
        }
        /// <summary>
        /// 流程加签审核
        /// </summary>
        /// <param name="operationCode">审核操作码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="des">加签说明</param>
        public async Task SignAuditFlow(string operationCode, string processId, string taskId, string des)
        {
            var userInfo = await this.CurrentUser();

            // 初始化流程引擎
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, taskId, userInfo);
            NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
            // 获取任务实体
            var taskEntiy =await _nWFTaskIBLL.GetEntity(taskId);
            if (taskEntiy == null)
            {
                throw (new Exception("找不到对应流程任务！"));
            }
            if (taskEntiy.F_IsFinished != 0)
            {
                throw (new Exception("该任务已经结束！"));
            }

            taskEntiy.F_ModifyDate = DateTime.Now;
            taskEntiy.F_ModifyUserId = userInfo.F_UserId;
            taskEntiy.F_ModifyUserName = userInfo.F_RealName;
            taskEntiy.F_IsFinished = 1;
            string taskUserId = userInfo.F_UserId;
            // 获取当前任务的执行人列表
            List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(taskId);
            bool isMyTask = false;
            Dictionary<string, string> taskUserMap = new Dictionary<string, string>();
            foreach (var item in taskUserList)
            {
                if (item.F_UserId == userInfo.F_UserId)
                {
                    isMyTask = true;
                }
                if (!taskUserMap.ContainsKey(userInfo.F_UserId))
                {
                    taskUserMap.Add(userInfo.F_UserId, "1");
                }
            }
            if (!isMyTask)
            {
                // 如果是委托任务
                var delegateList = await nWFProcessSerive.GetDelegateProcess(userInfo.F_UserId);
                foreach (var item in delegateList)
                {
                    if (taskUserMap.ContainsKey(item.F_UserId))
                    {
                        taskUserId = item.F_UserId;
                    }
                }
            }

            UserEntity userEntity =await _userIBLL.GetEntity(taskEntiy.F_FirstUserId);
            // 创建任务日志信息
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = operationCode,
                F_OperationName = "【加签】" + (operationCode == "agree" ? "同意" : "不同意"),
                F_NodeId = taskEntiy.F_NodeId,
                F_NodeName = taskEntiy.F_NodeName,
                F_PrevNodeId = taskEntiy.F_PrevNodeId,
                F_PrevNodeName = taskEntiy.F_PrevNodeName,
                F_Des = des,
                F_TaskId = taskId,
                F_TaskType = 3,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_TaskUserId = userInfo.F_UserId,
                F_TaskUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };
            if (userInfo.F_UserId != taskUserId)
            {
                // 说明是委托任务
                nWFTaskLogEntity.F_TaskUserId = taskUserId;
                nWFTaskLogEntity.F_TaskUserName =(await _userIBLL.GetEntity(taskUserId)).F_RealName;
            }
            // 给流程发起者一条通知信息
            NWFTaskMsgEntity nWFTaskMsgEntity = new NWFTaskMsgEntity()
            {
                F_ProcessId = nWFEngineParamConfig.ProcessId,
                F_FromUserId = nWFEngineParamConfig.CurrentUser.Id,
                F_FromUserAccount = nWFEngineParamConfig.CurrentUser.Account,
                F_FromUserName = nWFEngineParamConfig.CurrentUser.Name,
                F_ToUserId = nWFEngineParamConfig.CreateUser.Id,
                F_ToAccount = nWFEngineParamConfig.CreateUser.Account,
                F_ToName = nWFEngineParamConfig.CreateUser.Name,
                F_Title = nWFEngineParamConfig.SchemeName,
                F_Content = "你的流程有状态的更新：" + nWFEngineParamConfig.CurrentUser.Name + "【加签】" + (operationCode == "agree" ? "同意" : "不同意"),
                NodeId = nWFIEngine.GetStartNode().id,
                F_Id = Guid.NewGuid().ToString(),
                F_IsFinished = 0,
                F_CreateDate = DateTime.Now
            };

            NWFTaskRelationEntity nWFTaskRelationEntity = taskUserList.Find(t => t.F_UserId == taskUserId);
            nWFTaskRelationEntity.F_Time = DateTime.Now;
            if (operationCode == "agree")
            {
                nWFTaskRelationEntity.F_Result = 1;
            }
            else
            {
                nWFTaskRelationEntity.F_Result = 2;
            }

            // 创建任务
            List<NWFTaskEntity> taskList = new List<NWFTaskEntity>();

            NWFNodeInfo nodeInfo = nWFIEngine.GetNode(taskEntiy.F_NodeId);
            NWFTaskEntity nWFTaskEntity = new NWFTaskEntity();
            nWFTaskEntity.F_Id = Guid.NewGuid().ToString();
            nWFTaskEntity.F_IsFinished = 0;
            nWFTaskEntity.F_IsUrge = 0;
            nWFTaskEntity.F_CreateDate = DateTime.Now;

            nWFTaskEntity.F_ProcessId = nWFEngineParamConfig.ProcessId;
            nWFTaskEntity.F_NodeId = taskEntiy.F_NodeId;
            nWFTaskEntity.F_NodeName = taskEntiy.F_NodeName;
            nWFTaskEntity.F_PrevNodeId = taskEntiy.F_NodeId;
            nWFTaskEntity.F_PrevNodeName = taskEntiy.F_NodeName;

            nWFTaskEntity.F_CreateUserId = userInfo.F_UserId;
            nWFTaskEntity.F_CreateUserName = userInfo.F_RealName;
            nWFTaskEntity.F_TimeoutAction = taskEntiy.F_TimeoutAction;
            nWFTaskEntity.F_TimeoutInterval = taskEntiy.F_TimeoutInterval;
            nWFTaskEntity.F_TimeoutNotice = taskEntiy.F_TimeoutNotice;
            nWFTaskEntity.F_TimeoutStrategy = taskEntiy.F_TimeoutStrategy;
            nWFTaskEntity.nWFUserInfoList = new List<NWFUserInfo>();

            nWFTaskEntity.nWFUserInfoList.Add(new NWFUserInfo()
            {
                Id = taskEntiy.F_FirstUserId,
                Account = userEntity.F_Account,
                Name = userEntity.F_RealName
            });
            nWFTaskEntity.F_Type = 1;
            taskList.Add(nWFTaskEntity);
            // 创建任务消息
            List<NWFTaskMsgEntity> taskMsgList =(List<NWFTaskMsgEntity>) _CreateTaskMsg(taskList, nWFEngineParamConfig);

            List<NWFTaskMsgEntity> pTaskMsgList = new List<NWFTaskMsgEntity>();
            NWFEngineParamConfig pNWFEngineParamConfig = null;
            NWFIEngine pNWFIEngine = null;

            if (nWFEngineParamConfig.IsChild == 1)
            {
                pNWFIEngine =await _Bootstraper("", nWFEngineParamConfig.ParentProcessId, nWFEngineParamConfig.ParentTaskId, userInfo);
                pNWFEngineParamConfig = pNWFIEngine.GetConfig();
                // 获取父级流程
                nWFTaskMsgEntity.F_ToUserId = pNWFEngineParamConfig.CreateUser.Id;
                nWFTaskMsgEntity.F_ToName = pNWFEngineParamConfig.CreateUser.Name;
                nWFTaskMsgEntity.F_ToAccount = pNWFEngineParamConfig.CreateUser.Account;
                nWFTaskMsgEntity.F_Title = pNWFEngineParamConfig.SchemeName;
                nWFTaskMsgEntity.F_Content = "你的流程【子流程:" + nWFEngineParamConfig.SchemeName + "】有状态的更新：" + nWFEngineParamConfig.CurrentUser.Name + "加签" + (operationCode == "agree" ? "同意" : "不同意");
                nWFTaskMsgEntity.NodeId = pNWFIEngine.GetStartNode().id;

                pTaskMsgList.Add(nWFTaskMsgEntity);
            }
            else
            {
                taskMsgList.Add(nWFTaskMsgEntity);
            }
            await nWFProcessSerive.Save(nWFTaskLogEntity, nWFTaskRelationEntity, taskEntiy, null, null, null, taskList, taskMsgList);

            // 触发消息
            await _SendMsg(pTaskMsgList, pNWFIEngine);
            await _SendMsg(taskMsgList, nWFIEngine);
        }
        /// <summary>
        /// 确认阅读
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        public async Task ReferFlow(string processId, string taskId)
        {
            var userInfo = await this.CurrentUser();
            // 获取任务实体
            var taskEntiy =await _nWFTaskIBLL.GetEntity(taskId);
            if (taskEntiy == null)
            {
                throw (new Exception("找不到对应流程任务！"));
            }
            if (taskEntiy.F_IsFinished != 0)
            {
                throw (new Exception("该任务已经结束！"));
            }

            // 创建任务日志信息
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = "agreeRefer",
                F_OperationName = "查阅流程",
                F_NodeId = taskEntiy.F_NodeId,
                F_NodeName = taskEntiy.F_NodeName,
                F_PrevNodeId = taskEntiy.F_PrevNodeId,
                F_PrevNodeName = taskEntiy.F_PrevNodeName,
                F_TaskId = taskId,
                F_TaskType = 2,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_TaskUserId = userInfo.F_UserId,
                F_TaskUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };

            List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(taskId);
            NWFTaskRelationEntity nWFTaskRelationEntity = taskUserList.Find(t => t.F_UserId == userInfo.F_UserId);
            nWFTaskRelationEntity.F_Time = DateTime.Now;
            nWFTaskRelationEntity.F_Result = 1;
            if (taskUserList.FindAll(t => t.F_Result == 0).Count == 0)
            {
                taskEntiy.F_ModifyDate = DateTime.Now;
                taskEntiy.F_ModifyUserId = userInfo.F_UserId;
                taskEntiy.F_ModifyUserName = userInfo.F_RealName;
                taskEntiy.F_IsFinished = 1;
            }
            else
            {
                taskEntiy = null;
            }
            await nWFProcessSerive.Save(nWFTaskLogEntity, nWFTaskRelationEntity, taskEntiy);
        }
        /// <summary>
        /// 催办流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        public async Task UrgeFlow(string processId)
        {
            var userInfo =await this.CurrentUser();
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, "", userInfo);
            NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
            // 获取未完成的任务
            IEnumerable<NWFTaskEntity> taskList =await _nWFTaskIBLL.GetUnFinishTaskList(processId);

            List<NWFTaskEntity> updateTaskList = new List<NWFTaskEntity>();
            List<NWFTaskMsgEntity> taskMsgList = new List<NWFTaskMsgEntity>();

            foreach (var item in taskList)
            {
                if (item.F_Type != 2 && item.F_Type != 5)
                {// 审批 加签 子流程
                    item.F_IsUrge = 1;
                    updateTaskList.Add(item);
                    // 获取当前任务执行人
                    List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(item.F_Id);
                    NWFNodeInfo nodeInfo = nWFIEngine.GetNode(item.F_NodeId);

                    foreach (var user in taskUserList)
                    {
                        if (user.F_Result == 0 && user.F_Mark == 0 && user.F_UserId != userInfo.F_UserId)
                        {
                            // 创建一条任务消息
                            UserEntity userEntity =await _userIBLL.GetEntity(user.F_UserId);
                            NWFTaskMsgEntity nWFTaskMsgEntity = new NWFTaskMsgEntity()
                            {
                                F_ProcessId = processId,
                                F_FromUserId = userInfo.F_UserId,
                                F_FromUserAccount = userInfo.F_Account,
                                F_FromUserName = userInfo.F_RealName,
                                F_ToUserId = userEntity.F_UserId,
                                F_ToAccount = userEntity.F_Account,
                                F_ToName = userEntity.F_RealName,
                                F_Title = nWFEngineParamConfig.SchemeName,
                                F_Content = nWFEngineParamConfig.SchemeName + "：【" + nodeInfo.name + "】请尽快审核,来自【" + userInfo.F_RealName + "】",
                                NodeId = item.F_NodeId,
                                F_Id = Guid.NewGuid().ToString(),
                                F_IsFinished = 0,
                                F_CreateDate = DateTime.Now
                            };
                            taskMsgList.Add(nWFTaskMsgEntity);

                            // 触发消息
                            await _SendMsg(taskMsgList, nWFIEngine);
                        }
                    }
                }
            }

            // 创建任务日志信息
            NWFNodeInfo startNodeInfo = nWFIEngine.GetStartNode();
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = "urgeFlow",
                F_OperationName = "催办审核",
                F_NodeId = startNodeInfo.id,
                F_NodeName = startNodeInfo.name,
                F_PrevNodeId = startNodeInfo.id,
                F_PrevNodeName = startNodeInfo.id,
                F_TaskType = 9,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_TaskUserId = userInfo.F_UserId,
                F_TaskUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };

            // 查看是否有子流程
            IEnumerable<NWFProcessEntity> cProcessList =await nWFProcessSerive.GetChildProcessList(processId);
            foreach (var processEntity in cProcessList)
            {
                List<NWFTaskMsgEntity> pTaskMsgList = new List<NWFTaskMsgEntity>();
                IEnumerable<NWFTaskEntity> cTaskList =await _nWFTaskIBLL.GetUnFinishTaskList(processEntity.F_Id);
                NWFIEngine pNWFIEngine =await _Bootstraper("", processEntity.F_Id, "", userInfo);
                NWFEngineParamConfig pNWFEngineParamConfig = pNWFIEngine.GetConfig();
                foreach (var item in cTaskList)
                {
                    if (item.F_Type != 2 && item.F_Type != 5)
                    {// 审批 加签 子流程
                        item.F_IsUrge = 1;
                        updateTaskList.Add(item);
                        // 获取当前任务执行人
                        List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(item.F_Id);
                        NWFNodeInfo nodeInfo = pNWFIEngine.GetNode(item.F_NodeId);

                        foreach (var user in taskUserList)
                        {
                            if (user.F_Result == 0 && user.F_Mark == 0 && user.F_UserId != userInfo.F_UserId)
                            {
                                // 创建一条任务消息
                                UserEntity userEntity =await _userIBLL.GetEntity(user.F_UserId);
                                NWFTaskMsgEntity nWFTaskMsgEntity = new NWFTaskMsgEntity()
                                {
                                    F_ProcessId = processEntity.F_Id,
                                    F_FromUserId = userInfo.F_UserId,
                                    F_FromUserAccount = userInfo.F_Account,
                                    F_FromUserName = userInfo.F_RealName,
                                    F_ToUserId = userEntity.F_UserId,
                                    F_ToAccount = userEntity.F_Account,
                                    F_ToName = userEntity.F_RealName,
                                    F_Title = nWFEngineParamConfig.SchemeName + "的子流程-" + pNWFEngineParamConfig.SchemeName,
                                    F_Content = pNWFEngineParamConfig.SchemeName + "：【" + nodeInfo.name + "】请尽快审核,来自【" + userInfo.F_RealName + "】",
                                    NodeId = item.F_NodeId,
                                    F_Id = Guid.NewGuid().ToString(),
                                    F_IsFinished = 0,
                                    F_CreateDate = DateTime.Now
                                };
                                pTaskMsgList.Add(nWFTaskMsgEntity);

                                // 触发消息
                                await _SendMsg(pTaskMsgList, pNWFIEngine);
                                taskMsgList.AddRange(pTaskMsgList);
                            }
                        }
                    }
                }
            }

            await nWFProcessSerive.Save(nWFTaskLogEntity, updateTaskList, taskMsgList);
        }
        /// <summary>
        /// 撤销流程（只有在该流程未被处理的情况下）
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        public async Task RevokeFlow(string processId)
        {
            NWFProcessEntity processEntity = await nWFProcessSerive.GetEntity(processId);
            if (processEntity.F_IsStart != 1)
            {
                var userInfo = await this.CurrentUser();
                // 执行
                NWFIEngine nWFIEngine =await _Bootstraper("", processId, "", userInfo);
                NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
                var scheme = nWFIEngine.GetSchemeObj();

                // 删除任务
                var taskList =await _nWFTaskIBLL.GetALLTaskList(processId);
                await nWFProcessSerive.Save(processId, taskList, 2);

                await _TriggerMethod(scheme.closeDo, "1", nWFEngineParamConfig);

            }
        }

        /// <summary>
        /// 撤销审核（只有在下一个节点未被处理的情况下才能执行）
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">任务主键</param>
        public async Task<bool> RevokeAudit(string processId, string taskId)
        {
            bool res = false;
            var userInfo = await this.CurrentUser();
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, taskId, userInfo);
            // 获取任务，获取任务节点
            var taskEntity =await _nWFTaskIBLL.GetEntity(taskId);
            var nodeEntity = nWFIEngine.GetNode(taskEntity.F_NodeId);
            var taskLogEntity =await _nWFTaskIBLL.GetLogEntity(taskId, userInfo.F_UserId);

            if (string.IsNullOrEmpty(processId))
            {
                processId = taskEntity.F_ProcessId;
            }

            if (!await _nWFTaskIBLL.IsRevokeTask(processId, taskEntity.F_NodeId))
            {
                return false;
            }

            if (taskLogEntity.F_TaskType == 1)
            {// 普通审核才允许撤销审核
                if (taskEntity.F_IsFinished == 0 && nodeEntity.isAllAuditor == "2")
                {
                    var taskUserList3 = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(taskEntity.F_Id);
                    var taskUserEntity3 = taskUserList3.Find(t => t.F_UserId == userInfo.F_UserId);
                    if (nodeEntity.auditorType == "1")// 并行
                    {
                        // 创建任务日志信息
                        NWFTaskLogEntity nWFTaskLogEntity1 = new NWFTaskLogEntity()
                        {
                            F_ProcessId = processId,
                            F_OperationCode = "revokeAudit",
                            F_OperationName = "撤销审核",
                            F_NodeId = taskEntity.F_NodeId,
                            F_NodeName = taskEntity.F_NodeName,
                            F_PrevNodeId = taskEntity.F_PrevNodeId,
                            F_PrevNodeName = taskEntity.F_PrevNodeName,
                            F_TaskId = taskId,
                            F_TaskType = 100,
                            F_CreateUserId = userInfo.F_UserId,
                            F_CreateUserName = userInfo.F_RealName,
                            F_TaskUserId = userInfo.F_UserId,
                            F_TaskUserName = userInfo.F_RealName,
                            F_Id = Guid.NewGuid().ToString(),
                            F_CreateDate = DateTime.Now
                        };

                        await nWFProcessSerive.RevokeAudit(null, taskUserEntity3, null, nWFTaskLogEntity1);
                        return true;
                    }
                    else
                    {
                        int sort = (int)taskUserEntity3.F_Sort + 1;
                        var taskUserEntity4 = taskUserList3.Find(t => t.F_Sort == sort);
                        if (taskUserEntity4 != null && taskUserEntity4.F_Result == 0)
                        {
                            // 创建任务日志信息
                            NWFTaskLogEntity nWFTaskLogEntity2 = new NWFTaskLogEntity()
                            {
                                F_ProcessId = processId,
                                F_OperationCode = "revokeAudit",
                                F_OperationName = "撤销审核",
                                F_NodeId = taskEntity.F_NodeId,
                                F_NodeName = taskEntity.F_NodeName,
                                F_PrevNodeId = taskEntity.F_PrevNodeId,
                                F_PrevNodeName = taskEntity.F_PrevNodeName,
                                F_TaskId = taskId,
                                F_TaskType = 100,
                                F_CreateUserId = userInfo.F_UserId,
                                F_CreateUserName = userInfo.F_RealName,
                                F_TaskUserId = userInfo.F_UserId,
                                F_TaskUserName = userInfo.F_RealName,
                                F_Id = Guid.NewGuid().ToString(),
                                F_CreateDate = DateTime.Now
                            };

                            await nWFProcessSerive.RevokeAudit(null, taskUserEntity3, null, nWFTaskLogEntity2, taskUserEntity4);
                            return true;
                        }
                    }
                }
                else
                {
                    var taskList =await _nWFTaskIBLL.GetUnFinishTaskList(processId);
                    List<string> deleteTaskList = new List<string>();
                    // 撤销流程执行线段上绑定的相应方法
                    List<NWFLineInfo> lines = new List<NWFLineInfo>();
                    foreach (var taskItem in taskList)
                    {
                        if (taskItem.F_PrevNodeId == taskEntity.F_NodeId && taskItem.F_IsFinished == 0)
                        {
                            var taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(taskItem.F_Id);
                            if (taskUserList.FindAll(t => t.F_Result != 0).Count == 0)
                            {
                                deleteTaskList.Add(taskItem.F_Id);
                                //nWFIEngine.GetLines(taskEntity.F_NodeId, taskItem.F_NodeId, lines);
                            }
                        }
                    }
                    if (deleteTaskList.Count > 0)
                    {
                        taskEntity.F_IsFinished = 0;
                        var taskUserList2 = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(taskEntity.F_Id);
                        var taskUserEntity = taskUserList2.Find(t => t.F_UserId == userInfo.F_UserId);
                        // 创建任务日志信息
                        NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
                        {
                            F_ProcessId = processId,
                            F_OperationCode = "revokeAudit",
                            F_OperationName = "撤销审核",
                            F_NodeId = taskEntity.F_NodeId,
                            F_NodeName = taskEntity.F_NodeName,
                            F_PrevNodeId = taskEntity.F_PrevNodeId,
                            F_PrevNodeName = taskEntity.F_PrevNodeName,
                            F_TaskId = taskId,
                            F_TaskType = 100,
                            F_CreateUserId = userInfo.F_UserId,
                            F_CreateUserName = userInfo.F_RealName,
                            F_TaskUserId = userInfo.F_UserId,
                            F_TaskUserName = userInfo.F_RealName,
                            F_Id = Guid.NewGuid().ToString(),
                            F_CreateDate = DateTime.Now
                        };
                        await nWFProcessSerive.RevokeAudit(deleteTaskList, taskUserEntity, taskEntity, nWFTaskLogEntity);

                        await nWFIEngine.GetNextTaskNode(nodeEntity, taskLogEntity.F_OperationCode, false, lines);
                        foreach (var line in lines)
                        {
                            await _TriggerMethodR(line, taskEntity.F_Id, taskEntity.F_NodeName, nWFIEngine.GetConfig());
                        }

                        return true;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// 流程任务超时处理
        /// </summary>
        public async Task MakeTaskTimeout()
        {
            try
            {
                // 获取所有未完成的任务
                var taskList = await _nWFTaskIBLL.GetUnFinishTaskList();
                foreach (var task in taskList)
                {
                    try
                    {
                        if (task.F_CreateDate != null)
                        {
                            DateTime time = (DateTime)task.F_CreateDate;
                            bool isNext = false;
                            #region 流转到下一节点
                            // 是否需要流转到下一节点
                            if (task.F_TimeoutAction > 0)
                            {
                                DateTime actionTime = time.AddHours((int)task.F_TimeoutAction);// 获取需要流转到下一节点的时间
                                if (DateTime.Now >= actionTime)
                                {
                                    NWFIEngine nWFIEngine =await _Bootstraper("", task.F_ProcessId, task.F_Id, null);
                                    NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
                                    nWFEngineParamConfig.CurrentUser = new NWFUserInfo();
                                    nWFEngineParamConfig.CurrentUser.Id = "System";
                                    nWFEngineParamConfig.CurrentUser.Name = "系统";
                                    NWFNodeInfo nodeInfo = nWFIEngine.GetNode(task.F_NodeId);

                                    // 任务
                                    task.F_ModifyDate = DateTime.Now;
                                    task.F_IsFinished = 2;

                                    // 创建任务日志信息
                                    NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
                                    {
                                        F_ProcessId = task.F_ProcessId,
                                        F_OperationCode = "lrtimeout",
                                        F_OperationName = "超时",
                                        F_NodeId = task.F_NodeId,
                                        F_NodeName = task.F_NodeName,
                                        F_PrevNodeId = task.F_PrevNodeId,
                                        F_PrevNodeName = task.F_PrevNodeName,
                                        F_TaskId = task.F_Id,
                                        F_TaskType = 6,
                                        F_Id = Guid.NewGuid().ToString(),
                                        F_CreateDate = DateTime.Now,
                                        F_CreateUserId = "System",
                                        F_CreateUserName = "系统"
                                    };

                                    // 给流程发起者一条通知信息
                                    NWFTaskMsgEntity nWFTaskMsgEntity = new NWFTaskMsgEntity()
                                    {
                                        F_ProcessId = nWFEngineParamConfig.ProcessId,
                                        F_ToUserId = nWFEngineParamConfig.CreateUser.Id,
                                        F_ToAccount = nWFEngineParamConfig.CreateUser.Account,
                                        F_ToName = nWFEngineParamConfig.CreateUser.Name,
                                        F_Title = nWFEngineParamConfig.SchemeName,
                                        F_Content = "你的流程有状态的更新：【" + task.F_NodeName + "】超时流转",
                                        NodeId = nWFIEngine.GetStartNode().id,
                                        F_Id = Guid.NewGuid().ToString(),
                                        F_IsFinished = 0,
                                        F_CreateDate = DateTime.Now
                                    };

                                    // 获取下一节点信息
                                    List<NWFLineInfo> lineList = new List<NWFLineInfo>();
                                    List<NWFNodeInfo> list =await nWFIEngine.GetNextTaskNode(nodeInfo, "lrtimeout", false, lineList);

                                    // 创建任务
                                    List<NWFTaskEntity> nTaskList =(List<NWFTaskEntity>)await _CreateTask(list, nodeInfo, nWFEngineParamConfig);
                                    // 创建任务消息
                                    List<NWFTaskMsgEntity> taskMsgList =(List<NWFTaskMsgEntity>)_CreateTaskMsg(nTaskList, nWFEngineParamConfig);

                                    // 保存流程进程信息
                                    NWFProcessEntity nWFProcessEntity = new NWFProcessEntity()
                                    {
                                        F_Id = nWFEngineParamConfig.ProcessId,
                                        F_IsStart = 1
                                    };
                                    if (nWFEngineParamConfig.State == 1)
                                    {
                                        nWFProcessEntity.F_IsAgain = 1;
                                    }
                                    else if (nWFEngineParamConfig.State == 2)
                                    {
                                        nWFProcessEntity.F_IsFinished = 1;
                                    }

                                    #region 子流程处理

                                    List<NWFLineInfo> pLineList = new List<NWFLineInfo>();
                                    List<NWFTaskEntity> pTaskList = new List<NWFTaskEntity>();
                                    List<NWFTaskMsgEntity> pTaskMsgList = new List<NWFTaskMsgEntity>();
                                    NWFEngineParamConfig pNWFEngineParamConfig = null;
                                    NWFNodeInfo pNodeInfo = null;
                                    NWFIEngine pNWFIEngine = null;

                                    NWFProcessEntity pNWFProcessEntity = null;
                                    if (nWFEngineParamConfig.IsChild == 1)
                                    {
                                        pNWFIEngine =await _Bootstraper("", nWFEngineParamConfig.ParentProcessId, nWFEngineParamConfig.ParentTaskId, null);
                                        pNWFEngineParamConfig = pNWFIEngine.GetConfig();
                                        pNWFEngineParamConfig.CurrentUser = new NWFUserInfo();
                                        // 获取父级流程
                                        nWFTaskMsgEntity.F_ToUserId = pNWFEngineParamConfig.CreateUser.Id;
                                        nWFTaskMsgEntity.F_ToName = pNWFEngineParamConfig.CreateUser.Name;
                                        nWFTaskMsgEntity.F_ToAccount = pNWFEngineParamConfig.CreateUser.Account;
                                        nWFTaskMsgEntity.F_Title = pNWFEngineParamConfig.SchemeName;
                                        nWFTaskMsgEntity.F_Content = "你的流程【子流程:" + nWFEngineParamConfig.SchemeName + "】有状态的更新：【" + task.F_NodeName + "】超时流转";
                                        nWFTaskMsgEntity.NodeId = pNWFIEngine.GetStartNode().id;

                                        // 获取子流程
                                        NWFProcessEntity cNWFProcessEntity =await nWFProcessSerive.GetEntity(nWFEngineParamConfig.ProcessId);
                                        if (cNWFProcessEntity.F_IsAsyn == 0)
                                        {
                                            if (nWFEngineParamConfig.State == 2)
                                            {
                                                // 父节点信息
                                                NWFTaskEntity pTaskEntity =await _nWFTaskIBLL.GetEntity(nWFEngineParamConfig.ParentTaskId);
                                                pNodeInfo = pNWFIEngine.GetNode(pTaskEntity.F_NodeId);

                                                // 获取下一节点信息
                                                List<NWFNodeInfo> pList =await pNWFIEngine.GetNextTaskNode(pNodeInfo, "agree", false, pLineList);
                                                // 创建任务
                                                pTaskList = (List<NWFTaskEntity>)await _CreateTask(pList, pNodeInfo, pNWFEngineParamConfig);
                                                // 创建任务消息
                                                pTaskMsgList = (List<NWFTaskMsgEntity>)_CreateTaskMsg(pTaskList, pNWFEngineParamConfig);

                                                if (pNWFEngineParamConfig.State == 1)
                                                {
                                                    pNWFProcessEntity = new NWFProcessEntity();
                                                    pNWFProcessEntity.F_IsAgain = 1;
                                                }
                                                else if (pNWFEngineParamConfig.State == 2)
                                                {
                                                    pNWFProcessEntity = new NWFProcessEntity();
                                                    pNWFProcessEntity.F_IsFinished = 1;
                                                }
                                            }
                                        }
                                        pTaskMsgList.Add(nWFTaskMsgEntity);
                                    }
                                    else
                                    {
                                        taskMsgList.Add(nWFTaskMsgEntity);
                                    }
                                    #endregion


                                    // 触发消息
                                    await _SendMsg(pTaskMsgList, pNWFIEngine);
                                    // 触发消息
                                    await _SendMsg(taskMsgList, nWFIEngine);

                                    nTaskList.AddRange(pTaskList);
                                    taskMsgList.AddRange(pTaskMsgList);
                                    // 保存信息 任务日志 任务执行人状态更新 任务状态更新 流程进程状态更新 会签信息更新 新的任务列表 新的任务消息列表
                                    await nWFProcessSerive.Save(nWFTaskLogEntity, null, task, nWFProcessEntity, null, null, nTaskList, taskMsgList, pNWFProcessEntity);

                                    // 触发流程绑定方法(父级点事件)
                                    foreach (var line in pLineList)
                                    {
                                        await _TriggerMethod(line, "", pNodeInfo.name, "create", pNWFEngineParamConfig);
                                    }

                                    // 触发流程绑定方法
                                    foreach (var line in lineList)
                                    {
                                        await _TriggerMethod(line, task.F_Id, nodeInfo.name, "lrtimeout", nWFEngineParamConfig);
                                    }

                                    // 触发子流程节点方法
                                    foreach (var taskItem in taskList)
                                    {
                                        if (taskItem.F_Type == 4)
                                        {
                                            NWFNodeInfo cNodeInfo = nWFIEngine.GetNode(taskItem.F_NodeId);
                                            if (cNodeInfo == null)
                                            {
                                                cNodeInfo = pNWFIEngine.GetNode(taskItem.F_NodeId);
                                                await _TriggerMethod(cNodeInfo, taskItem.F_Id, cNodeInfo.name, taskItem.F_ChildProcessId, pNWFEngineParamConfig);

                                            }
                                            else
                                            {
                                                await _TriggerMethod(cNodeInfo, taskItem.F_Id, cNodeInfo.name, taskItem.F_ChildProcessId, nWFEngineParamConfig);
                                            }
                                        }
                                    }

                                    isNext = true;
                                }
                            }
                            #endregion

                            #region 消息提醒
                            // 消息
                            if (!isNext && string.IsNullOrEmpty(task.F_TimeoutStrategy) && task.F_TimeoutNotice > 0)
                            {
                                DateTime noticeTime = time.AddHours((int)task.F_TimeoutNotice);// 获取下一次发送消息时间
                                if (DateTime.Now  >= noticeTime)
                                {
                                    NWFProcessEntity nWFProcessEntity =await nWFProcessSerive.GetEntity(task.F_ProcessId);
                                    // 获取当前任务执行人
                                    List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(task.F_Id);
                                    // 创建任务消息
                                    List<NWFTaskMsgEntity> taskMsgList = new List<NWFTaskMsgEntity>();
                                    List<UserEntity> msgUserList = new List<UserEntity>();
                                    string content = nWFProcessEntity.F_SchemeName + "：【" + task.F_NodeName + "】请尽快审核,来自【系统提醒】";
                                    foreach (var taskUser in taskUserList)
                                    {
                                        // 创建一条任务消息
                                        UserEntity userEntity =await _userIBLL.GetEntity(taskUser.F_UserId);
                                        NWFTaskMsgEntity nWFTaskMsgEntity = new NWFTaskMsgEntity()
                                        {
                                            F_ProcessId = task.F_ProcessId,
                                            F_ToUserId = userEntity.F_UserId,
                                            F_ToAccount = userEntity.F_Account,
                                            F_ToName = userEntity.F_RealName,
                                            F_Title = nWFProcessEntity.F_SchemeName,
                                            F_Content = content,
                                            F_Id = Guid.NewGuid().ToString(),
                                            F_IsFinished = 0,
                                            F_CreateDate = DateTime.Now
                                        };
                                        taskMsgList.Add(nWFTaskMsgEntity);

                                        msgUserList.Add(userEntity);
                                    }
                                    await _mStrategyInfoIBLL.SendMessage(task.F_TimeoutStrategy, content, msgUserList.ToJson());

                                    NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
                                    {
                                        F_ProcessId = task.F_ProcessId,
                                        F_OperationCode = "timeoutFlow",
                                        F_OperationName = "超时提醒",
                                        F_NodeId = task.F_NodeId,
                                        F_NodeName = task.F_NodeName,
                                        F_PrevNodeId = task.F_NodeId,
                                        F_PrevNodeName = task.F_NodeName,
                                        F_TaskType = 10,
                                        F_Id = Guid.NewGuid().ToString(),
                                        F_CreateDate = DateTime.Now,
                                        F_CreateUserId = "System",
                                        F_CreateUserName = "系统"
                                    };

                                    // 保存消息和任务状态
                                    task.F_IsUrge = 1;
                                    if (task.F_TimeoutInterval > 0)
                                    {
                                        task.F_TimeoutNotice += task.F_TimeoutInterval;
                                    }
                                    else
                                    {
                                        task.F_TimeoutNotice = 0;
                                    }
                                    await nWFProcessSerive.Save(nWFTaskLogEntity, task, taskMsgList);
                                }
                            }
                            #endregion
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 获取流程当前任务需要处理的人
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFTaskEntity>> GetTaskUserList(string processId)
        {
            if (string.IsNullOrEmpty(processId))
            {
                return new List<NWFTaskEntity>();
            }

            var taskList =await _nWFTaskIBLL.GetUnFinishTaskList(processId);
            foreach (var item in taskList)
            {
                item.nWFUserInfoList = new List<NWFUserInfo>();
                // 获取当前任务执行人
                List<NWFTaskRelationEntity> taskUserList = (List<NWFTaskRelationEntity>)await _nWFTaskIBLL.GetTaskUserList(item.F_Id);
                foreach (var user in taskUserList)
                {
                    if (user.F_Result == 0 && user.F_Mark == 0)
                    {
                        item.nWFUserInfoList.Add(new NWFUserInfo()
                        {
                            Id = user.F_UserId
                        });
                    }
                }
            }
            return taskList;
        }
        /// <summary>
        /// 指派流程审核人
        /// </summary>
        /// <param name="list">任务列表</param>
        public async Task AppointUser(IEnumerable<NWFTaskEntity> list)
        {
            var userInfo = await this.CurrentUser();

            // 更新任务执行人
            List<NWFTaskRelationEntity> nWFTaskRelationlist = new List<NWFTaskRelationEntity>();
            List<string> taskList = new List<string>();
            string processId = "";
            string content = "";
            foreach (var task in list)
            {
                processId = task.F_ProcessId;
                taskList.Add(task.F_Id);
                content += "【" + task.F_NodeName + "】审核人更新为：";
                foreach (var taskUser in task.nWFUserInfoList)
                {
                    content += taskUser.Name + ",";

                    nWFTaskRelationlist.Add(new NWFTaskRelationEntity()
                    {
                        F_Id = Guid.NewGuid().ToString(),
                        F_Mark = 0,
                        F_TaskId = task.F_Id,
                        F_Result = 0,
                        F_UserId = taskUser.Id,
                        F_Time = DateTime.Now
                    });
                }
                content = content.Remove(content.Length - 1, 1);
                content += ";";
            }
            // 操作日志
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = "appoint",
                F_OperationName = content,
                F_TaskType = 100,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };

            await _nWFTaskIBLL.Save(nWFTaskRelationlist, taskList, nWFTaskLogEntity);
        }

        /// <summary>
        /// 作废流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        public async Task DeleteFlow(string processId)
        {
            var userInfo = await this.CurrentUser();
            // 执行
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, "", userInfo);
            NWFEngineParamConfig nWFEngineParamConfig = nWFIEngine.GetConfig();
            var scheme = nWFIEngine.GetSchemeObj();

            // 删除任务
            var taskList =await _nWFTaskIBLL.GetALLTaskList(processId);
            // 操作日志
            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
            {
                F_ProcessId = processId,
                F_OperationCode = "deleteFlow",
                F_OperationName = "作废流程",
                F_TaskType = 100,
                F_CreateUserId = userInfo.F_UserId,
                F_CreateUserName = userInfo.F_RealName,
                F_Id = Guid.NewGuid().ToString(),
                F_CreateDate = DateTime.Now
            };

            await nWFProcessSerive.Save(processId, taskList, 3, nWFTaskLogEntity);

            // 处理其子流程
            IEnumerable<NWFProcessEntity> cProcessList =await nWFProcessSerive.GetChildProcessList(processId);
            foreach (var processEntity in cProcessList)
            {
                NWFIEngine cNWFIEngine =await _Bootstraper("", processEntity.F_Id, "", userInfo);
                NWFEngineParamConfig cNWFEngineParamConfig = cNWFIEngine.GetConfig();
                var cScheme = nWFIEngine.GetSchemeObj();

                var cTaskList =await _nWFTaskIBLL.GetALLTaskList(processEntity.F_Id);
                await nWFProcessSerive.Save(processEntity.F_Id, cTaskList, 3);

                await _TriggerMethod(cScheme.closeDo, "2", cNWFEngineParamConfig);
            }

            await _TriggerMethod(scheme.closeDo, "2", nWFEngineParamConfig);
        }


        /// <summary>
        /// 给指定的流程添加审核节点
        /// </summary>
        /// <param name="processId">流程实例ID</param>
        /// <param name="bNodeId">开始节点</param>
        /// <param name="eNodeId">结束节点（审核任务的节点）</param>
        public async Task AddTask(string processId, string bNodeId, string eNodeId)
        {
            var userInfo = await this.CurrentUser();
            NWFIEngine nWFIEngine =await _Bootstraper("", processId, "", userInfo);
            NWFNodeInfo bNodeInfo = nWFIEngine.GetNode(bNodeId);
            NWFNodeInfo eNodeInfo = nWFIEngine.GetNode(eNodeId);
            List<NWFNodeInfo> list = new List<NWFNodeInfo>();
            list.Add(eNodeInfo);
            List<NWFTaskEntity> taskList =(List<NWFTaskEntity>)await _CreateTask(list, bNodeInfo, nWFIEngine.GetConfig());

            await nWFProcessSerive.SaveTask(taskList);
        }

        #endregion

        #region 获取sql语句
        /// <summary>
        /// 获取我的流程信息列表SQL语句
        /// </summary>
        /// <returns></returns>
        public string GetMySql()
        {
            return nWFProcessSerive.GetMySql();
        }
        /// <summary>
        /// 获取我的代办任务列表SQL语句
        /// </summary>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        public async Task<string> GetMyTaskSql(bool isBatchAudit = false)
        {
            var userInfo = await this.CurrentUser();
            return await nWFProcessSerive.GetMyTaskSql(userInfo, isBatchAudit);
        }
        /// <summary>
        /// 获取我的已办任务列表SQL语句
        /// </summary>
        /// <returns></returns>
        public string GetMyFinishTaskSql()
        {
            return nWFProcessSerive.GetMyFinishTaskSql();
        }
        #endregion
    }
}
