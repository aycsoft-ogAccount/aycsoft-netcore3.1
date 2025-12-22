using aycsoft.util;
using ce.autofac.extension;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.18
    /// 描 述：行政区域
    /// </summary>
    public interface AreaIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取区域列表数据
        /// </summary>
        /// <param name="parentId">父节点主键（0表示顶层）</param>
        /// <param name="keyword">关键字查询（名称/编号）</param>
        /// <returns></returns>
        Task<IEnumerable<AreaEntity>> GetList(string parentId, string keyword = "");
        /// <summary>
        /// 获取区域数据树（某一级的）
        /// </summary>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetTree(string parentId);
        /// <summary>
        /// 区域实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Task<AreaEntity> GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除区域
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task Delete(string keyValue);
        /// <summary>
        /// 保存区域表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="areaEntity">区域实体</param>
        /// <returns></returns>
        Task SaveEntity(string keyValue, AreaEntity areaEntity);
        #endregion
    }
}
