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
    /// 描 述：岗位管理
    /// </summary>
    public class PostService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public PostService()
        {
            fieldSql = @"
                    t.F_PostId,
                    t.F_ParentId,
                    t.F_Name,
                    t.F_EnCode,
                    t.F_CompanyId,
                    t.F_DepartmentId,
                    t.F_DeleteMark,
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
        /// 获取岗位数据列表（根据公司列表）
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        public Task<IEnumerable<PostEntity>> GetList(string companyId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Post t WHERE t.F_DeleteMark = 0 AND t.F_CompanyId =@companyId ORDER BY t.F_DepartmentId,t.F_ParentId,t.F_EnCode ");
            return this.BaseRepository().FindList<PostEntity>(strSql.ToString(), new { companyId });
        }

        /// <summary>
        /// 获取岗位数据列表（根据公司列表）
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门Id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<PostEntity>> GetList(string companyId, string departmentId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Post t WHERE t.F_DeleteMark = 0   ");
            if (!string.IsNullOrEmpty(companyId))
            {
                strSql.Append(" AND t.F_CompanyId =@companyId ");
            }
            if (!string.IsNullOrEmpty(departmentId))
            {
                strSql.Append(" AND t.F_DepartmentId =@departmentId ");
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND t.F_Name like @keyword ");
            }

            strSql.Append("ORDER BY t.F_DepartmentId,t.F_ParentId,t.F_EnCode");


            return this.BaseRepository().FindList<PostEntity>(strSql.ToString(), new { companyId, departmentId, keyword });
        }
        /// <summary>
        /// 获取岗位数据列表(根据主键串)
        /// </summary>
        /// <param name="postIds">根据主键串</param>
        /// <returns></returns>
        public Task<IEnumerable<PostEntity>> GetListByPostIds(string postIds)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Post t WHERE t.F_DeleteMark = 0 ");
            strSql.Append(" AND F_PostId in ('" + postIds.Replace(",", "','") + "')");

            return this.BaseRepository().FindList<PostEntity>(strSql.ToString());
        }
        /// <summary>
        /// 获取岗位的实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<PostEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<PostEntity>(keyValue);
        }
        /// <summary>
        /// 获取下级岗位id集合
        /// </summary>
        /// <param name="parentIds">父级Id集合</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetIdList(List<string> parentIds)
        {
            List<string> res = new List<string>();
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_Post t WHERE t.F_DeleteMark = 0 ");
            if (parentIds.Count > 0)
            {
                strSql.Append(" AND (  ");
                int num = 0;
                foreach (var item in parentIds)
                {
                    if (num > 0)
                    {
                        strSql.Append(" OR ");
                    }
                    strSql.Append(" F_ParentId = '" + item + "'");
                    num++;
                }
                strSql.Append(" )  ");
            }
            var list = await this.BaseRepository().FindList<PostEntity>(strSql.ToString());
            foreach (var item in list)
            {
                res.Add(item.F_PostId);
            }
            return res;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<PostEntity>(new { F_PostId = keyValue });
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
        /// 保存岗位（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="postEntity">岗位实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, PostEntity postEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                postEntity.F_PostId = keyValue;
                postEntity.F_ModifyDate = DateTime.Now;
                postEntity.F_ModifyUserId = this.GetUserId();
                postEntity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(postEntity);
            }
            else
            {
                postEntity.F_PostId = Guid.NewGuid().ToString();
                postEntity.F_CreateDate = DateTime.Now;
                postEntity.F_DeleteMark = 0;
                postEntity.F_CreateUserId = this.GetUserId();
                postEntity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(postEntity);
            }
        }
        #endregion
    }
}
