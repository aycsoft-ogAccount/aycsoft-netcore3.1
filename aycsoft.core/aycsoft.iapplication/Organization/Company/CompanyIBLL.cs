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
    /// 日 期：2022.09.24
    /// 描 述：公司管理
    /// </summary>
    public interface CompanyIBLL : IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取公司列表数据
        /// </summary>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        Task<IEnumerable<CompanyEntity>> GetList(string keyWord = "");
        /// <summary>
        /// 获取子公司列表信息
        /// </summary>
        /// <param name="pId">父级Id</param>
        /// <returns></returns>
        Task<IEnumerable<CompanyEntity>> GetListByPId(string pId);
        /// <summary>
        /// 获取公司信息实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<CompanyEntity> GetEntity(string keyValue);
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetTree(string parentId);
        /// <summary>
        /// 获取公司本身和子公司的id
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetSubNodes(string parentId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task Delete(string keyValue);
        /// <summary>
        /// 保存公司信息（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="companyEntity">公司实体</param>
        /// <returns></returns>
        Task SaveEntity(string keyValue, CompanyEntity companyEntity);
        #endregion
    }
}
