using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aycsoft.database;
using aycsoft.iapplication;
using aycsoft.util;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.14
    /// 描 述：看板配置信息
    /// </summary>
    public class LR_KBConfigInfoService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public LR_KBConfigInfoService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_KanBanId,
                t.F_ModeName,
                t.F_Type,
                t.F_TopValue,
                t.F_LeftValue,
                t.F_WidthValue,
                t.F_HightValue,
                t.F_SortCode,
                t.F_RefreshTime,
                t.F_Configuration
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBConfigInfoEntity>> GetList(string queryJson)
        {
            //参考写法
            //var queryParam = queryJson.ToJObject();
            // 虚拟参数
            //var dp = new DynamicParameters(new { });
            //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_KBConfigInfo t ");
            return this.BaseRepository().FindList<LR_KBConfigInfoEntity>(strSql.ToString());
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBConfigInfoEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_KBConfigInfo t ");
            return this.BaseRepository().FindList<LR_KBConfigInfoEntity>(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<LR_KBConfigInfoEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<LR_KBConfigInfoEntity>(keyValue);
        }
        /// <summary>
        /// 根据看板id获取所有配置
        /// </summary>
        /// <param name="keyValue">看板id</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBConfigInfoEntity>> GetListByKBId(string keyValue)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_KBConfigInfo t where t.F_KanBanId='" + keyValue + "' order by t.F_SortCode asc ");
            return this.BaseRepository().FindList<LR_KBConfigInfoEntity>(strSql.ToString());
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
            await this.BaseRepository().DeleteAny<LR_KBConfigInfoEntity>(new { F_Id = keyValue });
        }
        /// <summary>
        /// 根据看板id删除其所有配置信息
        /// </summary>
        /// <param name="keyValue">看板id</param>
        /// <returns></returns>
        public async Task DeleteByKBId(string keyValue)
        {
            await this.BaseRepository().DeleteAny<LR_KBConfigInfoEntity>(new { F_KanBanId = keyValue });
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, LR_KBConfigInfoEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
                await this.BaseRepository().Update(entity);
            }
            else
            {
                entity.F_Id = Guid.NewGuid().ToString();
                await this.BaseRepository().Insert(entity);
            }
        }

        #endregion

    }
}
