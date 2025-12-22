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
    /// 描 述：Excel数据导出设置
    /// </summary>
    public class ExcelExportService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public ExcelExportService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_GridId,
                t.F_ModuleId,
                t.F_ModuleBtnId,
                t.F_BtnName,
                t.F_EnabledMark,
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
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelExportEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string keyword = "";
            string moduleId = "";

            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Excel_Export t where 1=1 ");
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


            return this.BaseRepository().FindList<ExcelExportEntity>(strSql.ToString(), new { keyword = keyword, moduleId = moduleId }, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelExportEntity>> GetList(string moduleId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Excel_Export t where t.F_ModuleId = @moduleId AND t.F_EnabledMark = 1 ");
            return this.BaseRepository().FindList<ExcelExportEntity>(strSql.ToString(), new { moduleId = moduleId });
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<ExcelExportEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<ExcelExportEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<ExcelExportEntity>(new { F_Id = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, ExcelExportEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
                entity.F_ModifyDate = DateTime.Now;
                entity.F_ModifyUserId = this.GetUserId();
                entity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(entity);
            }
            else
            {
                entity.F_Id = Guid.NewGuid().ToString();
                entity.F_CreateDate = DateTime.Now;
                entity.F_EnabledMark = 1;
                entity.F_CreateUserId = this.GetUserId();
                entity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
