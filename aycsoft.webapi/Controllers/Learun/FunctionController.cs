using aycsoft.iapplication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.webapi.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.22
    /// 描 述：移动应用
    /// </summary>
    public class FunctionController : MvcControllerBase
    {
        private readonly MyFunctionIBLL _myFunctionIBLL;
        private readonly FunctionIBLL _functionIBLL;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="myFunctionIBLL">我的功能</param>
        /// <param name="functionIBLL">移动端功能</param>
        public FunctionController(MyFunctionIBLL myFunctionIBLL, FunctionIBLL functionIBLL) {
            _myFunctionIBLL = myFunctionIBLL;
            _functionIBLL = functionIBLL;
        }

        /// <summary>
        /// 获取全部移动功能数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list =await _functionIBLL.GetList();
            return Success(list);
        }
        /// <summary>
        /// 获取我的常用应用数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> MyList()
        {

            var list = await _myFunctionIBLL.GetList(this.GetUserId());
            List<string> res = new List<string>();
            foreach (var item in list)
            {
                res.Add(item.F_FunctionId);
            }
            return Success(res);
        }

        /// <summary>
        /// 更新我的常用应用
        /// </summary>
        /// <param name="ids">功能id集合字串</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateMyList([FromForm]string ids)
        {
            await _myFunctionIBLL.SaveEntity(this.GetUserId(), ids);
            return SuccessInfo("保存成功");
        }
    }
}