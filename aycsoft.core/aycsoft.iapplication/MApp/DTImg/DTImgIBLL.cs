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
    /// 日 期：2020.04.07
    /// 描 述：App首页图片管理 
    /// </summary>
    public interface DTImgIBLL: IBLL
    {
        #region 获取数据

        /// <summary> 
        /// 获取列表数据 
        /// </summary> 
        /// <returns></returns> 
        Task<IEnumerable<DTImgEntity>> GetList();

        /// <summary>
        /// 获取列表分页数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        Task<IEnumerable<DTImgEntity>> GetPageList(Pagination pagination, string queryJson);

        /// <summary> 
        /// 获取实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        Task<DTImgEntity> GetEntity(string keyValue);
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
        /// <param name="entity">实体</param> 
        /// <returns></returns> 
        Task SaveEntity(string keyValue, DTImgEntity entity);

        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用2禁用</param>
        Task UpdateState(string keyValue, int state);
        #endregion
    }
}
