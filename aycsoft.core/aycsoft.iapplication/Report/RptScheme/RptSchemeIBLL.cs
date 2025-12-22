using ce.autofac.extension;
using aycsoft.util;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.07
    /// 描 述：报表管理
    /// </summary>
    public interface RptSchemeIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        Task<IEnumerable<RptSchemeEntity>> GetPageList(Pagination pagination, string keyword);
        /// <summary>
        /// 获得报表数据
        /// </summary>
        /// <param name="dataSourceId">数据库id</param>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        Task<DataTable> GetReportData(string dataSourceId, string strSql);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Task<RptSchemeEntity> GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task DeleteEntity(string keyValue);
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        Task SaveEntity(string keyValue, RptSchemeEntity entity);
        #endregion
    }
}
