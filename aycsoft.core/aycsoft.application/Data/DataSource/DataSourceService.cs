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
    /// 描 述：数据源
    /// </summary>
    public class DataSourceService : ServiceBase
    {
        #region 属性 构造函数
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public DataSourceService()
        {
            fieldSql = @" 
                    t.F_Id,
                    t.F_Code,
                    t.F_Name,
                    t.F_DbId,
                    t.F_Description,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_CreateDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName,
                    t.F_ModifyDate
                    ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<DataSourceEntity>> GetPageList(Pagination pagination, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_DataSource t ");
            strSql.Append(" WHERE 1=1 ");

            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" AND ( t.F_Name like @keyword OR t.F_Code like @keyword ) ");
                keyword = "%" + keyword + "%";
            }
            return this.BaseRepository().FindList<DataSourceEntity>(strSql.ToString(), new { keyword = keyword }, pagination);
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DataSourceEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_DataSource t ");
            return this.BaseRepository().FindList<DataSourceEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public Task<DataSourceEntity> GetEntityByCode(string code)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(",t.F_Sql FROM LR_Base_DataSource t where F_Code =@code ");
            return this.BaseRepository().FindEntity<DataSourceEntity>(strSql.ToString(), new { code });
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DataSourceEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<DataSourceEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据源
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            DataSourceEntity entity = new DataSourceEntity()
            {
                F_Id = keyValue,
            };
            await this.BaseRepository().Delete(entity);
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataSourceEntity">数据源实体</param>
        /// <returns></returns>
        public async Task<bool> SaveEntity(string keyValue, DataSourceEntity dataSourceEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_DataSource t where F_Code =@code AND F_Id != @keyValue  ");
                DataSourceEntity entity = await this.BaseRepository().FindEntity<DataSourceEntity>(strSql.ToString(), new { code = dataSourceEntity.F_Code, keyValue });
                if (entity != null)
                {
                    return false;
                }
                dataSourceEntity.F_Id = keyValue;
                dataSourceEntity.F_ModifyDate = DateTime.Now;
                dataSourceEntity.F_ModifyUserId = this.GetUserId();
                dataSourceEntity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(dataSourceEntity);
            }
            else
            {
                DataSourceEntity entity = await GetEntityByCode(dataSourceEntity.F_Code);
                if (entity != null)
                {
                    return false;
                }
                dataSourceEntity.F_Id = Guid.NewGuid().ToString();
                dataSourceEntity.F_CreateDate = DateTime.Now;
                dataSourceEntity.F_CreateUserId = this.GetUserId();
                dataSourceEntity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(dataSourceEntity);
            }
            return true;
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取数据源的数据
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public async Task<DataTable> GetDataTable(string code, UserEntity userInfo, string queryJson = "{}")
        {
            DataSourceEntity entity = await GetEntityByCode(code);
            if (entity == null)
            {
                return new DataTable();
            }
            else
            {
                if (string.IsNullOrEmpty(queryJson))
                {
                    queryJson = "{}";
                }
                var queryParam = queryJson.ToJObject();
                var strWhere = new StringBuilder();
                foreach (var item in queryParam.Properties())
                {
                    if (!string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        strWhere.Append(" AND " + item.Name + " = '" + item.Value + "'");
                    }
                }

                string sql = string.Format(" select * From ({0})t where 1=1 {1} ", entity.F_Sql, strWhere.ToString());

                if (!string.IsNullOrEmpty(entity.F_Sql))
                {
                    // 流程当前执行人
                    sql = sql.Replace("{userId}", "'" + userInfo.F_UserId + "'");
                    sql = sql.Replace("{userAccount}", "'" + userInfo.F_Account + "'");
                    sql = sql.Replace("{companyId}", "'" + userInfo.F_CompanyId + "'");
                    sql = sql.Replace("{departmentId}", "'" + userInfo.F_DepartmentId + "'");
                }

                return await this.BaseRepository(entity.F_DbId).FindTable(sql);
            }
        }
        /// <summary>
        /// 获取数据源的数据(分页)
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public async Task<DataTable> GetDataTable(string code, Pagination pagination, UserEntity userInfo, string queryJson = "{}")
        {
            DataSourceEntity entity = await GetEntityByCode(code);
            if (entity == null)
            {
                return new DataTable();
            }
            else
            {
                if (string.IsNullOrEmpty(queryJson))
                {
                    queryJson = "{}";
                }
                var queryParam = queryJson.ToJObject();
                var strWhere = new StringBuilder();
                foreach (var item in queryParam.Properties())
                {
                    if (!string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        strWhere.Append(" AND " + item.Name + " = '" + item.Value + "'");
                    }
                }

                string sql = string.Format(" select * From ({0})t where 1=1 {1} ", entity.F_Sql, strWhere.ToString());

                if (!string.IsNullOrEmpty(entity.F_Sql))
                {
                    // 流程当前执行人
                    sql = sql.Replace("{userId}", "'" + userInfo.F_UserId + "'");
                    sql = sql.Replace("{userAccount}", "'" + userInfo.F_Account + "'");
                    sql = sql.Replace("{companyId}", "'" + userInfo.F_CompanyId + "'");
                    sql = sql.Replace("{departmentId}", "'" + userInfo.F_DepartmentId + "'");
                }
                return await this.BaseRepository(entity.F_DbId).FindTable(sql, pagination);
            }
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="parentId">父级ID</param>
        /// <param name="Id">ID</param>
        /// <param name="showId">显示ID</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTree(string code, string parentId, string Id, string showId)
        {
            DataSourceEntity entity = await GetEntityByCode(code);
            if (entity == null)
            {
                return new List<TreeModel>();
            }
            else
            {
                DataTable list = await this.BaseRepository(entity.F_DbId).FindTable(entity.F_Sql);
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (DataRow item in list.Rows)
                {
                    TreeModel node = new TreeModel
                    {
                        id = item[Id].ToString(),
                        text = item[showId].ToString(),
                        value = item[Id].ToString(),
                        showcheck = false,
                        checkstate = 0,
                        isexpand = true,
                        parentId = item[parentId].ToString()
                    };
                    treeList.Add(node);
                }
                return treeList.ToTree();
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="code">数据库连接编码</param>
        /// <param name="strSql">sql</param>
        /// <returns></returns>
        public Task<DataTable> GetDataTableBySql(string code, string strSql)
        {
            return this.BaseRepository(code).FindTable(strSql);
        }
        /// <summary>
        /// 获取sql的列
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetDataColName(string code)
        {
            DataSourceEntity entity = await GetEntityByCode(code);
            if (entity == null || string.IsNullOrEmpty(entity.F_Sql))
            {
                return new List<string>();
            }
            else
            {
                string sql = entity.F_Sql;
                sql = sql.Replace("={userId}", " is not null ");
                sql = sql.Replace("={userAccount}", " is not null");
                sql = sql.Replace("={companyId}", " is not null");
                sql = sql.Replace("={departmentId}", " is not null");
                sql = sql.Replace("= {userId}", " is not null");
                sql = sql.Replace("= {userAccount}", " is not null");
                sql = sql.Replace("= {companyId}", " is not null");
                sql = sql.Replace("= {departmentId}", " is not null");

                return await this.BaseRepository(entity.F_DbId).GetSqlColName(sql);
            }

        }

        #endregion
    }
}
