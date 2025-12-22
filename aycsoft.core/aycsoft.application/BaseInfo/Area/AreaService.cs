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
    /// 日 期：2022.09.18
    /// 描 述：行政区域
    /// </summary>
    public class AreaService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public AreaService()
        {
            fieldSql = @"
                        t.F_AreaId,
                        t.F_ParentId,
                        t.F_AreaCode,
                        t.F_AreaName,
                        t.F_QuickQuery,
                        t.F_SimpleSpelling,
                        t.F_Layer,
                        t.F_SortCode,
                        t.F_DeleteMark,
                        t.F_EnabledMark,
                        t.F_Description,
                        t.F_CreateDate,
                        t.F_CreateUserId,
                        t.F_CreateUserName,
                        t.F_ModifyDate,
                        t.F_ModifyUserId,
                        t.F_ModifyUserName ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 区域列表
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<AreaEntity>> GetList(string parentId,string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Area t WHERE t.F_EnabledMark = 1");
            strSql.Append(" AND F_ParentId = @parentId ");
            if (string.IsNullOrEmpty(parentId))
            {
                parentId = "0";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" AND (F_AreaName like @keyword or F_AreaCode like @keyword)");
                keyword = "%" + keyword + "%";
            }

            strSql.Append(" ORDER BY t.F_AreaCode ");
            return this.BaseRepository().FindList<AreaEntity>(strSql.ToString(), new { parentId, keyword });
        }
        /// <summary>
        /// 区域实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<AreaEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<AreaEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await this.BaseRepository().DeleteAny<AreaEntity>(new { F_AreaId = keyValue });
        }
        /// <summary>
        /// 保存区域表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="areaEntity">区域实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, AreaEntity areaEntity)
        {
            areaEntity.F_QuickQuery = Str.ConvertPinYin(areaEntity.F_AreaName).ToUpper();
            areaEntity.F_SimpleSpelling = Str.PinYin(areaEntity.F_AreaName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.F_AreaId = keyValue;
                areaEntity.F_ModifyDate = DateTime.Now;
                areaEntity.F_ModifyUserId = this.GetUserId();
                areaEntity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(areaEntity);
            }
            else
            {
                areaEntity.F_AreaId = Guid.NewGuid().ToString();
                areaEntity.F_CreateDate = DateTime.Now;
                areaEntity.F_DeleteMark = 0;
                areaEntity.F_EnabledMark = 1;

                areaEntity.F_CreateUserId = this.GetUserId();
                areaEntity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(areaEntity);
            }

        }
        #endregion
    }
}
