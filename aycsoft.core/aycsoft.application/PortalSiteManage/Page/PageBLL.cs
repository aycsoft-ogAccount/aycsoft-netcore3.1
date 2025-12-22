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
    /// 日 期：2022.11.20
    /// 描 述：门户网站页面配置
    /// </summary>
    public class PageBLL:BLLBase, PageIBLL,BLL
    {
        private readonly PageService pageService = new PageService();

        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public Task<IEnumerable<PageEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return pageService.GetPageList(pagination, queryJson);
        }
        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary>
        /// <returns></returns> 
        public Task<IEnumerable<PageEntity>> GetList()
        {
            return pageService.GetList();
        }
        /// <summary> 
        /// 获取LR_PS_Page表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public Task<PageEntity> GetEntity(string keyValue)
        {
            return pageService.GetEntity(keyValue);
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
            await pageService.DeleteEntity(keyValue);
        }

        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        /// <returns></returns> 
        public async Task SaveEntity(string keyValue, PageEntity entity)
        {
            await pageService.SaveEntity(keyValue, entity);
        }

        #endregion
    }
}
