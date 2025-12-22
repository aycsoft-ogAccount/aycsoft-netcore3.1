using ce.autofac.extension;
using aycsoft.iapplication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.02.10
    /// 描 述：流程会签
    /// </summary>
    public class NWFConfluenceBLL : BLLBase,NWFConfluenceIBLL,BLL
    {

        private NWFConfluenceService wfConfluenceService = new NWFConfluenceService();

        #region 获取数据
        /// <summary>
        /// 获取会签记录
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFConfluenceEntity>> GetList(string processId, string nodeId)
        {
            return wfConfluenceService.GetList(processId, nodeId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存成功的会签记录
        /// </summary>
        /// <param name="entity">实体</param>
        public async Task SaveEntity(NWFConfluenceEntity entity)
        {
            await wfConfluenceService.SaveEntity(entity);
        }
        /// <summary>
        /// 删除会签节点数据
        /// </summary>
        /// <param name="processId">实例主键</param>
        /// <param name="nodeId">节点主键</param>
        public async Task DeleteEntity(string processId, string nodeId)
        {
            await wfConfluenceService.DeleteEntity(processId, nodeId);
        }
        #endregion
    }
}
