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
    /// 日 期：2022.09.24
    /// 描 述：编号规则
    /// </summary>
    public class CodeRuleService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public CodeRuleService()
        {
            fieldSql = @"
                    t.F_RuleId,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_CurrentNumber,
                    t.F_RuleFormatJson,
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
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<CodeRuleEntity>> GetPageList(Pagination pagination, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_CodeRule t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ");
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" AND ( F_EnCode LIKE @keyword OR F_FullName LIKE @keyword )  ");
                keyword = '%' + keyword + '%';
            }
            return this.BaseRepository().FindList<CodeRuleEntity>(strSql.ToString(), new { keyword }, pagination);
        }
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<CodeRuleEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_CodeRule t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ");
            return this.BaseRepository().FindList<CodeRuleEntity>(strSql.ToString());
        }
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<CodeRuleEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<CodeRuleEntity>(keyValue);
        }
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="enCode">规则编码</param>
        /// <returns></returns>
        public Task<CodeRuleEntity> GetEntityByCode(string enCode)
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_CodeRule t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 AND F_EnCode = @enCode");

            return this.BaseRepository().FindEntity<CodeRuleEntity>(strSql.ToString(), new { enCode });
        }
        /// <summary>
        /// 获取种子
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public Task<CodeRuleSeedEntity> GetSeedEntity(string ruleId)
        {
            return this.BaseRepository().FindEntity<CodeRuleSeedEntity>(" select * from lr_base_coderuleseed where  F_RuleId = @ruleId", new { ruleId });
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<CodeRuleEntity>(new { F_RuleId = keyValue });
                await db.DeleteAny<CodeRuleSeedEntity>(new { F_RuleId = keyValue });
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存规则表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="codeRuleEntity">规则实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, CodeRuleEntity codeRuleEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                codeRuleEntity.F_RuleId = keyValue;
                codeRuleEntity.F_ModifyDate = DateTime.Now;
                codeRuleEntity.F_ModifyUserId = this.GetUserId();
                codeRuleEntity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(codeRuleEntity);
            }
            else
            {
                codeRuleEntity.F_RuleId = Guid.NewGuid().ToString();
                codeRuleEntity.F_CreateDate = DateTime.Now;
                codeRuleEntity.F_DeleteMark = 0;
                codeRuleEntity.F_EnabledMark = 1;
                codeRuleEntity.F_CreateUserId = this.GetUserId();
                codeRuleEntity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(codeRuleEntity);
            }
        }
        #endregion

        #region 单据编码处理
        /// <summary>
        /// 获取当前编号规则种子列表
        /// </summary>
        /// <param name="ruleId">编号规则主键</param>
        /// <param name="userInfo">当前登录者信息</param>
        /// <returns></returns>
        public async Task<IEnumerable<CodeRuleSeedEntity>> GetSeedList(string ruleId, UserEntity userInfo)
        {
            //获取当前最大种子
            List<CodeRuleSeedEntity> codeRuleSeedList = (List<CodeRuleSeedEntity>)await this.BaseRepository().FindList<CodeRuleSeedEntity>(" select * from lr_base_coderuleseed where  F_RuleId = @ruleId", new { ruleId });
            if (codeRuleSeedList.Count == 0)
            {
                //说明没有种子，插入一条种子
                CodeRuleSeedEntity codeRuleSeedEntity = new CodeRuleSeedEntity();
                codeRuleSeedEntity.F_RuleSeedId = Guid.NewGuid().ToString();
                codeRuleSeedEntity.F_CreateDate = DateTime.Now;
                codeRuleSeedEntity.F_ModifyDate = DateTime.Now;
                codeRuleSeedEntity.F_CreateUserId = userInfo.F_UserId;
                codeRuleSeedEntity.F_CreateUserName = userInfo.F_RealName;
                codeRuleSeedEntity.F_SeedValue = 1;
                codeRuleSeedEntity.F_RuleId = ruleId;
                await this.BaseRepository().Insert<CodeRuleSeedEntity>(codeRuleSeedEntity);
                codeRuleSeedList.Add(codeRuleSeedEntity);
            }
            return codeRuleSeedList;
        }
        /// <summary>
        /// 保存单据编号规则种子
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="codeRuleSeedEntity">种子实体</param>
        /// <param name="userInfo">当前登录者信息</param>
        public async Task SaveSeed(string keyValue, CodeRuleSeedEntity codeRuleSeedEntity, UserEntity userInfo)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                codeRuleSeedEntity.F_RuleSeedId = Guid.NewGuid().ToString();
                codeRuleSeedEntity.F_CreateDate = DateTime.Now;
                codeRuleSeedEntity.F_ModifyDate = DateTime.Now;
                codeRuleSeedEntity.F_CreateUserId = userInfo.F_UserId;
                codeRuleSeedEntity.F_CreateUserName = userInfo.F_RealName;
                await this.BaseRepository().Insert(codeRuleSeedEntity);
            }
            else
            {
                codeRuleSeedEntity.F_RuleSeedId = keyValue;
                codeRuleSeedEntity.F_ModifyDate = DateTime.Now;
                codeRuleSeedEntity.F_ModifyUserId = userInfo.F_UserId;
                codeRuleSeedEntity.F_ModifyUserName = userInfo.F_RealName;
                await this.BaseRepository().Update(codeRuleSeedEntity);
            }
        }
        /// <summary>
        /// 删除种子，表示被占用了
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="ruleId">规则主键</param>
        public async Task DeleteSeed(string userId, string ruleId)
        {
            await this.BaseRepository().DeleteAny<CodeRuleSeedEntity>(new { F_UserId = userId, F_RuleId = ruleId });
        }
        #endregion
    }
}
