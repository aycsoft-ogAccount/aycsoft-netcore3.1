using aycsoft.iapplication;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core 力软敏捷开发框架
    /// Copyright (c) 2021-present 力软信息技术（苏州）有限公司
    /// 创建人：young
    /// 日 期：2022.10.25
    /// 描 述：数据权限
    /// </summary>
    public class DataAuthorizeService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string sql;
        /// <summary>
        /// 
        /// </summary>
        public DataAuthorizeService()
        {
            sql = @"
                t.F_Id,
                t.F_Name,
                t.F_Code,
                t.F_Type,
                t.F_ObjectId,
                t.F_ObjectType, 
                t.F_Formula,
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
        /// 获取对应编码的所有的权限设置
        /// </summary>
        /// <param name="code">数据权限编码</param>
        /// <param name="objectId">用户或角色主键</param>
        /// <returns></returns>
        public Task<IEnumerable<DataAuthorizeEntity>> GetList(string code, string objectId)
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(sql);
            strSql.Append(" FROM lr_base_dataauth t where t.F_Code = @code AND t.F_ObjectId in (" + objectId + ") ");

            return this.BaseRepository().FindList<DataAuthorizeEntity>(strSql.ToString(), new { code });
        }
        /// <summary>
        /// 获取数据权限列表（分页）
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <param name="objectId">用户或角色主键</param>
        /// <param name="type">1.普通权限 2.自定义表单权限</param>
        /// <returns></returns>
        public Task<IEnumerable<DataAuthorizeEntity>> GetPageList(Pagination pagination, string keyword, string objectId, int type)
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(sql);
            strSql.Append(" FROM lr_base_dataauth t where 1=1 ");


            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND ( t.F_Name like @keyword Or t.F_Code like @keyword )");
            }

            if (!string.IsNullOrEmpty(objectId))
            {
                strSql.Append(" AND ( t.F_ObjectId = @objectId )");
            }


            if (type == 2)
            {// 自定义表单
                strSql.Append(" AND t.F_Type = 2 ");
            }
            else
            {
                strSql.Append(" AND (t.F_Type != 2 OR t.F_Type is Null) ");
            }

            return this.BaseRepository().FindList<DataAuthorizeEntity>(strSql.ToString(), new { keyword, objectId }, pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DataAuthorizeEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<DataAuthorizeEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<DataAuthorizeEntity>(new { F_Id = keyValue });
        }
        /// <summary>
        ///  保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">数据实体</param>
        public async Task SaveEntity(string keyValue, DataAuthorizeEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
                entity.F_ModifyDate = DateTime.Now;
                entity.F_ModifyUserId = this.GetUserId();
                entity.F_ModifyUserName = this.GetUserName();
                await this.BaseRepository().Update(entity);
            }
            else
            {
                entity.F_Id = Guid.NewGuid().ToString();
                entity.F_CreateDate = DateTime.Now;
                entity.F_CreateUserId = this.GetUserId();
                entity.F_CreateUserName = this.GetUserName();

                await this.BaseRepository().Insert(entity);

            }
        }

        #endregion
    }
}
