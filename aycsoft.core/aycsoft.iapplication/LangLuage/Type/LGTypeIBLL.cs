using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.22
    /// 描 述：语言类型
    /// </summary>
    public interface LGTypeIBLL : IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取语言类型全部数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LGTypeEntity>> GetAllData();
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LGTypeEntity>> GetList(string queryJson);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<LGTypeEntity> GetEntity(string keyValue);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<LGTypeEntity> GetEntityByCode(string keyValue);
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
        Task SaveEntity(string keyValue, LGTypeEntity entity);
        /// <summary>
        /// 设为主语言
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task SetMainLG(string keyValue);

        #endregion

    }
}
