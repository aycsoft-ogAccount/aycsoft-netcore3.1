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
    /// 日 期：2022.11.14
    /// 描 述：看板信息
    /// </summary>
    public class LR_KBKanBanInfoService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public LR_KBKanBanInfoService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_KanBanName,
                t.F_KanBanCode,
                t.F_RefreshTime,
                t.F_TemplateId,
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
        /// <param name="queryJson">请求参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBKanBanInfoEntity>> GetList(string queryJson)
        {
            //参考写法
            //var queryParam = queryJson.ToJObject();
            // 虚拟参数
            //var dp = new DynamicParameters(new { });
            //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_KBKanBanInfo t ");
            return this.BaseRepository().FindList<LR_KBKanBanInfoEntity>(strSql.ToString());
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">请求参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBKanBanInfoEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_KBKanBanInfo t ");
            return this.BaseRepository().FindList<LR_KBKanBanInfoEntity>(strSql.ToString(), pagination);

        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<LR_KBKanBanInfoEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<LR_KBKanBanInfoEntity>(keyValue);
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
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<LR_KBKanBanInfoEntity>(new { F_Id = keyValue });
                await db.DeleteAny<LR_KBConfigInfoEntity>(new { F_KanBanId = keyValue });
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="kanbaninfo">看板信息</param>
        /// <param name="kbconfigInfo">看板配置信息</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, string kanbaninfo, string kbconfigInfo)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                LR_KBKanBanInfoEntity lR_KBKanBanInfoEntity = kanbaninfo.ToObject<LR_KBKanBanInfoEntity>();
                List<LR_KBConfigInfoEntity> list = kbconfigInfo.ToObject<List<LR_KBConfigInfoEntity>>();
                /////新增编辑看板信息
                if (string.IsNullOrEmpty(keyValue))
                {
                    lR_KBKanBanInfoEntity.F_Id = Guid.NewGuid().ToString();
                    lR_KBKanBanInfoEntity.F_CreateDate = DateTime.Now;
                    lR_KBKanBanInfoEntity.F_CreateUserId = this.GetUserId();
                    lR_KBKanBanInfoEntity.F_CreateUserName = this.GetUserName();

                    await db.Insert(lR_KBKanBanInfoEntity);
                }
                else
                {
                    lR_KBKanBanInfoEntity.F_Id = keyValue;
                    lR_KBKanBanInfoEntity.F_ModifyDate = DateTime.Now;
                    lR_KBKanBanInfoEntity.F_ModifyUserId = this.GetUserId();
                    lR_KBKanBanInfoEntity.F_ModifyUserName = this.GetUserName();

                    await db.Update(lR_KBKanBanInfoEntity);
                    await db.DeleteAny<LR_KBConfigInfoEntity>(new { F_KanBanId = keyValue });
                }

                //处理看板配置信息
                foreach (var item in list)
                {
                    item.F_Id = Guid.NewGuid().ToString();
                    item.F_KanBanId = lR_KBKanBanInfoEntity.F_Id;
                    await db.Insert(item);
                }
                db.Commit();//事务提交
            }
            catch (Exception)
            {
                db.Rollback();//事务回滚
                throw;
            }
        }
        #endregion

    }
}
