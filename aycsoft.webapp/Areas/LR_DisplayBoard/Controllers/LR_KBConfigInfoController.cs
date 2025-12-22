using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_DisplayBoard.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：看板配置信息
    /// </summary>
    [Area("LR_DisplayBoard")]
    public class LR_KBConfigInfoController : MvcControllerBase
    {
        private readonly LR_KBConfigInfoIBLL _lR_KBConfigInfoIBLL;

        public LR_KBConfigInfoController(LR_KBConfigInfoIBLL lR_KBConfigInfoIBLL) {
            _lR_KBConfigInfoIBLL = lR_KBConfigInfoIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 表格配置页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TableForm()
        {
            return View();
        }
        /// <summary>
        /// 统计指标新增编辑界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult StatisticsForm()
        {
            return View();
        }
        /// <summary>
        /// 统计指标单个配置弹出界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ColStatisForm()
        {
            return View();
        }
        /// <summary>
        /// 饼图/柱状图/折线/仪表盘 模块配置界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ChartForm()
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
            var data =await _lR_KBConfigInfoIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _lR_KBConfigInfoIBLL.GetPageList(paginationobj, queryJson);
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var data =await _lR_KBConfigInfoIBLL.GetEntity(keyValue);
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
            await _lR_KBConfigInfoIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, LR_KBConfigInfoEntity entity)
        {
            await _lR_KBConfigInfoIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 根据配置信息获取数据
        /// </summary>
        /// <param name="configuration">配置信息</param>
        /// <param name="type">类型statistics统计;2表格3图表</param>
        /// <returns></returns>
        [AjaxOnly]
        public async Task<IActionResult> GetConfigData(string configInfoList)
        {
            List<ConfigInfoModel> list = configInfoList.ToObject<List<ConfigInfoModel>>();

            var data =await _lR_KBConfigInfoIBLL.GetConfigData(list);
            return Success(data);
        }
        /// <summary>
        /// 根据路径获取接口数据（仅限get方法）
        /// </summary>
        /// <param name="path">接口路径</param>
        /// <returns></returns>
        public async Task<IActionResult> GetApiData(string path)
        {
            var data =await _lR_KBConfigInfoIBLL.GetApiData(path);
            return Success(data);
        }
        #endregion
    }
}