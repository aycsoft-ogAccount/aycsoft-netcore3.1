using Dapper;
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
    /// 日 期：2022.11.20
    /// 描 述：详细信息维护
    /// </summary>
    public class ArticleService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public ArticleService()
        {
            fieldSql= @"
                t.F_Id,
                t.F_Title,
                t.F_Category,
                t.F_Content,
                t.F_PushDate,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_ModifyDate,
                t.F_ModifyUserId,
                t.F_ModifyUserName
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">条件参数</param>
        /// <returns></returns>
        public Task<IEnumerable<ArticleEntity>> GetList( string queryJson )
        {
            //参考写法
            var queryParam = queryJson.ToJObject();
            // 虚拟参数
            //var dp = new DynamicParameters(new { });
            //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_PS_Article t where 1=1 ");

            string category = "";
            if (!queryParam["category"].IsEmpty())
            {
                strSql.Append(" AND t.F_Category = @category  ");
                category = queryParam["category"].ToString();
            }

            return this.BaseRepository().FindList<ArticleEntity>(strSql.ToString(), new { category });
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">条件参数</param>
        /// <returns></returns>
        public Task<IEnumerable<ArticleEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var dp = new DynamicParameters(new { });
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@"
                        t.F_Id,
                        t.F_Title,
                        t.F_Category,
                        t.F_PushDate,
                        t.F_CreateDate,
                        t.F_CreateUserId,
                        t.F_CreateUserName,
                        t.F_ModifyDate,
                        t.F_ModifyUserId,
                        t.F_ModifyUserName
                 ");
            strSql.Append(" FROM LR_PS_Article t ");
            strSql.Append(" where 1=1");
            if (queryParam.HasValues)
            {
                if (!queryParam["F_Category"].IsEmpty())
                {
                    dp.Add("F_Category", queryParam["F_Category"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Category = @F_Category ");
                }
                if (!queryParam["F_Title"].IsEmpty())
                {
                    dp.Add("F_Title", "%" + queryParam["F_Title"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Title like @F_Title ");
                }
                if (!queryParam["dateBegin"].IsEmpty() && !queryParam["dateEnd"].IsEmpty())
                {
                    dp.Add("dateBegin", queryParam["dateBegin"].ToString(), DbType.String);
                    dp.Add("dateEnd", queryParam["dateEnd"].ToString(), DbType.String);
                    strSql.Append(" AND (t.F_PushDate >= @dateBegin AND t.F_PushDate <= @dateEnd )");
                }
            }
            return this.BaseRepository().FindList<ArticleEntity>(strSql.ToString(), dp, pagination);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<ArticleEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<ArticleEntity>(keyValue);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            await this.BaseRepository().DeleteAny<ArticleEntity>(new { F_Id = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, ArticleEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
                entity.F_ModifyDate = DateTime.Now;
                entity.F_ModifyUserId = this.GetUserId();
                entity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(entity);
            }
            else
            {
                entity.F_Id = Guid.NewGuid().ToString();
                entity.F_CreateDate = DateTime.Now;
                entity.F_CreateUserId = this.GetUserId();
                entity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
