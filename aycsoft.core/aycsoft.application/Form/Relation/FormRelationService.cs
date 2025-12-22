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
    /// 日 期：2022.11.22
    /// 描 述：表单关联功能
    /// </summary>
    public class FormRelationService : ServiceBase
    {
        #region 属性 构造函数
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public FormRelationService()
        {
            fieldSql = @" 
                    t.F_Id,
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
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<FormRelationEntity>> GetPageList(Pagination pagination, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" ,s.F_Name as F_FormId,m.F_FullName as F_ModuleId  FROM LR_Form_Relation t ");
            strSql.Append(" LEFT JOIN LR_Form_SchemeInfo s ON t.F_FormId = s.F_Id ");
            strSql.Append(" LEFT JOIN LR_Base_Module m ON t.F_ModuleId = m.F_ModuleId WHERE 1=1 ");
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" AND (s.F_Name like @keyword OR m.F_FullName like @keyword ) ");
                keyword = "%" + keyword + "%";
            }
            return this.BaseRepository().FindList<FormRelationEntity>(strSql.ToString(), new { keyword }, pagination);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<FormRelationEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" ,t.F_FormId,m.F_FullName as F_ModuleId  FROM LR_Form_Relation t ");
            strSql.Append(" LEFT JOIN LR_Base_Module m ON t.F_ModuleId = m.F_ModuleId WHERE 1=1 ");
            return this.BaseRepository().FindList<FormRelationEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FormRelationEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<FormRelationEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            await this.BaseRepository().DeleteAny<FormRelationEntity>(new { F_Id = keyValue});
        }
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="formRelationEntity">表单与功能信息</param>
        public async Task SaveEntity(string keyValue, FormRelationEntity formRelationEntity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                formRelationEntity.F_Id = Guid.NewGuid().ToString();
                formRelationEntity.F_CreateDate = DateTime.Now;
                formRelationEntity.F_CreateUserId = this.GetUserId();
                formRelationEntity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(formRelationEntity);
            }
            else
            {
                formRelationEntity.F_Id = keyValue;
                formRelationEntity.F_ModifyDate = DateTime.Now;
                formRelationEntity.F_ModifyUserId = this.GetUserId();
                formRelationEntity.F_ModifyUserName = this.GetUserName();

                await this.BaseRepository().Update(formRelationEntity);
            }
        }
        #endregion
    }
}
