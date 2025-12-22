using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_AppManager
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.08
    /// 描 述：移动功能管理
    /// </summary>
    [Area("LR_AppManager")]
    public class FunctionManagerController : MvcControllerBase
    {
        private readonly FunctionIBLL _functionIBLL;
        public FunctionManagerController(FunctionIBLL functionIBLL) {
            _functionIBLL = functionIBLL;
        }


        #region 视图功能
        /// <summary>
        /// 管理页面
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
        /// <param name="type">类型</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string keyword, string type)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _functionIBLL.GetPageList(paginationobj, keyword, type);
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
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetForm(string keyValue)
        {

            FunctionEntity entity =await _functionIBLL.GetEntity(keyValue);
            FunctionSchemeEntity schemeEntity =await _functionIBLL.GetScheme(entity.F_SchemeId);

            var jsonData = new
            {
                entity,
                schemeEntity
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取树形移动功能列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetCheckTree()
        {
            var data =await _functionIBLL.GetCheckTree();
            return Success(data);
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _functionIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="strEntity">实体对象字串</param>
        /// <param name="strSchemeEntity">模板实体对象字串</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, string strEntity, string strSchemeEntity)
        {
            FunctionEntity entity = strEntity.ToObject<FunctionEntity>();
            FunctionSchemeEntity schemeEntity = strSchemeEntity.ToObject<FunctionSchemeEntity>();
            await _functionIBLL.SaveEntity(keyValue, entity, schemeEntity);
            return SuccessInfo("保存成功！");
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
            await _functionIBLL.UpdateState(keyValue, state);
            return SuccessInfo((state == 1 ? "启用" : "禁用") + "成功！");
        }
        #endregion
    }
}