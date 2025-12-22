using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.application;
using aycsoft.util;
using System.Collections.Generic;
using System.Threading.Tasks;
using aycsoft.iappdev;

namespace aycsoft.appdev
{
    /// <summary>
    /// Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期： 2020-06-18 06:35:30
    /// 描 述： 测试代码生成器 f_parent
    /// </summary>
    public class TestBLL : BLLBase, ITestBLL,BLL
    {
        private readonly TestService testService = new TestService();

        #region 获取数据
        /// <summary>
        /// 获取主表f_parent的所有列表数据
        /// </summary>
        /// <param name="queryJson">查询参数,json字串</param>
        /// <returns></returns>
        public Task<IEnumerable<F_parentEntity>> GetList(string queryJson)
        {
            return testService.GetList(queryJson);
        }

        /// <summary>
        /// 获取主表f_parent的分页列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数,json字串</param>
        /// <returns></returns>
        public Task<IEnumerable<F_parentEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return testService.GetPageList(pagination, queryJson);
        }
        
        /// <summary>
        /// 获取f_children(f_children)的列表实体数据
        /// </summary>
        /// <param name="f_parentId">与表f_parent的关联字段</param>
        /// <returns></returns>
        public Task<IEnumerable<F_childrenEntity>> GetF_childrenList(string f_parentId)
        {
            return testService.GetF_childrenList(f_parentId);
        }


        /// <summary>
        /// 获取主表f_parent的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<F_parentEntity> GetEntity(string keyValue)
        {
            return testService.GetEntity(keyValue);
        }


        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主表主键</param>
        public async Task Delete(string keyValue)
        {
            await testService.Delete(keyValue);
        }
        /// <summary>
        /// 新增,更新
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="f_parentEntity">f_parent实体数据</param>
        /// <param name="f_childrenList">f_children实体数据列表</param>
        /// <returns></returns>
        public async Task<string> SaveEntity(string keyValue ,F_parentEntity f_parentEntity,IEnumerable<F_childrenEntity> f_childrenList)
        {
            return await testService.SaveEntity(keyValue ,f_parentEntity,f_childrenList);
        }
        #endregion
    }
}
