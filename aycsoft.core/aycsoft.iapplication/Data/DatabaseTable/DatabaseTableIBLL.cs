using aycsoft.util;
using ce.autofac.extension;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：数据库表管理
    /// </summary>
    public interface DatabaseTableIBLL : IBLL
    {
        #region 获取数据
        /// <summary>
        /// 数据表列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetTableList(string code, string tableName = "");
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetTreeList(string code);

        /// <summary>
        /// 数据表字段列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetTableFiledList(string code, string tableName);
        /// <summary>
        /// 获取数据表字段树形数据
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetFiledTreeList(string code, string tableName);
        /// <summary>
        /// 数据库表数据列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <param name="field">字段</param>
        /// <param name="logic">逻辑</param>
        /// <param name="keyword">关键字</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        Task<DataTable> GetTableDataList(string code, string tableName, string field, string logic, string keyword, Pagination pagination);
        /// <summary>
        /// 数据库表数据列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        Task<DataTable> GetTableDataList(string code, string tableName);
        /// <summary>
        /// 给定查询语句查询字段
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="strSql">表名</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetSqlColName(string code, string strSql);
        #endregion

        #region 提交数据
        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">编码</param>
        /// <param name="tableRemark">表备注</param>
        /// <param name="colList">字段列表</param>
        /// <returns></returns>
        Task<string> CreateTable(string code, string tableName, string tableRemark, List<DatabaseTableFieldModel> colList);
        #endregion

        #region 扩展方法
        /// <summary>
        /// C#实体数据类型
        /// </summary>
        /// <param name="datatype">数据库字段类型</param>
        /// <returns></returns>
        string FindModelsType(string datatype);
        /// <summary>
        /// 判断数据表字段重复
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名</param>
        /// <param name="filedsJson">数据字段</param>
        /// <returns></returns>
        Task<bool> ExistFiled(string keyValue, string tableName, string keyName, string filedsJson);
        #endregion
    }
}
