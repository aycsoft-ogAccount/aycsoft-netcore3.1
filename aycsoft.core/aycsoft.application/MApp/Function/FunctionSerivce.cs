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
    /// 日 期：2020.04.07
    /// 描 述：移动端功能管理
    /// </summary>
    public class FunctionSerivce: ServiceBase
    {
        #region 属性 构造函数
        private readonly string sql;
        /// <summary>
        /// 
        /// </summary>
        public FunctionSerivce()
        {
            sql = @" 
                    t.F_Id,
                    t.F_Type,
                    t.F_FormId,
                    t.F_CodeId,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_ModifyDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName,
                    t.F_Icon,
                    t.F_Name,
                    t.F_SchemeId,
                    t.F_EnabledMark,
                    t.F_SortCode,
                    t.F_Url,
                    t.F_IsSystem
                    ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="type">分类</param>
        /// <returns></returns>
        public Task<IEnumerable<FunctionEntity>> GetPageList(Pagination pagination, string keyword, string type)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(sql);
            strSql.Append(" FROM LR_App_Function t where 1=1 ");

            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" AND ( t.F_Name like @keyword ) ");
                keyword = "%" + keyword + "%";
            }
            if (!string.IsNullOrEmpty(type))
            {
                strSql.Append(" AND t.F_Type = @type ");
            }
            return this.BaseRepository().FindList<FunctionEntity>(strSql.ToString(), new { keyword, type }, pagination);
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<FunctionEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(sql);
            strSql.Append(" ,s.F_Scheme FROM LR_App_Function t LEFT JOIN LR_App_FnScheme s on  t.F_SchemeId = s.F_Id where t.F_EnabledMark = 1 ORDER BY F_SortCode ");

            return this.BaseRepository().FindList<FunctionEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取移动功能模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FunctionSchemeEntity> GetScheme(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<FunctionSchemeEntity>(keyValue);
        }
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FunctionEntity> GetEntity(string keyValue) {
            return this.BaseRepository().FindEntityByKey<FunctionEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                FunctionEntity entity =await db.FindEntityByKey<FunctionEntity>(keyValue);
                string schemeId = entity.F_SchemeId;
                if (!string.IsNullOrEmpty(schemeId))
                {
                    await db.DeleteAny<FunctionSchemeEntity>(new { F_Id = schemeId });
                }
                await db.DeleteAny<FunctionEntity>(new { F_Id = keyValue });
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="functionEntity">功能信息</param>
        /// <param name="functionSchemeEntity">功能模板信息</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, FunctionEntity functionEntity, FunctionSchemeEntity functionSchemeEntity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                // 如果是代码开发功能
                if (functionEntity.F_IsSystem == 1)
                {
                    if (!string.IsNullOrEmpty(functionEntity.F_SchemeId))
                    {
                        await db.DeleteAny<FunctionSchemeEntity>(new { F_Id = functionEntity.F_SchemeId });
                    }
                }
                else
                {
                    #region 模板信息
                    if (string.IsNullOrEmpty(functionEntity.F_SchemeId))
                    {
                        functionSchemeEntity.F_Id = Guid.NewGuid().ToString();
                        await db.Insert(functionSchemeEntity);
                        functionEntity.F_SchemeId = functionSchemeEntity.F_Id;
                    }
                    else
                    {
                        functionSchemeEntity.F_Id = functionEntity.F_SchemeId;
                        await db.Update(functionSchemeEntity);
                    }
                    #endregion
                }

                if (string.IsNullOrEmpty(keyValue))
                {
                    functionEntity.F_Id = Guid.NewGuid().ToString();
                    functionEntity.F_CreateDate = DateTime.Now;
                    functionEntity.F_EnabledMark = 1;
                    functionEntity.F_CreateUserId = this.GetUserId();
                    functionEntity.F_CreateUserName = this.GetUserName();
                    await db.Insert(functionEntity);
                }
                else
                {
                    functionEntity.F_Id = keyValue;
                    functionEntity.F_ModifyDate = DateTime.Now;
                    functionEntity.F_ModifyUserId = this.GetUserId();
                    functionEntity.F_ModifyUserName = this.GetUserName();
                    await db.Update(functionEntity);
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
        /// 更新状态
        /// </summary>
        /// <param name="keyValue">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public async Task UpdateState(string keyValue, int state)
        {
            FunctionEntity entity = new FunctionEntity
            {
                F_Id = keyValue,
                F_EnabledMark = state
            };
            await this.BaseRepository().Update(entity);
        }
        #endregion
    }
}
