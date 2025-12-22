using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.25
    /// 描 述：IP过滤
    /// </summary>
    public interface FilterIPIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        Task<IEnumerable<FilterIPEntity>> GetList(string objectId, string visitType);
        /// <summary>
        /// 过滤IP实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Task<FilterIPEntity> GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task DeleteEntiy(string keyValue);
        /// <summary>
        /// 保存过滤IP表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterIPEntity">过滤IP实体</param>
        /// <returns></returns>
        Task SaveForm(string keyValue, FilterIPEntity filterIPEntity);
        #endregion
    }
}
