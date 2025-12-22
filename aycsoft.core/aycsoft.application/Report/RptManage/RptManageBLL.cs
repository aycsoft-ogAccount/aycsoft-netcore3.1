using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.07
    /// 描 述：报表文件管理
    /// </summary>
    public class RptManageBLL :BLLBase, RptManageIBLL, BLL
    {
        private readonly RptManageService rptManageService = new RptManageService();
        private readonly DataItemBLL _dataItemBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataItemBLL"></param>
        public RptManageBLL(DataItemBLL dataItemBLL)
        {
            _dataItemBLL = dataItemBLL;
        }

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<RptManageEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return rptManageService.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取LR_RPT_FileInfo表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<RptManageEntity> GetEntity(string keyValue)
        {
            return rptManageService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取报表文件树
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetFileTree()
        {
            var list = await _dataItemBLL.GetDetailList("ReportSort");
            var fileList = await rptManageService.GetList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_ItemValue;
                node.text = item.F_ItemName;
                node.value = item.F_ItemValue;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = true;
                node.parentId = item.F_ParentId == null ? "0" : item.F_ParentId;
                treeList.Add(node);
            }
            foreach (var file in fileList)
            {
                TreeModel node = new TreeModel();
                node.id = file.F_Id;
                node.text = file.F_Name;
                node.value = file.F_File;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = false;
                node.parentId = file.F_Type;
                treeList.Add(node);
            }
            return treeList.ToTree();
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
            await rptManageService.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, RptManageEntity entity)
        {
            await rptManageService.SaveEntity(keyValue, entity);
        }

        #endregion

    }
}
