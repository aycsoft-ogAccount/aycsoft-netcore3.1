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
    /// 日 期：2022.09.24
    /// 描 述：部门管理
    /// </summary>
    public class DepartmentBLL : DepartmentIBLL, BLL
    {
        #region 属性
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly UserIBLL _userIBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIBLL"></param>
        public DepartmentBLL(UserIBLL userIBLL)
        {
            _userIBLL = userIBLL;
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取部门列表信息(根据公司Id)
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentEntity>> GetList(string companyId)
        {
            return departmentService.GetList(companyId);
        }
        /// <summary>
        /// 获取部门列表信息(根据公司Id)
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public async Task<IEnumerable<DepartmentEntity>> GetList(string companyId, string keyWord)
        {
            List<DepartmentEntity> list = (List<DepartmentEntity>)await GetList(companyId);
            if (!string.IsNullOrEmpty(keyWord))
            {
                list = list.FindAll(t => t.F_FullName.Contains(keyWord) || t.F_EnCode.Contains(keyWord) || t.F_ShortName.Contains(keyWord));
            }
            return list;
        }

        /// <summary>
        /// 获取部门列表信息(根据公司Id 和 父级 id)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="pid">父级 id</param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentEntity>> GetListByPid(string companyId, string pid)
        {
            return departmentService.GetListByPid(companyId, pid);
        }

        /// <summary>
        /// 获取部门列表信息
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentEntity>> GetAllList(string keyWord) {
            return departmentService.GetAllList(keyWord);
        }

        /// <summary>
        /// 获取部门列表信息(根据部门ID集合)
        /// </summary>
        /// <param name="keyValues">部门ID集合</param>
        /// <returns></returns>
        public Task<IEnumerable<DepartmentEntity>> GetListByKeys(List<string> keyValues)
        {
            return departmentService.GetListByKeys(keyValues);
        }
        /// <summary>
        /// 获取部门数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DepartmentEntity> GetEntity(string keyValue)
        {
            return departmentService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="companyId">公司id</param>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTree(string companyId, string parentId)
        {
            if (string.IsNullOrEmpty(companyId))
            {// 如果公司主键没有的话，需要加载公司信息
                return new List<TreeModel>();
            }

            List<DepartmentEntity> list = (List<DepartmentEntity>)await GetList(companyId);
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel
                {
                    id = item.F_DepartmentId,
                    text = item.F_FullName,
                    value = item.F_DepartmentId,
                    showcheck = false,
                    checkstate = 0,
                    isexpand = true,
                    parentId = item.F_ParentId,
                    exid1 = item.F_EnCode,
                    exid2 = "d"
                };

                treeList.Add(node);
            }
            return treeList.ToTree(parentId);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="companylist">公司数据列表</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTree(IEnumerable<CompanyEntity> companylist)
        {
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var companyone in companylist)
            {
                List<TreeModel> departmentTree = (List<TreeModel>)await GetTree(companyone.F_CompanyId, "");
                if (departmentTree.Count > 0)
                {
                    TreeModel node = new TreeModel
                    {
                        id = companyone.F_CompanyId,
                        text = companyone.F_FullName,
                        value = companyone.F_CompanyId,
                        showcheck = false,
                        checkstate = 0,
                        isexpand = true,
                        parentId = "0",
                        hasChildren = true,
                        ChildNodes = departmentTree,
                        complete = true,
                        exid1 = companyone.F_EnCode,
                        exid2 = "c"
                    };
                    treeList.Add(node);
                }
            }
            return treeList;
        }

        /// <summary>
        /// 获取部门本身和子部门的id
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="parentId">父级ID</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetSubNodes(string companyId, string parentId)
        {
            if (string.IsNullOrEmpty(parentId) || string.IsNullOrEmpty(companyId))
            {
                return new List<string>();
            }
            List<string> res = new List<string>();
            res.Add(parentId);
            List<TreeModel> list = (List<TreeModel>)await GetTree(companyId, parentId);
            GetSubNodes(list, res);
            return res;
        }
        /// <summary>
        /// 遍历树形数据获取全部子节点ID
        /// </summary>
        /// <param name="list">树形数据列表</param>
        /// <param name="ourList">输出数据列表</param>
        private void GetSubNodes(List<TreeModel> list, List<string> ourList)
        {
            foreach (var item in list)
            {
                ourList.Add(item.id);
                if (item.hasChildren)
                {
                    GetSubNodes(item.ChildNodes, ourList);
                }
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await departmentService.Delete(keyValue);
        }
        /// <summary>
        /// 保存部门信息（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, DepartmentEntity departmentEntity)
        {
            await departmentService.SaveEntity(keyValue, departmentEntity);
        }
        #endregion
    }
}
