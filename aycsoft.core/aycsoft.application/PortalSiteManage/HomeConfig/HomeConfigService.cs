using Dapper;
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
    /// 日 期：2022.11.20
    /// 描 述：门户网站首页配置
    /// </summary>
    public class HomeConfigService : ServiceBase
    {
        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public Task<IEnumerable<HomeConfigEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@" 
                t.F_Id, 
                t.F_Name 
                ");
            strSql.Append("  FROM LR_PS_HomeConfig t ");
            strSql.Append("  WHERE 1=1 ");
            var queryParam = queryJson.ToJObject();
            // 虚拟参数 
            var dp = new DynamicParameters(new { });
            return this.BaseRepository().FindList<HomeConfigEntity>(strSql.ToString(), dp, pagination);
        }
        /// <summary> 
        /// 获取LR_PS_HomeConfig表实体数据
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns>
        public Task<HomeConfigEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<HomeConfigEntity>(keyValue);
        }
        /// <summary>
        /// 获取实体根据类型
        /// </summary>
        /// <param name="type">1.顶部文字2.底部文字3.底部地址4.logo图片5.微信图片6.顶部菜单7.底部菜单8.轮播图片9.模块 10底部logo</param>
        /// <returns></returns>
        public Task<HomeConfigEntity> GetEntityByType(string type)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@" 
                t.F_Id, 
                t.F_Name,
                t.F_Type,
                t.F_UrlType,
                t.F_Url,
                t.F_Img,
                t.F_ParentId,
                t.F_Sort,
                t.F_Scheme
                ");
            strSql.Append("  FROM LR_PS_HomeConfig t ");
            strSql.Append("  WHERE t.F_Type = @type Order by F_Sort ");
            return this.BaseRepository().FindEntity<HomeConfigEntity>(strSql.ToString(), new { type });
        }

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public Task<IEnumerable<HomeConfigEntity>> GetList(string type)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@" 
                t.F_Id, 
                t.F_Name,
                t.F_Type,
                t.F_UrlType,
                t.F_Url,
                t.F_Img,
                t.F_ParentId,
                t.F_Sort,
                t.F_Scheme
                ");
            strSql.Append("  FROM LR_PS_HomeConfig t ");
            strSql.Append("  WHERE t.F_Type = @type Order by F_Sort "); 
            return this.BaseRepository().FindList<HomeConfigEntity>(strSql.ToString(), new { type });
        }
        /// <summary>
        /// 获取所有的配置列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<HomeConfigEntity>> GetALLList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@" 
                t.F_Id, 
                t.F_Name,
                t.F_Type,
                t.F_UrlType,
                t.F_Url,
                t.F_Img,
                t.F_ParentId,
                t.F_Sort,
                t.F_Scheme
                ");
            strSql.Append("  FROM LR_PS_HomeConfig t where t.F_Type != 4 AND  t.F_Type != 5 AND t.F_Type != 10 AND t.F_Type != 9");
            strSql.Append("  Order by F_Sort ");
            // 虚拟参数 
            var dp = new DynamicParameters(new { });
            return this.BaseRepository().FindList<HomeConfigEntity>(strSql.ToString());
        }
        #endregion

        #region 提交数据 

        /// <summary> 
        /// 删除实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        public async Task DeleteEntity(string keyValue)
        {
            await this.BaseRepository().DeleteAny<HomeConfigEntity>(new { F_Id = keyValue });
        }

        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        /// <returns></returns> 
        public async Task SaveEntity(string keyValue, HomeConfigEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
                await this.BaseRepository().Update(entity);
            }
            else
            {
                entity.F_Id = Guid.NewGuid().ToString();
                await this.BaseRepository().Insert(entity);
            }
        }

        #endregion
    }
}
