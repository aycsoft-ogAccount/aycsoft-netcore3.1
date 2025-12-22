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
    /// 日 期：2020.02.10
    /// 描 述：流程会签
    /// </summary>
    public class NWFConfluenceService : ServiceBase
    {
        #region 获取数据
        /// <summary>
        /// 获取会签记录
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFConfluenceEntity>> GetList(string processId, string nodeId)
        {
            return this.BaseRepository().FindList<NWFConfluenceEntity>("select * from lr_nwf_confluence where F_ProcessId = @processId AND F_NodeId = @nodeId",new { processId, nodeId });
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存成功的会签记录
        /// </summary>
        /// <param name="entity">实体</param>
        public async Task SaveEntity(NWFConfluenceEntity entity)
        {
            string processId = entity.F_ProcessId;
            string nodeId = entity.F_NodeId;
            string formNodeId = entity.F_FormNodeId;
            NWFConfluenceEntity oldEntity = await this.BaseRepository().FindEntity<NWFConfluenceEntity>("select * from lr_nwf_confluence where F_ProcessId = @processId AND F_NodeId = @nodeId AND F_FormNodeId = @formNodeId", new { processId, nodeId, formNodeId });
            if (oldEntity == null)
            {
                entity.F_Id = Guid.NewGuid().ToString();
                await this.BaseRepository().Insert(entity);
            }
        }
        /// <summary>
        /// 删除会签节点数据
        /// </summary>
        /// <param name="processId">实例主键</param>
        /// <param name="nodeId">节点主键</param>
        public async Task DeleteEntity(string processId, string nodeId)
        {
            await this.BaseRepository().DeleteAny<NWFConfluenceEntity>(new { F_ProcessId= processId, F_NodeId = nodeId });
        }


        #endregion
    }
}
