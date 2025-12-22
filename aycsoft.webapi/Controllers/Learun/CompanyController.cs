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
    /// 描 述：公司接口
    /// </summary>
    public class CompanyController : MvcControllerBase
    {
        private readonly CompanyIBLL _companyIBLL;
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="companyIBLL"></param>
        public CompanyController(CompanyIBLL companyIBLL)
        {
            _companyIBLL = companyIBLL;
        }

        /// <summary>
        /// 获取所有公司数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> AllList()
        {
            var data = await _companyIBLL.GetList();
            return Success(data);
        }

        /// <summary>
        /// 获取子公司数据
        /// </summary>
        /// <param name="pId">父级Id，第一级为0</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(string pId)
        {
            var data = await _companyIBLL.GetListByPId(pId);
            return Success(data);
        }

        /// <summary>
        /// 获公司具体信息
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Info(string companyId)
        {
            var data = await _companyIBLL.GetEntity(companyId);
            return Success(data);
        }

        /// <summary>
        /// 根据关键字获取公司列表
        /// </summary>
        /// <param name="keywords">检索关键字</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ByKeywords(string keywords)
        {
            var data = await _companyIBLL.GetList(keywords);
            return Success(data);
        }
    }
}