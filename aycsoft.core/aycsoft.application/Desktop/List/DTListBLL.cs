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
    /// 日 期：2022.10.31
    /// 描 述：桌面消息列表配置
    /// </summary>
    public class DTListBLL : BLLBase, DTListIBLL, BLL
    {
        private readonly DTListService dTListService = new DTListService();

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DTListEntity>> GetList()
        {
            return dTListService.GetList();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<DTListEntity>> GetList(string keyword)
        {
            return dTListService.GetList(keyword);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DTListEntity> GetEntity(string keyValue)
        {
            return dTListService.GetEntity(keyValue);
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
            await dTListService.DeleteEntity(keyValue);
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, DTListEntity entity)
        {
            await dTListService.SaveEntity(keyValue, entity);
        }

        #endregion
    }
}
