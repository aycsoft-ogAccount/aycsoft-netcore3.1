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
    public class Repository: IRepository
    {
        #region 构造
        /// <summary>
        /// 数据库操作接口
        /// </summary>
        public IDataBase db;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="idatabase"></param>
        public Repository(IDataBase idatabase)
        {
            this.db = idatabase;
        }
        #endregion

        #region 事务
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public IRepository BeginTrans() {
            db.BeginTrans();
            return this;
        }
        /// <summary>
        /// 提交
        /// </summary>
        public void Commit() {
            db.Commit();
        }
        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback() {
            db.Rollback();
        }
        #endregion

        #region 执行sql语句
        /// <summary>
        /// 执行sql语句(带参数)
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<int> ExecuteSql(string strSql, object param = null) {
            return this.db.ExecuteSql(strSql, param);
        }
        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<int> ExecuteProc(string procName, object param = null)
        {
            return this.db.ExecuteProc(procName, param);
        }
        /// <summary>
        /// 执行存储过程(查询实体数据)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<T> ExecuteProc<T>(string procName, object param = null) where T : class {
            return this.db.ExecuteProc<T>(procName, param);
        }
        /// <summary>
        /// 执行存储过程(查询实体数据)
        /// </summary>
        /// <returns></returns>
        /// <param name="procName">存储过程名称.</param>
        /// <param name="param">参数</param>
        public Task<dynamic> QueryFirstProc(string procName, object param = null) {
            return this.db.QueryFirstProc(procName, param);
        }
        /// <summary>
        /// 执行存储过程(获取列表数据)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> QueryProc<T>(string procName, object param = null) where T : class {
            return this.db.QueryProc<T>(procName, param);
        }
        /// <summary>
        /// 执行存储过程(获取列表数据)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> QueryProc(string procName, object param = null) {
            return this.db.QueryProc(procName, param);
        }
        #endregion

        #region 对像实体 新增/修改/删除      
        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public Task<int> Insert<T>(T entity) where T : class {
            return this.db.Insert<T>(entity);
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <returns></returns>
        /// <param name="entity">实体数据</param>
        /// <param name="isOnlyHasValue">是否只更新有值的字段</param>
        /// <typeparam name="T">类型</typeparam>
        public Task<int> Update<T>(T entity, bool isOnlyHasValue = true) where T : class {
            return this.db.Update<T>(entity, isOnlyHasValue);
        }
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <returns></returns>
        /// <param name="entity">实体数据</param>
        /// <typeparam name="T">类型</typeparam>
        public Task<int> Delete<T>(T entity) where T : class {
            return this.db.Delete<T>(entity);
        }
        /// <summary>
        /// 删除数据根据给定的字段值
        /// </summary>
        /// <returns></returns>
        /// <param name="param">参数</param>
        /// <typeparam name="T">类型</typeparam>
        public Task<int> DeleteAny<T>(object param) where T : class {
            return this.db.DeleteAny<T>(param);
        }
        #endregion

        #region 对象实体查询
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="keyValue">主键</param>
        /// <typeparam name="T">类</typeparam>
        public Task<T> FindEntityByKey<T>(object keyValue) where T : class {
            return this.db.FindEntityByKey<T>(keyValue);
        }
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <typeparam name="T">类</typeparam>
        public Task<T> FindEntity<T>(string sql, object param = null) where T : class {
            return this.db.FindEntity<T>(sql, param);
        }

        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="param">参数</param>
        /// <typeparam name="T">类</typeparam>
        public Task<T> FindEntity<T>(object param) where T : class {
            return this.db.FindEntity<T>(param);
        }
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="sql">sql语句.</param>
        /// <param name="param">参数.</param>
        public Task<dynamic> FindEntity(string sql, object param = null) {
            return this.db.FindEntity(sql, param);
        }



        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="sql">sql语句.</param>
        /// <param name="param">参数.</param>
        /// <typeparam name="T">类型.</typeparam>
        public Task<IEnumerable<T>> FindList<T>(string sql, object param = null) where T : class {
            return this.db.FindList<T>(sql, param);
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="param">参数.</param>
        /// <typeparam name="T">类型.</typeparam>
        public Task<IEnumerable<T>> FindList<T>(object param) where T : class {
            return this.db.FindList<T>(param);
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="sql">sql语句.</param>
        /// <param name="param">参数.</param>
        public Task<IEnumerable<dynamic>> FindList(string sql, object param = null) {
            return this.db.FindList(sql, param);
        }
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
        public Task<(IEnumerable<T> list, int total)> FindList<T>(string sql, object param, string orderField, bool isAsc, int pageSize, int pageIndex) where T : class {
            return this.db.FindList<T>(sql, param, orderField, isAsc, pageSize, pageIndex);
        }
        /// <summary>
        /// 获取数据列表（分页）
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> FindList<T>(string sql, object param, Pagination pagination) where T : class {
            return this.db.FindList<T>(sql,param, pagination);
        }
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
        public Task<(IEnumerable<dynamic> list, int total)> FindList(string sql, object param, string orderField, bool isAsc, int pageSize, int pageIndex) {
            return this.db.FindList(sql,param, orderField, isAsc, pageSize, pageIndex);
        }
        /// <summary>
        /// 获取数据列表（分页）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> FindList(string sql, object param, Pagination pagination) {
            return this.db.FindList(sql, param, pagination);
        }

        /// <summary>
        /// 获取表的所有数据
        /// </summary>
        /// <returns>列表数据</returns>
        /// <typeparam name="T">类</typeparam>
        public Task<IEnumerable<T>> FindAll<T>() where T : class {
            return this.db.FindAll<T>();
        }
        #endregion

        #region 数据源查询
        /// <summary>
        /// 查询方法，返回datatable
        /// </summary>
        /// <returns>datatable数据</returns>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        public Task<DataTable> FindTable(string sql, object param = null) {
            return this.db.FindTable(sql,param);
        }
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
        public Task<(DataTable list, int total)> FindTable(string sql, object param, string orderField, bool isAsc, int pageSize, int pageIndex) {
            return this.db.FindTable(sql,param, orderField, isAsc, pageSize, pageIndex);
        }
        /// <summary>
        /// 分页查询方法，返回datatable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public Task<DataTable> FindTable(string sql, object param, Pagination pagination) {
            return this.db.FindTable(sql,param, pagination);
        }
        #endregion

        #region 获取数据库表信息
        /// <summary>
        /// 获取数据库表信息
        /// </summary>
        /// <returns>数据库表信息</returns>
        public Task<IEnumerable<dynamic>> GetDataBaseTable() {
            return this.db.GetDataBaseTable();
        }
        /// <summary>
        /// 获取表的字段信息
        /// </summary>
        /// <returns>表的字段信息</returns>
        /// <param name="tableName">表名</param>
        public Task<IEnumerable<dynamic>> GetDataBaseTableFields(string tableName) {
            return this.db.GetDataBaseTableFields(tableName);
        }
        /// <summary>
        /// 获取数据库地址信息
        /// </summary>
        /// <returns>数据库地址信息</returns>
        public string GetDataSource() {
            return this.db.GetDataSource();
        }
        /// <summary>
        /// 测试数据库是否能链接成功
        /// </summary>
        /// <returns>The connection.</returns>
        public string TestConnection() {
            return this.db.TestConnection();
        }

        /// <summary>
        /// 获取sql语句的字段
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetSqlColName(string strSql) {
            return this.db.GetSqlColName(strSql);
        }
        #endregion
    }
}
