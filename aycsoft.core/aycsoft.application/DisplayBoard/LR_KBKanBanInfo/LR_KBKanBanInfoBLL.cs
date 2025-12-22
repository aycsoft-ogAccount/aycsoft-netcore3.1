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
    /// 日 期：2022.11.14
    /// 描 述：看板信息
    /// </summary>
    public class LR_KBKanBanInfoBLL : BLLBase, LR_KBKanBanInfoIBLL, BLL
    {
        private readonly LR_KBKanBanInfoService lR_KBKanBanInfoService = new LR_KBKanBanInfoService();
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">请求参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBKanBanInfoEntity>> GetList(string queryJson)
        {
            return lR_KBKanBanInfoService.GetList(queryJson);
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">请求参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBKanBanInfoEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return lR_KBKanBanInfoService.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<LR_KBKanBanInfoEntity> GetEntity(string keyValue)
        {
            return lR_KBKanBanInfoService.GetEntity(keyValue);
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
            await lR_KBKanBanInfoService.DeleteEntity(keyValue);
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="kanbaninfo">看板信息</param>
        /// <param name="kbconfigInfo">看板配置信息</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, string kanbaninfo, string kbconfigInfo)
        {
            await lR_KBKanBanInfoService.SaveEntity(keyValue, kanbaninfo, kbconfigInfo);
        }

        #endregion
        #region
        /// <summary>
        /// 获取看板模板（加入空模板）
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LR_KBKanBanInfoEntity>> GetTemptList()
        {
            List<LR_KBKanBanInfoEntity> list = new List<LR_KBKanBanInfoEntity>();
            var data = await GetList(null);
            foreach (var item in data)
            {
                list.Add(item);
            }
            LR_KBKanBanInfoEntity kanBanInfoEntity = new LR_KBKanBanInfoEntity
            {
                F_Id = "12",
                F_KanBanName = "空模板",
                F_KanBanCode = "kanban00001"
            };
            list.Add(kanBanInfoEntity);
            return list;
        }
        #endregion
    }
}
