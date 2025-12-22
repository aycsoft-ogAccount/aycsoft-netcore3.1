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
    /// 日 期：2022.09.10
    /// 描 述：功能模块
    /// </summary>
    public class ModuleBLL : BLLBase, ModuleIBLL, BLL
    {
        private readonly ModuleService moduleService = new ModuleService();
        private readonly AuthorizeIBLL _authorizeIBLL;
        private readonly UserRelationIBLL _userRelationIBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizeIBLL"></param>
        /// <param name="userRelationIBLL"></param>
        public ModuleBLL(AuthorizeIBLL authorizeIBLL, UserRelationIBLL userRelationIBLL) {
            _authorizeIBLL = authorizeIBLL;
            _userRelationIBLL = userRelationIBLL;
        }

        #region 功能模块
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleEntity>> GetModuleList()
        {
            List<ModuleEntity> list = (List<ModuleEntity>)await moduleService.GetList();

            var userInfo = await this.CurrentUser();
            /*关联权限*/
            if (userInfo.F_SecurityLevel != 1)// 不是系统管理员
            {

                string roleIds = await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 1);
                string objectIds = userInfo.F_UserId + (string.IsNullOrEmpty(roleIds) ? "" : ("," + roleIds));
                List<string> itemIdList = (List<string>)await _authorizeIBLL.GetItemIdListByobjectIds(objectIds, 1);
                list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleId) >= 0);
            }

            return list;
        }
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <param name="objectId">对应的角色或用户</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleEntity>> GetModuleList(string objectId)
        {
            List<ModuleEntity> list = (List<ModuleEntity>)await moduleService.GetList();
            List<string> itemIdList = (List<string>)await _authorizeIBLL.GetItemIdListByobjectIds(objectId, 1);
            list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleId) >= 0);
            return list;
        }
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public Task<ModuleEntity> GetModuleByUrl(string url)
        {
            return moduleService.GetEntityByUrl(url);
        }
        /// <summary>
        ///  功能列表(code)
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public Task<ModuleEntity> GetEntityByCode(string code)
        {
            return moduleService.GetEntityByCode(code);
        }

        /// <summary>
        /// 获取功能列表的树形数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetModuleTree()
        {
            List<ModuleEntity> modulelist =(List<ModuleEntity>) await GetModuleList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in modulelist) {
                TreeModel node = new TreeModel();
                node.id = item.F_ModuleId;
                node.text = item.F_FullName;
                node.value = item.F_EnCode;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = (item.F_AllowExpand == 1);
                node.icon = item.F_Icon;
                node.parentId = item.F_ParentId;
                treeList.Add(node);
            }
            return treeList.ToTree();
        }
        /// <summary>
        ///  获取功能列表的树形数据(带勾选框)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetModuleCheckTree()
        {
            List<ModuleEntity> modulelist = (List<ModuleEntity>)await GetModuleList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in modulelist)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_ModuleId;
                node.text = item.F_FullName;
                node.value = item.F_EnCode;
                node.showcheck = true;
                node.checkstate = 0;
                node.isexpand = false;
                node.icon = item.F_Icon;
                node.parentId = item.F_ParentId;
                treeList.Add(node);
            }
            return treeList.ToTree();
        }
        /// <summary>
        /// 获取功能列表的树形数据(只有展开项)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetExpendModuleTree()
        {
            List<ModuleEntity> modulelist = (List<ModuleEntity>)await GetModuleList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in modulelist)
            {
                if (item.F_Target == "expand")
                {
                    TreeModel node = new TreeModel();
                    node.id = item.F_ModuleId;
                    node.text = item.F_FullName;
                    node.value = item.F_EnCode;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.isexpand = true;
                    node.icon = item.F_Icon;
                    node.parentId = item.F_ParentId;
                    treeList.Add(node);
                }
            }
            return treeList.ToTree();
        }
        /// <summary>
        /// 根据父级主键获取数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleEntity>> GetModuleListByParentId(string keyword, string parentId)
        {
            List<ModuleEntity> list = (List<ModuleEntity>)await GetModuleList();
            list = list.FindAll(t => t.F_ParentId == parentId);
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.FindAll(t => t.F_FullName.Contains(keyword) || t.F_EnCode.Contains(keyword));
            }
            return list;
        }
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<ModuleEntity> GetModuleEntity(string keyValue)
        {
            return moduleService.GetEntity(keyValue);
        }
        #endregion

        #region 模块按钮
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleButtonEntity>> GetButtonListNoAuthorize(string moduleId)
        {
            List<ModuleButtonEntity> list = (List<ModuleButtonEntity>)await moduleService.GetButtonList(moduleId);
            return list;
        }
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleButtonEntity>> GetButtonList(string moduleId)
        {
            List<ModuleButtonEntity> list = (List<ModuleButtonEntity>)await moduleService.GetButtonList(moduleId);
            var userInfo = await this.CurrentUser();
            /*关联权限*/
            if (userInfo.F_SecurityLevel != 1)// 不是系统管理员
            {
                string roleIds = await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 1);
                string objectIds = userInfo.F_UserId + (string.IsNullOrEmpty(roleIds) ? "" : ("," + roleIds));
                List<string> itemIdList = (List<string>)await _authorizeIBLL.GetItemIdListByobjectIds(objectIds, 2);
                list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleButtonId) >= 0);
            }

            return list;
        }
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleButtonEntity>> GetButtonList()
        {
            List<ModuleButtonEntity> list = (List<ModuleButtonEntity>)await moduleService.GetButtonList();
            var userInfo = await this.CurrentUser();
            /*关联权限*/
            if (userInfo.F_SecurityLevel != 1)// 不是系统管理员
            {
                string roleIds = await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 1);
                string objectIds = userInfo.F_UserId + (string.IsNullOrEmpty(roleIds) ? "" : ("," + roleIds));
                List<string> itemIdList = (List<string>)await _authorizeIBLL.GetItemIdListByobjectIds(objectIds, 2);
                list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleButtonId) >= 0);
            }

            return list;
        }
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleButtonEntity>> GetButtonListByUrl(string url)
        {
            ModuleEntity moduleEntity = await GetModuleByUrl(url);
            if (moduleEntity == null)
            {
                return new List<ModuleButtonEntity>();
            }
            return await GetButtonList(moduleEntity.F_ModuleId);
        }
        /// <summary>
        /// 获取按钮列表树形数据（基于功能模块）
        /// </summary>
        /// <param name="objectId">需要设置的角色对象</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetButtonCheckTree(string objectId)
        {
            List<ModuleEntity> modulelist =(List<ModuleEntity>)await GetModuleList(objectId);
            List<TreeModel> treeList = new List<TreeModel>();
            List<ModuleButtonEntity> buttonAllList = (List<ModuleButtonEntity>)await GetButtonList();
            foreach (var module in modulelist)
            {
                TreeModel node = new TreeModel();
                node.id = module.F_ModuleId + "_aycsoft_moduleId";
                node.text = module.F_FullName;
                node.value = module.F_EnCode;
                node.showcheck = true;
                node.checkstate = 0;
                node.isexpand = true;
                node.icon = module.F_Icon;
                node.parentId = module.F_ParentId + "_aycsoft_moduleId";
                if (module.F_Target != "expand")
                {
                    List<ModuleButtonEntity> buttonList = buttonAllList.FindAll(t=>t.F_ModuleId == module.F_ModuleId);
                    if (buttonList.Count > 0)
                    {
                        treeList.Add(node);
                    }
                    foreach (var button in buttonList)
                    {
                        TreeModel buttonNode = new TreeModel();
                        buttonNode.id = button.F_ModuleButtonId;
                        buttonNode.text = button.F_FullName;
                        buttonNode.value = button.F_EnCode;
                        buttonNode.showcheck = true;
                        buttonNode.checkstate = 0;
                        buttonNode.isexpand = true;
                        buttonNode.icon = "fa fa-wrench";
                        buttonNode.parentId = (button.F_ParentId == "0" ? button.F_ModuleId + "_aycsoft_moduleId" : button.F_ParentId);
                        treeList.Add(buttonNode);
                    }
                }
                else
                {
                    node.isexpand = false;
                    treeList.Add(node);
                }
            }
            return treeList.ToTree();
        }
        #endregion

        #region 模块视图
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleColumnEntity>> GetColumnList(string moduleId)
        {
            List<ModuleColumnEntity> list = (List<ModuleColumnEntity>)await moduleService.GetColumnList(moduleId);
            var userInfo = await this.CurrentUser();
            /*关联权限*/
            if (userInfo.F_SecurityLevel != 1)// 不是系统管理员
            {

                string roleIds = await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 1);
                string objectIds = userInfo.F_UserId + (string.IsNullOrEmpty(roleIds) ? "" : ("," + roleIds));
                List<string> itemIdList = (List<string>)await _authorizeIBLL.GetItemIdListByobjectIds(objectIds, 3);
                list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleColumnId) >= 0);
            }

            return list;
        }
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleColumnEntity>> GetColumnList()
        {
            List<ModuleColumnEntity> list = (List<ModuleColumnEntity>)await moduleService.GetColumnList();
            var userInfo = await this.CurrentUser();
            /*关联权限*/
            if (userInfo.F_SecurityLevel != 1)// 不是系统管理员
            {

                string roleIds = await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 1);
                string objectIds = userInfo.F_UserId + (string.IsNullOrEmpty(roleIds) ? "" : ("," + roleIds));
                List<string> itemIdList = (List<string>)await _authorizeIBLL.GetItemIdListByobjectIds(objectIds, 3);
                list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleColumnId) >= 0);
            }

            return list;
        }
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleColumnEntity>> GetColumnListByUrl(string url)
        {
            ModuleEntity moduleEntity =await GetModuleByUrl(url);
            if (moduleEntity == null)
            {
                return new List<ModuleColumnEntity>();
            }
            return await GetColumnList(moduleEntity.F_ModuleId);
        }
        /// <summary>
        /// 获取按钮列表树形数据（基于功能模块）
        /// </summary>
        /// <param name="objectId">需要设置的角色对象</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetColumnCheckTree(string objectId)
        {
            List<ModuleEntity> modulelist =(List<ModuleEntity>) await GetModuleList(objectId);
            List<TreeModel> treeList = new List<TreeModel>();
            List<ModuleColumnEntity> columnAllList = (List<ModuleColumnEntity>)await GetColumnList();
            foreach (var module in modulelist)
            {
                TreeModel node = new TreeModel();
                node.id = module.F_ModuleId + "_aycsoft_moduleId";
                node.text = module.F_FullName;
                node.value = module.F_EnCode;
                node.showcheck = true;
                node.checkstate = 0;
                node.isexpand = true;
                node.icon = module.F_Icon;
                node.parentId = module.F_ParentId + "_aycsoft_moduleId";

                if (module.F_Target != "expand")
                {
                    List<ModuleColumnEntity> columnList = columnAllList.FindAll(t=>t.F_ModuleId == module.F_ModuleId);
                    if (columnList.Count > 0)
                    {
                        treeList.Add(node);
                    }
                    foreach (var column in columnList)
                    {
                        TreeModel columnNode = new TreeModel();
                        columnNode.id = column.F_ModuleColumnId;
                        columnNode.text = column.F_FullName;
                        columnNode.value = column.F_EnCode;
                        columnNode.showcheck = true;
                        columnNode.checkstate = 0;
                        columnNode.isexpand = true;
                        columnNode.icon = "fa fa-filter";
                        columnNode.parentId = column.F_ModuleId + "_aycsoft_moduleId";
                        treeList.Add(columnNode);
                    }
                }
                else
                {
                    treeList.Add(node);
                }
            }
            return treeList.ToTree();
        }
        #endregion

        #region 模块表单
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleFormEntity>> GetFormList(string moduleId)
        {
            List<ModuleFormEntity> list = (List<ModuleFormEntity>)await moduleService.GetFormList(moduleId);

            var userInfo = await this.CurrentUser();
            /*关联权限*/
            if (userInfo.F_SecurityLevel != 1)// 不是系统管理员
            {

                string roleIds = await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 1);
                string objectIds = userInfo.F_UserId + (string.IsNullOrEmpty(roleIds) ? "" : ("," + roleIds));
                List<string> itemIdList = (List<string>)await _authorizeIBLL.GetItemIdListByobjectIds(objectIds, 4);
                list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleFormId) >= 0);
            }

            return list;
        }
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleFormEntity>> GetFormList()
        {
            List<ModuleFormEntity> list = (List<ModuleFormEntity>)await moduleService.GetFormList();

            var userInfo = await this.CurrentUser();
            /*关联权限*/
            if (userInfo.F_SecurityLevel != 1)// 不是系统管理员
            {

                string roleIds = await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 1);
                string objectIds = userInfo.F_UserId + (string.IsNullOrEmpty(roleIds) ? "" : ("," + roleIds));
                List<string> itemIdList = (List<string>)await _authorizeIBLL.GetItemIdListByobjectIds(objectIds, 4);
                list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleFormId) >= 0);
            }

            return list;
        }
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleFormEntity>> GetFormListByUrl(string url)
        {
            ModuleEntity moduleEntity = await GetModuleByUrl(url);
            if (moduleEntity == null)
            {
                return new List<ModuleFormEntity>();
            }
            return await GetFormList(moduleEntity.F_ModuleId);
        }
        /// <summary>
        /// 获取表单字段树形数据（基于功能模块）
        /// </summary>
        /// <param name="objectId">需要设置的角色对象</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetFormCheckTree(string objectId)
        {
            List<ModuleEntity> modulelist =(List<ModuleEntity>) await GetModuleList(objectId);
            List<TreeModel> treeList = new List<TreeModel>();
            List<ModuleFormEntity> columnAllList = (List<ModuleFormEntity>)await GetFormList();
            foreach (var module in modulelist)
            {
                TreeModel node = new TreeModel();
                node.id = module.F_ModuleId + "_aycsoft_moduleId";
                node.text = module.F_FullName;
                node.value = module.F_EnCode;
                node.showcheck = true;
                node.checkstate = 0;
                node.isexpand = true;
                node.icon = module.F_Icon;
                node.parentId = module.F_ParentId + "_aycsoft_moduleId";

                if (module.F_Target != "expand")
                {
                    List<ModuleFormEntity> columnList = columnAllList.FindAll(t=>t.F_ModuleId == module.F_ModuleId);
                    if (columnList.Count > 0)
                    {
                        treeList.Add(node);
                    }
                    foreach (var column in columnList)
                    {
                        TreeModel columnNode = new TreeModel();
                        columnNode.id = column.F_ModuleFormId;
                        columnNode.text = column.F_FullName;
                        columnNode.value = column.F_EnCode;
                        columnNode.showcheck = true;
                        columnNode.checkstate = 0;
                        columnNode.isexpand = true;
                        columnNode.icon = "fa fa-filter";
                        columnNode.parentId = column.F_ModuleId + "_aycsoft_moduleId";
                        treeList.Add(columnNode);
                    }
                }
                else
                {
                    treeList.Add(node);
                }
            }
            return treeList.ToTree();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模块功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public async Task<bool> Delete(string keyValue)
        {
            List<ModuleEntity> list = (List<ModuleEntity>) await GetModuleListByParentId("", keyValue);
            if (list.Count > 0)
            {
                return false;
            }
            await moduleService.Delete(keyValue);
            return true;
        }
        /// <summary>
        /// 保存模块功能实体（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">实体</param>
        /// <param name="moduleButtonEntitys">按钮列表</param>
        /// <param name="moduleColumnEntitys">视图列集合</param>
        /// <param name="moduleFormEntitys">表单字段集合</param>
        public async Task SaveEntity(string keyValue, ModuleEntity moduleEntity, List<ModuleButtonEntity> moduleButtonEntitys, List<ModuleColumnEntity> moduleColumnEntitys, List<ModuleFormEntity> moduleFormEntitys)
        {
            await moduleService.SaveEntity(keyValue, moduleEntity, moduleButtonEntitys, moduleColumnEntitys, moduleFormEntitys);
        }
        #endregion
    }
}
