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
    /// 日 期：2022.10.23
    /// 描 述：数据表草稿
    /// </summary>
    public class DbDraftService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public DbDraftService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_Content,
                t.F_Remark,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<DbDraftEntity>> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_DbDraft t where 1=1 ");

            string keyword = "";
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"] + "%";
                strSql.Append(" and ( t.F_Name like @keyword or  t.F_Remark like @keyword ) ");
            }

            return this.BaseRepository().FindList<DbDraftEntity>(strSql.ToString(), new { keyword = keyword });
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DbDraftEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<DbDraftEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<DbDraftEntity>(new { F_Id = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, DbDraftEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
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
