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
    /// 日 期：2022.10.22
    /// 描 述：语言类型
    /// </summary>
    public class LGTypeService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public LGTypeService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_Code,
                t.F_IsMain
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取语言类型全部数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<LGTypeEntity>> GetAllData()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Lg_Type");
            return this.BaseRepository().FindList<LGTypeEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<LGTypeEntity>> GetList(string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Lg_Type t WHERE 1=1 ");
            //参考写法
            var queryParam = queryJson.ToJObject();
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            if (queryParam.Property("keyword") != null && queryParam.Property("keyword").ToString() != "")
            {
                dp.Add("F_Name", "%" + queryParam["keyword"] + "%", DbType.String);
                strSql.Append(" AND t.F_Name like @F_Name ");
            }
            strSql.Append(" ORDER BY t.F_IsMain DESC ");
            return this.BaseRepository().FindList<LGTypeEntity>(strSql.ToString(), dp);
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<LGTypeEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<LGTypeEntity>(keyValue);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public Task<LGTypeEntity> GetEntityByCode(string code)
        {

            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Lg_Type where F_Code =@code ");
            return this.BaseRepository().FindEntity<LGTypeEntity>(strSql.ToString(), new { code });
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
            var entity = await this.BaseRepository().FindEntityByKey<LGTypeEntity>(keyValue);
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<LGMapEntity>(new { F_TypeCode = entity.F_Code });
                await db.DeleteAny<LGTypeEntity>(new { F_Id = keyValue });

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
            }
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, LGTypeEntity entity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //更改映照表对应字段
                    var oldentity = await GetEntity(keyValue);
                    var sql = "UPDATE LR_Lg_Map SET F_TypeCode='" + entity.F_Code + "' WHERE F_TypeCode='" + oldentity.F_Code + "'";
                    await db.ExecuteSql(sql);
                    entity.F_Id = keyValue;
                    await db.Update(entity);
                }
                else
                {
                    entity.F_Id = Guid.NewGuid().ToString();
                    await db.Insert(entity);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 设为主语言
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public async Task SetMainLG(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                string sql = "UPDATE LR_Lg_Type SET F_IsMain=0 WHERE F_IsMain=1";
                await db.ExecuteSql(sql);
                string sql1 = "UPDATE LR_Lg_Type SET F_IsMain=1 WHERE F_Id='" + keyValue + "'";
                await db.ExecuteSql(sql1);
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion

    }
}
