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
    /// 日 期：2022.09.25
    /// 描 述：用户关联对象
    /// </summary>
    public class UserRelationService : ServiceBase
    {
        #region 构造函数和属性
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public UserRelationService()
        {
            fieldSql = @"
                t.F_UserRelationId,
                t.F_UserId,
                t.F_Category,
                t.F_ObjectId,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取对象主键列表信息
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="category">分类:1-角色2-岗位</param>
        /// <returns></returns>
        public Task<IEnumerable<UserRelationEntity>> GetObjectIdList(string userId, int category)
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_UserRelation t WHERE t.F_UserId = @userId AND t.F_Category =  @category ");
            return this.BaseRepository().FindList<UserRelationEntity>(strSql.ToString(), new { userId, category });
        }
        /// <summary>
        /// 获取用户主键列表信息
        /// </summary>
        /// <param name="objectId">关联角色或岗位组件</param>
        /// <returns></returns>
        public Task<IEnumerable<UserRelationEntity>> GetUserIdList(string objectId)
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_UserRelation t WHERE t.F_ObjectId = @objectId");
            return this.BaseRepository().FindList<UserRelationEntity>(strSql.ToString(), new { objectId });
        }
        /// <summary>
        /// 获取用户主键列表信息
        /// </summary>
        /// <param name="objectIdList">关联角色或岗位组件</param>
        /// <returns></returns>
        public Task<IEnumerable<UserRelationEntity>> GetUserIdList(IEnumerable<string> objectIdList)
        {
            var strSql = new StringBuilder();
            strSql.Append(" SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_UserRelation t where 1=1 ");

            List<string> objectIdList2 = (List<string>)objectIdList;
            if (objectIdList2.Count > 0)
            {
                strSql.Append(" AND (  ");
                int num = 0;
                foreach (var item in objectIdList)
                {
                    if (num > 0)
                    {
                        strSql.Append(" OR ");
                    }
                    strSql.Append(" F_ObjectId = '" + item + "'");
                    num++;
                }
                strSql.Append(" )  ");
            }

            return this.BaseRepository().FindList<UserRelationEntity>(strSql.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存用户对应对象数据
        /// </summary>
        /// <param name="objectId">人员和角色主键</param>
        /// <param name="userRelationEntityList">列表</param>
        public async Task SaveEntityList(string objectId, IEnumerable<UserRelationEntity> userRelationEntityList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<UserRelationEntity>(new { F_ObjectId = objectId });
                foreach (UserRelationEntity userRelationEntity in userRelationEntityList)
                {
                    await db.Insert(userRelationEntity);
                }
                db.Commit();

            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
