using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.27
    /// 描 述：编码规则
    /// </summary>
    [Area("LR_SystemModule")]
    public class CodeRuleController : MvcControllerBase
    {
        private readonly CodeRuleIBLL _codeRuleIBLL;

        public CodeRuleController(CodeRuleIBLL codeRuleIBLL) {
            _codeRuleIBLL = codeRuleIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 单据编号管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 单据编号规则
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FormatForm()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _codeRuleIBLL.GetPageList(paginationobj, keyword);
            var jsonData = new
            {
                rows = data,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList()
        {
            var data =await _codeRuleIBLL.GetList();
            return Success(data);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, CodeRuleEntity entity)
        {
            await _codeRuleIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _codeRuleIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取单据编码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEnCode(string code)
        {
            var data = await _codeRuleIBLL.GetBillCode(code);
            return Success(data);
        }
        #endregion
    }
}