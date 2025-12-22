using aycsoft.util;
using ce.autofac.extension;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：数据库连接
    /// </summary>
    public interface DatabaseLinkIBLL : IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DatabaseLinkEntity>> GetList();
        /// <summary>
        /// 获取列表数据(去掉连接串地址信息)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DatabaseLinkEntity>> GetListByNoConnection();
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        Task<IEnumerable<DatabaseLinkEntity>> GetListByNoConnection(string keyword);
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetTreeList();
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetTreeListEx();
        /// <summary>
        /// 获取数据连接实体
        /// </summary>
        /// <param name="databaseLinkId">主键</param>
        /// <returns></returns>
        Task<DatabaseLinkEntity> GetEntity(string databaseLinkId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除自定义查询条件
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task Delete(string keyValue);
        /// <summary>
        /// 保存自定义查询（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="databaseLinkEntity">实体</param>
        /// <returns></returns>
        Task<bool> SaveEntity(string keyValue, DatabaseLinkEntity databaseLinkEntity);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 测试数据数据库是否能连接成功
        /// </summary>
        /// <param name="connection">连接串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="keyValue">主键</param>
        Task<string> TestConnection(string connection, string dbType, string keyValue);
        #endregion
    }
}
