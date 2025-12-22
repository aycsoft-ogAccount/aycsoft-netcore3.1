using aycsoft.iapplication;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.01
    /// 描 述：任务计划模板信息
    /// </summary>
    public class TSSchemeService : ServiceBase
    {
        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public Task<IEnumerable<TSSchemeInfoEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" 
                    SELECT
	                    t.F_Id,
	                    t.F_Name,
	                    t.F_Description,
                        t.F_IsActive,
	                    t.F_BeginTime,
                        t.F_EndType,
	                    t.F_EndTime
                    FROM
	                    LR_TS_SchemeInfo t
                ");
            strSql.Append("  WHERE 1=1 ");
            var queryParam = queryJson.ToJObject();
            string keyWord = "";
            if (!queryParam["keyWord"].IsEmpty())
            {
                keyWord = "%" + queryParam["keyWord"].ToString() + "%";
                strSql.Append("  AND t.F_Name like @keyWord ");
            }

            return this.BaseRepository().FindList<TSSchemeInfoEntity>(strSql.ToString(), new { keyWord }, pagination);
        }
        /// <summary>
        /// 获取所有启用的任务
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<TSSchemeInfoEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append(@" 
                    SELECT
	                    t.F_Id,
	                    t.F_Name,
                        t.F_IsActive,
	                    t.F_BeginTime,
                        t.F_EndType,
	                    t.F_EndTime,
                        t.F_Description,
                        t.F_Scheme
                    FROM
	                    LR_TS_SchemeInfo t
                ");
            strSql.Append("  WHERE t.F_IsActive = 1 ");
            return this.BaseRepository().FindList<TSSchemeInfoEntity>(strSql.ToString());
        }
        /// <summary> 
        /// 获取表实体数据
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public Task<TSSchemeInfoEntity> GetSchemeInfoEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<TSSchemeInfoEntity>(keyValue);
        }
        #endregion

        #region 提交数据 
        /// <summary> 
        /// 删除实体数据
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public async Task DeleteEntity(string keyValue)
        {
            await this.BaseRepository().DeleteAny<TSSchemeInfoEntity>(new { F_Id = keyValue });
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary> 
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param> 
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, TSSchemeInfoEntity entity)
        {
            TSSchemeModel model = entity.F_Scheme.ToObject<TSSchemeModel>();
            if (model.startType == 1)
            {
                entity.F_BeginTime = DateTime.Now;
            }
            else
            {
                entity.F_BeginTime = model.startTime;
            }

            entity.F_EndType = model.endType;
            if (model.endType == 1)
            {
                entity.F_EndTime = DateTime.MaxValue.AddDays(-1);
            }
            else
            {
                entity.F_EndTime = model.endTime;
            }


            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
                await this.BaseRepository().Update(entity);
            }
            else
            {
                entity.F_Id = Guid.NewGuid().ToString();
                entity.F_IsActive = 1;
                await this.BaseRepository().Insert(entity);
            }
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateEntity(string keyValue, TSSchemeInfoEntity entity)
        {
            entity.F_Id = keyValue;
            await this.BaseRepository().Update(entity);
        }
        #endregion

        #region 执行sql语句和存储过程
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public async Task ExecuteBySql(string code, string sql)
        {
            await this.BaseRepository(code).ExecuteSql(sql);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public async Task ExecuteProc(string code, string name)
        {
            await this.BaseRepository(code).ExecuteProc(name);
        }

        #endregion
    }
}
