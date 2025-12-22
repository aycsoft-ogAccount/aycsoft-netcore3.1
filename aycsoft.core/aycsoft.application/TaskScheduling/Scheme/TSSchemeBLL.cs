using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.01
    /// 描 述：任务计划模板信息
    /// </summary>
    public class TSSchemeBLL : BLLBase, TSSchemeIBLL, BLL
    {
        private readonly TSSchemeService schemeService = new TSSchemeService();



        #region 获取数据

        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public Task<IEnumerable<TSSchemeInfoEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return schemeService.GetPageList(pagination, queryJson);
        }

        /// <summary> 
        /// 获取列表数据 
        /// </summary> 
        /// <returns></returns> 
        public Task<IEnumerable<TSSchemeInfoEntity>> GetList()
        {
            return schemeService.GetList();
        }
        /// <summary> 
        /// 获取表实体数据
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public Task<TSSchemeInfoEntity> GetSchemeInfoEntity(string keyValue)
        {
            return schemeService.GetSchemeInfoEntity(keyValue);
        }
        #endregion

        #region 提交数据 
        /// <summary> 
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public async Task DeleteEntity(string keyValue)
        {
            await schemeService.DeleteEntity(keyValue);
            await QuartzHelper.DeleteJob(keyValue);
        }

        /// <summary> 
        /// 保存实体数据（新增、修改）
        /// </summary> 
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">实体</param> 
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, TSSchemeInfoEntity schemeInfoEntity)
        {
            await schemeService.SaveEntity(keyValue, schemeInfoEntity);
            await QuartzHelper.DeleteJob(keyValue);
            await QuartzHelper.AddJob(schemeInfoEntity.F_Id, schemeInfoEntity.F_Scheme.ToObject<TSSchemeModel>());
        }
        #endregion

        #region 扩展应用
        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="keyValue">任务主键</param>
        public async Task PauseJob(string keyValue)
        {
            TSSchemeInfoEntity schemeInfoEntity = new TSSchemeInfoEntity()
            {
                F_IsActive = 2
            };
            await schemeService.UpdateEntity(keyValue, schemeInfoEntity);
            await QuartzHelper.DeleteJob(keyValue);
        }
        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="keyValue">任务进程主键</param>
        public async Task EnAbleJob(string keyValue)
        {
            TSSchemeInfoEntity schemeInfoEntity = new TSSchemeInfoEntity()
            {
                F_IsActive = 1
            };
            var entity = await GetSchemeInfoEntity(keyValue);

            await schemeService.UpdateEntity(keyValue, schemeInfoEntity);
            await QuartzHelper.AddJob(keyValue, entity.F_Scheme.ToObject<TSSchemeModel>());
        }
        #endregion


        #region 执行sql语句和存储过程
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public async Task ExecuteBySql(string code, string sql)
        {
            await schemeService.ExecuteBySql(code, sql);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="code">数据库编码</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public async Task ExecuteProc(string code, string name)
        {
            await schemeService.ExecuteProc(code, name);
        }

        #endregion
    }
}
