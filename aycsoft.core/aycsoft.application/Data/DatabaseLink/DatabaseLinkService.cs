using aycsoft.database;
using aycsoft.iapplication;
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
    /// 日 期：2022.09.19
    /// 描 述：数据库连接
    /// </summary>
    public class DatabaseLinkService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public DatabaseLinkService()
        {
            fieldSql = @"
                    t.F_DatabaseLinkId,
                    t.F_ServerAddress,
                    t.F_DBName,
                    t.F_DBAlias,
                    t.F_DbType,
                    t.F_DbConnection,
                    t.F_DESEncrypt,
                    t.F_SortCode,
                    t.F_DeleteMark,
                    t.F_EnabledMark,
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
        /// 获取数据库信息列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DatabaseLinkEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_DatabaseLink t WHERE  t.F_DeleteMark = 0 ");
            return this.BaseRepository().FindList<DatabaseLinkEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">键值</param>
        /// <returns></returns>
        public Task<DatabaseLinkEntity> GetEntity(string keyValue)
        {

            return this.BaseRepository().FindEntityByKey<DatabaseLinkEntity>(keyValue);
        }


        #endregion

        #region 提交数据
        /// <summary>
        /// 删除自定义查询条件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            var entity = await GetEntity(keyValue);
            await this.BaseRepository().DeleteAny<DatabaseLinkEntity>(new { F_DatabaseLinkId = keyValue });
            DbCaChe.RemoveValue(entity.F_DBName);
        }
        /// <summary>
        /// 保存自定义查询（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="databaseLinkEntity">数据库实体</param>
        /// <returns></returns>
        public async Task<bool> SaveEntity(string keyValue, DatabaseLinkEntity databaseLinkEntity)
        {
            /*测试数据库连接串"******";*/
            if (!string.IsNullOrEmpty(keyValue) && databaseLinkEntity.F_DbConnection == "******")
            {
                databaseLinkEntity.F_DbConnection = null;// 不更新连接串地址
            }
            else
            {
                try
                {
                    databaseLinkEntity.F_ServerAddress = this.BaseRepository(databaseLinkEntity.F_DbConnection, databaseLinkEntity.F_DbType).GetDataSource();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(keyValue))
            {
                databaseLinkEntity.F_DatabaseLinkId = keyValue;
                databaseLinkEntity.F_ModifyDate = DateTime.Now;
                databaseLinkEntity.F_ModifyUserId = this.GetUserId();
                databaseLinkEntity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(databaseLinkEntity);
            }
            else
            {
                databaseLinkEntity.F_DatabaseLinkId = Guid.NewGuid().ToString();
                databaseLinkEntity.F_CreateDate = DateTime.Now;
                databaseLinkEntity.F_DeleteMark = 0;
                databaseLinkEntity.F_EnabledMark = 1;
                databaseLinkEntity.F_CreateUserId = this.GetUserId();
                databaseLinkEntity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(databaseLinkEntity);
            }
            DbCaChe.RemoveValue(databaseLinkEntity.F_DBName);
            return true;
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 测试数据数据库是否能连接成功
        /// </summary>
        /// <param name="connection">连接串</param>
        /// <param name="dbType">数据库类型</param>
        public string TestConnection(string connection, string dbType)
        {
            try
            {
                var db = this.BaseRepository(connection, dbType).BeginTrans();
                db.Commit();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
