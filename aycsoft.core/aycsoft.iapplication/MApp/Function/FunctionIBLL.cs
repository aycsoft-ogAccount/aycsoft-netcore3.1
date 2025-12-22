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
    /// 描 述：移动端功能管理
    /// </summary>
    public interface FunctionIBLL:IBLL
    {

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="type">分类</param>
        /// <returns></returns>
        Task<IEnumerable<FunctionEntity>> GetPageList(Pagination pagination, string keyword, string type);
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FunctionEntity>> GetList();
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<FunctionEntity> GetEntity(string keyValue);
        /// <summary>
        /// 获取移动功能模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<FunctionSchemeEntity> GetScheme(string keyValue);
        /// <summary>
        /// 获取树形移动功能列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetCheckTree();
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task Delete(string keyValue);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="functionEntity">功能信息</param>
        /// <param name="functionSchemeEntity">功能模板信息</param>
        Task SaveEntity(string keyValue, FunctionEntity functionEntity, FunctionSchemeEntity functionSchemeEntity);
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="keyValue">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        Task UpdateState(string keyValue, int state);
        #endregion
    }
}
