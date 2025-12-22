using ce.autofac.extension;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：会签记录操作
    /// </summary>
    public interface NWFConfluenceIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取会签记录
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        Task<IEnumerable<NWFConfluenceEntity>> GetList(string processId, string nodeId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存成功的会前记录
        /// </summary>
        /// <param name="entity">实体</param>
        Task SaveEntity(NWFConfluenceEntity entity);
        /// <summary>
        /// 删除会签节点数据
        /// </summary>
        /// <param name="processId">实例主键</param>
        /// <param name="nodeId">节点主键</param>
        Task DeleteEntity(string processId, string nodeId);
        #endregion
    }
}
