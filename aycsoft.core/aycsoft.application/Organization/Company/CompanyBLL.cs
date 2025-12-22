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
    /// 描 述：公司管理
    /// </summary>
    public class CompanyBLL : CompanyIBLL, BLL
    {
        #region 属性
        private readonly CompanyService companyService = new CompanyService();
        private readonly UserIBLL _userIBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIBLL"></param>
        public CompanyBLL(UserIBLL userIBLL) {
            _userIBLL = userIBLL;
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取公司列表数据
        /// </summary>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<CompanyEntity>> GetList(string keyWord = "")
        {
            return companyService.GetList(keyWord);
        }

        /// <summary>
        /// 获取子公司列表信息
        /// </summary>
        /// <param name="pId">父级Id</param>
        /// <returns></returns>
        public Task<IEnumerable<CompanyEntity>> GetListByPId(string pId) {
            return companyService.GetListByPId(pId);
        }

        /// <summary>
        /// 获取公司信息实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<CompanyEntity> GetEntity(string keyValue)
        {
            return companyService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTree(string parentId) {
            List<CompanyEntity> list = (List<CompanyEntity>)await GetList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel
                {
                    id = item.F_CompanyId,
                    text = item.F_FullName,
                    value = item.F_CompanyId,
                    showcheck = false,
                    checkstate = 0,
                    isexpand = true,
                    parentId = item.F_ParentId
                };
                treeList.Add(node);
            }
            return treeList.ToTree(parentId);
        }
        /// <summary>
        /// 获取公司本身和子公司的id
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns></returns>
        public async  Task<IEnumerable<string>> GetSubNodes(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                return new List<string>();
            }
            List<string> res = new List<string>();
            res.Add(parentId);
            List<TreeModel> list = (List<TreeModel>)await GetTree(parentId);
            GetSubNodes(list, res);
            return res;
        }
        /// <summary>
        /// 遍历树形数据获取全部子节点ID
        /// </summary>
        /// <param name="list">树形数据列表</param>
        /// <param name="outList">输出数据列表</param>
        private void GetSubNodes(List<TreeModel> list, List<string> outList) {
            foreach (var item in list) {
                outList.Add(item.id);
                if (item.hasChildren) {
                    GetSubNodes(item.ChildNodes, outList);
                }
            }
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await companyService.Delete(keyValue);
        }
        /// <summary>
        /// 保存公司信息（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="companyEntity">公司实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, CompanyEntity companyEntity)
        {                 
            await companyService.SaveEntity(keyValue, companyEntity);
        }
        #endregion
    }
}
