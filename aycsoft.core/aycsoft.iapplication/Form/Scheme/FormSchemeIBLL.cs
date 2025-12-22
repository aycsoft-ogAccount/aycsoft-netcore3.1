using ce.autofac.extension;
using aycsoft.util;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.21
    /// 描 述：表单模板
    /// </summary>
    public interface FormSchemeIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取自定义表单列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FormSchemeInfoEntity>> GetCustmerSchemeInfoList();
        /// <summary>
        /// 获取表单分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        Task<IEnumerable<FormSchemeInfoEntity>> GetSchemeInfoPageList(Pagination pagination, string keyword, string category);
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        Task<IEnumerable<FormSchemeEntity>> GetSchemePageList(Pagination pagination, string schemeInfoId);
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<FormSchemeInfoEntity> GetSchemeInfoEntity(string keyValue);
        /// <summary>
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<FormSchemeEntity> GetSchemeEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task Delete(string keyValue);
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        Task SaveEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity, FormSchemeEntity schemeEntity);
        /// <summary>
        /// 保存模板基础信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        Task SaveSchemeInfoEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity);
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        Task UpdateScheme(string schemeInfoId, string schemeId);
        /// <summary>
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        Task UpdateState(string schemeInfoId, int state);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="sql">数据权限sql</param>
        /// <returns></returns>
        Task<DataTable> GetFormPageList(string schemeInfoId, Pagination pagination, string queryJson,string sql);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="sql">数据权限sql</param>
        /// <returns></returns>
        Task<DataTable> GetFormList(string schemeInfoId, string queryJson, string sql);
        /// <summary>
        /// 获取自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<Dictionary<string, DataTable>> GetInstanceForm(string schemeInfoId, string keyValue);
        /// <summary>
        /// 获取自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="processIdName">流程实例关联字段名</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<Dictionary<string, DataTable>> GetInstanceForm(string schemeInfoId, string processIdName, string keyValue);
        /// <summary>
        /// 保存自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="processIdName">流程关联字段名</param>
        /// <param name="keyValue">数据主键值</param>
        /// <param name="formData">自定义表单数据</param>
        Task SaveInstanceForm(string schemeInfoId, string processIdName, string keyValue, string formData);

        /// <summary>
        /// 删除自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="keyValue">数据主键值</param>
        Task DeleteInstanceForm(string schemeInfoId, string keyValue);
        #endregion
    }
}
