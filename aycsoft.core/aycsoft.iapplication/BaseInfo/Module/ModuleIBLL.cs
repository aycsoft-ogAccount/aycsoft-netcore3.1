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
    /// 日 期：2022.09.10
    /// 描 述：功能模块
    /// </summary>
    public interface ModuleIBLL : IBLL
    {
        #region 功能模块
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ModuleEntity>> GetModuleList();
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        Task<ModuleEntity> GetModuleByUrl(string url);
        /// <summary>
        ///  功能列表(code)
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        Task<ModuleEntity> GetEntityByCode(string code);
        /// <summary>
        /// 获取功能列表的树形数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetModuleTree();
        /// <summary>
        ///  获取功能列表的树形数据(带勾选框)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetModuleCheckTree();
        /// <summary>
        /// 获取功能列表的树形数据(只有展开项)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetExpendModuleTree();
        /// <summary>
        /// 根据父级主键获取数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        Task<IEnumerable<ModuleEntity>> GetModuleListByParentId(string keyword, string parentId);
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Task<ModuleEntity> GetModuleEntity(string keyValue);
        #endregion

        #region 模块按钮
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        Task<IEnumerable<ModuleButtonEntity>> GetButtonListNoAuthorize(string moduleId);
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        Task<IEnumerable<ModuleButtonEntity>> GetButtonList(string moduleId);
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ModuleButtonEntity>> GetButtonList();
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        Task<IEnumerable<ModuleButtonEntity>> GetButtonListByUrl(string url);
        /// <summary>
        /// 获取按钮列表树形数据（基于功能模块）
        /// </summary>
        /// <param name="objectId">需要设置的角色对象</param>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetButtonCheckTree(string objectId);
        #endregion

        #region 模块视图
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        Task<IEnumerable<ModuleColumnEntity>> GetColumnList(string moduleId);
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ModuleColumnEntity>> GetColumnList();
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        Task<IEnumerable<ModuleColumnEntity>> GetColumnListByUrl(string url);
        /// <summary>
        /// 获取按钮列表树形数据（基于功能模块）
        /// </summary>
        /// <param name="objectId">需要设置的角色对象</param>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetColumnCheckTree(string objectId);
        #endregion

        #region 模块表单
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        Task<IEnumerable<ModuleFormEntity>> GetFormList(string moduleId);
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ModuleFormEntity>> GetFormList();
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        Task<IEnumerable<ModuleFormEntity>> GetFormListByUrl(string url);
        /// <summary>
        /// 获取表单字段树形数据（基于功能模块）
        /// </summary>
        /// <param name="objectId">需要设置的角色对象</param>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetFormCheckTree(string objectId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模块功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        Task<bool> Delete(string keyValue);
        /// <summary>
        /// 保存模块功能实体（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">实体</param>
        /// <param name="moduleButtonEntitys">按钮列表</param>
        /// <param name="moduleColumnEntitys">视图列集合</param>
        /// <param name="moduleFormEntitys">表单字段集合</param>
        Task SaveEntity(string keyValue, ModuleEntity moduleEntity, List<ModuleButtonEntity> moduleButtonEntitys, List<ModuleColumnEntity> moduleColumnEntitys, List<ModuleFormEntity> moduleFormEntitys);
        #endregion
    }
}
