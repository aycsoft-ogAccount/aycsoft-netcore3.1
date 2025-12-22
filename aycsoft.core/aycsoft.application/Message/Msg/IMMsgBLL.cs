using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：即时通讯消息内容
    /// </summary>
    public class IMMsgBLL : BLLBase, IMMsgIBLL, BLL
    {
        private readonly IMMsgService iMMsgService = new IMMsgService();

        #region 获取数据

        /// <summary>
        /// 获取列表数据(最近的10条聊天记录)
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<IMMsgEntity>> GetList(string sendUserId, string recvUserId)
        {
            return iMMsgService.GetList(sendUserId, recvUserId);
        }


        /// <summary>
        /// 获取列表数据(小于某个时间点的5条记录)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public Task<IEnumerable<IMMsgEntity>> GetListByTime(string myUserId, string otherUserId, DateTime time)
        {
            return iMMsgService.GetListByTime(myUserId, otherUserId, time);
        }
        /// <summary>
        /// 获取列表数据(大于某个时间的所有数据)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public Task<IEnumerable<IMMsgEntity>> GetListByTime2(string myUserId, string otherUserId, DateTime time)
        {
            return iMMsgService.GetListByTime2(myUserId, otherUserId, time);
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="sendUserId"></param>
        /// <param name="recvUserId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Task<IEnumerable<IMMsgEntity>> GetPageList(Pagination pagination, string sendUserId, string recvUserId, string keyword)
        {
            return iMMsgService.GetPageList(pagination, sendUserId, recvUserId, keyword);
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
            await iMMsgService.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 保存实体数据（新增）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(IMMsgEntity entity)
        {
            await iMMsgService.SaveEntity(entity);
        }
        #endregion
    }
}
