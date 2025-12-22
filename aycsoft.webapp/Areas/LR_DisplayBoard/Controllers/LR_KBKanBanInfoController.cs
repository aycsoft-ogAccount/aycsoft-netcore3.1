using System.Threading.Tasks;
using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_DisplayBoard.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.14
    /// 描 述：看板信息
    /// </summary>
    [Area("LR_DisplayBoard")]
    public class LR_KBKanBanInfoController : MvcControllerBase
    {
        private readonly LR_KBKanBanInfoIBLL _lR_KBKanBanInfoIBLL;
        private readonly LR_KBConfigInfoIBLL _lR_KBConfigInfoIBLL;

        public LR_KBKanBanInfoController(LR_KBKanBanInfoIBLL lR_KBKanBanInfoIBLL, LR_KBConfigInfoIBLL lR_KBConfigInfoIBLL) {
            _lR_KBKanBanInfoIBLL = lR_KBKanBanInfoIBLL;
            _lR_KBConfigInfoIBLL = lR_KBConfigInfoIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Form(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                ViewBag.KanBanCode = await this.GetRuleCode("KanBanCode");
            }

            return View();
        }
        /// <summary>
        /// 看板预览界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PreviewForm()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string queryJson)
        {
            var data = await _lR_KBKanBanInfoIBLL.GetList(queryJson);

            return Success(data);
        }
        /// <summary>
        /// 获取模版列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTemptList()
        {
            var data = await _lR_KBKanBanInfoIBLL.GetTemptList();
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _lR_KBKanBanInfoIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var baseinfo =await _lR_KBKanBanInfoIBLL.GetEntity(keyValue);
            var configinfo =await _lR_KBConfigInfoIBLL.GetListByKBId(keyValue);
            var data = new
            {
                baseinfo,
                configinfo
            };
            return Success(data);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _lR_KBKanBanInfoIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, string kanbaninfo, string kbconfigInfo)
        {
            await _lR_KBKanBanInfoIBLL.SaveEntity(keyValue, kanbaninfo, kbconfigInfo);
            if (string.IsNullOrEmpty(keyValue))
            {
                await this.UseRuleSeed("KanBanCode");//新增占用看板编号
            }
            return SuccessInfo("保存成功！");
        }
        #endregion

    }
}
