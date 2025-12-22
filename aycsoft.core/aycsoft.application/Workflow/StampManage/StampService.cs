using Dapper;
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
    /// 日 期：2020.03.13
    /// 描 述：印章管理
    /// </summary>
    public class StampService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public StampService()
        {
            //sql字段
            fieldSql = @"
                        F_StampId,
                        F_StampName,
                        F_Description,
                        F_StampType,
                        F_ImgFile,
                        F_User,
                        F_EnabledMark
                        ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取印章信息（根据名称/状态:启用或停用模糊查询）
        /// </summary>
        /// <param name="keyWord">名称/状态</param>
        /// <returns></returns>
        public Task<IEnumerable<StampEntity>> GetList(string keyWord)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append("SELECT");
            Sql.Append(this.fieldSql);
            Sql.Append("FROM LR_Base_Stamp s where F_User = @userId  ");
            Sql.Append("and s.F_EnabledMark =1  ");
            if (!string.IsNullOrEmpty(keyWord))
            {
                Sql.AppendFormat(" and s.F_StampName LIKE '%{0}%'", keyWord);
            }

            string userId = this.GetUserId();
            return this.BaseRepository().FindList<StampEntity>(Sql.ToString(),new { userId });
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public Task<IEnumerable<StampEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var dp = new DynamicParameters(new { });
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Stamp s  Where 1=1 ");
            if (queryParam.HasValues)
            {
                if (!queryParam["F_StampName"].IsEmpty())
                {
                    dp.Add("F_StampName", "%" + queryParam["F_StampName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.F_StampName like @F_StampName ");
                }
                if (!queryParam["F_EnabledMark"].IsEmpty())
                {
                    dp.Add("F_EnabledMark", queryParam["F_EnabledMark"].ToString(), DbType.String);
                    strSql.Append(" AND s.F_EnabledMark=@F_EnabledMark");
                }
                if (!queryParam["F_StampType"].IsEmpty())
                {
                    dp.Add("F_StampType", queryParam["F_StampType"].ToString(), DbType.String);
                    strSql.Append(" AND s.F_StampType = @F_StampType");
                }
            }
            return this.BaseRepository().FindList<StampEntity>(strSql.ToString(), dp, pagination);
        }

        /// <summary>
        /// 获取印章实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<StampEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<StampEntity>(keyValue);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 保存印章信息（新增/编辑）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public async Task SaveEntity(string keyValue, StampEntity entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.F_StampId = Guid.NewGuid().ToString(); //产生印章编号
                entity.F_EnabledMark = 1;//默认状态为启用
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                entity.F_StampId = keyValue;
                await this.BaseRepository().Update(entity);
            }
        }

        /// <summary>
        /// 删除印章信息
        /// </summary>
        /// <param name="keyVlaue">主键</param>
        public async Task DeleteEntity(string keyVlaue)
        {
            await this.BaseRepository().DeleteAny<StampEntity>(new { F_StampId = keyVlaue });//删除操作
        }
        #endregion
    }

}
