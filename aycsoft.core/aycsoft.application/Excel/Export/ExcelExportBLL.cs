using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：Excel数据导出设置
    /// </summary>
    public class ExcelExportBLL : BLLBase, ExcelExportIBLL, BLL
    {
        private readonly ExcelExportService excelExportService = new ExcelExportService();

        #region 获取数据
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelExportEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return excelExportService.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelExportEntity>> GetList(string moduleId)
        {
            return excelExportService.GetList(moduleId);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<ExcelExportEntity> GetEntity(string keyValue)
        {
            return excelExportService.GetEntity(keyValue);
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
            await excelExportService.DeleteEntity(keyValue);
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, ExcelExportEntity entity)
        {
            await excelExportService.SaveEntity(keyValue, entity);
        }

        #endregion
    }
}
