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
    /// 描 述：报表管理
    /// </summary>
    public class RptSchemeService : ServiceBase
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<RptSchemeEntity>> GetPageList(Pagination pagination, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT * FROM lr_rpt_scheme t where 1 = 1 ");
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND ( F_EnCode Like @keyword OR F_FullName Like @keyword )");
            }

            return this.BaseRepository().FindList<RptSchemeEntity>(strSql.ToString(),new { keyword }, pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<RptSchemeEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<RptSchemeEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            await this.BaseRepository().DeleteAny<RptSchemeEntity>(new { F_TempId = keyValue });
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, RptSchemeEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_TempId = keyValue;
                entity.F_ModifyDate = DateTime.Now;
                await this.BaseRepository().Update(entity);
            }
            else
            {
                entity.F_TempId = Guid.NewGuid().ToString();
                entity.F_CreateDate = DateTime.Now;
                await this.BaseRepository().Insert(entity);
            }

        }
        #endregion
    }
}
