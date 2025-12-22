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
    /// 描 述：部门管理
    /// </summary>
    public interface DepartmentIBLL : IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取部门列表信息(根据公司Id)
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <returns></returns>
        Task<IEnumerable<DepartmentEntity>> GetList(string companyId);
        /// <summary>
        /// 获取部门列表信息(根据公司Id)
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        Task<IEnumerable<DepartmentEntity>> GetList(string companyId, string keyWord);

        /// <summary>
        /// 获取部门列表信息
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <returns></returns>
        Task<IEnumerable<DepartmentEntity>> GetAllList(string keyWord);
        
        /// <summary>
        /// 获取部门列表信息(根据公司Id 和 父级 id)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="pid">父级 id</param>
        /// <returns></returns>
        Task<IEnumerable<DepartmentEntity>> GetListByPid(string companyId, string pid);
        /// <summary>
        /// 获取部门列表信息(根据部门ID集合)
        /// </summary>
        /// <param name="keyValues">部门ID集合</param>
        /// <returns></returns>
        Task<IEnumerable<DepartmentEntity>> GetListByKeys(List<string> keyValues);
        /// <summary>
        /// 获取部门数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<DepartmentEntity> GetEntity(string keyValue);
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="companyId">公司id</param>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetTree(string companyId, string parentId);
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="companylist">公司数据</param>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetTree(IEnumerable<CompanyEntity> companylist);
        /// <summary>
        /// 获取部门本身和子部门的id
        /// </summary>
        /// <param name="companyId">公司id主键</param>
        /// <param name="parentId">父级ID</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetSubNodes(string companyId, string parentId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task Delete(string keyValue);
        /// <summary>
        /// 保存部门信息（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        Task SaveEntity(string keyValue, DepartmentEntity departmentEntity);
        #endregion
    }
}
