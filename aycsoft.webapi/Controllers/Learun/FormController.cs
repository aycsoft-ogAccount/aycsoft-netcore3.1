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
    /// 描 述：自定义表单接口
    /// </summary>
    public class FormController : MvcControllerBase
    {
        private readonly FormSchemeIBLL _formSchemeIBLL;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="formSchemeIBLL">表单接口</param>
        public FormController(FormSchemeIBLL formSchemeIBLL)
        {
            _formSchemeIBLL = formSchemeIBLL;
        }

        /// <summary>
        /// 获取表单模板数据
        /// </summary>
        /// <param name="id">模板主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Scheme(string id)
        {
            FormSchemeInfoEntity schemeInfoEntity = await _formSchemeIBLL.GetSchemeInfoEntity(id);
            if (schemeInfoEntity != null)
            {
                FormSchemeEntity schemeEntity = await _formSchemeIBLL.GetSchemeEntity(schemeInfoEntity.F_SchemeId);
                return Success(schemeEntity);
            }
            else
            {
                return NotFound();
            }

        }
        /// <summary>
        /// 获取自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="keyValue">主键值</param>
        /// <param name="processIdName">流程绑定字段名</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Data(string schemeInfoId, string keyValue, string processIdName)
        {
            if (string.IsNullOrEmpty(processIdName))
            {
                var data = await _formSchemeIBLL.GetInstanceForm(schemeInfoId, keyValue);
                return Success(data);
            }
            else
            {
                var data = await _formSchemeIBLL.GetInstanceForm(schemeInfoId, processIdName, keyValue);
                return Success(data);
            }
        }


        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="keyValue">主键值</param>
        /// <param name="processIdName">流程绑定字段名</param>
        /// <param name="formData">表单数据</param>
        /// <returns></returns>
        [HttpPost]
        private async Task<IActionResult> Save([FromForm]string schemeInfoId, [FromForm]string keyValue, [FromForm]string processIdName, [FromForm]string formData)
        {
            await _formSchemeIBLL.SaveInstanceForm(schemeInfoId, processIdName, keyValue, formData);
            return SuccessInfo("保存成功");
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        private async Task<IActionResult> DeleteForm([FromForm]string schemeInfoId, [FromForm]string keyValue)
        {
            await _formSchemeIBLL.DeleteInstanceForm(schemeInfoId, keyValue);
            return SuccessInfo("删除成功");
        }
    }
}