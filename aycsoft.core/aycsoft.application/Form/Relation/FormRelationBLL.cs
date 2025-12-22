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
    /// 日 期：2022.11.22
    /// 描 述：表单关联功能
    /// </summary>
    public class FormRelationBLL :BLLBase, FormRelationIBLL,BLL
    {
        private readonly FormRelationService formRelationService = new FormRelationService();

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<FormRelationEntity>> GetPageList(Pagination pagination, string keyword)
        {
            return formRelationService.GetPageList(pagination, keyword);
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FormRelationEntity> GetEntity(string keyValue)
        {
            return formRelationService.GetEntity(keyValue);
        }


        /// <summary>
        /// 获取树形数据列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTree()
        {
            var list = await formRelationService.GetList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_Id;
                node.text = item.F_ModuleId;
                node.value = item.F_FormId;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = true;
                node.parentId = "0";
                treeList.Add(node);
            }
            return treeList.ToTree();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            await formRelationService.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="formRelationEntity">表单与功能信息</param>
        public async Task SaveEntity(string keyValue, FormRelationEntity formRelationEntity)
        {
            await formRelationService.SaveEntity(keyValue, formRelationEntity);
        }
        #endregion
    }
}
