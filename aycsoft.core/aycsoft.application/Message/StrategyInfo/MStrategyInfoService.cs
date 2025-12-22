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
    /// 日 期：2022.11.05
    /// 描 述：消息策略
    /// </summary>
    public class MStrategyInfoService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public MStrategyInfoService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_StrategyName,
                t.F_StrategyCode,
                t.F_SendRole,
                t.F_MessageType,
                t.F_Description,
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
        /// <returns></returns>
        public Task<IEnumerable<MStrategyInfoEntity>> GetList()
        {
            //参考写法
            //var queryParam = queryJson.ToJObject();
            // 虚拟参数
            //var dp = new DynamicParameters(new { });
            //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_MS_StrategyInfo t ");
            return this.BaseRepository().FindList<MStrategyInfoEntity>(strSql.ToString());
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<MStrategyInfoEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var dp = new DynamicParameters(new { });
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_MS_StrategyInfo t  where 1=1");
            if (queryParam.HasValues)
            {
                if (!queryParam["keyword"].IsEmpty())
                {
                    dp.Add("keyword", "%" + queryParam["keyword"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_StrategyName like @keyword  or t.F_StrategyCode like @keyword");
                }
                if (!queryParam["dateBegin"].IsEmpty() && !queryParam["dateEnd"].IsEmpty())
                {
                    dp.Add("dateBegin", queryParam["dateBegin"].ToString(), DbType.String);
                    dp.Add("dateEnd", queryParam["dateEnd"].ToString(), DbType.String);
                    strSql.Append(" AND (t.F_CreateDate >= @dateBegin AND t.F_CreateDate <= @dateEnd )");
                }
            }
            return this.BaseRepository().FindList<MStrategyInfoEntity>(strSql.ToString(), dp, pagination);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<MStrategyInfoEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<MStrategyInfoEntity>(keyValue);
        }
        /// <summary>
        /// 根据策略编码获取策略
        /// </summary>
        /// <param name="code">策略编码</param>
        /// <returns></returns>
        public Task<MStrategyInfoEntity> GetEntityByCode(string code)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_MS_StrategyInfo t  where F_StrategyCode=@code ");

            return this.BaseRepository().FindEntity<MStrategyInfoEntity>(strSql.ToString(), new { code });
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
            await this.BaseRepository().DeleteAny<MStrategyInfoEntity>(new { F_Id = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, MStrategyInfoEntity entity)
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
