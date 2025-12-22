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
    /// 日 期：2022.11.22
    /// 描 述：表单模板
    /// </summary>
    public class FormSchemeService : ServiceBase
    {
        #region 属性 构造函数
        private readonly string schemeInfoFieldSql;
        private readonly string schemeFieldSql;
        /// <summary>
        /// 
        /// </summary>
        public FormSchemeService()
        {
            schemeInfoFieldSql = @" 
                        t.F_Id,
                        t.F_Name,
                        t.F_Category,
                        t.F_SchemeId,
                        t.F_EnabledMark,
                        t.F_Description
            ";

            schemeFieldSql = @" 
                        t.F_Id,
                        t.F_SchemeInfoId,
                        t.F_Type,
                        t.F_CreateDate,
                        t.F_CreateUserId,
                        t.F_CreateUserName
                        ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取自定义表单列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<FormSchemeInfoEntity>> GetCustmerSchemeInfoList()
        {
            return this.BaseRepository().FindAll<FormSchemeInfoEntity>();
        }

        /// <summary>
        /// 获取表单分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        public Task<IEnumerable<FormSchemeInfoEntity>> GetSchemeInfoPageList(Pagination pagination, string keyword, string category)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(schemeInfoFieldSql);
            strSql.Append(",t1.F_Type,t1.F_CreateDate,t1.F_CreateUserId,t1.F_CreateUserName ");
            strSql.Append(" FROM LR_Form_SchemeInfo t LEFT JOIN LR_Form_Scheme t1 ON t.F_SchemeId = t1.F_Id WHERE 1=1 ");

            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" AND t.F_Name like @keyword  ");
                keyword = "%" + keyword + "%";
            }
            if (!string.IsNullOrEmpty(category))
            {
                strSql.Append(" AND t.F_Category = @category ");
            }

            return this.BaseRepository().FindList<FormSchemeInfoEntity>(strSql.ToString(), new { keyword, category }, pagination);
        }
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public Task<IEnumerable<FormSchemeEntity>> GetSchemePageList(Pagination pagination, string schemeInfoId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(schemeFieldSql);
            strSql.Append(" FROM LR_Form_Scheme t WHERE 1=1 ");
            strSql.Append(" AND t.F_SchemeInfoId = @schemeInfoId ");

            return this.BaseRepository().FindList<FormSchemeEntity>(strSql.ToString(), new { schemeInfoId }, pagination);
        }
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FormSchemeInfoEntity> GetSchemeInfoEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<FormSchemeInfoEntity>(keyValue);
        }
        /// <summary>
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FormSchemeEntity> GetSchemeEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<FormSchemeEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();

            try
            {
                await db.DeleteAny<FormSchemeInfoEntity>(new { F_Id = keyValue });
                await db.DeleteAny<FormSchemeEntity>(new { F_SchemeInfoId = keyValue });
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        public async Task SaveEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity, FormSchemeEntity schemeEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    schemeInfoEntity.F_Id = Guid.NewGuid().ToString();
                }
                else
                {
                    schemeInfoEntity.F_Id = keyValue;
                }

                #region 模板信息
                if (schemeEntity != null)
                {
                    schemeEntity.F_SchemeInfoId = schemeInfoEntity.F_Id;
                    schemeEntity.F_Id = Guid.NewGuid().ToString();
                    schemeEntity.F_CreateDate = DateTime.Now;
                    schemeEntity.F_CreateUserId = this.GetUserId();
                    schemeEntity.F_CreateUserName = this.GetUserName();

                    await db.Insert(schemeEntity);
                    schemeInfoEntity.F_SchemeId = schemeEntity.F_Id;
                }
                #endregion

                #region 模板基础信息
                if (!string.IsNullOrEmpty(keyValue))
                {
                    await db.Update(schemeInfoEntity);
                }
                else
                {
                    await db.Insert(schemeInfoEntity);
                }
                #endregion

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存模板基础信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        public async Task SaveSchemeInfoEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                schemeInfoEntity.F_Id = keyValue;
                await this.BaseRepository().Update(schemeInfoEntity);
            }
            else
            {
                schemeInfoEntity.F_Id = Guid.NewGuid().ToString();
                await this.BaseRepository().Insert(schemeInfoEntity);
            }
        }
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        public async Task UpdateScheme(string schemeInfoId, string schemeId)
        {
            FormSchemeEntity formSchemeEntity =await GetSchemeEntity(schemeId);
            FormSchemeInfoEntity entity = new FormSchemeInfoEntity
            {
                F_Id = schemeInfoId,
                F_SchemeId = schemeId
            };
            if (formSchemeEntity.F_Type != 1)
            {
                entity.F_EnabledMark = 0;
            }
            await this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public async Task UpdateState(string schemeInfoId, int state)
        {
            FormSchemeInfoEntity entity = new FormSchemeInfoEntity
            {
                F_Id = schemeInfoId,
                F_EnabledMark = state
            };
            await this.BaseRepository().Update(entity);
        }         
        #endregion
    }
}
