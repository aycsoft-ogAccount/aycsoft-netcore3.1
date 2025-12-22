using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aycsoft.webapi.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.21
    /// 描 述：数据字典接口
    /// </summary>
    public class DataItemController : MvcControllerBase
    {
        private readonly DataItemIBLL _dataItemIBLL;
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="dataItemIBLL">数据字典业务接口</param>
        public DataItemController(DataItemIBLL dataItemIBLL) {
            _dataItemIBLL = dataItemIBLL;
        }

        /// <summary>
        /// 获取数据字典详细列表
        /// </summary>
        /// <param name="code">数据字典编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(string code)
        {
            var data =await _dataItemIBLL.GetDetailList(code, "");
            return Success(data);
        }
    }
}