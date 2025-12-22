using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.07
    /// 描 述：App首页图片管理 
    /// </summary> 
    public class DTImgBLL : BLLBase, DTImgIBLL, BLL
    {
        private readonly DTImgService dTImgService = new DTImgService();

        #region 获取数据

        /// <summary> 
        /// 获取列表数据 
        /// </summary> 
        /// <returns></returns> 
        public Task<IEnumerable<DTImgEntity>> GetList()
        {
            return dTImgService.GetList();
        }

        /// <summary> 
        /// 获取列表分页数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public Task<IEnumerable<DTImgEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return dTImgService.GetPageList(pagination, queryJson);
        }

        /// <summary> 
        /// 获取实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public Task<DTImgEntity> GetEntity(string keyValue)
        {
            return dTImgService.GetEntity(keyValue);
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
            await dTImgService.DeleteEntity(keyValue);
        }

        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        /// <returns></returns> 
        public async Task SaveEntity(string keyValue, DTImgEntity entity)
        {
            await dTImgService.SaveEntity(keyValue, entity);
        }

        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用2禁用</param>
        public async Task UpdateState(string keyValue, int state)
        {
            DTImgEntity entity = new DTImgEntity
            {
                F_EnabledMark = state
            };
            await SaveEntity(keyValue, entity);
        }
        #endregion
    }
}
