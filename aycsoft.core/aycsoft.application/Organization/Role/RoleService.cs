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
    /// 描 述：角色管理
    /// </summary>
    public class RoleService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public RoleService()
        {
            fieldSql = @"
                    t.F_RoleId,
                    t.F_Category,
                    t.F_EnCode,
                    t.F_FullName,
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
        /// 获取角色数据列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<RoleEntity>> GetList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Role t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ORDER BY t.F_FullName ");
            return this.BaseRepository().FindList<RoleEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取角色数据列表（分页）
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<RoleEntity>> GetPageList(Pagination pagination, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Role t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ");

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND( t.F_FullName like @keyword or t.F_EnCode like @keyword ) ");
            }

            return this.BaseRepository().FindList<RoleEntity>(strSql.ToString(), new { keyword }, pagination);
        }
        /// <summary>
        /// 获取角色数据列表
        /// </summary>
        /// <param name="roleIds">主键串</param>
        /// <returns></returns>
        public Task<IEnumerable<RoleEntity>> GetListByRoleIds(string roleIds)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Role t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0  ");
            strSql.Append(" AND F_RoleId in ('" + roleIds.Replace(",", "','") + "')");
            strSql.Append(" ORDER BY t.F_FullName ");
            return this.BaseRepository().FindList<RoleEntity>(strSql.ToString());
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<RoleEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<RoleEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<RoleEntity>(new { F_RoleId = keyValue });
                await db.ExecuteSql(" Delete  From LR_BASE_USERRELATION where F_OBJECTID = @keyValue  ", new { keyValue });

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存角色（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="roleEntity">角色实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, RoleEntity roleEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.F_ModifyUserId = this.GetUserId();
                roleEntity.F_ModifyUserName = this.GetUserName();
                roleEntity.F_RoleId = keyValue;
                roleEntity.F_ModifyDate = DateTime.Now;

                await this.BaseRepository().Update(roleEntity);
            }
            else
            {
                roleEntity.F_CreateUserId = this.GetUserId();
                roleEntity.F_CreateUserName = this.GetUserName();

                roleEntity.F_RoleId = Guid.NewGuid().ToString();
                roleEntity.F_CreateDate = DateTime.Now;
                roleEntity.F_DeleteMark = 0;
                roleEntity.F_EnabledMark = 1;
                roleEntity.F_Category = "1";
                await this.BaseRepository().Insert(roleEntity);
            }
        }
        #endregion
    }
}
