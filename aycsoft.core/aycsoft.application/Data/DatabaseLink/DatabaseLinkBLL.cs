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
    /// 描 述：数据库连接
    /// </summary>
    public class DatabaseLinkBLL : BLLBase, DatabaseLinkIBLL, BLL
    {
        #region 属性
        private readonly DatabaseLinkService databaseLinkService = new DatabaseLinkService();
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<DatabaseLinkEntity>> GetList()
        {
            return databaseLinkService.GetList();
        }

        /// <summary>
        /// 获取列表数据(去掉连接串地址信息)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DatabaseLinkEntity>> GetListByNoConnection()
        {
            var list = await GetList();
            foreach (var item in list)
            {
                item.F_DbConnection = "******";
            }
            return list;
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public async Task<IEnumerable<DatabaseLinkEntity>> GetListByNoConnection(string keyword)
        {
            List<DatabaseLinkEntity> list = (List<DatabaseLinkEntity>)await GetListByNoConnection();

            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.FindAll(t => t.F_DBName.Contains(keyword) || t.F_DBAlias.Contains(keyword) || t.F_ServerAddress.Contains(keyword));
            }
            return list;
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTreeList()
        {
            var list = await GetList();
            List<TreeModel> treelist = new List<TreeModel>();
            Dictionary<string, List<TreeModel>> dic = new Dictionary<string, List<TreeModel>>();

            TreeModel mynode = new TreeModel();
            mynode.id = "lrsystemdb";
            mynode.text = "本地数据库";
            mynode.value = "lrsystemdb";
            mynode.complete = true;
            mynode.hasChildren = false;
            treelist.Add(mynode);
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_DBName;
                node.text = item.F_DBAlias;
                node.value = item.F_DBName;
                node.complete = true;
                node.hasChildren = false;

                if (!dic.ContainsKey(item.F_ServerAddress))
                {
                    TreeModel pnode = new TreeModel();
                    pnode.id = item.F_ServerAddress;
                    pnode.text = item.F_ServerAddress;
                    pnode.value = "aycsoftServerAddress";
                    pnode.isexpand = true;
                    pnode.complete = true;
                    pnode.hasChildren = true;
                    pnode.ChildNodes = new List<TreeModel>();
                    treelist.Add(pnode);
                    dic.Add(item.F_ServerAddress, pnode.ChildNodes);
                }
                dic[item.F_ServerAddress].Add(node);
            }
            return treelist;
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTreeListEx()
        {
            var list = await GetList();
            List<TreeModel> treelist = new List<TreeModel>();
            Dictionary<string, List<TreeModel>> dic = new Dictionary<string, List<TreeModel>>();

            TreeModel mynode = new TreeModel();
            mynode.id = "lrsystemdb";
            mynode.text = "本地数据库";
            mynode.value = "lrsystemdb";
            mynode.complete = false;
            mynode.isexpand = false;
            mynode.hasChildren = true;
            treelist.Add(mynode);

            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_DBName;
                node.text = item.F_DBAlias;
                node.value = item.F_DBName;
                node.complete = false;
                node.isexpand = false;
                node.hasChildren = true;

                if (!dic.ContainsKey(item.F_ServerAddress))
                {
                    TreeModel pnode = new TreeModel();
                    pnode.id = item.F_ServerAddress;
                    pnode.text = item.F_ServerAddress;
                    pnode.value = "aycsoftServerAddress";
                    pnode.isexpand = true;
                    pnode.complete = true;
                    pnode.hasChildren = true;
                    pnode.ChildNodes = new List<TreeModel>();
                    treelist.Add(pnode);
                    dic.Add(item.F_ServerAddress, pnode.ChildNodes);
                }
                dic[item.F_ServerAddress].Add(node);
            }
            return treelist;
        }
        /// <summary>
        /// 获取数据连接实体
        /// </summary>
        /// <param name="databaseLinkId">主键</param>
        /// <returns></returns>
        public Task<DatabaseLinkEntity> GetEntity(string databaseLinkId)
        {
            return databaseLinkService.GetEntity(databaseLinkId);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除自定义查询条件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await databaseLinkService.Delete(keyValue);
        }
        /// <summary>
        /// 保存自定义查询（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="databaseLinkEntity">部门实体</param>
        /// <returns></returns>
        public async Task<bool> SaveEntity(string keyValue, DatabaseLinkEntity databaseLinkEntity)
        {
            return await databaseLinkService.SaveEntity(keyValue, databaseLinkEntity);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 测试数据数据库是否能连接成功
        /// </summary>
        /// <param name="connection">连接串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="keyValue">主键</param>
        public async Task<string> TestConnection(string connection, string dbType, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue) && connection == "******")
            {
                DatabaseLinkEntity entity = await GetEntity(keyValue);
                connection = entity.F_DbConnection;
            }

            return databaseLinkService.TestConnection(connection, dbType);
        }
        #endregion
    }
}
