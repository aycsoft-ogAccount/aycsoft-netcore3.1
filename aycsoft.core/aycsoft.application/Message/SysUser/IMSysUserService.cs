using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aycsoft.database;
using aycsoft.iapplication;
using aycsoft.util;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：即时通讯用户注册
    /// </summary>
    public class IMSysUserService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public IMSysUserService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_Code,
                t.F_Icon,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_Description
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<IMSysUserEntity>> GetList(string keyWord)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_SysUser t where  1=1 ");

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = "%" + keyWord + "%";
                strSql.Append(" AND t.F_Name Like @keyWord ");
            }

            return this.BaseRepository().FindList<IMSysUserEntity>(strSql.ToString(), new { keyWord = keyWord });
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<IMSysUserEntity>> GetPageList(Pagination pagination, string keyWord)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_SysUser t where  1=1  ");

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = "%" + keyWord + "%";
                strSql.Append(" AND t.F_Name Like @keyWord ");
            }

            return this.BaseRepository().FindList<IMSysUserEntity>(strSql.ToString(), new { keyWord }, pagination);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<IMSysUserEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<IMSysUserEntity>(keyValue);
        }
        /// <summary>
        /// 获取实体数据byCode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task<IMSysUserEntity> GetEntityByCode(string code)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_IM_SysUser t where  t.F_Code = @code ");
            return this.BaseRepository().FindEntity<IMSysUserEntity>(strSql.ToString(), new { code });
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据(虚拟)
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public async Task DeleteEntity(string keyValue)
        {
            await this.BaseRepository().DeleteAny<IMSysUserEntity>(new { F_Id = keyValue });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, IMSysUserEntity entity)
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
                entity.F_CreateUserId = this.GetUserId();
                entity.F_CreateUserName = this.GetUserName();
                await this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
