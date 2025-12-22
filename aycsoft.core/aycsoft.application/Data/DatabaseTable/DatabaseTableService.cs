using aycsoft.database;
using aycsoft.iapplication;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：数据库表管理
    /// </summary>
    public class DatabaseTableService : ServiceBase
    {
        #region 获取数据
        /// <summary>
        /// 数据表列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> GetTableList(string code)
        {
            return this.BaseRepository(code).GetDataBaseTable();            
        }
        /// <summary>
        /// 数据表字段列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> GetTableFiledList(string code, string tableName)
        {
            return this.BaseRepository(code).GetDataBaseTableFields(tableName);
        }

        /// <summary>
        /// 数据库表数据列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表明</param>
        /// <param name="field">字段</param>
        /// <param name="logic">逻辑</param>
        /// <param name="keyword">关键字</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public Task<DataTable> GetTableDataList(string code, string tableName, string field, string logic, string keyword, Pagination pagination)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM " + tableName + " WHERE 1=1");
            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(logic) && logic != "Null" && logic != "NotNull")
            {
                strSql.Append(" AND " + field + " ");
                switch (logic)
                {
                    case "Equal":           //等于
                        strSql.Append(" = ");
                        break;
                    case "NotEqual":        //不等于
                        strSql.Append(" <> ");
                        break;
                    case "Greater":         //大于
                        strSql.Append(" > ");
                        break;
                    case "GreaterThan":     //大于等于
                        strSql.Append(" >= ");
                        break;
                    case "Less":            //小于
                        strSql.Append(" < ");
                        break;
                    case "LessThan":        //小于等于
                        strSql.Append(" >= ");
                        break;
                    case "Like":            //包含
                        strSql.Append(" like ");
                        keyword = "%" + keyword + "%";
                        break;
                    default:
                        break;
                }
                strSql.Append("@keyword ");
            }
            if (logic == "Null")
            {
                strSql.Append(" AND " + field + " is null ");
            }
            else if (logic == "NotNull")
            {
                strSql.Append(" AND " + field + " is not null ");
            }

            return this.BaseRepository(code).FindTable(strSql.ToString(), new { keyword }, pagination);
        }
        /// <summary>
        /// 数据库表数据列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public Task<DataTable> GetTableDataList(string code, string tableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM " + tableName);
            return this.BaseRepository(code).FindTable(strSql.ToString());
        }

        /// <summary>
        /// 数据库表数据列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="strSql">表名</param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetSqlColName(string code, string strSql)
        {
            return this.BaseRepository(code).GetSqlColName(strSql);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName"></param>
        /// <param name="tableRemark"></param>
        /// <param name="colList"></param>
        /// <returns></returns>
        public async Task<string> CreateTable(string code, string tableName, string tableRemark, List<DatabaseTableFieldModel> colList) {
            var db = this.BaseRepository(code).BeginTrans();
            try
            {
                string sql = "";
                switch (this.DBType(code))//SqlServer,Oracle,MySql
                {
                    case "SqlServer":
                        sql = GetSqlServerCreateTableSql(tableName, tableRemark, colList);
                        await db.ExecuteSql(sql);
                        break;
                    case "MySql":
                        sql = GetMySqlCreateTableSql(tableName, tableRemark, colList);
                        await db.ExecuteSql(sql);
                        break;
                    case "Oracle":
                        await GetOracleCreateTableSql(tableName, tableRemark, colList, db);
                        break;
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }

            return "建表成功";
        }
        /// <summary>
        /// 获取sqlserver建表语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tableRemark">表备注</param>
        /// <param name="colList">字段集合</param>
        /// <returns></returns>
        private string GetSqlServerCreateTableSql(string tableName, string tableRemark, List<DatabaseTableFieldModel> colList) {
            StringBuilder sql = new StringBuilder();

            sql.Append("CREATE TABLE " + tableName + " ( ");//表名
            foreach (var item in colList)
            {
                sql.Append(item.f_column + " " + item.f_datatype);//列名+类型
                if (item.f_datatype == "varchar" &&  !string.IsNullOrEmpty(item.f_length))
                {
                    sql.Append("(" + item.f_length + ") ");//长度
                }
                if (item.f_key == "1")
                {
                    sql.Append(" PRIMARY KEY ");//是否主键
                }
                else if (item.f_isnullable == "0")
                {
                    sql.Append(" NOT NULL ");//是否为空
                }
                sql.Append(",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            foreach (var item in colList)
            {
                if (!string.IsNullOrEmpty(item.f_remark))
                { 
                    //添加列备注
                    sql.Append(" execute sp_addextendedproperty 'MS_Description','" + item.f_remark + "','user','dbo','table','" + tableName + "','column','" + item.f_column + "';");
                }
            }
            //添加表备注
            if (!string.IsNullOrEmpty(tableRemark))
            {
                sql.Append(" execute sp_addextendedproperty 'MS_Description','" + tableRemark + "','user','dbo','table','" + tableName + "',null,null;  ");
            }

            return sql.ToString();
        }
        /// <summary>
        /// 获取MySql建表语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tableRemark">表备注</param>
        /// <param name="colList">字段集合</param>
        /// <returns></returns>
        private string GetMySqlCreateTableSql(string tableName, string tableRemark, List<DatabaseTableFieldModel> colList)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("CREATE TABLE " + tableName + " ( ");//表名
            foreach (var item in colList)
            {
                sql.Append(item.f_column + " " + item.f_datatype);//列名+类型
                if (item.f_datatype == "varchar" && !string.IsNullOrEmpty(item.f_length))
                {
                    sql.Append("(" + item.f_length + ") ");//长度
                }
                if (item.f_key == "1")
                {
                    sql.Append(" PRIMARY KEY ");//是否主键
                }
                else if (item.f_isnullable == "0")
                {
                    sql.Append(" NOT NULL ");//是否为空
                }
                if (!string.IsNullOrEmpty(item.f_remark))
                {
                    sql.Append(" COMMENT '" + item.f_remark + "'");//列备注
                }
                sql.Append(",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            //添加表备注
            if (!string.IsNullOrEmpty(tableRemark))
            {
                sql.Append(" ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='"+ tableRemark + "';");
            }

            return sql.ToString();
        }
        /// <summary>
        /// 获取Oracle建表语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tableRemark">表备注</param>
        /// <param name="colList">字段集合</param>
        /// <param name="db">数据库操作上下文</param>
        /// <returns></returns>
        private async Task GetOracleCreateTableSql(string tableName, string tableRemark, List<DatabaseTableFieldModel> colList, IRepository db)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("CREATE TABLE \"" + tableName.ToUpper() + "\" ( ");//表名
            foreach (var item in colList)
            {
                sql.Append("\"" +item.f_column.ToUpper() + "\" " + GetOracleDataType(item.f_datatype));//列名+类型
                if (item.f_datatype == "varchar" && !string.IsNullOrEmpty(item.f_length))
                {
                    sql.Append("(" + item.f_length + " CHAR) ");//长度
                }
                if (item.f_key == "1")
                {
                    sql.Append(" PRIMARY KEY NOT NULL ");//是否主键
                }
                else if (item.f_isnullable == "0")
                {
                    sql.Append(" NOT NULL ");//是否为空
                }
                else {
                    sql.Append(" NULL ");//是否为空
                }
                sql.Append(",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");

            await db.ExecuteSql(sql.ToString());

            ////添加表备注
            if (!string.IsNullOrEmpty(tableRemark))
            {
                await db.ExecuteSql(" COMMENT ON TABLE \"" + tableName.ToUpper() + "\" is '" + tableRemark + "'  ");
            }

            foreach (var item in colList)
            {
                if (!string.IsNullOrEmpty(item.f_remark))
                {
                    //添加列备注
                    await db.ExecuteSql(" COMMENT ON COLUMN \"" + tableName.ToUpper() + "\".\"" + item.f_column.ToUpper() + "\" is '" + item.f_remark + "'");
                }
            }
        }
        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private string GetOracleDataType(string dataType) {
            string res = "";
            switch (dataType) {
                case "varchar":
                    res = "VARCHAR2";
                    break;
                case "datetime":
                    res = "DATE";
                    break;
                case "int":
                case "decimal":
                    res = "NUMBER(11)";
                    break;
            }

            return res;
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 判断数据表字段重复
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名</param>
        /// <param name="filedsJson">数据字段</param>
        /// <returns></returns>
        public async Task<bool> ExistFiled(string keyValue,string tableName,string keyName, string filedsJson) {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from " + tableName + " where 1=1 ");
            if (!string.IsNullOrEmpty(keyValue)) {
                strSql.Append(" AND " + keyName + " !=@keyValue ");
            }
            var args = filedsJson.ToJObject();
            foreach (var item in args.Properties())
            {
                if (!string.IsNullOrEmpty(item.Value.ToString()))
                {
                    strSql.Append(" AND " + item.Name + " = '" + item.Value + "'");
                }
            }

            var entity = await this.BaseRepository().FindEntity(strSql.ToString(),new { keyValue });
            if (entity == null)
            {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion
    }
}
