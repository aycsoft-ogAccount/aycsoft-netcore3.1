using aycsoft.iapplication;
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
    /// 日 期：2022.10.31
    /// 描 述：桌面图表配置
    /// </summary>
    public class DTChartService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public DTChartService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Icon,
                t.F_DataSourceId,
                t.F_Name,
                t.F_Type,
                t.F_Sort,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_CreateDate,
                t.F_Sql,
                t.F_Description,
                t.F_Proportion1,
                t.F_Proportion2,
                t.F_Proportion3,
                t.F_Proportion4
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DTChartEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_DT_Chart t Order by t.F_Sort ");
            return this.BaseRepository().FindList<DTChartEntity>(strSql.ToString());
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<DTChartEntity>> GetList(string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_DT_Chart t where 1=1 ");

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" t.F_Name like @keyword ");
            }
            strSql.Append(" Order by t.F_Sort ");

            return this.BaseRepository().FindList<DTChartEntity>(strSql.ToString(), new { keyword });
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DTChartEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<DTChartEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<DTChartEntity>(new { F_Id = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, DTChartEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
                await this.BaseRepository().Update(entity);
            }
            else
            {
                entity.F_Id = Guid.NewGuid().ToString();
                entity.F_CreateDate = DateTime.Now;
                await this.BaseRepository().Insert(entity);
            }
        }

        #endregion
    }
}
