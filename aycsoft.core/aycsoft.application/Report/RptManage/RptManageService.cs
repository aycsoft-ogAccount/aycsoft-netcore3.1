using Dapper;
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
    /// 日 期：2020.04.07
    /// 描 述：报表文件管理
    /// </summary>
    public class RptManageService : ServiceBase
    {
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<RptManageEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@"
                t.F_Id,
                t.F_Code,
                t.F_Name,
                t.F_Type,
                t.F_SortCode,
                f.F_FileName as F_File,
                t.F_Description
                ");
            strSql.Append("  FROM LR_RPT_FileInfo t INNER JOIN LR_Base_AnnexesFile f on t.F_File=f.F_FolderId ");
            strSql.Append("  WHERE 1=1 ");
            var queryParam = queryJson.ToJObject();
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            if (!queryParam["F_Code"].IsEmpty())
            {
                dp.Add("F_Code", "%" + queryParam["F_Code"].ToString() + "%", DbType.String);
                strSql.Append(" AND t.F_Code Like @F_Code ");
            }
            if (!queryParam["F_Name"].IsEmpty())
            {
                dp.Add("F_Name", "%" + queryParam["F_Name"].ToString() + "%", DbType.String);
                strSql.Append(" AND t.F_Name Like @F_Name ");
            }
            if (!queryParam["F_Type"].IsEmpty())
            {
                dp.Add("F_Type", queryParam["F_Type"].ToString(), DbType.String);
                strSql.Append(" AND t.F_Type = @F_Type ");
            }
            this.BaseRepository().FindTable("select * from tb");
            return this.BaseRepository().FindList<RptManageEntity>(strSql.ToString(), dp, pagination);
        }
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<RptManageEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@"
                t.F_Id,
                t.F_Code,
                t.F_Name,
                t.F_Type,
                t.F_SortCode,
                f.F_FileName as F_File,
                t.F_Description
                ");
            strSql.Append("  FROM LR_RPT_FileInfo t INNER JOIN LR_Base_AnnexesFile f on t.F_File=f.F_FolderId ");
            return this.BaseRepository().FindList<RptManageEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<RptManageEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<RptManageEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<RptManageEntity>(new { F_Id = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, RptManageEntity entity)
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
                entity.F_CreateUserId = this.GetUserId();
                entity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(entity);
            }
        }

        #endregion

    }
}
