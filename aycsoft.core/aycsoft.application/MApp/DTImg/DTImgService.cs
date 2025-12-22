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
    /// 描 述：App首页图片管理 
    /// </summary> 
    public class DTImgService : ServiceBase
    {
        #region 构造函数和属性 

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public DTImgService()
        {
            fieldSql = @" 
                t.F_Id, 
                t.F_Des, 
                t.F_FileName, 
                t.F_EnabledMark, 
                t.F_SortCode 
            ";
        }
        #endregion

        #region 获取数据 

        /// <summary> 
        /// 获取列表数据 
        /// </summary> 
        /// <returns></returns> 
        public Task<IEnumerable<DTImgEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_App_DTImg t WHERE t.F_EnabledMark = 1 Order by  t.F_SortCode  ");
            return this.BaseRepository().FindList<DTImgEntity>(strSql.ToString());
        }

        /// <summary> 
        /// 获取列表分页数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public Task<IEnumerable<DTImgEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_App_DTImg t  where 1=1 ");

            // 关键字
            string keyword = "";
            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = "%" + queryParam["keyword"].ToString() + "%";
                strSql.Append(" AND t.F_Des like @keyword ");
            }

            return this.BaseRepository().FindList<DTImgEntity>(strSql.ToString(), new { keyword }, pagination);
        }

        /// <summary> 
        /// 获取实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public Task<DTImgEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<DTImgEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<DTImgEntity>(new { F_Id = keyValue });
        }

        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        /// <returns></returns> 
        public async Task SaveEntity(string keyValue, DTImgEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
                await this.BaseRepository().Update(entity);
            }
            else
            {
                entity.F_Id = Guid.NewGuid().ToString();
                entity.F_EnabledMark = 1;
                await this.BaseRepository().Insert(entity);
            }
        }

        #endregion
    }
}
