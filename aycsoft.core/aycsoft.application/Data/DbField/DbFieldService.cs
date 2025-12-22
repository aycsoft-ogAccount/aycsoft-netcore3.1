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
    /// 描 述：常用字段
    /// </summary>
    public class DbFieldService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public DbFieldService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_DataType,
                t.F_Length,
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
        /// <returns></returns>
        public Task<IEnumerable<DbFieldEntity>> GetList(string queryJson)
        {
            //参考写法
            var queryParam = queryJson.ToJObject();
            // 虚拟参数
            //var dp = new DynamicParameters(new { });
            //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_DbField t where 1=1 ");

            string keyword = "";
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" and ( t.F_Name like @keyword or  t.F_Remark like @keyword ) ");
            }


            return this.BaseRepository().FindList<DbFieldEntity>(strSql.ToString(), new { keyword });
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<DbFieldEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();

            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_DbField t where 1=1 ");

            string keyword = "";
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" and ( t.F_Name like @keyword or  t.F_Remark like @keyword ) ");
            }

            return this.BaseRepository().FindList<DbFieldEntity>(strSql.ToString(), new { keyword }, pagination);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DbFieldEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<DbFieldEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<DbFieldEntity>(new { F_Id = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, DbFieldEntity entity)
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
