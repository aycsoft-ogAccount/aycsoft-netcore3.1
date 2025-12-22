using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.util;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：即时通讯消息内容
    /// </summary>
    public interface IMMsgIBLL : IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表数据(最近的10条聊天记录)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IMMsgEntity>> GetList(string sendUserId, string recvUserId);
        /// <summary>
        /// 获取列表数据(小于某个时间点的5条记录)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        Task<IEnumerable<IMMsgEntity>> GetListByTime(string myUserId, string otherUserId, DateTime time);
        /// <summary>
        /// 获取列表数据(大于某个时间的所有数据)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        Task<IEnumerable<IMMsgEntity>> GetListByTime2(string myUserId, string otherUserId, DateTime time);
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="sendUserId"></param>
        /// <param name="recvUserId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<IEnumerable<IMMsgEntity>> GetPageList(Pagination pagination, string sendUserId, string recvUserId, string keyword);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task SaveEntity(IMMsgEntity entity);
        #endregion
    }
}
