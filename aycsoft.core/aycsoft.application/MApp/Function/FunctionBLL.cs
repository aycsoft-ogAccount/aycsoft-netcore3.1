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
    /// 日 期：2020.04.07
    /// 描 述：移动端功能管理
    /// </summary>
    public class FunctionBLL:BLLBase, FunctionIBLL,BLL
    {
        private readonly FunctionSerivce functionSerivce = new FunctionSerivce();
        private readonly AuthorizeIBLL _authorizeIBLL;
        private readonly DataItemIBLL _dataItemIBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizeIBLL"></param>
        /// <param name="dataItemIBLL"></param>
        public FunctionBLL(AuthorizeIBLL authorizeIBLL, DataItemIBLL dataItemIBLL) {
            _authorizeIBLL = authorizeIBLL;
            _dataItemIBLL = dataItemIBLL;
        }

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="type">分类</param>
        /// <returns></returns>
        public Task<IEnumerable<FunctionEntity>> GetPageList(Pagination pagination, string keyword, string type)
        {
            return functionSerivce.GetPageList(pagination, keyword, type);
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FunctionEntity>> GetList()
        {
            var userInfo = await this.CurrentUser();
            List<FunctionEntity> list = (List<FunctionEntity>)await functionSerivce.GetList();
            /*关联权限*/
            if (userInfo.F_SecurityLevel != 1)
            {
                string roleIds = await this.CurrentUserRoleIds();
                string objectIds = userInfo.F_UserId + (string.IsNullOrEmpty(roleIds) ? "" : ("," + roleIds));
                List<string> itemIdList =(List<string>)await _authorizeIBLL.GetItemIdListByobjectIds(objectIds, 5);
                list = list.FindAll(t => itemIdList.IndexOf(t.F_Id) >= 0);
            }

            return list;
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FunctionEntity> GetEntity(string keyValue)
        {
            return functionSerivce.GetEntity(keyValue);
        }

        /// <summary>
        /// 获取移动功能模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FunctionSchemeEntity> GetScheme(string keyValue)
        {
            return functionSerivce.GetScheme(keyValue);
        }

        /// <summary>
        /// 获取树形移动功能列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetCheckTree()
        {
            List<TreeModel> treeList = new List<TreeModel>();
            var dataItemList = await _dataItemIBLL.GetDetailList("function");
            var list = await GetList();
            Dictionary<string, List<TreeModel>> map = new Dictionary<string, List<TreeModel>>();
            foreach (var item in list)
            {
                if (!map.ContainsKey(item.F_Type))
                {
                    map[item.F_Type] = new List<TreeModel>();
                }
                TreeModel treeItem = new TreeModel
                {
                    id = item.F_Id,
                    text = item.F_Name,
                    value = item.F_Id,
                    showcheck = true,
                    checkstate = 0,
                    parentId = item.F_Type + "_LRDataItem"
                };
                map[item.F_Type].Add(treeItem);
                treeItem.complete = true;
            }
            foreach (var item in dataItemList)
            {
                if (map.ContainsKey(item.F_ItemValue))
                {
                    TreeModel treeItem = new TreeModel
                    {
                        id = item.F_ItemValue + "_LRDataItem",
                        text = item.F_ItemName,
                        value = item.F_ItemValue + "_LRDataItem",
                        showcheck = true,
                        checkstate = 0,
                        parentId = "0",
                        hasChildren = true,
                        complete = true,
                        isexpand = true,
                        ChildNodes = map[item.F_ItemValue]
                    };
                    treeList.Add(treeItem);
                }
            }
            return treeList;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await functionSerivce.Delete(keyValue);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="functionEntity">功能信息</param>
        /// <param name="functionSchemeEntity">功能模板信息</param>
        public async Task SaveEntity(string keyValue, FunctionEntity functionEntity, FunctionSchemeEntity functionSchemeEntity)
        {
            await functionSerivce.SaveEntity(keyValue, functionEntity, functionSchemeEntity);
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="keyValue">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public async Task UpdateState(string keyValue, int state)
        {
            await functionSerivce.UpdateState(keyValue, state);
        }
        #endregion
    }
}
