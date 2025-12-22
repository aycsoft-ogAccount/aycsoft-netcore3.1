using aycsoft.database;
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
    /// 日 期：2022.09.19
    /// 描 述：数据字典管理服务类
    /// </summary>
    public class DataItemService : ServiceBase
    {
        #region 属性 构造函数
        private readonly string fieldSql;
        private readonly string detailFieldSql;
        /// <summary>
        /// 
        /// </summary>
        public DataItemService()
        {
            fieldSql = @" 
                    t.F_ItemId,
                    t.F_ParentId,
                    t.F_ItemCode,
                    t.F_ItemName,
                    t.F_IsTree,
                    t.F_IsNav,
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
            detailFieldSql = @"
                    t.F_ItemDetailId,
                    t.F_ItemId,
                    t.F_ParentId,
                    t.F_ItemCode,
                    t.F_ItemName,
                    t.F_ItemValue,
                    t.F_QuickQuery,
                    t.F_SimpleSpelling,
                    t.F_IsDefault,
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

        #region 数据字典分类管理
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DataItemEntity>> GetClassifyList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + fieldSql + " FROM LR_Base_DataItem t WHERE t.F_DeleteMark = 0 Order By t.F_ParentId,t.F_SortCode ");
            return this.BaseRepository().FindList<DataItemEntity>(strSql.ToString());
        }

        /// <summary>
        /// 通过编号获取字典分类实体
        /// </summary>
        /// <param name="itemCode">编码</param>
        /// <returns></returns>
        public Task<DataItemEntity> GetClassifyEntityByCode(string itemCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + fieldSql + " FROM LR_Base_DataItem t WHERE t.F_DeleteMark = 0 AND F_ItemCode =@itemCode ");
            return this.BaseRepository().FindEntity<DataItemEntity>(strSql.ToString(), new { itemCode });
        }

        /// <summary>
        /// 通过编号获取字典分类实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DataItemEntity> GetClassifyEntityByKey(string keyValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + fieldSql + " FROM LR_Base_DataItem t WHERE t.F_DeleteMark = 0 AND F_ItemId =@keyValue ");
            return this.BaseRepository().FindEntity<DataItemEntity>(strSql.ToString(), new { keyValue });
        }

        /// <summary>
        /// 删除分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteClassify(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<DataItemEntity>(new { F_ItemId = keyValue });
                await db.DeleteAny<DataItemDetailEntity>(new { F_ItemId = keyValue });
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存分类数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public async Task SaveClassifyEntity(string keyValue, DataItemEntity entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.F_ItemId = Guid.NewGuid().ToString();
                entity.F_CreateDate = DateTime.Now;
                //entity.F_EnabledMark = 1;
                entity.F_DeleteMark = 0;
                entity.F_CreateUserId = this.GetUserId();
                entity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                entity.F_ItemId = keyValue;
                entity.F_ModifyDate = DateTime.Now;
                entity.F_ModifyUserId = this.GetUserId();
                entity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(entity);
            }
        }
        #endregion

        #region 数据字典明细
        /// <summary>
        /// 获取数据字典明显根据分类编号
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <returns></returns>
        public Task<IEnumerable<DataItemDetailEntity>> GetDetailList(string itemCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + detailFieldSql + @" FROM LR_Base_DataItemDetail t
                            INNER JOIN LR_Base_DataItem t2 ON t.F_ItemId = t2.F_ItemId
                            WHERE t2.F_ItemCode = @itemCode AND t.F_DeleteMark = 0  Order By t.F_SortCode
                           ");
            return this.BaseRepository().FindList<DataItemDetailEntity>(strSql.ToString(), new { itemCode });
        }
        /// <summary>
        /// 获取数据字典明细实体类
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DataItemDetailEntity> GetDetailEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<DataItemDetailEntity>(keyValue);
        }
        /// <summary>
        /// 删除明细数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteDetail(string keyValue)
        {
            await this.BaseRepository().DeleteAny<DataItemDetailEntity>(new { F_ItemDetailId = keyValue });
        }
        /// <summary>
        /// 保存明细数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public async Task SaveDetailEntity(string keyValue, DataItemDetailEntity entity)
        {
            entity.F_QuickQuery = Str.ConvertPinYin(entity.F_ItemName).ToUpper();
            entity.F_SimpleSpelling = Str.PinYin(entity.F_ItemName);
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.F_ItemDetailId = Guid.NewGuid().ToString();
                entity.F_CreateDate = DateTime.Now;
                entity.F_DeleteMark = 0;
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                entity.F_ItemDetailId = keyValue;
                entity.F_ModifyDate = DateTime.Now;
                await this.BaseRepository().Update(entity);
            }
        }
        #endregion
    }
}
