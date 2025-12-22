using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using System;
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
    /// 描 述：数据库表管理
    /// </summary>
    public class DatabaseTableBLL : BLLBase, DatabaseTableIBLL, BLL
    {
        #region 属性
        private readonly DatabaseTableService databaseTableService = new DatabaseTableService();
        private readonly DatabaseLinkIBLL _databaseLinkIBLL;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseLinkIBLL"></param>
        public DatabaseTableBLL(DatabaseLinkIBLL databaseLinkIBLL)
        {
            _databaseLinkIBLL = databaseLinkIBLL;
        }

        #region 获取数据
        /// <summary>
        /// 数据表列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> GetTableList(string code, string tableName = "")
        {
            List<dynamic> list = (List<dynamic>)await databaseTableService.GetTableList(code);
            if (!string.IsNullOrEmpty(tableName))
            {
                list = list.FindAll(t => t.name == tableName);
            }
            return list;
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTreeList(string code)
        {
            List<TreeModel> list;
            if (string.IsNullOrEmpty(code))
            {
                list = (List<TreeModel>)await _databaseLinkIBLL.GetTreeListEx();
            }
            else
            {
                list = new List<TreeModel>();
                List<dynamic> databaseTableList = (List<dynamic>)await GetTableList(code);
                foreach (var item in databaseTableList)
                {
                    TreeModel node = new TreeModel();
                    node.id = code + item.name;
                    node.text = item.name;
                    node.value = code;
                    node.title = item.tdescription;
                    node.complete = true;
                    node.isexpand = false;
                    node.hasChildren = false;
                    list.Add(node);
                }
            }
            return list;
        }

        /// <summary>
        /// 数据表字段列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public Task<IEnumerable<dynamic>> GetTableFiledList(string code, string tableName)
        {
            return databaseTableService.GetTableFiledList(code, tableName);
        }
        /// <summary>
        /// 获取数据表字段树形数据
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetFiledTreeList(string code, string tableName)
        {

            var list = await GetTableFiledList(code, tableName);
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.f_column;
                node.text = item.f_column;
                node.value = item.f_column;
                node.title = item.f_remark;
                node.complete = true;
                node.isexpand = false;
                node.hasChildren = false;
                node.showcheck = true;
                treeList.Add(node);
            }
            return treeList;

        }
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
        public Task<DataTable> GetTableDataList(string code, string tableName, string field, string logic, string keyword, Pagination pagination)
        {
            return databaseTableService.GetTableDataList(code, tableName, field, logic, keyword, pagination);
        }
        /// <summary>
        /// 数据库表数据列表
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public Task<DataTable> GetTableDataList(string code, string tableName)
        {
            return databaseTableService.GetTableDataList(code, tableName);
        }
        /// <summary>
        /// 给定查询语句查询字段
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="strSql">表名</param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetSqlColName(string code, string strSql)
        {
            return databaseTableService.GetSqlColName(code, strSql);
        }

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
        public Task<string> CreateTable(string code, string tableName, string tableRemark, List<DatabaseTableFieldModel> colList)
        {
            return databaseTableService.CreateTable(code, tableName, tableRemark, colList);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// C#实体数据类型
        /// </summary>
        /// <param name="datatype">数据库字段类型</param>
        /// <returns></returns>
        public string FindModelsType(string datatype)
        {
            string res = "";
            datatype = datatype.ToLower();
            switch (datatype)
            {
                case "int":
                case "number":
                case "integer":
                case "smallint":
                    res = "int?";
                    break;
                case "tinyint":
                    res = "byte?";
                    break;
                case "numeric":
                case "real":
                case "decimal":
                case "number(8,2)":
                case "money":
                case "smallmoney":
                    res = "decimal?";
                    break;
                case "float":
                    res = "float?";
                    break;
                case "char":
                case "varchar":
                case "nvarchar2":
                case "text":
                case "nchar":
                case "nvarchar":
                case "ntext":
                    res = "string";
                    break;
                case "bit":
                    res = "bool?";
                    break;
                case "datetime2":
                case "datetime":
                case "date":
                case "smalldatetime":
                    res = "DateTime?";
                    break;
                default:
                    res = "string";
                    break;
            }

            if (datatype.Contains("number("))
            {
                res = "decimal?";
            }
            return res;
        }

        /// <summary>
        /// 判断数据表字段重复
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyName">主键名</param>
        /// <param name="filedsJson">数据字段</param>
        /// <returns></returns>
        public Task<bool> ExistFiled(string keyValue, string tableName, string keyName, string filedsJson)
        {
            return databaseTableService.ExistFiled(keyValue, tableName, keyName, filedsJson);
        }
        #endregion
    }
}
