using ce.autofac.extension;
using aycsoft.util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：流程委托
    /// </summary>
    public interface NWFDelegateIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字(被委托人)</param>
        /// <returns></returns>
        Task<IEnumerable<NWFDelegateRuleEntity>> GetPageList(Pagination pagination, string keyword);
        /// <summary>
        /// 根据委托人获取委托记录
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NWFDelegateRuleEntity>> GetList();
        /// <summary>
        /// 获取关联的模板数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NWFDelegateRelationEntity>> GetRelationList(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="wfDelegateRuleEntity">实体数据</param>
        /// <param name="schemeInfoList">关联模板主键</param>
        Task SaveEntity(string keyValue, NWFDelegateRuleEntity wfDelegateRuleEntity, string[] schemeInfoList);
        /// <summary>
        /// 更新委托规则状态信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state"></param>
        Task UpdateState(string keyValue, int state);
        #endregion
    }
}
