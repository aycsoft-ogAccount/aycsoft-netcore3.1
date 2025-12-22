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
    /// 日 期：2022.10.25
    /// 描 述：IP过滤
    /// </summary>
    public class FilterIPService : ServiceBase
    {
        #region 获取数据
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        public Task<IEnumerable<FilterIPEntity>> GetList(string objectId, string visitType)
        {

            var strSql = new StringBuilder();
            strSql.Append(@" SELECT
                t.F_FilterIPId, 
                t.F_ObjectType,
                t.F_ObjectId,
                t.F_VisitType,
                t.F_Type,
                t.F_IPLimit,
                t.F_SortCode,
                t.F_DeleteMark,
                t.F_EnabledMark,
                t.F_Description,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_ModifyDate,
                t.F_ModifyUserId,
                t.F_ModifyUserName FROM LR_Base_FilterIP t  where F_ObjectId = @objectId ");
            int _visittype = 0;
            if (!string.IsNullOrEmpty(visitType))
            {
                _visittype = visitType.ToInt();

                strSql.Append(" AND F_VisitType = @_visittype ");
            }
            strSql.Append(" Order By F_CreateDate ");

            return this.BaseRepository().FindList<FilterIPEntity>(strSql.ToString(),new { objectId, _visittype });
        }
        /// <summary>
        /// 过滤IP实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<FilterIPEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<FilterIPEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntiy(string keyValue)
        {
            await this.BaseRepository().DeleteAny<FilterIPEntity>(new { F_FilterIPId = keyValue });
        }
        /// <summary>
        /// 保存过滤IP表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterIPEntity">过滤IP实体</param>
        /// <returns></returns>
        public async Task SaveForm(string keyValue, FilterIPEntity filterIPEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                filterIPEntity.F_ModifyUserId = this.GetUserId();
                filterIPEntity.F_ModifyUserName = this.GetUserName();

                filterIPEntity.F_FilterIPId = keyValue;
                filterIPEntity.F_ModifyDate = DateTime.Now;
                await this.BaseRepository().Update(filterIPEntity);
            }
            else
            {
                filterIPEntity.F_CreateUserId = this.GetUserId();
                filterIPEntity.F_CreateUserName = this.GetUserName();

                filterIPEntity.F_FilterIPId = Guid.NewGuid().ToString();
                filterIPEntity.F_CreateDate = DateTime.Now;
                filterIPEntity.F_DeleteMark = 0;
                filterIPEntity.F_EnabledMark = 1;
                await this.BaseRepository().Insert(filterIPEntity);
            }
        }
        #endregion
    }
}
