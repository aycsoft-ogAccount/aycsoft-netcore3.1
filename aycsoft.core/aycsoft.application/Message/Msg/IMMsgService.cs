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
    /// 日 期：2022.11.05
    /// 描 述：即时通讯消息内容
    /// </summary>
    public class IMMsgService : ServiceBase
    {

        private readonly IMContactsService iMContactsService = new IMContactsService();

        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public IMMsgService()
        {
            fieldSql = @"
                t.F_MsgId,
                t.F_IsSystem,
                t.F_SendUserId,
                t.F_RecvUserId,
                t.F_Content,
                t.F_CreateDate
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据(最近的10条聊天记录)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IMMsgEntity>> GetList(string sendUserId, string recvUserId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_Msg t where  (t.F_SendUserId = @sendUserId and  t.F_RecvUserId = @recvUserId ) or  (t.F_SendUserId = @recvUserId and  t.F_RecvUserId = @sendUserId ) ");

            Pagination pagination = new Pagination
            {
                page = 1,
                rows = 10,
                sidx = "F_CreateDate",
                sord = "DESC"
            };

            await this.BaseRepository().ExecuteSql(" Update LR_IM_Contacts Set F_ISREAD = 2 where  F_MyUserId = @sendUserId AND  F_OtherUserId =  @recvUserId  ", new { sendUserId, recvUserId });

            return await this.BaseRepository().FindList<IMMsgEntity>(strSql.ToString(), new { sendUserId, recvUserId }, pagination);
        }


        /// <summary>
        /// 获取列表数据(小于某个时间点的5条记录)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public Task<IEnumerable<IMMsgEntity>> GetListByTime(string myUserId, string otherUserId, DateTime time)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_Msg t where  ((t.F_SendUserId = @myUserId and  t.F_RecvUserId = @otherUserId ) or  (t.F_SendUserId = @otherUserId and  t.F_RecvUserId = @myUserId )) AND t.F_CreateDate <= @time ");

            Pagination pagination = new Pagination();
            pagination.page = 1;
            pagination.rows = 5;
            pagination.sidx = "F_CreateDate";
            pagination.sord = "DESC";

            return this.BaseRepository().FindList<IMMsgEntity>(strSql.ToString(), new { myUserId, otherUserId, time }, pagination);
        }
        /// <summary>
        /// 获取列表数据(大于某个时间的所有数据)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public Task<IEnumerable<IMMsgEntity>> GetListByTime2(string myUserId, string otherUserId, DateTime time)
        {
            time = time.AddSeconds(1);
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_Msg t where  ((t.F_SendUserId = @myUserId and  t.F_RecvUserId = @otherUserId ) or  (t.F_SendUserId = @otherUserId and  t.F_RecvUserId = @myUserId )) AND t.F_CreateDate >= @time Order By  F_CreateDate ASC ");

            return this.BaseRepository().FindList<IMMsgEntity>(strSql.ToString(), new { myUserId, otherUserId, time });
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="sendUserId"></param>
        /// <param name="recvUserId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Task<IEnumerable<IMMsgEntity>> GetPageList(Pagination pagination, string sendUserId, string recvUserId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_Msg t where  ((t.F_SendUserId = @sendUserId and  t.F_RecvUserId = @recvUserId ) or  (t.F_SendUserId = @recvUserId and  t.F_RecvUserId = @sendUserId )) ");

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND F_Content like @keyword ");
            }

            return this.BaseRepository().FindList<IMMsgEntity>(strSql.ToString(), new { sendUserId, recvUserId, keyword }, pagination);
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
            await this.BaseRepository().DeleteAny<IMMsgEntity>(new { F_MsgId = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增）
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task SaveEntity(IMMsgEntity entity)
        {

            IMContactsEntity myContacts = new IMContactsEntity();
            IMContactsEntity otherContacts = new IMContactsEntity();

            myContacts.F_MyUserId = entity.F_SendUserId;
            myContacts.F_OtherUserId = entity.F_RecvUserId;
            myContacts.F_Content = entity.F_Content;

            otherContacts.F_MyUserId = entity.F_RecvUserId;
            otherContacts.F_OtherUserId = entity.F_SendUserId;
            otherContacts.F_Content = entity.F_Content;

            var myContactsTmp = await iMContactsService.GetEntity(myContacts.F_MyUserId, myContacts.F_OtherUserId);
            var otherContactsTmp = await iMContactsService.GetEntity(otherContacts.F_MyUserId, otherContacts.F_OtherUserId);

            var db = this.BaseRepository().BeginTrans();
            try
            {
                myContacts.F_IsRead = 2;
                myContacts.F_Time = DateTime.Now;
                if (myContactsTmp == null)
                {
                    myContacts.F_Id = Guid.NewGuid().ToString();
                    await db.Insert(myContacts);
                }
                else
                {
                    myContacts.F_Id = myContactsTmp.F_Id;
                    await db.Update(myContacts);
                }

                otherContacts.F_IsRead = 1;
                otherContacts.F_Time = DateTime.Now;
                if (otherContactsTmp == null)
                {
                    otherContacts.F_Id = Guid.NewGuid().ToString();
                    await db.Insert(otherContacts);
                }
                else
                {
                    otherContacts.F_Id = otherContactsTmp.F_Id;
                    await db.Update(otherContacts);
                }


                entity.F_MsgId = Guid.NewGuid().ToString();
                entity.F_CreateDate = DateTime.Now;

                await db.Insert(entity);

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
