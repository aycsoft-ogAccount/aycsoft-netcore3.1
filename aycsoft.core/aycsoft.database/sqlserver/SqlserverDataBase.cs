using cd.dapper.extension;
using Dapper;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.database.sqlserver
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.09
    /// 描 述：数据库操作方法实现
    /// </summary>
    public class SqlserverDataBase : IDataBase
    {

        private ISqlAdapter sqlAdapter;

        /// <summary>
        /// 当前使用的数据库链接对象
        /// </summary>
        private DbConnection dbConnection { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        private DbTransaction dbTransaction { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connString">数据库连接地址</param>
        public SqlserverDataBase(string connString)
        {
            sqlAdapter = new SqlserverAdapter();
            dbConnection = new SqlConnection(connString);
        }

        #region 事务
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns>The trans.</returns>
        public IDataBase BeginTrans()
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbTransaction = dbConnection.BeginTransaction();
            return this;
        }
        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            try
            {
                dbTransaction.Commit();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {

            try
            {
                dbTransaction.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Close();
            }
        }


        /// <summary>
        /// 关闭连接 内存回收
        /// </summary>
        public void Close()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Dispose();
                dbTransaction = null;
            }
            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection.Dispose();
                dbConnection = null;
            }
        }
        #endregion

        #region 执行sql语句
        /// <summary>
        /// 执行sql语句(带参数)
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<int> ExecuteSql(string strSql, object param = null)
        {
            try
            {
                var res = await dbConnection.ExecuteAsync(strSql, param, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<int> ExecuteProc(string procName, object param = null)
        {
            try
            {
                var res = await dbConnection.ExecuteAsync(procName, param, dbTransaction, null, CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 执行存储过程(查询实体数据)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<T> ExecuteProc<T>(string procName, object param = null) where T : class
        {
            try
            {
                var res = await dbConnection.QueryFirstAsync<T>(procName, param, dbTransaction, null, CommandType.StoredProcedure);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 执行存储过程(查询实体数据)
        /// </summary>
        /// <returns></returns>
        /// <param name="procName">存储过程名称.</param>
        /// <param name="param">参数</param>
        public async Task<dynamic> QueryFirstProc(string procName, object param = null)
        {
            try
            {
                var res = await dbConnection.QueryFirstAsync(procName, param, dbTransaction, null, CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 执行存储过程(获取列表数据)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryProc<T>(string procName, object param = null) where T : class
        {
            try
            {
                var res = await dbConnection.QueryAsync<T>(procName, param, dbTransaction, null, CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 执行存储过程(获取列表数据)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> QueryProc(string procName, object param = null)
        {
            try
            {
                var res = await dbConnection.QueryAsync(procName, param, dbTransaction, null, CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        #endregion

        #region 对像实体 新增/修改/删除      
        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task<int> Insert<T>(T entity) where T : class
        {
            try
            {
                string sql = SqlHelper.Insert<T>(sqlAdapter);
                var res = await dbConnection.ExecuteAsync(sql, entity, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <returns></returns>
        /// <param name="entity">实体数据</param>
        /// <param name="isOnlyHasValue">是否只更新有值的字段</param>
        /// <typeparam name="T">类型</typeparam>
        public async Task<int> Update<T>(T entity, bool isOnlyHasValue = true) where T : class
        {
            try
            {
                string sql = "";
                if (isOnlyHasValue)
                {
                    var args = entity.ToJson().ToJObject();
                    List<string> hasNoValueList = new List<string>();
                    List<string> nullValueList = new List<string>();
                    foreach (var item in args.Properties())
                    {
                        if (item.Value.Type == Newtonsoft.Json.Linq.JTokenType.Null)
                        {
                            hasNoValueList.Add(item.Name);
                        }
                        else if (item.Value.ToString() == "" || item.Value.ToString() == "1000-01-01 00:00:00")
                        {
                            nullValueList.Add(item.Name);
                        }
                    }
                    sql = SqlHelper.Update<T>(sqlAdapter, hasNoValueList);

                    foreach (var item in nullValueList)
                    {
                        sql = sql.Replace("?" + item, "null");
                    }
                }
                else
                {
                    sql = SqlHelper.Update<T>(sqlAdapter);
                }
                var res = await dbConnection.ExecuteAsync(sql, entity, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <returns></returns>
        /// <param name="entity">实体数据</param>
        /// <typeparam name="T">类型</typeparam>
        public async Task<int> Delete<T>(T entity) where T : class
        {
            try
            {
                string sql = SqlHelper.Delete<T>(sqlAdapter);
                var res = await dbConnection.ExecuteAsync(sql, entity, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 删除数据根据给定的字段值
        /// </summary>
        /// <returns></returns>
        /// <param name="param">参数</param>
        /// <typeparam name="T">类型</typeparam>
        public async Task<int> DeleteAny<T>(object param) where T : class
        {
            try
            {
                if (param == null)
                {
                    param = new { };
                }
                var jparam = param.ToJson().ToJObject();
                List<string> fields = new List<string>();
                foreach (var item in jparam.Properties())
                {
                    if (!string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        fields.Add(item.Name);
                    }
                }
                string sql = SqlHelper.Delete<T>(sqlAdapter, fields);
                var res = await dbConnection.ExecuteAsync(sql, param, dbTransaction);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        #endregion

        #region 对象实体查询
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="keyValue">主键</param>
        /// <typeparam name="T">类</typeparam>
        public async Task<T> FindEntityByKey<T>(object keyValue) where T : class
        {
            try
            {
                string sql = SqlHelper.Select<T>(sqlAdapter);
                var res = await dbConnection.QueryFirstOrDefaultAsync<T>(sql, new { id = keyValue }, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <typeparam name="T">类</typeparam>
        public async Task<T> FindEntity<T>(string sql, object param = null) where T : class
        {
            try
            {
                var res = await dbConnection.QueryFirstOrDefaultAsync<T>(sql, param, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="param">参数</param>
        /// <typeparam name="T">类</typeparam>
        public async Task<T> FindEntity<T>(object param) where T : class
        {
            try
            {
                if (param == null)
                {
                    param = new { };
                }
                string sql = SqlHelper.SelectAll<T>(sqlAdapter) + " where 1=1 ";
                var jparam = param.ToJson().ToJObject();
                foreach (var item in jparam.Properties())
                {
                    if (!string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        sql += " AND " + item.Name + "=@" + item.Name;
                    }
                }
                var res = await dbConnection.QueryFirstOrDefaultAsync<T>(sql, param, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取单个实体数据
        /// </summary>
        /// <returns>单个实体数据</returns>
        /// <param name="sql">sql语句.</param>
        /// <param name="param">参数.</param>
        public async Task<dynamic> FindEntity(string sql, object param = null)
        {

            try
            {
                var res = await dbConnection.QueryFirstOrDefaultAsync(sql, param, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="sql">sql语句.</param>
        /// <param name="param">参数.</param>
        /// <typeparam name="T">类型.</typeparam>
        public async Task<IEnumerable<T>> FindList<T>(string sql, object param = null) where T : class
        {
            try
            {
                var res = await dbConnection.QueryAsync<T>(sql, param, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="param">参数.</param>
        /// <typeparam name="T">类型.</typeparam>
        public async Task<IEnumerable<T>> FindList<T>(object param) where T : class
        {
            try
            {
                if (param == null)
                {
                    param = new { };
                }
                string sql = SqlHelper.SelectAll<T>(sqlAdapter) + " where 1=1 ";
                var jparam = param.ToJson().ToJObject();
                foreach (var item in jparam.Properties())
                {
                    if (!string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        sql += " AND " + item.Name + "=@" + item.Name;
                    }
                }
                var res = await dbConnection.QueryAsync<T>(sql, param, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns>列表数据</returns>
        /// <param name="sql">sql语句.</param>
        /// <param name="param">参数.</param>
        public async Task<IEnumerable<dynamic>> FindList(string sql, object param = null)
        {
            try
            {
                var res = await dbConnection.QueryAsync(sql, param, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
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
        public async Task<(IEnumerable<T> list, int total)> FindList<T>(string sql, object param, string orderField, bool isAsc, int pageSize, int pageIndex) where T : class
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetPageSql(sql, orderField, isAsc, pageSize, pageIndex));
                var resTotal = await dbConnection.ExecuteScalarAsync("Select Count(1) From (" + sql + ") As t", param, dbTransaction);
                int total = Convert.ToInt32(resTotal);
                var list = await dbConnection.QueryAsync<T>(sb.ToString(), param, dbTransaction);
                return (list, total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取数据列表（分页）
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindList<T>(string sql, object param, Pagination pagination) where T : class {
            bool isAsc = true && (string.IsNullOrEmpty(pagination.sord) || pagination.sord.ToUpper() == "ASC");
            var (list, total) = await FindList<T>(sql, param, pagination.sidx, isAsc, pagination.rows, pagination.page);
            pagination.records = total;
            return list;
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
        public async Task<(IEnumerable<dynamic> list, int total)> FindList(string sql, object param, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetPageSql(sql, orderField, isAsc, pageSize, pageIndex));
                var resTotal = await dbConnection.ExecuteScalarAsync("Select Count(1) From (" + sql + ") As t", param, dbTransaction);
                int total = Convert.ToInt32(resTotal);
                var list = await dbConnection.QueryAsync(sb.ToString(), param, dbTransaction);
                return (list, total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取数据列表（分页）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> FindList(string sql, object param, Pagination pagination) {
            bool isAsc = true && (string.IsNullOrEmpty(pagination.sord) || pagination.sord.ToUpper() == "ASC");
            var (list, total) = await FindList(sql, param, pagination.sidx, isAsc, pagination.rows, pagination.page);
            pagination.records = total;
            return list;
        }

        /// <summary>
        /// 获取表的所有数据
        /// </summary>
        /// <returns>列表数据</returns>
        /// <typeparam name="T">类</typeparam>
        public async Task<IEnumerable<T>> FindAll<T>() where T : class
        {
            try
            {
                string sql = SqlHelper.SelectAll<T>(sqlAdapter);
                var res = await dbConnection.QueryAsync<T>(sql, null, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        #endregion

        #region 数据源查询
        /// <summary>
        /// 查询方法，返回datatable
        /// </summary>
        /// <returns>datatable数据</returns>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        public async Task<DataTable> FindTable(string sql, object param = null)
        {
            try
            {
                var IDataReader = await dbConnection.ExecuteReaderAsync(sql, param, dbTransaction);
                var dt = DBCommonHelper.IDataReaderToDataTable(IDataReader);
                IDataReader.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 分页查询方法，返回datatable
        /// </summary>
        /// <returns>datatable数据</returns>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        public async Task<(DataTable list, int total)> FindTable(string sql, object param, string orderField, bool isAsc, int pageSize, int pageIndex)
        {

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetPageSql(sql, orderField, isAsc, pageSize, pageIndex));
                int total = Convert.ToInt32(await dbConnection.ExecuteScalarAsync("Select Count(1) From (" + sql + ") As t", param, dbTransaction));
                var IDataReader = await dbConnection.ExecuteReaderAsync(sb.ToString(), param, dbTransaction);
                var dt = DBCommonHelper.IDataReaderToDataTable(IDataReader);
                IDataReader.Dispose();
                return (dt, total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 分页查询方法，返回datatable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public async Task<DataTable> FindTable(string sql, object param, Pagination pagination) {
            bool isAsc = true && (string.IsNullOrEmpty(pagination.sord) || pagination.sord.ToUpper() == "ASC");
            var (list, total) = await FindTable(sql, param, pagination.sidx, isAsc, pagination.rows, pagination.page);
            pagination.records = total;
            return list;
        }
        #endregion

        #region 获取数据库表信息
        /// <summary>
        /// 获取数据库表信息
        /// </summary>
        /// <returns>数据库表信息</returns>
        public async Task<IEnumerable<dynamic>> GetDataBaseTable()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"DECLARE @TableInfo TABLE ( name VARCHAR(50) , sumrows VARCHAR(11) , reserved VARCHAR(50) , data VARCHAR(50) , index_size VARCHAR(50) , unused VARCHAR(50) , pk VARCHAR(50) )
                            DECLARE @TableName TABLE ( name VARCHAR(50) )
                            DECLARE @name VARCHAR(50)
                            DECLARE @pk VARCHAR(50)
                            INSERT INTO @TableName ( name ) SELECT o.name FROM sysobjects o , sysindexes i WHERE o.id = i.id AND o.Xtype = 'U' AND i.indid < 2 ORDER BY i.rows DESC , o.name
                            WHILE EXISTS ( SELECT 1 FROM @TableName ) BEGIN SELECT TOP 1 @name = name FROM @TableName DELETE @TableName WHERE name = @name DECLARE @objectid INT SET @objectid = OBJECT_ID(@name) SELECT @pk = COL_NAME(@objectid, colid) FROM sysobjects AS o INNER JOIN sysindexes AS i ON i.name = o.name INNER JOIN sysindexkeys AS k ON k.indid = i.indid WHERE o.xtype = 'PK' AND parent_obj = @objectid AND k.id = @objectid INSERT INTO @TableInfo ( name , sumrows , reserved , data , index_size , unused ) EXEC sys.sp_spaceused @name UPDATE @TableInfo SET pk = @pk WHERE name = @name END
                            SELECT F.name as name, F.reserved  as reserved, F.data as data, F.index_size as index_size, RTRIM(F.sumrows) AS sumrows , F.unused as unused, ISNULL(p.tdescription, f.name) AS tdescription , F.pk as pk
                            FROM @TableInfo F LEFT JOIN ( SELECT name = CASE WHEN A.COLORDER = 1 THEN D.NAME ELSE '' END , tdescription = CASE WHEN A.COLORDER = 1 THEN ISNULL(F.VALUE, '') ELSE '' END FROM SYSCOLUMNS A LEFT JOIN SYSTYPES B ON A.XUSERTYPE = B.XUSERTYPE INNER JOIN SYSOBJECTS D ON A.ID = D.ID AND D.XTYPE = 'U' AND D.NAME <> 'DTPROPERTIES' LEFT JOIN sys.extended_properties F ON D.ID = F.major_id WHERE a.COLORDER = 1 AND F.minor_id = 0 ) P ON F.name = p.name
                             ORDER BY f.name  ");
                var res = await dbConnection.QueryAsync(strSql.ToString(), null, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取表的字段信息
        /// </summary>
        /// <returns>表的字段信息</returns>
        /// <param name="tableName">表名</param>
        public async Task<IEnumerable<dynamic>> GetDataBaseTableFields(string tableName)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT [f_number] = a.colorder , [f_column] = a.name , [f_datatype] = b.name , [f_length] = COLUMNPROPERTY(a.id, a.name, 'PRECISION') , [f_identity] = CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN '1' ELSE '' END , [f_key] = CASE WHEN EXISTS ( SELECT 1 FROM sysobjects WHERE xtype = 'PK' AND parent_obj = a.id AND name IN ( SELECT name FROM sysindexes WHERE indid IN ( SELECT indid FROM sysindexkeys WHERE id = a.id AND colid = a.colid ) ) ) THEN '1' ELSE '' END , [f_isnullable] = CASE WHEN a.isnullable = 1 THEN '1' ELSE '' END , [f_defaults] = ISNULL(e.text, '') , [f_remark] = ISNULL(g.[value], a.name)
                                FROM syscolumns a LEFT JOIN systypes b ON a.xusertype = b.xusertype INNER JOIN sysobjects d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties' LEFT JOIN syscomments e ON a.cdefault = e.id LEFT JOIN sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id LEFT JOIN sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0
                                WHERE d.name = @tableName
                                ORDER BY a.id , a.colorder");
                var res = await dbConnection.QueryAsync(strSql.ToString(), new { tableName }, dbTransaction);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取数据库地址信息
        /// </summary>
        /// <returns>数据库地址信息</returns>
        public string GetDataSource()
        {
            try
            {
                return dbConnection.DataSource;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 测试数据库是否能链接成功
        /// </summary>
        /// <returns>The connection.</returns>
        public string TestConnection()
        {
            try
            {
                BeginTrans();
                Commit();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取sql语句的字段
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetSqlColName(string strSql)
        {
            try
            {
                DataTable dt = null;
                strSql = "select TOP 1 * from(" + strSql + ")t";
                var IDataReader = await dbConnection.ExecuteReaderAsync(strSql, null, dbTransaction);
                dt = DBCommonHelper.IDataReaderToDataTable(IDataReader);

                List<string> res = new List<string>();
                foreach (DataColumn item in dt.Columns)
                {
                    res.Add(item.ColumnName);
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取分页语句
        /// </summary>
        /// <returns></returns>
        /// <param name="strSql">sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">是否是升序</param>
        /// <param name="pageSize">每一页大小</param>
        /// <param name="pageIndex">页码</param>
        private string GetPageSql(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            int num1 = (pageIndex) * pageSize;
            string OrderBy = "";

            if (!string.IsNullOrEmpty(orderField))
            {
                if (orderField.ToUpper().IndexOf("ASC", StringComparison.Ordinal) + orderField.ToUpper().IndexOf("DESC", StringComparison.Ordinal) > 0)
                {
                    OrderBy = " Order By " + orderField;
                }
                else
                {
                    OrderBy = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            else
            {
                OrderBy = "order by (select 0)";
            }
            sb.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
            sb.Append(" As rowNum, * From (" + strSql + ")  T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
            return sb.ToString();
        }

        #endregion
    }
}
