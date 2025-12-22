using System.Threading.Tasks;
using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;

namespace aycsoft.webapp.Areas.LR_LGManager.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.22
    /// 描 述：多语言类型控制器
    /// </summary>
    [Area("LR_LGManager")]
    public class LGTypeController : MvcControllerBase
    {
        private readonly LGTypeIBLL _lGTypeIBLL;

        public LGTypeController(LGTypeIBLL lGTypeIBLL)
        {
            _lGTypeIBLL = lGTypeIBLL;
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
        public IActionResult Form()
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
            var data = await _lGTypeIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var data = await _lGTypeIBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntityByCode(string keyValue)
        {
            var data = await _lGTypeIBLL.GetEntityByCode(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _lGTypeIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, LGTypeEntity entity)
        {
            await _lGTypeIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 设为主语言
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SetMainLG(string keyValue)
        {
            await _lGTypeIBLL.SetMainLG(keyValue);
            return SuccessInfo("保存成功！");
        }
        #endregion
    }
}
