using Dapper;
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
    /// 日 期：2022.11.20
    /// 描 述：门户网站页面配置
    /// </summary>
    public class PageService : ServiceBase
    {
        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public Task<IEnumerable<PageEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@" 
                t.F_Id, 
                t.F_Title,
                t.F_Type,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_ModifyDate,
                t.F_ModifyUserId,
                t.F_ModifyUserName
                ");
            strSql.Append("  FROM LR_PS_Page t ");
            strSql.Append("  WHERE 1=1 ");
            var queryParam = queryJson.ToJObject();
            // 虚拟参数 
            var dp = new DynamicParameters(new { });
            return this.BaseRepository().FindList<PageEntity>(strSql.ToString(), dp, pagination);
        }
        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary> 
        /// <returns></returns> 
        public Task<IEnumerable<PageEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@" 
                t.F_Id, 
                t.F_Title,
                t.F_Type,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_ModifyDate,
                t.F_ModifyUserId,
                t.F_ModifyUserName
                ");
            strSql.Append("  FROM LR_PS_Page t ");
            return this.BaseRepository().FindList<PageEntity>(strSql.ToString());
        }
        /// <summary> 
        /// 获取LR_PS_Page表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public Task<PageEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<PageEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<PageEntity>(new { F_Id = keyValue });
        }

        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        /// <returns></returns> 
        public async Task SaveEntity(string keyValue, PageEntity entity)
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
