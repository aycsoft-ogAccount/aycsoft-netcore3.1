using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：数据源
    /// </summary>
    public class DataSourceBLL : BLLBase, DataSourceIBLL, BLL
    {
        private readonly DataSourceService dataSourceService = new DataSourceService();

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<DataSourceEntity>> GetPageList(Pagination pagination, string keyword)
        {
            return dataSourceService.GetPageList(pagination, keyword);
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DataSourceEntity>> GetList()
        {
            return dataSourceService.GetList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public Task<DataSourceEntity> GetEntityByCode(string code)
        {
            return dataSourceService.GetEntityByCode(code);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据源
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            await dataSourceService.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataSourceEntity">数据源实体</param>
        /// <returns></returns>
        public async Task<bool> SaveEntity(string keyValue, DataSourceEntity dataSourceEntity)
        {
            return await dataSourceService.SaveEntity(keyValue, dataSourceEntity);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取数据源的数据
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public async Task<DataTable> GetDataTable(string code, string queryJson = "{}")
        {
            var userInfo = await this.CurrentUser();
            return await dataSourceService.GetDataTable(code, userInfo, queryJson);
        }
        /// <summary>
        /// 获取数据源的数据(分页)
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public async Task<DataTable> GetDataTable(string code, Pagination pagination, string queryJson = "{}")
        {
            var userInfo = await this.CurrentUser();
            return await dataSourceService.GetDataTable(code, pagination, userInfo, queryJson);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="parentId">父级ID</param>
        /// <param name="Id">ID</param>
        /// <param name="showId">显示ID</param>
        /// <returns></returns>
        public Task<IEnumerable<TreeModel>> GetTree(string code, string parentId, string Id, string showId)
        {
            return dataSourceService.GetTree(code, parentId, Id, showId);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="code">数据库连接编码</param>
        /// <param name="strSql">sql</param>
        /// <returns></returns>
        public Task<DataTable> GetDataTableBySql(string code, string strSql)
        {
            return dataSourceService.GetDataTableBySql(code, strSql);
        }

        /// <summary>
        /// 获取sql的列
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetDataColName(string code)
        {
            return dataSourceService.GetDataColName(code);
        }

        #endregion
    }
}
