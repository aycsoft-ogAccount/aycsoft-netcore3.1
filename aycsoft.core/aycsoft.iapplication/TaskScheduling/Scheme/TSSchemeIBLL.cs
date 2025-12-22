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
    /// 日 期：2022.11.01
    /// 描 述：任务计划模板信息
    /// </summary>
    public interface TSSchemeIBLL : IBLL
    {

        #region 获取数据
        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        Task<IEnumerable<TSSchemeInfoEntity>> GetPageList(Pagination pagination, string queryJson);
        /// <summary> 
        /// 获取列表数据 
        /// </summary> 
        /// <returns></returns> 
        Task<IEnumerable<TSSchemeInfoEntity>> GetList();
        /// <summary> 
        /// 获取表实体数据
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        Task<TSSchemeInfoEntity> GetSchemeInfoEntity(string keyValue);
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
        /// <param name="schemeInfoEntity">实体</param> 
        /// <returns></returns>
        Task SaveEntity(string keyValue, TSSchemeInfoEntity schemeInfoEntity);
        #endregion

        #region 扩展应用
        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="keyValue">任务主键</param>
        Task PauseJob(string keyValue);
        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="keyValue">任务进程主键</param>
        Task EnAbleJob(string keyValue);
        #endregion

        #region 执行sql语句和存储过程
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        Task ExecuteBySql(string code, string sql);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        Task ExecuteProc(string code, string name);

        #endregion
    }
}
