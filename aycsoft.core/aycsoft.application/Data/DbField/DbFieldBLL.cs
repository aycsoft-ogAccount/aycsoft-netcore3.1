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
    /// 日 期：2022.10.23
    /// 描 述：常用字段
    /// </summary>
    public class DbFieldBLL : BLLBase, DbFieldIBLL, BLL
    {
        private readonly DbFieldService commonFieldService = new DbFieldService();

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DbFieldEntity>> GetList(string queryJson)
        {
            return commonFieldService.GetList(queryJson);
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<DbFieldEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return commonFieldService.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DbFieldEntity> GetEntity(string keyValue)
        {
            return commonFieldService.GetEntity(keyValue);
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
            await commonFieldService.DeleteEntity(keyValue);
        }


        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, DbFieldEntity entity)
        {
            await commonFieldService.SaveEntity(keyValue, entity);
        }

        #endregion
    }
}
