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
    /// 描 述：部门接口
    /// </summary>
    public class DepartmentController : MvcControllerBase
    {
        private readonly DepartmentIBLL _departmentIBLL;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="departmentIBLL">部门接口</param>
        public DepartmentController(DepartmentIBLL departmentIBLL) {
            _departmentIBLL = departmentIBLL;
        }

        /// <summary>
        /// 根据公司 id 获取部门列表
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(string companyId)
        {
            var data = await _departmentIBLL.GetList(companyId);
            return Success(data);
        }

        /// <summary>
        /// 根据 id 获取单个部门详情
        /// </summary>
        /// <param name="departmentId">部门主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Info(string departmentId)
        {
            var data = await _departmentIBLL.GetEntity(departmentId);
            return Success(data);
        }
    }
}