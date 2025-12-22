using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.util;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：即时通讯用户注册
    /// </summary>
    public interface IMSysUserIBLL : IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        Task<IEnumerable<IMSysUserEntity>> GetList(string keyWord);

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        Task<IEnumerable<IMSysUserEntity>> GetPageList(Pagination pagination, string keyWord);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<IMSysUserEntity> GetEntity(string keyValue);
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
        Task SaveEntity(string keyValue, IMSysUserEntity entity);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="userIdList">用户列表</param>
        /// <param name="content">消息内容</param>
        Task SendMsg(string code, IEnumerable<string> userIdList, string content);
        #endregion 
    }
}
