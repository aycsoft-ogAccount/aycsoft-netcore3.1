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
    /// 描 述：Excel数据导入设置
    /// </summary>
    public class ExcelImportService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        private readonly string fieldSql2;
        /// <summary>
        /// 
        /// </summary>
        public ExcelImportService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_ModuleId,
                t.F_ModuleBtnId,
                t.F_BtnName,
                t.F_DbId,
                t.F_DbTable,
                t.F_ErrorType,
                t.F_EnabledMark,
                t.F_Description,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_ModifyDate,
                t.F_ModifyUserId,
                t.F_ModifyUserName
            ";
            fieldSql2 = @"
                t.F_Id,
                t.F_ImportId,
                t.F_Name,
                t.F_ColName,
                t.F_OnlyOne,
                t.F_RelationType,
                t.F_DataItemCode,
                t.F_Value,
                t.F_DataSourceId,
                t.F_SortCode
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件参数</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelImportEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string keyword = "";
            string moduleId = "";

            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Excel_Import t where 1=1 ");

            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" AND t.F_Name like @keyword ");
            }
            if (!queryParam["moduleId"].IsEmpty())
            {
                moduleId = queryParam["moduleId"].ToString();
                strSql.Append(" AND t.F_ModuleId = @moduleId ");
            }
            return this.BaseRepository().FindList<ExcelImportEntity>(strSql.ToString(), new { keyword, moduleId }, pagination);
        }
        /// <summary>
        /// 获取导入配置列表根据模块ID
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelImportEntity>> GetList(string moduleId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Excel_Import t where t.F_ModuleId = @moduleId AND t.F_EnabledMark = 1 ");

            return this.BaseRepository().FindList<ExcelImportEntity>(strSql.ToString(), new { moduleId });
        }

        /// <summary>
        /// 获取配置信息实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<ExcelImportEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<ExcelImportEntity>(keyValue);
        }
        /// <summary>
        /// 获取配置字段列表
        /// </summary>
        /// <param name="importId">配置信息主键</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelImportFieldEntity>> GetFieldList(string importId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql2);
            strSql.Append(" FROM LR_Excel_ImportFileds t where t.F_ImportId=@importId ORDER By  t.F_SortCode ");

            return this.BaseRepository().FindList<ExcelImportFieldEntity>(strSql.ToString(), new { importId });
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {

            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<ExcelImportEntity>(new { F_Id = keyValue });
                await db.DeleteAny<ExcelImportFieldEntity>(new { F_ImportId = keyValue });
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体数据</param>
        /// <param name="filedList">字段列表</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, ExcelImportEntity entity, List<ExcelImportFieldEntity> filedList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.F_Id = keyValue;
                    entity.F_ModifyDate = DateTime.Now;
                    entity.F_ModifyUserId = this.GetUserId();
                    entity.F_ModifyUserName = this.GetUserName();
                    await db.Update(entity);
                }
                else
                {
                    entity.F_Id = Guid.NewGuid().ToString();
                    entity.F_CreateDate = DateTime.Now;
                    entity.F_CreateUserId = this.GetUserId();
                    entity.F_CreateUserName = this.GetUserName();
                    entity.F_EnabledMark = 1;
                    await db.Insert(entity);
                }
                await db.DeleteAny<ExcelImportFieldEntity>(new { F_ImportId = entity.F_Id });
                foreach (var item in filedList)
                {
                    item.F_Id = Guid.NewGuid().ToString();
                    item.F_ImportId = entity.F_Id;
                    await db.Insert(item);
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
        /// 更新配置主表
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <param name="userInfo">当前执行人信息</param>
        public async Task UpdateEntity(string keyValue, ExcelImportEntity entity, UserEntity userInfo)
        {
            entity.F_Id = keyValue;
            entity.F_ModifyDate = DateTime.Now;
            entity.F_ModifyUserId = userInfo.F_UserId;
            entity.F_ModifyUserName = userInfo.F_RealName;
            await this.BaseRepository().Update(entity);
        }
        #endregion
    }
}
