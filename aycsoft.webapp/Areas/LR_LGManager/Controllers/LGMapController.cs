using System.Threading.Tasks;
using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

namespace aycsoft.webapp.Areas.LR_LGManager.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.22
    /// 描 述：多语言语言映射控制器
    /// </summary>
    [Area("LR_LGManager")]
    public class LGMapController : MvcControllerBase
    {
        private readonly LGMapIBLL _lGMapIBLL;

        public LGMapController(LGMapIBLL lGMapIBLL)
        {
            _lGMapIBLL = lGMapIBLL;
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
        /// <summary>
        /// 数据字典语言
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DataItemLG()
        {
            return View();
        }
        /// <summary>
        /// 系统功能语言
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SystemModuleLG()
        {
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="TypeCode">语言类型编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetListByTypeCode(string TypeCode)
        {
            var data = await _lGMapIBLL.GetListByTypeCode(TypeCode);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="typeList">语言类型列表</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson, string typeList)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _lGMapIBLL.GetPageList(paginationobj, queryJson, typeList);
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
        /// 获取列表分页数据
        /// </summary>
        /// <param name="dataList">需要翻译的列表</param>
        /// <param name="typeList">语言类型列表</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string dataList, string typeList)
        {
            var data = await _lGMapIBLL.GetList(dataList, typeList);
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
            var data = await _lGMapIBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 根据名称获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetListByName(string name)
        {
            var data = await _lGMapIBLL.GetListByName(name);
            return Success(data);
        }
        /// <summary>
        /// 根据名称与类型获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="typeCode">语言类型编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetListByNameAndType(string name, string typeCode)
        {
            var data = await _lGMapIBLL.GetListByNameAndType(name, typeCode);
            return Success(data);
        }
        /// <summary>
        /// 根据语言类型编码获取语言包
        /// </summary>
        /// <param name="typeCode">语言类型编码</param>
        /// <param name="isMain">是否是主语言</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetLanguageByCode(string typeCode, bool isMain)
        {
            var data = await _lGMapIBLL.GetMap(typeCode, isMain);
            return Success(data);
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="code">一组语言编码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string code)
        {
            await _lGMapIBLL.DeleteEntity(code);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="nameList">原列表</param>
        /// <param name="newNameList">新列表</param>
        /// <param name="code">一组语言编码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string nameList, string newNameList, string code)
        {
            await _lGMapIBLL.SaveEntity(nameList, newNameList, code);
            return SuccessInfo("保存成功！");
        }
        #endregion
    }
}
