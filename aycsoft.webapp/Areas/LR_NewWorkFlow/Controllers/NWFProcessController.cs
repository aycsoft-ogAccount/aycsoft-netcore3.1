using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_NewWorkFlow.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.03.16
    /// 描 述：流程实例
    /// </summary>
    [Area("LR_NewWorkFlow")]
    public class NWFProcessController : MvcControllerBase
    {
        private NWFProcessIBLL _nWFProcessIBLL;
        private NWFSchemeIBLL _nWFSchemeIBLL;
        private NWFTaskIBLL _nWFTaskIBLL;
        //private UserIBLL userIBLL = new UserBLL();
        //private UserRelationIBLL userRelationIBLL = new UserRelationBLL();

        public NWFProcessController(NWFProcessIBLL nWFProcessIBLL, NWFSchemeIBLL nWFSchemeIBLL, NWFTaskIBLL nWFTaskIBLL) {
            _nWFProcessIBLL = nWFProcessIBLL;
            _nWFSchemeIBLL = nWFSchemeIBLL;
            _nWFTaskIBLL = nWFTaskIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 视图功能
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 发起流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ReleaseForm()
        {
            return View();
        }
        /// <summary>
        /// 流程容器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NWFContainerForm()
        {
            return View();
        }
        /// <summary>
        /// 查看节点审核信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LookNodeForm()
        {
            return View();
        }

        /// <summary>
        /// 创建流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateForm()
        {
            return View();
        }
        /// <summary>
        /// 审核流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AuditFlowForm()
        {
            return View();
        }
        /// <summary>
        /// 加签审核流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignAuditFlowForm()
        {
            return View();
        }
        /// <summary>
        /// 加签审核
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignFlowForm()
        {
            return View();
        }

        /// <summary>
        /// 流程进度查看（父子流程）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LookFlowForm()
        {
            return View();
        }

        /// <summary>
        /// 监控页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MonitorIndex()
        {
            return View();
        }
        /// <summary>
        /// 监控详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MonitorDetailsIndex()
        {
            return View();
        }
        /// <summary>
        /// 查看各个节点表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MonitorForm()
        {
            return View();
        }
        /// <summary>
        /// 指定审核人
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AppointForm()
        {
            return View();
        }
        /// <summary>
        /// 添加审核节点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddTaskForm()
        {
            return View();
        }
        /// <summary>
        /// 批量审核页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BatchAuditIndex()
        {
            return View();
        }

        /// <summary>
        /// 选择下一节点审核人员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SelectUserForm()
        {
            return View();
        }
        /// <summary>
        /// 签名弹层
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignForm()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPorcessList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = await _nWFProcessIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = list,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTaskPageList(string pagination, string queryJson, string categoryId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            IEnumerable<NWFProcessEntity> list = new List<NWFProcessEntity>();

            switch (categoryId)
            {
                case "1":
                    list =await _nWFProcessIBLL.GetMyPageList(paginationobj, queryJson);
                    break;
                case "2":
                    list =await _nWFProcessIBLL.GetMyTaskPageList(paginationobj, queryJson);
                    break;
                case "3":
                    list =await _nWFProcessIBLL.GetMyFinishTaskPageList(paginationobj, queryJson);
                    break;
            }

            var jsonData = new
            {
                rows = list,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取批量审核任务清单
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetBatchTaskPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _nWFProcessIBLL.GetMyTaskPageList(paginationobj, queryJson, true);
            var jsonData = new
            {
                rows = data,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTask(string taskId)
        {
            var data =await _nWFTaskIBLL.GetEntity(taskId);
            return Success(data);
        }
        #endregion

        #region 保存更新删除
        /// <summary>
        /// 删除流程进程实体
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteEntity(string processId)
        {
            await _nWFProcessIBLL.DeleteEntity(processId);
            return SuccessInfo("删除成功");
        }
        #endregion

        #region 流程API
        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <param name="code">流程编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetSchemeByCode(string code)
        {
            var schemeInfo = await _nWFSchemeIBLL.GetInfoEntityByCode(code);
            if (schemeInfo != null)
            {
                var data =await _nWFSchemeIBLL.GetSchemeEntity(schemeInfo.F_SchemeId);
                return Success(data);
            }
            return Fail("找不到该流程模板");
        }
        /// <summary>
        /// 根据流程进程主键获取流程模板
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetSchemeByProcessId(string processId)
        {
            var processEntity =await _nWFProcessIBLL.GetEntity(processId);
            if (processEntity != null)
            {
                if (string.IsNullOrEmpty(processEntity.F_SchemeId))
                {
                    var schemeInfo =await _nWFSchemeIBLL.GetInfoEntityByCode(processEntity.F_SchemeCode);
                    if (schemeInfo != null)
                    {
                        var data =await _nWFSchemeIBLL.GetSchemeEntity(schemeInfo.F_SchemeId);
                        return Success(data);
                    }
                }
                else
                {
                    var data =await _nWFSchemeIBLL.GetSchemeEntity(processEntity.F_SchemeId);
                    return Success(data);
                }
            }
            return Fail("找不到该流程模板");
        }
        /// <summary>
        /// 获取流程下一节点审核人员
        /// </summary>
        /// <param name="code">流程编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="nodeId">节点ID</param>
        /// <param name="operationCode">操作编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetNextAuditors(string code, string processId, string taskId, string nodeId, string operationCode)
        {
            var data =await _nWFProcessIBLL.GetNextAuditors(code, processId, taskId, nodeId, operationCode);
            return Success(data);
        }
        /// <summary>
        /// 获取流程进程信息
        /// </summary>
        /// <param name="processId">进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetProcessDetails(string processId, string taskId)
        {
            var data =await _nWFProcessIBLL.GetProcessDetails(processId, taskId);
            if (!string.IsNullOrEmpty(data.childProcessId))
            {
                processId = data.childProcessId;
            }

            var taskNode =await _nWFProcessIBLL.GetTaskUserList(processId);

            var jsonData = new
            {
                info = data,
                task = taskNode
            };

            return Success(jsonData);
        }

        /// <summary>
        /// 获取子流程详细信息
        /// </summary>
        /// <param name="processId">父流程进程主键</param>
        /// <param name="taskId">父流程子流程发起主键</param>
        /// <param name="nodeId">父流程发起子流程节点Id</param>
        /// <param name="schemeCode">子流程流程模板编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetChildProcessDetails(string processId, string taskId, string nodeId, string schemeCode)
        {
            var data =await _nWFProcessIBLL.GetChildProcessDetails(processId, taskId, schemeCode, nodeId);
            var taskNode =await _nWFProcessIBLL.GetTaskUserList(data.childProcessId);
            var jsonData = new
            {
                info = data,
                task = taskNode
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 获取流程当前任务需要处理的人
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTaskUserList(string processId)
        {
            var data = await _nWFProcessIBLL.GetTaskUserList(processId);
            return Success(data);
        }
        /// <summary>
        /// 保存草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveDraft(string processId, string schemeCode, string createUserId)
        {
            await _nWFProcessIBLL.SaveDraft(processId, schemeCode, createUserId);
            return SuccessInfo("流程草稿保存成功");
        }
        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteDraft(string processId)
        {
            await _nWFProcessIBLL.DeleteDraft(processId);
            return SuccessInfo("流程草稿删除成功");
        }

        /// <summary>
        /// 创建流程
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="title">流程进程自定义标题</param>
        /// <param name="level">流程进程等级</param>
        /// <param name="auditors">下一节点审核人</param>
        /// <param name="createUserId">流程创建人</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> CreateFlow(string schemeCode, string processId, string title, int level, string auditors, string createUserId)
        {
            try
            {
                await _nWFProcessIBLL.CreateFlow(schemeCode, processId, title, level, auditors, createUserId);
                return SuccessInfo("流程创建成功");
            }
            catch (Exception)
            {
                await _nWFProcessIBLL.SaveDraft(processId, schemeCode, createUserId);
                throw;
            }

        }

        /// <summary>
        /// 创建流程(子流程)
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="parentProcessId">父流程进程主键</param>
        /// <param name="parentTaskId">父流程任务主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> CreateChildFlow(string schemeCode, string processId, string parentProcessId, string parentTaskId)
        {
            await _nWFProcessIBLL.CreateChildFlow(schemeCode, processId, parentProcessId, parentTaskId);
            return SuccessInfo("流程创建成功");
        }

        /// <summary>
        /// 重新创建流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> AgainCreateFlow(string processId)
        {
            await _nWFProcessIBLL.AgainCreateFlow(processId);
            return SuccessInfo("流程重新创建成功");

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
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> AuditFlow(string operationCode, string operationName, string processId, string taskId, string des, string auditors, string stamp, string signUrl)
        {
            await _nWFProcessIBLL.AuditFlow(operationCode, operationName, processId, taskId, des, auditors, stamp, signUrl);
            return SuccessInfo("流程审批成功");
        }
        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="operationCode">流程审批操作码agree 同意 disagree 不同意</param>
        /// <param name="taskIds">任务串</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> AuditFlows(string operationCode, string taskIds)
        {
            await _nWFProcessIBLL.AuditFlows(operationCode, taskIds);
            return SuccessInfo("流程批量审批成功");
        }
        /// <summary>
        /// 流程加签
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="userId">加签人员</param>
        /// <param name="des">加签说明</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SignFlow(string processId, string taskId, string userId, string des)
        {
            await _nWFProcessIBLL.SignFlow(processId, taskId, userId, des);
            return SuccessInfo("流程加签成功");
        }
        /// <summary>
        /// 流程加签审核
        /// </summary>
        /// <param name="operationCode">审核操作码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="des">加签说明</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SignAuditFlow(string operationCode, string processId, string taskId, string des)
        {
            await _nWFProcessIBLL.SignAuditFlow(operationCode, processId, taskId, des);
            return SuccessInfo("加签审批成功");
        }

        /// <summary>
        /// 确认阅读
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> ReferFlow(string processId, string taskId)
        {
            await _nWFProcessIBLL.ReferFlow(processId, taskId);
            return SuccessInfo("确认成功");
        }

        /// <summary>
        /// 催办流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UrgeFlow(string processId)
        {
            await _nWFProcessIBLL.UrgeFlow(processId);
            return SuccessInfo("催办成功");
        }
        /// <summary>
        /// 撤销流程（只有在该流程未被处理的情况下）
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> RevokeFlow(string processId)
        {
            await _nWFProcessIBLL.RevokeFlow(processId);
            return SuccessInfo("撤销成功");
        }
        /// <summary>
        /// 撤销审核
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> RevokeAudit(string processId, string taskId)
        {
            var res = await _nWFProcessIBLL.RevokeAudit(processId, taskId);
            if (res)
            {
                return SuccessInfo("撤销成功");
            }
            else
            {
                return Fail("撤销失败，当前不允许撤销！");
            }

        }

        /// <summary>
        /// 指派流程审核人
        /// </summary>
        /// <param name="strList">任务列表</param>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> AppointUser(string strList)
        {
            IEnumerable<NWFTaskEntity> list = strList.ToObject<IEnumerable<NWFTaskEntity>>();
            await _nWFProcessIBLL.AppointUser(list);
            return SuccessInfo("指派成功");
        }
        /// <summary>
        /// 作废流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteFlow(string processId)
        {
            await _nWFProcessIBLL.DeleteFlow(processId);
            return SuccessInfo("作废成功");
        }


       

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="bNodeId"></param>
        /// <param name="eNodeId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> AddTask(string processId, string bNodeId, string eNodeId)
        {
            await _nWFProcessIBLL.AddTask(processId, bNodeId, eNodeId);
            return SuccessInfo("添加成功");
        }
        #endregion
    }
}