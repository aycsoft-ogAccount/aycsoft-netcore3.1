using aycsoft.util;
using ce.autofac.extension;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.iappdev
{
    /// <summary>
    /// Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期： 2020-06-18 06:35:30
    /// 描 述： 测试代码生成器 f_parent
    /// </summary>
    public interface ITestBLL : IBLL
    {

        #region 获取数据
        /// <summary>
        /// 获取主表f_parent的所有列表数据
        /// </summary>
        /// <param name="queryJson">查询参数,json字串</param>
        /// <returns></returns>
        Task<IEnumerable<F_parentEntity>> GetList(string queryJson);

        /// <summary>
        /// 获取主表f_parent的分页列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数,json字串</param>
        /// <returns></returns>
        Task<IEnumerable<F_parentEntity>> GetPageList(Pagination pagination, string queryJson);
        
        /// <summary>
        /// 获取f_children(f_children)的列表实体数据
        /// </summary>
        /// <param name="f_parentId">与表f_parent的关联字段</param>
        /// <returns></returns>
        Task<IEnumerable<F_childrenEntity>> GetF_childrenList(string f_parentId);


        /// <summary>
        /// 获取主表f_parent的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<F_parentEntity> GetEntity(string keyValue);


        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主表主键</param>
        Task Delete(string keyValue);
        /// <summary>
        /// 新增,更新
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="f_parentEntity">f_parent实体数据</param>
        /// <param name="f_childrenList">f_children实体数据列表</param>
        /// <returns></returns>
        Task<string> SaveEntity(string keyValue ,F_parentEntity f_parentEntity,IEnumerable<F_childrenEntity> f_childrenList);
        #endregion
    }
}
