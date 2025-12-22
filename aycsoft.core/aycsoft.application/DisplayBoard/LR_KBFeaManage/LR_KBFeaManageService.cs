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
    /// 日 期：2022.11.14
    /// 描 述：看板发布
    /// </summary>
    public class LR_KBFeaManageService : ServiceBase
    {
        #region 构造函数和属性

        private string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public LR_KBFeaManageService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_FullName,
                t.F_EnCode,
                t.F_ParentId,
                t.F_Icon,
                t.F_KanBanId,
                t.F_ModuleId,
                t.F_SortCode,
                t.F_Description
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBFeaManageEntity>> GetList(string queryJson)
        {
            //参考写法
            //var queryParam = queryJson.ToJObject();
            // 虚拟参数
            //var dp = new DynamicParameters(new { });
            //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_KBFeaManage t ");
            return this.BaseRepository().FindList<LR_KBFeaManageEntity>(strSql.ToString());

        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBFeaManageEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(@"
                                t.F_Id,
                                t.F_FullName,
                                t.F_EnCode,
                                (select F_FullName from LR_Base_Module where F_ModuleId=t.F_ParentId) as F_ParentId,
                                t.F_Icon,
                                (select F_KanBanName from LR_KBKanBanInfo where F_Id=t.F_KanBanId) as F_KanBanId,
                                t.F_ModuleId,
                                t.F_SortCode,
                                t.F_Description
                              ");
            strSql.Append(" FROM LR_KBFeaManage t ");
            return this.BaseRepository().FindList<LR_KBFeaManageEntity>(strSql.ToString(), pagination);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<LR_KBFeaManageEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<LR_KBFeaManageEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<LR_KBFeaManageEntity>(new { F_Id = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, LR_KBFeaManageEntity entity)
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
