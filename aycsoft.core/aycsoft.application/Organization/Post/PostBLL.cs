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
    /// 描 述：岗位管理
    /// </summary>
    public class PostBLL :BLLBase , PostIBLL ,BLL
    {
        private readonly PostService postService = new PostService();
        private readonly DepartmentIBLL _departmentIBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentIBLL"></param>
        public PostBLL(DepartmentIBLL departmentIBLL) {
            _departmentIBLL = departmentIBLL;
        }


        #region 获取数据
        /// <summary>
        /// 获取岗位数据列表（根据公司列表）
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        public Task<IEnumerable<PostEntity>> GetList(string companyId)
        {
            return postService.GetList(companyId);
        }
        /// <summary>
        /// 获取岗位数据列表（根据公司列表）
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门Id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<PostEntity>> GetList(string companyId, string departmentId, string keyword)
        {
            return postService.GetList(companyId, departmentId, keyword);
        }
        /// <summary>
        /// 获取岗位数据列表(根据主键串)
        /// </summary>
        /// <param name="postIds">根据主键串</param>
        /// <returns></returns>
        public Task<IEnumerable<PostEntity>> GetListByPostIds(string postIds)
        {
            return postService.GetListByPostIds(postIds);
        }
        /// <summary>
        /// 获取树形结构数据
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTree(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                return new List<TreeModel>();
            }
            List<PostEntity> list =(List<PostEntity>)await GetList(companyId);
            List<TreeModel> treeList = new List<TreeModel>();

            List<string> dList = new List<string>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_PostId;
                node.text = item.F_Name;
                dList.Add(item.F_DepartmentId);

                node.value = item.F_DepartmentId;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = true;
                node.parentId = item.F_ParentId;
                treeList.Add(node);
            }
            if (dList.Count > 0) {
                List<DepartmentEntity> departmentList =(List<DepartmentEntity>) await _departmentIBLL.GetListByKeys(dList);
                foreach (var item in treeList)
                {
                    DepartmentEntity departmentEntity = departmentList.Find(t => t.F_DepartmentId == item.value);
                    item.value = item.id;
                    if (departmentEntity != null)
                    {
                        item.text = "【" + departmentEntity.F_FullName + "】" + item.text;
                    }
                }
            }


            return treeList.ToTree();
        }
        /// <summary>
        /// 获取岗位实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<PostEntity> GetEntity(string keyValue) {
            return postService.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await postService.Delete(keyValue);
        }
        /// <summary>
        /// 保存岗位（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="postEntity">岗位实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, PostEntity postEntity)
        {
            await postService.SaveEntity(keyValue, postEntity);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 判断是否是有关联
        /// </summary>
        /// <param name="beginId">开始岗位主键</param>
        /// <param name="map">对方的岗位集合</param>
        /// <returns></returns>
        private async Task<bool> HasRelation(string beginId, Dictionary<string,int> map) {
            bool res = false;
            var entity = await postService.GetEntity(beginId);
            if (entity == null || entity.F_ParentId == "0")
            {
                res = false;
            }
            else if (map.ContainsKey(entity.F_ParentId))
            {
                res = true;
            }
            else {
                res =await HasRelation(entity.F_ParentId, map);
            }
            return res;
        }

        /// <summary>
        /// 判断是否是上级
        /// </summary>
        /// <param name="myId">自己的岗位</param>
        /// <param name="otherId">对方的岗位</param>
        /// <returns></returns>
        public async Task<bool> IsUp(string myId,string otherId) {
            bool res = false;
            if (!string.IsNullOrEmpty(myId) && !string.IsNullOrEmpty(otherId)) {
                string[] myList = myId.Split(',');
                string[] otherList = myId.Split(',');
                Dictionary<string, int> map = new Dictionary<string, int>();
                foreach (var otherItem in otherList)
                {
                    if (!map.ContainsKey(otherItem)) {
                        map.Add(otherItem, 1);
                    }
                }
                foreach (var myItem in myList) {
                    if (await HasRelation(myItem, map)) {
                        res = true;
                        break;
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// 判断是否是下级
        /// </summary>
        /// <param name="myId">自己的岗位</param>
        /// <param name="otherId">对方的岗位</param>
        /// <returns></returns>
        public async Task<bool> IsDown(string myId, string otherId)
        {
            bool res = false;
            if (!string.IsNullOrEmpty(myId) && !string.IsNullOrEmpty(otherId))
            {
                string[] myList = myId.Split(',');
                string[] otherList = myId.Split(',');
                Dictionary<string, int> map = new Dictionary<string, int>();
                 foreach (var myItem in myList)
                    {
                    if (!map.ContainsKey(myItem))
                    {
                        map.Add(myItem, 1);
                    }
                }
                foreach (var otherItem in otherList)
                {
                    if (await HasRelation(otherItem, map))
                    {
                        res = true;
                        break;
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// 获取上级岗位人员ID
        /// </summary>
        /// <param name="strPostIds">岗位id</param>
        /// <param name="level">级数</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetUpIdList(string strPostIds, int level) {
            List<string> res = new List<string>();
            if (!string.IsNullOrEmpty(strPostIds) && level > 0 && level < 6) {// 现在支持1-5级查找
                string[] postIdList = strPostIds.Split(',');
                bool isHave = false; // 判断是否指定级数的职位
                foreach (var postId in postIdList) {
                    isHave = false;
                    var entity = await postService.GetEntity(postId);
                    if (entity != null) {
                        string parentId = entity.F_ParentId;
                        PostEntity parentEntity = null;
                        for (int i = 0; i < level; i++)
                        {
                            parentEntity = await postService.GetEntity(parentId);
                            if (parentEntity != null)
                            {
                                parentId = parentEntity.F_ParentId;
                                if (i == (level - 1))
                                {
                                    isHave = true;
                                }
                            }
                            else {
                                break;
                            }
                        }
                        if (isHave)
                        {
                            if (parentEntity != null) {
                                res.Add(parentEntity.F_PostId);
                            }
                        }
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// 获取下级岗位人员ID
        /// </summary>
        /// <param name="strPostIds">岗位id</param>
        /// <param name="level">级数</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetDownIdList(string strPostIds, int level)
        {
            List<string> res = new List<string>();
            if (!string.IsNullOrEmpty(strPostIds) && level > 0 && level < 6)
            {// 现在支持1-5级查找
                string[] postIdList = strPostIds.Split(',');
                bool isHave = false; // 判断是否指定级数的职位
                List<string> parentList = new List<string>();
                parentList.AddRange(postIdList);
                for (int i = 0; i < level; i++)
                {
                    parentList =(List<string>) await postService.GetIdList(parentList);
                    if (parentList.Count > 0)
                    {
                        if (i == (level - 1))
                        {
                            isHave = true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (isHave)
                {
                    res.AddRange(parentList);
                }
            }
            return res;
        }
        #endregion
    }
}
