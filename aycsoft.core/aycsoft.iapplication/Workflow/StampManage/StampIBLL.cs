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
    /// 日 期：2020.03.13
    /// 描 述：印章管理
    /// </summary>
    public interface StampIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 模糊查询（根据名称/状态（启用或者停用））
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        Task<IEnumerable<StampEntity>> GetList(string keyWord);
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        Task<IEnumerable<StampEntity>> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取印章实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<StampEntity> GetEntity(string keyValue);
        #endregion

        #region 提交数据

        /// <summary>
        /// 保存印章信息（新增/编辑）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        Task SaveEntity(string keyValue, StampEntity entity);

        /// <summary>
        /// 删除印章信息
        /// </summary>
        /// <param name="keyVlaue">主键</param>
        Task DeleteEntity(string keyVlaue);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1启用 0禁用</param>
        Task UpdateState(string keyValue, int state);
        /// <summary>
        /// 密码匹配
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<bool> EqualPassword(string keyValue, string password);
        #endregion
    }
}
