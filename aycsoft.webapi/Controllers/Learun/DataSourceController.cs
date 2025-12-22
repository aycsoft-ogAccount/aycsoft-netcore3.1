using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapi.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.22
    /// 描 述：数据源接口
    /// </summary>
    public class DataSourceController : MvcControllerBase
    {
        private readonly DataSourceIBLL _dataSourceIBLL;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dataSourceIBLL">数据源接口</param>
        public DataSourceController(DataSourceIBLL dataSourceIBLL) {
            _dataSourceIBLL = dataSourceIBLL;
        }

        /// <summary>
        /// 获取数据源数据
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DataTable(string code)
        {
            var data =await _dataSourceIBLL.GetDataTable(code, "");
            return Success(data);
        }
    }
}