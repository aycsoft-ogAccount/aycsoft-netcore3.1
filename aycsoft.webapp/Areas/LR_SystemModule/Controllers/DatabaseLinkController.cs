using System.Threading.Tasks;
using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.22
    /// 描 述：数据库连接控制器
    /// </summary>
    [Area("LR_SystemModule")]
    public class DatabaseLinkController : MvcControllerBase
    {
        private readonly DatabaseLinkIBLL _databaseLinkIBLL;

        public DatabaseLinkController(DatabaseLinkIBLL databaseLinkIBLL)
        {
            _databaseLinkIBLL = databaseLinkIBLL;
        }

        #region 获取视图
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
        /// 获取数据列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string keyword)
        {
            var data = await _databaseLinkIBLL.GetListByNoConnection(keyword);
            return Success(data);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTreeList()
        {
            var data = await _databaseLinkIBLL.GetTreeList();
            return Success(data);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, DatabaseLinkEntity entity)
        {
            bool res = await _databaseLinkIBLL.SaveEntity(keyValue, entity);
            if (res)
            {
                return SuccessInfo("保存成功！");
            }
            else
            {
                return Fail("保存失败,连接串信息有误！");
            }
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _databaseLinkIBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 测试连接串是否正确
        /// </summary>
        /// <param name="connection">连接串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> TestConnection(string connection, string dbType, string keyValue)
        {
            var res = await _databaseLinkIBLL.TestConnection(connection, dbType, keyValue);
            if (res == "success")
            {
                return SuccessInfo("连接成功！");
            }
            else
            {
                return Fail(res);
            }

        }
        #endregion
    }
}
