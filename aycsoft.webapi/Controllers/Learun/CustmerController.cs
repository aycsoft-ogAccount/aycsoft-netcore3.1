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
    /// 日 期：2020.04.21
    /// 描 述：自定义表单接口
    /// </summary>
    public class CustmerController : MvcControllerBase
    {
        private readonly FormSchemeIBLL _formSchemeIBLL;
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="formSchemeIBLL"></param>
        public CustmerController(FormSchemeIBLL formSchemeIBLL) {
            _formSchemeIBLL = formSchemeIBLL;
        }

        /// <summary>
        /// 获取自定义表单功能分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="formId">表单主键</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        private async Task<IActionResult> PageList(Pagination pagination,string formId,string queryJson)
        {
            var data =await _formSchemeIBLL.GetFormPageList(formId, pagination, queryJson,"");
            var jsonData = new
            {
                rows = data,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Success(jsonData);
        }
    }
}