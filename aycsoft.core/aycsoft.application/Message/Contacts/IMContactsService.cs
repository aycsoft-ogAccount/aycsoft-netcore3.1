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
    /// 日 期：2022.11.05
    /// 描 述：最近联系人列表
    /// </summary>
    public class IMContactsService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public IMContactsService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_MyUserId,
                t.F_OtherUserId,
                t.F_Content,
                t.F_Time,
                t.F_IsRead
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public Task<IEnumerable<IMContactsEntity>> GetList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_Contacts t where  t.F_MyUserId = @userId Order By t.F_Time Desc ");
            return this.BaseRepository().FindList<IMContactsEntity>(strSql.ToString(), new { userId });
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public Task<IEnumerable<IMContactsEntity>> GetList(string userId, DateTime time)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_Contacts t where  t.F_MyUserId = @userId AND t.F_Time >= @time  Order By t.F_Time Asc ");
            return this.BaseRepository().FindList<IMContactsEntity>(strSql.ToString(), new { userId, time });
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="userId">发送人</param>
        /// <param name="otherUserId">接收人</param>
        /// <returns></returns>
        public Task<IMContactsEntity> GetEntity(string userId, string otherUserId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_Contacts t where  t.F_MyUserId = @userId AND t.F_OtherUserId = @otherUserId ");
            return this.BaseRepository().FindEntity<IMContactsEntity>(strSql.ToString(), new { userId, otherUserId });
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(IMContactsEntity entity)
        {
            var entityTmp = await GetEntity(entity.F_MyUserId, entity.F_OtherUserId);
            entity.F_IsRead = 2;
            if (entityTmp == null)
            {
                entity.F_Id = Guid.NewGuid().ToString();
                entity.F_Time = DateTime.Now;
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                entity.F_Id = entityTmp.F_Id;
                entity.F_Time = DateTime.Now;
                await this.BaseRepository().Update(entity);
            }
        }

        /// <summary>
        /// 更新记录读取状态
        /// </summary>
        /// <param name="myUserId">自己本身用户ID</param>
        /// <param name="otherUserId">对方用户ID</param>
        public async Task UpdateState(string myUserId, string otherUserId)
        {
            var entity = await GetEntity(myUserId, otherUserId);
            if (entity != null)
            {
                entity.F_IsRead = 2;
                await this.BaseRepository().Update(entity);
            }
        }

        /// <summary>
        /// 删除最近联系人
        /// </summary>
        /// <param name="myUserId">发起者id</param>
        /// <param name="otherUserId">发给人ID</param>
        public async Task DeleteEntity(string myUserId, string otherUserId)
        {
            await this.BaseRepository().DeleteAny<IMContactsEntity>(new { F_MyUserId = myUserId, F_OtherUserId = otherUserId });
        }
        #endregion
    }
}
