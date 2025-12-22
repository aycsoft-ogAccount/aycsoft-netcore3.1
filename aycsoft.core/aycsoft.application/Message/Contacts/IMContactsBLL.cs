using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.iapplication;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：最近联系人列表
    /// </summary>
    public class IMContactsBLL : BLLBase, IMContactsIBLL, BLL
    {
        private readonly IMContactsService iMContactsService = new IMContactsService();

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public Task<IEnumerable<IMContactsEntity>> GetList(string userId)
        {
            return iMContactsService.GetList(userId);
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public Task<IEnumerable<IMContactsEntity>> GetList(string userId, DateTime time)
        {
            return iMContactsService.GetList(userId, time);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="userId">发送人</param>
        /// <param name="otherUserId">接收人</param>
        /// <returns></returns>
        public Task<IMContactsEntity> GetEntity(string userId, string otherUserId)
        {
            return iMContactsService.GetEntity(userId, otherUserId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task SaveEntity(IMContactsEntity entity)
        {
            await iMContactsService.SaveEntity(entity);
        }
        /// <summary>
        /// 更新记录读取状态
        /// </summary>
        /// <param name="myUserId">自己本身用户ID</param>
        /// <param name="otherUserId">对方用户ID</param>
        public async Task UpdateState(string myUserId, string otherUserId)
        {
            await iMContactsService.UpdateState(myUserId, otherUserId);
        }
        /// <summary>
        /// 删除最近联系人
        /// </summary>
        /// <param name="myUserId">发起者id</param>
        /// <param name="otherUserId">对方用户ID</param>
        public async Task DeleteEntity(string myUserId, string otherUserId)
        {
            await iMContactsService.DeleteEntity(myUserId, otherUserId);
        }
        #endregion
    }
}
