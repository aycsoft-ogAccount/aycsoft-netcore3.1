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
    /// 描 述：数据字典管理
    /// </summary>
    public class DataItemBLL : BLLBase, DataItemIBLL, BLL
    {
        #region 属性
        private readonly DataItemService dataItemService = new DataItemService();
        #endregion

        #region 数据字典分类
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DataItemEntity>> GetClassifyList()
        {
            return dataItemService.GetClassifyList();
        }

        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="keyword">关键词（名称/编码）</param>
        /// <param name="enabledMark">是否只取有效</param>
        /// <returns></returns>
        public async Task<IEnumerable<DataItemEntity>> GetClassifyList(string keyword, bool enabledMark = true)
        {
            List<DataItemEntity> list = (List<DataItemEntity>)await GetClassifyList();
            if (enabledMark)
            {
                list = list.FindAll(t => t.F_EnabledMark.Equals(1));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.FindAll(t => t.F_ItemName.Contains(keyword) || t.F_ItemCode.Contains(keyword));
            }

            return list;
        }
        /// <summary>
        /// 获取分类树形数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetClassifyTree()
        {
            List<DataItemEntity> classifyList = (List<DataItemEntity>)await GetClassifyList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in classifyList)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_ItemId;
                node.text = item.F_ItemName;
                node.value = item.F_ItemCode;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = true;
                node.parentId = item.F_ParentId;
                treeList.Add(node);
            }
            return treeList.ToTree();
        }
        /// <summary>
        /// 保存分类数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public async Task SaveClassifyEntity(string keyValue, DataItemEntity entity)
        {
            await dataItemService.SaveClassifyEntity(keyValue, entity);
        }
        /// <summary>
        /// 删除分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteClassify(string keyValue)
        {
            await dataItemService.DeleteClassify(keyValue);
        }
        /// <summary>
        /// 通过编号获取字典分类实体
        /// </summary>
        /// <param name="itemCode">编码</param>
        /// <returns></returns>
        public Task<DataItemEntity> GetClassifyEntityByCode(string itemCode)
        {
            return dataItemService.GetClassifyEntityByCode(itemCode);
        }
        #endregion

        #region 字典明细
        /// <summary>
        /// 获取数据字典明显
        /// </summary>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        public Task<IEnumerable<DataItemDetailEntity>> GetDetailList(string itemCode)
        {
            return dataItemService.GetDetailList(itemCode);
        }
        /// <summary>
        /// 获取数据字典明显
        /// </summary>
        /// <param name="itemCode">分类编码</param>
        /// <param name="keyword">关键词（名称/值）</param>
        /// <returns></returns>
        public async Task<IEnumerable<DataItemDetailEntity>> GetDetailList(string itemCode, string keyword)
        {
            List<DataItemDetailEntity> list = (List<DataItemDetailEntity>)await GetDetailList(itemCode);
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.FindAll(t => t.F_ItemName.Contains(keyword) || t.F_ItemValue.Contains(keyword));
            }
            return list;
        }
        /// <summary>
        /// 获取数据字典明显
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public async Task<IEnumerable<DataItemDetailEntity>> GetDetailListByParentId(string itemCode, string parentId)
        {
            List<DataItemDetailEntity> list = (List<DataItemDetailEntity>)await GetDetailList(itemCode);
            if (!string.IsNullOrEmpty(parentId))
            {
                list = list.FindAll(t => t.F_ParentId.ContainsEx(parentId));
            }
            else
            {
                list = list.FindAll(t => t.F_ParentId.ContainsEx("0"));
            }

            return list;
        }
        /// <summary>
        /// 获取字典明细树形数据
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetDetailTree(string itemCode)
        {
            List<DataItemDetailEntity> list = (List<DataItemDetailEntity>)await GetDetailList(itemCode);
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_ItemDetailId;
                node.text = item.F_ItemName;
                node.value = item.F_ItemValue;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = true;
                node.parentId = item.F_ParentId == null ? "0" : item.F_ParentId;
                treeList.Add(node);
            }
            return treeList.ToTree();
        }
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemValue">项目值</param>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        public async Task<bool> ExistDetailItemValue(string keyValue, string itemValue, string itemCode)
        {
            bool res = false;
            List<DataItemDetailEntity> list = (List<DataItemDetailEntity>)await GetDetailList(itemCode);

            if (string.IsNullOrEmpty(keyValue))
            {
                res = list.FindAll(t => t.F_ItemValue.Equals(itemValue)).Count <= 0;
            }
            else
            {
                res = list.FindAll(t => t.F_ItemValue.Equals(itemValue) && !t.F_ItemDetailId.Equals(keyValue)).Count <= 0;
            }
            return res;
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemName">项目名</param>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        public async Task<bool> ExistDetailItemName(string keyValue, string itemName, string itemCode)
        {
            bool res = false;
            List<DataItemDetailEntity> list = (List<DataItemDetailEntity>)await GetDetailList(itemCode);

            if (string.IsNullOrEmpty(keyValue))
            {
                res = list.FindAll(t => t.F_ItemName.Equals(itemName)).Count <= 0;
            }
            else
            {
                res = list.FindAll(t => t.F_ItemName.Equals(itemName) && !t.F_ItemDetailId.Equals(keyValue)).Count <= 0;
            }
            return res;
        }
        /// <summary>
        /// 保存明细数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public async Task SaveDetailEntity(string keyValue, DataItemDetailEntity entity)
        {
            DataItemEntity classifyEntity = await dataItemService.GetClassifyEntityByKey(entity.F_ItemId);
            if (classifyEntity.F_IsTree != 1 || string.IsNullOrEmpty(entity.F_ParentId))
            {
                entity.F_ParentId = "0";
            }
            await dataItemService.SaveDetailEntity(keyValue, entity);
        }
        /// <summary>
        /// 虚拟删除明细数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteDetail(string keyValue)
        {
            await dataItemService.DeleteDetail(keyValue);
        }
        #endregion
    }
}
