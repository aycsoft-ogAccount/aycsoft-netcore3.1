using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapi.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.17
    /// 描 述：流程api
    /// </summary>
    [Route("newwf/[action]")]
    public class NewWorkFlowController : MvcControllerBase
    {
        private readonly NWFSchemeIBLL _nWFSchemeIBLL;
        private readonly NWFProcessIBLL _nWFProcessIBLL;
        private readonly FormSchemeIBLL _formSchemeIBLL;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="nWFSchemeIBLL">流程模板</param>
        /// <param name="nWFProcessIBLL">流程实例</param>
        /// <param name="formSchemeIBLL">表单模板</param>
        public NewWorkFlowController(NWFSchemeIBLL nWFSchemeIBLL, NWFProcessIBLL nWFProcessIBLL, FormSchemeIBLL formSchemeIBLL)
        {
            _nWFSchemeIBLL = nWFSchemeIBLL;
            _nWFProcessIBLL = nWFProcessIBLL;
            _formSchemeIBLL = formSchemeIBLL;
        }

        /// <summary>
        /// 获取我的流程实例信息
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> MyProcessList(Pagination pagination, string queryJson)
        {
            var list = await _nWFProcessIBLL.GetMyPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = list,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Success(jsonData);
        }


        /// <summary>
        /// /获取我的任务列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> MyTaskList(Pagination pagination, string queryJson)
        {
            var list = await _nWFProcessIBLL.GetMyTaskPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = list,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取我已处理的任务列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> MyMakeTaskList(Pagination pagination, string queryJson)
        {
            var list = await _nWFProcessIBLL.GetMyFinishTaskPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = list,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Success(jsonData);
        }


        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SchemePageList(Pagination pagination, string queryJson)
        {
            var list = await _nWFSchemeIBLL.GetAppInfoPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = list,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <param name="code">流程编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SchemeByCode(string code)
        {
            var schemeInfo = await _nWFSchemeIBLL.GetInfoEntityByCode(code);
            if (schemeInfo != null)
            {
                var data = await _nWFSchemeIBLL.GetSchemeEntity(schemeInfo.F_SchemeId);
                return Success(data);
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
        public async Task<IActionResult> NextAuditors(string code, string processId, string taskId, string nodeId, string operationCode)
        {
            var data = await _nWFProcessIBLL.GetNextAuditors(code, processId, taskId, nodeId, operationCode);
            return Success(data);
        }
        /// <summary>
        /// 获取流程进程信息
        /// </summary>
        /// <param name="processId">进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ProcessDetails(string processId, string taskId)
        {
            var data = await _nWFProcessIBLL.GetProcessDetails(processId, taskId);
            if (!string.IsNullOrEmpty(data.childProcessId))
            {
                processId = data.childProcessId;
            }

            var taskNode = await _nWFProcessIBLL.GetTaskUserList(processId);

            var jsonData = new
            {
                info = data,
                task = taskNode
            };

            return Success(jsonData);
        }

        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="code">流程模板编码</param>
        /// <param name="processId">流程实例主键</param>
        /// <param name="title">流程标题</param>
        /// <param name="level">流程等级</param>
        /// <param name="auditors">下一节点审核人信息</param>
        /// <param name="formreq">自定义表单请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]string code, [FromForm]string processId, [FromForm]string title, [FromForm]int level, [FromForm]string auditors, [FromForm]string formreq)
        {
            await SaveForm(formreq);
            await _nWFProcessIBLL.CreateFlow(code, processId, title, level, auditors, this.GetUserId());
            return this.SuccessInfo("创建成功");
        }

        /// <summary>
        /// 重新创建流程
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="formreq">自定义表单请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AgainCreateFlow([FromForm]string processId, [FromForm]string formreq)
        {
            await SaveForm(formreq);
            await _nWFProcessIBLL.AgainCreateFlow(processId);
            return SuccessInfo("重新创建成功");
        }

        /// <summary>
        /// 创建流程(子流程)
        /// </summary>
        /// <param name="code">流程模板编码</param>
        /// <param name="processId">流程实例主键</param>
        /// <param name="parentProcessId">父级流程实例主键</param>
        /// <param name="parentTaskId">父级流程任务主键</param>
        /// <param name="formreq">表单请求数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateChildFlow([FromForm]string code, [FromForm]string processId, [FromForm]string parentProcessId, [FromForm]string parentTaskId, [FromForm]string formreq)
        {
            await SaveForm(formreq);
            await _nWFProcessIBLL.CreateChildFlow(code, processId, parentProcessId, parentTaskId);
            return SuccessInfo("子流程创建成功");
        }

        /// <summary>
        /// 保存草稿(流程)
        /// </summary>
        /// <param name="code">流程模板编码</param>
        /// <param name="processId">流程实例主键</param>
        /// <param name="formreq">表单请求数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveDraft([FromForm]string code, [FromForm]string processId, [FromForm]string formreq)
        {
            await SaveForm(formreq);
            if (!string.IsNullOrEmpty(processId))
            {
                await _nWFProcessIBLL.SaveDraft(processId, code, this.GetUserId());
            }
            return SuccessInfo("保存成功");
        }

        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteDraft([FromForm]string processId)
        {
            await _nWFProcessIBLL.DeleteDraft(processId);
            return SuccessInfo("草稿删除成功");
        }

        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="operationCode">审核编码</param>
        /// <param name="operationName">审核编码中文意思</param>
        /// <param name="processId">流程实例主键</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="des">审核备注信息</param>
        /// <param name="auditors">下一节点审核人信息</param>
        /// <param name="signUrl">审核人签名信息</param>
        /// <param name="formreq">自定义表单信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AuditFlow([FromForm]string operationCode, [FromForm]string operationName, [FromForm]string processId, [FromForm]string taskId, [FromForm]string des, [FromForm]string auditors, [FromForm]string signUrl, [FromForm]string formreq)
        {

            await SaveForm(formreq);
            await _nWFProcessIBLL.AuditFlow(operationCode, operationName, processId, taskId, des, auditors, "", signUrl);
            return SuccessInfo("审批成功");
        }

        /// <summary>
        /// 流程加签
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="des">审核备注信息</param>
        /// <param name="userId">加签人ID</param>
        /// <param name="formreq">自定义表单信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignFlow([FromForm]string processId, [FromForm]string taskId, [FromForm]string des, [FromForm]string userId, [FromForm]string formreq)
        {
            await SaveForm(formreq);
            await _nWFProcessIBLL.SignFlow(processId, taskId, userId, des);
            return SuccessInfo("加签成功");
        }

        /// <summary>
        /// 流程加签审核
        /// </summary>
        /// <param name="operationCode">审核码</param>
        /// <param name="processId">流程实例主键</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="des">审核备注信息</param>
        /// <param name="userId">加签人ID</param>
        /// <param name="formreq">自定义表单信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SignAuditFlow([FromForm]string operationCode, [FromForm]string processId, [FromForm]string taskId, [FromForm]string des, [FromForm]string userId, [FromForm]string formreq)
        {
            await SaveForm(formreq);
            await _nWFProcessIBLL.SignAuditFlow(operationCode, processId, taskId, des);
            return SuccessInfo("加签审批成功");
        }


        /// <summary>
        /// 催办流程
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UrgeFlow([FromForm]string processId)
        {
            await _nWFProcessIBLL.UrgeFlow(processId);
            return SuccessInfo("催办成功");
        }

        /// <summary>
        /// 撤销流程（只有在该流程未被处理的情况下）
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RevokeFlow([FromForm]string processId)
        {
            await _nWFProcessIBLL.RevokeFlow(processId);
            return SuccessInfo("撤销成功");
        }

        /// <summary>
        /// 确认阅读
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReferFlow([FromForm]string processId, [FromForm]string taskId)
        {
            await _nWFProcessIBLL.ReferFlow(processId, taskId);
            return SuccessInfo("确认成功");
        }

        /// <summary>
        /// 保存自定义表单数据
        /// </summary>
        /// <param name="formreq">请求参数</param>
        private async Task SaveForm(string formreq)
        {
            FormParam formParam = formreq.ToObject<FormParam>();
            await _formSchemeIBLL.SaveInstanceForm(formParam.schemeInfoId, formParam.processIdName, formParam.keyValue, formParam.formData);
        }

        /// <summary>
        /// 自定义表单提交参数
        /// </summary>
        private class FormParam
        {
            /// <summary>
            /// 流程模板id
            /// </summary>
            public string schemeInfoId { get; set; }
            /// <summary>
            /// 关联字段名称
            /// </summary>
            public string processIdName { get; set; }
            /// <summary>
            /// 数据主键值
            /// </summary>
            public string keyValue { get; set; }
            /// <summary>
            /// 表单数据
            /// </summary>
            public string formData { get; set; }
        }

    }
}