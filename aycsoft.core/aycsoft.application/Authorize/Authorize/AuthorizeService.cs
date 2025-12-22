using aycsoft.database;
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
    /// 日 期：2022.09.10
    /// 描 述：授權功能
    /// </summary>
    public class AuthorizeService : ServiceBase
    {
        #region 属性 构造函数
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public AuthorizeService()
        {
            fieldSql = @" 
                t.F_AuthorizeId,
                t.F_ObjectType,
                t.F_ObjectId,
                t.F_ItemType,
                t.F_ItemId,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName
                ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取授权功能主键数据列表
        /// </summary>
        /// <param name="objectId">对象主键（角色,用户）</param>
        /// <param name="itemType">项目类型:1-菜单2-按钮3-视图</param>
        /// <returns></returns>
        public Task<IEnumerable<AuthorizeEntity>> GetList(string objectId, int itemType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + fieldSql + " FROM LR_Base_Authorize t WHERE t.F_ObjectId = @objectId  AND t.F_ItemType = @itemType Order By t.F_ItemType,t.F_CreateDate ");
            return this.BaseRepository().FindList<AuthorizeEntity>(strSql.ToString(), new { objectId = objectId, itemType = itemType });
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加授权
        /// </summary>
        /// <param name="objectType">权限分类-1角色2用户</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="type">功能类型</param>
        public async Task SaveAppAuthorize(int objectType, string objectId, string[] moduleIds, int type)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<AuthorizeEntity>(new { F_ObjectId = objectId, F_ItemType = type });

                #region 功能
                foreach (string item in moduleIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.F_CreateUserId = this.GetUserId();
                    authorizeEntity.F_CreateUserName = this.GetUserName();
                    authorizeEntity.F_AuthorizeId = Guid.NewGuid().ToString();
                    authorizeEntity.F_CreateDate = DateTime.Now;

                    authorizeEntity.F_ObjectType = objectType;
                    authorizeEntity.F_ObjectId = objectId;
                    authorizeEntity.F_ItemType = type;
                    authorizeEntity.F_ItemId = item;
                    await db.Insert(authorizeEntity);
                }
                #endregion

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
            }
        }
        #endregion
    }
}
