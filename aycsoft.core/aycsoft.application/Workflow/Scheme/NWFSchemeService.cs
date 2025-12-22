using Dapper;
using aycsoft.database;
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
    /// 日 期：2020.02.11
    /// 描 述：工作流模板(新)
    /// </summary>
    public class NWFSchemeService: ServiceBase
    {
        #region 获取数据
        /// <summary>
        /// 获取流程分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFSchemeInfoEntity>> GetInfoPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT t.* ");
            strSql.Append(",t1.F_Type,t1.F_CreateDate,t1.F_CreateUserId,t1.F_CreateUserName ");
            strSql.Append(" FROM LR_NWF_SchemeInfo t LEFT JOIN LR_NWF_Scheme t1 ON t.F_SchemeId = t1.F_Id WHERE 1=1 ");

            var dp = new DynamicParameters();
            if (!string.IsNullOrEmpty(queryJson))
            {
                var queryParam = queryJson.ToJObject();
                if (!queryParam["keyword"].IsEmpty())
                {
                    strSql.Append(" AND ( t.F_Name like @keyword OR t.F_Code like @keyword ) ");
                    dp.Add("keyword", "%" + queryParam["keyword"].ToString() + "%", DbType.String);
                }
                if (!queryParam["category"].IsEmpty())
                {
                    strSql.Append(" AND t.F_Category = @category ");
                    dp.Add("category", queryParam["category"].ToString(), DbType.String);
                }
            }
            return this.BaseRepository().FindList<NWFSchemeInfoEntity>(strSql.ToString(), dp, pagination);
        }
        /// <summary>
        /// 获取自定义流程列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="postIds">岗位id</param>
        /// <param name="roleIds">角色id</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFSchemeInfoEntity>> GetInfoList(string userId, string postIds, string roleIds)
        {
            string ids = "";
            if (!string.IsNullOrEmpty(userId)) {
                ids +="'" + userId + "'";
            }
            if (!string.IsNullOrEmpty(postIds))
            {
                if (ids != "") {
                    ids += ",";
                }
                ids += "'" + postIds.Replace(",", "','") + "'";
            }
            if (!string.IsNullOrEmpty(roleIds))
            {
                if (ids != "")
                {
                    ids += ",";
                }
                ids += "'" + roleIds.Replace(",", "','") + "'";
            }
            var list = await this.BaseRepository().FindList<NWFSchemeAuthEntity>(" select * from lr_nwf_schemeauth where F_ObjId is null or F_ObjId in ("+ ids + ") ");


            string schemeinfoIds = "";
            foreach (var item in list)
            {
                schemeinfoIds += "'" + item.F_SchemeInfoId + "',";
            }
            schemeinfoIds = "(" + schemeinfoIds.Remove(schemeinfoIds.Length - 1, 1) + ")";

            var strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM LR_NWF_SchemeInfo t WHERE t.F_EnabledMark = 1  AND t.F_Mark = 1 AND t.F_Id in " + schemeinfoIds);

            return await this.BaseRepository().FindList<NWFSchemeInfoEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<NWFSchemeInfoEntity>> GetALLInfoList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM LR_NWF_SchemeInfo t WHERE t.F_EnabledMark = 1 ");
            return this.BaseRepository().FindList<NWFSchemeInfoEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取流程模板分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="userId">用户id</param>
        /// <param name="postIds">岗位id</param>
        /// <param name="roleIds">角色id</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFSchemeInfoEntity>> GetAppInfoPageList(Pagination pagination,string queryJson, string userId, string postIds, string roleIds)
        {
            string ids = "";
            if (!string.IsNullOrEmpty(userId))
            {
                ids += "'" + userId + "'";
            }
            if (!string.IsNullOrEmpty(postIds))
            {
                if (ids != "")
                {
                    ids += ",";
                }
                ids += "'" + postIds.Replace(",", "','") + "'";
            }
            if (!string.IsNullOrEmpty(roleIds))
            {
                if (ids != "")
                {
                    ids += ",";
                }
                ids += "'" + roleIds.Replace(",", "','") + "'";
            }
            var list = await this.BaseRepository().FindList<NWFSchemeAuthEntity>(" select * from lr_nwf_schemeauth where F_ObjId is null or F_ObjId in (" + ids + ") ");


            string schemeinfoIds = "";
            foreach (var item in list)
            {
                schemeinfoIds += "'" + item.F_SchemeInfoId + "',";
            }
            schemeinfoIds = "(" + schemeinfoIds.Remove(schemeinfoIds.Length - 1, 1) + ")";


            var strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM LR_NWF_SchemeInfo t WHERE t.F_EnabledMark = 1  AND t.F_Mark = 1 AND F_IsInApp = 1 AND t.F_Id in " + schemeinfoIds);

            var queryParam = queryJson.ToJObject();
            string keyword = "";
            if (!queryParam["keyword"].IsEmpty())
            {
                strSql.Append(" AND ( t.F_Name like @keyword OR t.F_Code like @keyword ) ");
                keyword = "%" + queryParam["keyword"].ToString() + "%";
            }
            return await this.BaseRepository().FindList<NWFSchemeInfoEntity>(strSql.ToString(), new { keyword }, pagination);
        }

        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<NWFSchemeInfoEntity> GetInfoEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<NWFSchemeInfoEntity>(keyValue);
        }
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        public Task<NWFSchemeInfoEntity> GetInfoEntityByCode(string code)
        {
            return this.BaseRepository().FindEntity<NWFSchemeInfoEntity>("select * from lr_nwf_schemeinfo where F_Code = @code", new { code });
        }

        /// <summary>
        /// 获取流程模板权限列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFSchemeAuthEntity>> GetAuthList(string schemeInfoId)
        {
            return this.BaseRepository().FindList<NWFSchemeAuthEntity>("select * from lr_nwf_schemeauth where F_SchemeInfoId =@schemeInfoId ", new { schemeInfoId });
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">流程信息主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFSchemeEntity>> GetSchemePageList(Pagination pagination, string schemeInfoId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT t.F_Id,t.F_SchemeInfoId,t.F_Type,t.F_CreateDate,t.F_CreateUserId,t.F_CreateUserName");
            strSql.Append(" FROM LR_NWF_Scheme t ");
            strSql.Append(" WHERE t.F_SchemeInfoId = @schemeInfoId ");
            return this.BaseRepository().FindList<NWFSchemeEntity>(strSql.ToString(), new { schemeInfoId }, pagination);
        }
        /// <summary>
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<NWFSchemeEntity> GetSchemeEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<NWFSchemeEntity>(keyValue);
        }
        


        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<NWFSchemeInfoEntity>(new { F_Id = keyValue });
                await db.DeleteAny<NWFSchemeAuthEntity>(new { F_SchemeInfoId = keyValue });
                await db.DeleteAny<NWFSchemeEntity>(new { F_SchemeInfoId = keyValue });
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
        /// <param name="infoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        /// <param name="authList">模板权限信息</param>
        public async Task SaveEntity(string keyValue, NWFSchemeInfoEntity infoEntity, NWFSchemeEntity schemeEntity, IEnumerable<NWFSchemeAuthEntity> authList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    infoEntity.F_Id = Guid.NewGuid().ToString();
                    infoEntity.F_EnabledMark = 1;
                }
                else
                {
                    infoEntity.F_Id = keyValue;
                }

                #region 模板信息
                if (schemeEntity != null)
                {
                    schemeEntity.F_SchemeInfoId = infoEntity.F_Id;
                    schemeEntity.F_Id = Guid.NewGuid().ToString();
                    schemeEntity.F_CreateDate = DateTime.Now;
                    schemeEntity.F_CreateUserId = this.GetUserId();
                    schemeEntity.F_CreateUserName = this.GetUserName();
                    await db.Insert(schemeEntity);
                    infoEntity.F_SchemeId = schemeEntity.F_Id;
                }
                #endregion

                #region 模板基础信息
                if (!string.IsNullOrEmpty(keyValue))
                {
                    await db.Update(infoEntity);
                }
                else
                {
                    await db.Insert(infoEntity);
                }
                #endregion

                #region 流程模板权限信息
                string schemeInfoId = infoEntity.F_Id;
                await db.DeleteAny<NWFSchemeAuthEntity>(new { F_SchemeInfoId = schemeInfoId });
                foreach (var item in authList)
                {
                    item.F_Id = Guid.NewGuid().ToString();
                    item.F_SchemeInfoId = schemeInfoId;
                    await db.Insert(item);
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
        /// 更新流程模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        public async Task UpdateScheme(string schemeInfoId, string schemeId)
        {
            NWFSchemeEntity nWFSchemeEntity = await GetSchemeEntity(schemeId);
            NWFSchemeInfoEntity entity = new NWFSchemeInfoEntity
            {
                F_Id = schemeInfoId,
                F_SchemeId = schemeId
            };
            if (nWFSchemeEntity.F_Type != 1)
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
            NWFSchemeInfoEntity entity = new NWFSchemeInfoEntity
            {
                F_Id = schemeInfoId,
                F_EnabledMark = state
            };
            await this.BaseRepository().Update(entity);
        }
        #endregion
    }
}
