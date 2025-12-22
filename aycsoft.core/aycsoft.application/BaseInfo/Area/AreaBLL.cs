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
    /// 日 期：2022.09.18
    /// 描 述：行政区域
    /// </summary>
    public class AreaBLL : BLLBase, AreaIBLL,BLL
    {
        private readonly AreaService areaService = new AreaService();

        #region 获取数据
        /// <summary>
        /// 获取区域列表数据
        /// </summary>
        /// <param name="parentId">父节点主键（0表示顶层）</param>
        /// <param name="keyword">关键字查询（名称/编号）</param>
        /// <returns></returns>
        public Task<IEnumerable<AreaEntity>> GetList(string parentId, string keyword = "")
        {
            return areaService.GetList(parentId, keyword);
        }
        /// <summary>
        /// 区域实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<AreaEntity> GetEntity(string keyValue)
        {
            return areaService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取区域数据树（某一级的）
        /// </summary>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTree(string parentId)
        {
            List<TreeModel> treeList = new List<TreeModel>();
            var list =(List<AreaEntity>) await GetList(parentId);

            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_AreaId;
                node.text = item.F_AreaName;
                node.value = item.F_AreaCode;
                node.showcheck = false;
                node.checkstate = 0;
                node.hasChildren = true;
                node.isexpand = false;
                node.complete = false;
                treeList.Add(node);
            }
            return treeList;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await areaService.Delete(keyValue);
        }
        /// <summary>
        /// 保存区域表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="areaEntity">区域实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, AreaEntity areaEntity)
        {
            if (areaEntity.F_ParentId != "0")
            {
                AreaEntity entity = await GetEntity(areaEntity.F_ParentId);
                if (entity != null)
                {
                    areaEntity.F_Layer = entity.F_Layer + 1;
                }
            }
            else
            {
                areaEntity.F_Layer = 1;
            }
            await areaService.SaveEntity(keyValue, areaEntity);
        }
        #endregion
    }
}
