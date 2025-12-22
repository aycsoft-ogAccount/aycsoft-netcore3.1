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
    /// 日 期：2022.09.24
    /// 描 述：部门管理
    /// </summary>
    public class DepartmentService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public DepartmentService()
        {
            fieldSql = @"
                    t.F_DepartmentId,
                    t.F_CompanyId,
                    t.F_ParentId,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_ShortName,
                    t.F_Nature,
                    t.F_Manager,
                    t.F_OuterPhone,
                    t.F_InnerPhone,
                    t.F_Email,
                    t.F_Fax,
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
        /// 获取部门列表信息(根据公司Id)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentEntity>> GetList(string companyId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Department t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 AND F_CompanyId = @companyId ORDER BY t.F_ParentId,t.F_FullName ");
            return this.BaseRepository().FindList<DepartmentEntity>(strSql.ToString(), new { companyId });
        }

        /// <summary>
        /// 获取部门列表信息(根据公司Id 和 父级 id)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="pid">父级 id</param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentEntity>> GetListByPid(string companyId,string pid)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Department t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 AND F_CompanyId = @companyId AND F_ParentId = @pid ORDER BY t.F_FullName ");
            return this.BaseRepository().FindList<DepartmentEntity>(strSql.ToString(), new { companyId,pid });
        }

        /// <summary>
        /// 获取部门列表信息(根据公司Id)
        /// </summary>
        /// <param name="keyWord">公司主键</param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentEntity>> GetAllList(string keyWord)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Department t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ");
            if (!string.IsNullOrEmpty(keyWord)) {
                keyWord = "%" + keyWord + "%";
                strSql.Append("AND ( F_FullName like @keyWord OR F_EnCode like @keyWord OR F_ShortName like @keyWord ) ");
            }
            strSql.Append(" ORDER BY t.F_ParentId,t.F_FullName ");

            return this.BaseRepository().FindList<DepartmentEntity>(strSql.ToString(), new { keyWord });
        }
        /// <summary>
        /// 获取部门列表信息(根据部门ID集合)
        /// </summary>
        /// <param name="keyValues">部门ID集合</param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentEntity>> GetListByKeys(List<string> keyValues)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Department t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ");
            if (keyValues.Count > 0)
            {
                strSql.Append(" AND (  ");
                int num = 0;
                foreach (var item in keyValues)
                {
                    if (num > 0)
                    {
                        strSql.Append(" OR ");
                    }
                    strSql.Append(" F_DepartmentId = '" + item + "'");
                    num++;
                }
                strSql.Append(" )  ");
            }

            return this.BaseRepository().FindList<DepartmentEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取部门列表信息(根据公司Id)
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentEntity>> GetAllList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Department t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ");
            return this.BaseRepository().FindList<DepartmentEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取部门数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DepartmentEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<DepartmentEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await this.BaseRepository().DeleteAny<DepartmentEntity>(new { F_DepartmentId = keyValue });
        }
        /// <summary>
        /// 保存部门表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, DepartmentEntity departmentEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                departmentEntity.F_DepartmentId = keyValue;
                departmentEntity.F_ModifyDate = DateTime.Now;
                departmentEntity.F_ModifyUserId = this.GetUserId();
                departmentEntity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(departmentEntity);
            }
            else
            {
                departmentEntity.F_DepartmentId = Guid.NewGuid().ToString();
                departmentEntity.F_CreateDate = DateTime.Now;
                departmentEntity.F_DeleteMark = 0;
                departmentEntity.F_EnabledMark = 1;
                departmentEntity.F_CreateUserId = this.GetUserId();
                departmentEntity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(departmentEntity);
            }
        }
        #endregion
    }
}
