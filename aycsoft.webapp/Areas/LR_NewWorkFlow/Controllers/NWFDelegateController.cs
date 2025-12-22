using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_NewWorkFlow.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.03.16
    /// 描 述：流程委托
    /// </summary>
    [Area("LR_NewWorkFlow")]
    public class NWFDelegateController : MvcControllerBase
    {
        private readonly NWFDelegateIBLL _nWFDelegateIBLL;

        public NWFDelegateController(NWFDelegateIBLL nWFDelegateIBLL) {
            _nWFDelegateIBLL = nWFDelegateIBLL;
        }


        #region 视图功能
        /// <summary>
        /// 管理界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
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
            var data =await _nWFDelegateIBLL.GetPageList(paginationobj, keyword);
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
        /// 获取关联模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetRelationList(string keyValue)
        {
            var relationList =await _nWFDelegateIBLL.GetRelationList(keyValue);
            return Success(relationList);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存委托信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="strEntity">委托信息实体</param>
        /// <param name="strSchemeInfo">模板信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, string strEntity, string strSchemeInfo)
        {
            NWFDelegateRuleEntity entity = strEntity.ToObject<NWFDelegateRuleEntity>();
            await _nWFDelegateIBLL.SaveEntity(keyValue, entity, strSchemeInfo.Split(','));
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 删除模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _nWFDelegateIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }

        /// <summary>
        /// 启用/停用表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpDateSate(string keyValue, int state)
        {
            await _nWFDelegateIBLL.UpdateState(keyValue, state);
            return SuccessInfo((state == 1 ? "启用" : "禁用") + "成功！");
        }
        #endregion
    }
}