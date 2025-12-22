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
    /// 日 期：2022.11.21
    /// 描 述：表单关联功能
    /// </summary>
    public interface FormRelationIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        Task<IEnumerable<FormRelationEntity>> GetPageList(Pagination pagination, string keyword);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<FormRelationEntity> GetEntity(string keyValue);

        /// <summary>
        /// 获取树形数据列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetTree();
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task DeleteEntity(string keyValue);
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="formRelationEntity">表单与功能信息</param>
        Task SaveEntity(string keyValue, FormRelationEntity formRelationEntity);
        #endregion
    }
}
