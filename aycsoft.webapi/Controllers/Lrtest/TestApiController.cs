using aycsoft.iappdev;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.webapi.Controllers
{
    /// <summary>
    /// Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期： 2020-06-18 06:35:30
    /// 描 述： 测试代码生成器 f_parent
    /// </summary>
    [Route("Lrtest/[controller]/[action]")]
    public class TestApiController : MvcControllerBase
    {
        private readonly ITestBLL _iTestBLL;
        /// <summary>
        /// 初始方法
        /// </summary>
        /// <param name="iTestBLL">接口</param>
        public TestApiController(ITestBLL iTestBLL)
        {
            _iTestBLL = iTestBLL;
        }

        #region 获取数据
        /// <summary>
        /// 获取主表f_parent的所有列表数据
        /// </summary>
        /// <param name="queryJson">查询参数,json字串</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList(string queryJson)
        {
            var list = await _iTestBLL.GetList(queryJson);
            return Success(list);
        }

        /// <summary>
        /// 获取主表f_parent的分页列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数,json字串</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPageList([FromQuery]Pagination pagination, [FromQuery]string queryJson)
        {
            var list = await _iTestBLL.GetPageList(pagination,queryJson);
            var jsonData = new
            {
                rows = list,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取f_children(f_children)的列表实体数据
        /// </summary>
        /// <param name="f_parentId">与表f_parent的关联字段</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetF_childrenList(string f_parentId)
        {
            var data =await _iTestBLL.GetF_childrenList(f_parentId);
            return Success(data);
        }


        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetForm(string keyValue)
        {
            var f_parent =await _iTestBLL.GetEntity(keyValue);
            var f_children = await _iTestBLL.GetF_childrenList(f_parent.F_Id);
            var jsonData = new
            {
                f_parent
                ,f_children
            };
            return Success(jsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _iTestBLL.Delete(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 新增,更新
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="strF_parentEntity">f_parent实体数据</param>
        /// <param name="strF_childrenList">f_children实体数据列表JSON字串</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveForm(string keyValue ,string strF_parentEntity,string strF_childrenList)
        {
            var f_parentEntity = strF_parentEntity.ToObject<F_parentEntity>();
            var f_childrenList = strF_childrenList.ToObject<IEnumerable<F_childrenEntity>>();

            var res = await _iTestBLL.SaveEntity(keyValue ,f_parentEntity,f_childrenList);
            return Success("保存成功！",res);
        }
        #endregion       
    }
}