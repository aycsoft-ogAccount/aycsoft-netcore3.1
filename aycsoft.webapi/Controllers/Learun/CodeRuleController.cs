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
    /// 描 述：编码规则接口
    /// </summary>
    public class CodeRuleController : MvcControllerBase
    {
        private readonly CodeRuleIBLL _codeRuleIBLL;
        /// <summary>
        /// 初始方法
        /// </summary>
        public CodeRuleController(CodeRuleIBLL codeRuleIBLL)
        {
            _codeRuleIBLL = codeRuleIBLL;
        }


        /// <summary>
        /// 获取编码单号
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Code(string code)
        {
            var data =await _codeRuleIBLL.GetBillCode(code);
            return Success(data);
        }
    }
}