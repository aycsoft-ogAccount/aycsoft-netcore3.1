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
    /// 日 期：2022.09.24
    /// 描 述：公司管理
    /// </summary>
    public class CompanyService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public CompanyService()
        {
            fieldSql = @"
                    t.F_CompanyId,
                    t.F_Category,
                    t.F_ParentId,
                    t.F_EnCode,
                    t.F_ShortName,
                    t.F_FullName,
                    t.F_Nature,
                    t.F_OuterPhone,
                    t.F_InnerPhone,
                    t.F_Fax,
                    t.F_Postalcode,
                    t.F_Email,
                    t.F_Manager,
                    t.F_ProvinceId,
                    t.F_CityId,
                    t.F_CountyId,
                    t.F_Address,
                    t.F_WebAddress,
                    t.F_FoundedTime,
                    t.F_BusinessScope,
                    t.F_SortCode,
                    t.F_DeleteMark,
                    t.F_EnabledMark,
                    t.F_Description,
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
        /// 获取公司列表信息
        /// </summary>
        /// <param name="keyWord">查询关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<CompanyEntity>> GetList(string keyWord)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Company t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ");

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = "%" + keyWord + "%";
                strSql.Append("AND( F_FullName like @keyWord or F_EnCode like @keyWord or F_ShortName like @keyWord ) ");
            }

            strSql.Append(" ORDER BY t.F_ParentId,t.F_FullName ");
            return this.BaseRepository().FindList<CompanyEntity>(strSql.ToString(), new { keyWord });
        }
        /// <summary>
        /// 获取子公司列表信息
        /// </summary>
        /// <param name="pId">父级Id</param>
        /// <returns></returns>
        public Task<IEnumerable<CompanyEntity>> GetListByPId(string pId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Company t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 AND t.F_ParentId = @pId ");

            strSql.Append(" ORDER BY t.F_ParentId,t.F_FullName ");
            return this.BaseRepository().FindList<CompanyEntity>(strSql.ToString(), new { pId });
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<CompanyEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<CompanyEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await this.BaseRepository().DeleteAny<CompanyEntity>(new { F_CompanyId = keyValue });
        }
        /// <summary>
        /// 保存公司表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="companyEntity">公司实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, CompanyEntity companyEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                companyEntity.F_ModifyUserId = this.GetUserId();
                companyEntity.F_ModifyUserName = this.GetUserName();
                companyEntity.F_CompanyId = keyValue;
                companyEntity.F_ModifyDate = DateTime.Now;

                await this.BaseRepository().Update(companyEntity);
            }
            else
            {
                companyEntity.F_CreateUserId = this.GetUserId();
                companyEntity.F_CreateUserName = this.GetUserName();
                companyEntity.F_CompanyId = Guid.NewGuid().ToString();
                companyEntity.F_CreateDate = DateTime.Now;
                companyEntity.F_DeleteMark = 0;
                companyEntity.F_EnabledMark = 1;
                await this.BaseRepository().Insert(companyEntity);
            }

        }
        #endregion
    }
}
