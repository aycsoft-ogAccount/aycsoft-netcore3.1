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
    /// 日 期：2022.11.14
    /// 描 述：看板信息
    /// </summary>
    public interface LR_KBKanBanInfoIBLL : IBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        Task<IEnumerable<LR_KBKanBanInfoEntity>> GetList(string queryJson);
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        Task<IEnumerable<LR_KBKanBanInfoEntity>> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<LR_KBKanBanInfoEntity> GetEntity(string keyValue);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="kanbaninfo">看板信息</param>
        /// <param name="kbconfigInfo">看板配置信息</param>
        /// <returns></returns>
        Task SaveEntity(string keyValue, string kanbaninfo, string kbconfigInfo);
        #endregion

        #region
        /// <summary>
        /// 获取看板模板（加入空模板）
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LR_KBKanBanInfoEntity>> GetTemptList();
        #endregion
    }
}
