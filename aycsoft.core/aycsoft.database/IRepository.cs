using aycsoft.util;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace aycsoft.database
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.10
    /// 描 述：数据库接口类
    /// </summary>
    public interface IRepository
    {
        #region 事务
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        IRepository BeginTrans();
        /// <summary>
        /// 提交
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
        #endregion

        #region 执行sql语句
        /// <summary>
        /// 执行sql语句(带参数)
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        Task<int> ExecuteSql(string strSql, object param = null);
        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        Task<int> ExecuteProc(string procName, object param = null);
        /// <summary>
        /// 执行存储过程(查询实体数据)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        Task<T> ExecuteProc<T>(string procName, object param = null) where T : class;
        /// <summary>
        /// 执行存储过程(查询实体数据)
        /// </summary>
        /// <returns></returns>
        /// <param name="procName">存储过程名称.</param>
        /// <param name="param">参数</param>
        Task<dynamic> QueryFirstProc(string procName, object param = null);
        /// <summary>
        /// 执行存储过程(获取列表数据)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryProc<T>(string procName, object param = null) where T : class;
        /// <summary>
        /// 执行存储过程(获取列表数据)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryProc(string procName, object param = null);
        #endregion

        #region 对像实体 新增/修改/删除      
        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        Task<int> Insert<T>(T entity) where T : class;
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <returns></returns>
        /// <param name="entity">实体数据</param>
        /// <param name="isOnlyHasValue">是否只更新有值的字段</param>
        /// <typeparam name="T">类型</typeparam>
        Task<int> Update<T>(T entity, bool isOnlyHasValue = true) where T : class;
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <returns></returns>
        /// <param name="entity">实体数据</param>
        /// <typeparam name="T">类型</typeparam>
        Task<int> Delete<T>(T entity) where T : class;
        /// <summary>
        /// 删除数据根据给定的字段值
        /// </summary>
        /// <returns></returns>
        /// <param name="param">参数</param>
        /// <typeparam name="T">类型</typeparam>
        Task<int> DeleteAny<T>(object param) where T : class;
        #endregion

        #region 对象实体查询
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="keyValue">主键</param>
        /// <typeparam name="T">类</typeparam>
        Task<T> FindEntityByKey<T>(object keyValue) where T : class;
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <typeparam name="T">类</typeparam>
        Task<T> FindEntity<T>(string sql, object param = null) where T : class;
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="param">参数</param>
        /// <typeparam name="T">类</typeparam>
        Task<T> FindEntity<T>(object param) where T : class;
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="sql">sql语句.</param>
        /// <param name="param">参数.</param>
        Task<dynamic> FindEntity(string sql, object param = null);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="sql">sql语句.</param>
        /// <param name="param">参数.</param>
        /// <typeparam name="T">类型.</typeparam>
        Task<IEnumerable<T>> FindList<T>(string sql, object param = null) where T : class;
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="param">参数.</param>
        /// <typeparam name="T">类型.</typeparam>
        Task<IEnumerable<T>> FindList<T>(object param) where T : class;
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="sql">sql语句.</param>
        /// <param name="param">参数.</param>
        Task<IEnumerable<dynamic>> FindList(string sql, object param = null);
        /// <summary>
        /// 获取数据列表（分页）
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <typeparam name="T">类</typeparam>
        Task<(IEnumerable<T> list, int total)> FindList<T>(string sql, object param, string orderField, bool isAsc, int pageSize, int pageIndex) where T : class;
        /// <summary>
        /// 获取数据列表（分页）
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindList<T>(string sql, object param, Pagination pagination) where T : class;
        /// <summary>
        /// 获取数据列表（分页）
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        Task<(IEnumerable<dynamic> list, int total)> FindList(string sql, object param, string orderField, bool isAsc, int pageSize, int pageIndex);
        /// <summary>
        /// 获取数据列表（分页）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> FindList(string sql, object param, Pagination pagination);

        /// <summary>
        /// 获取表的所有数据
        /// </summary>
        /// <returns>列表数据</returns>
        /// <typeparam name="T">类</typeparam>
        Task<IEnumerable<T>> FindAll<T>() where T : class;
        #endregion

        #region 数据源查询
        /// <summary>
        /// 查询方法，返回datatable
        /// </summary>
        /// <returns>datatable数据</returns>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        Task<DataTable> FindTable(string sql, object param = null);
        /// <summary>
        /// 分页查询方法，返回datatable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns>list数据,total 总共条数</returns>
        Task<(DataTable list, int total)> FindTable(string sql, object param, string orderField, bool isAsc, int pageSize, int pageIndex);
        /// <summary>
        /// 分页查询方法，返回datatable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        Task<DataTable> FindTable(string sql, object param, Pagination pagination);
        #endregion

        #region 获取数据库表信息
        /// <summary>
        /// 获取数据库表信息
        /// </summary>
        /// <returns>数据库表信息</returns>
        Task<IEnumerable<dynamic>> GetDataBaseTable();
        /// <summary>
        /// 获取表的字段信息
        /// </summary>
        /// <returns>表的字段信息</returns>
        /// <param name="tableName">表名</param>
        Task<IEnumerable<dynamic>> GetDataBaseTableFields(string tableName);
        /// <summary>
        /// 获取数据库地址信息
        /// </summary>
        /// <returns>数据库地址信息</returns>
        string GetDataSource();
        /// <summary>
        /// 测试数据库是否能链接成功
        /// </summary>
        /// <returns>The connection.</returns>
        string TestConnection();
        /// <summary>
        /// 获取sql语句的字段
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetSqlColName(string strSql);
        #endregion
    }
}
