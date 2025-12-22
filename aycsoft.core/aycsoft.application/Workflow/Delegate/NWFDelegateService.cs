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
    /// 日 期：2020.02.10
    /// 描 述：流程委托
    /// </summary>
    public class NWFDelegateService: ServiceBase
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字(被委托人)</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFDelegateRuleEntity>> GetPageList(Pagination pagination, string keyword, UserEntity userInfo)
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from lr_nwf_delegaterule where 1=1");
            if (userInfo.F_SecurityLevel == 1)
            {
                strSql.Append(" AND  F_CreateUserId = @userId");
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND  F_ToUserName like @keyword");
            }
            return this.BaseRepository().FindList<NWFDelegateRuleEntity>(strSql.ToString(),new { keyword, userId = userInfo.F_UserId }, pagination);
        }
        /// <summary>
        /// 根据委托人获取委托记录
        /// </summary>
        /// <param name="userInfo">当前登录者信息</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFDelegateRuleEntity>> GetList(UserEntity userInfo)
        {
            string userId = userInfo.F_UserId;
            DateTime datetime = DateTime.Now;
            var strSql = new StringBuilder();
            strSql.Append("select * from lr_nwf_delegaterule where F_ToUserId = @userId AND F_BeginDate >= @datetime AND F_EndDate <= @datetime ");

            return this.BaseRepository().FindList<NWFDelegateRuleEntity>(strSql.ToString(),new { userId , datetime });
        }
        /// <summary>
        /// 获取关联的模板数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<NWFDelegateRelationEntity>> GetRelationList(string keyValue)
        {
            var strSql = new StringBuilder();
            strSql.Append("select * from lr_nwf_delegaterelation where F_DelegateRuleId = @keyValue");

            return this.BaseRepository().FindList<NWFDelegateRelationEntity>(strSql.ToString(), new { keyValue });
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<NWFDelegateRuleEntity>(new { F_Id = keyValue });
                await db.DeleteAny<NWFDelegateRelationEntity>(new { F_DelegateRuleId = keyValue });
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="wfDelegateRuleEntity">实体数据</param>
        /// <param name="schemeInfoList">关联模板主键</param>
        public async Task SaveEntity(string keyValue, NWFDelegateRuleEntity wfDelegateRuleEntity, string[] schemeInfoList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    wfDelegateRuleEntity.F_Id = Guid.NewGuid().ToString();
                    wfDelegateRuleEntity.F_CreateDate = DateTime.Now;
                    wfDelegateRuleEntity.F_EnabledMark = 1;
                    wfDelegateRuleEntity.F_CreateUserId = this.GetUserId();
                    wfDelegateRuleEntity.F_CreateUserName = this.GetUserName();

                    await db.Insert(wfDelegateRuleEntity);
                }
                else
                {
                    wfDelegateRuleEntity.F_Id = keyValue;
                    await db.Update(wfDelegateRuleEntity);
                    await db.DeleteAny<NWFDelegateRelationEntity>(new { F_DelegateRuleId = keyValue });
                }

                foreach (string schemeInfoId in schemeInfoList)
                {
                    NWFDelegateRelationEntity wfDelegateRuleRelationEntity = new NWFDelegateRelationEntity();
                    wfDelegateRuleRelationEntity.F_Id = Guid.NewGuid().ToString();
                    wfDelegateRuleRelationEntity.F_DelegateRuleId = wfDelegateRuleEntity.F_Id;
                    wfDelegateRuleRelationEntity.F_SchemeInfoId = schemeInfoId;
                    await db.Insert(wfDelegateRuleRelationEntity);
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
        /// 更新委托规则状态信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state"></param>
        public async Task UpdateState(string keyValue, int state)
        {
            NWFDelegateRuleEntity wfDelegateRuleEntity = new NWFDelegateRuleEntity
            {
                F_Id = keyValue,
                F_EnabledMark = state
            };
            await this.BaseRepository().Update(wfDelegateRuleEntity);
        }
        #endregion
    }
}
