using ce.autofac.extension;
using aycsoft.iapplication;
using System;
using System.Collections.Generic;
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
    public class UserRelationBLL: UserRelationIBLL,BLL
    {
        private readonly UserRelationService  userRelationService = new UserRelationService();
        
        #region 获取数据
        /// <summary>
        /// 获取对象主键列表信息
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="category">分类:1-角色2-岗位</param>
        /// <returns></returns>
        public Task<IEnumerable<UserRelationEntity>> GetObjectIdList(string userId, int category)
        {
            return userRelationService.GetObjectIdList(userId, category);
        }
        /// <summary>
        /// 获取对象主键列表信息
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="category">分类:1-角色2-岗位</param>
        /// <returns></returns>
        public async Task<string> GetObjectIds(string userId, int category)
        {
            string res = "";
            var list = await GetObjectIdList(userId, category);
            foreach (UserRelationEntity item in list)
            {
                if (res != "")
                {
                    res += ",";
                }
                res += item.F_ObjectId;
            }
            return res;
        }
        /// <summary>
        /// 获取用户主键列表信息
        /// </summary>
        /// <param name="objectId">关联或角色主键集合</param>
        /// <returns></returns>
        public Task<IEnumerable<UserRelationEntity>> GetUserIdList(string objectId)
        {
            return userRelationService.GetUserIdList(objectId);
        }
        /// <summary>
        /// 获取用户主键列表信息
        /// </summary>
        /// <param name="objectIdList">关联或角色主键集合</param>
        /// <returns></returns>
        public Task<IEnumerable<UserRelationEntity>> GetUserIdList(IEnumerable<string> objectIdList)
        {
            return userRelationService.GetUserIdList(objectIdList);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存用户对应对象数据
        /// </summary>
        /// <param name="objectId">对应对象主键</param>
        /// <param name="category">分类:1-角色2-岗位</param>
        /// <param name="userIds">对用户主键列表</param>
        public async Task SaveEntityList(string objectId, int category, string userIds)
        {
            List<UserRelationEntity> list = new List<UserRelationEntity>();
            if (!string.IsNullOrEmpty(userIds)) {
                string[] userIdList = userIds.Split(',');
                foreach (string userId in userIdList)
                {
                    UserRelationEntity userRelationEntity = new UserRelationEntity();

                    userRelationEntity.F_UserRelationId = Guid.NewGuid().ToString();
                    userRelationEntity.F_CreateDate = DateTime.Now;

                    userRelationEntity.F_UserId = userId;
                    userRelationEntity.F_Category = category;
                    userRelationEntity.F_ObjectId = objectId;
                    list.Add(userRelationEntity);
                }
            }
            await userRelationService.SaveEntityList(objectId, list);
        }
        #endregion
    }
}
