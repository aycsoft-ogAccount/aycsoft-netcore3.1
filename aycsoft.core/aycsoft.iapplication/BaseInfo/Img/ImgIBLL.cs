using ce.autofac.extension;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.17
    /// 描 述：图片保存
    /// </summary>
    public interface ImgIBLL : IBLL
    {
        #region 获取数据
        /// <summary> 
        /// 获取实体数据
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        Task<ImgEntity> GetEntity(string keyValue);
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
        Task SaveEntity(string keyValue, ImgEntity entity);

        #endregion
    }
}
