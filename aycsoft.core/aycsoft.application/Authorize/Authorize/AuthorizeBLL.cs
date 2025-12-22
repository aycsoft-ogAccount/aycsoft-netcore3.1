using ce.autofac.extension;
using aycsoft.iapplication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.10
    /// 描 述：授權功能
    /// </summary>
    public class AuthorizeBLL : BLLBase, AuthorizeIBLL, BLL
    {
        private readonly AuthorizeService authorizeService = new AuthorizeService();
       
        #region 获取数据
        /// <summary>
        /// 获取被授权的功能的主键字串
        /// </summary>
        /// <param name="objectId">对象主键（角色,用户）</param>
        /// <param name="itemType">项目类型:1-菜单2-按钮3-视图</param>
        /// <returns></returns>
        public Task<IEnumerable<AuthorizeEntity>> GetList(string objectId, int itemType)
        {
            return authorizeService.GetList(objectId, itemType);
        }
        /// <summary>
        /// 获取被授权的功能的主键字串
        /// </summary>
        /// <param name="objectIds">对象主键串（角色,用户）</param>
        /// <param name="itemType">功能类型1菜单功能2按钮3视图列表</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetItemIdListByobjectIds(string objectIds, int itemType)
        {
            List<string> reslist = new List<string>();
            string[] objectIdlist = objectIds.Split(',');

            foreach (string objectId in objectIdlist)
            {
                var list = await authorizeService.GetList(objectId, itemType);
                foreach (var item in list) {
                    reslist.Add(item.F_ItemId);
                }
            }
            return reslist;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加授权
        /// </summary>
        /// <param name="objectType">权限分类-1角色2用户</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="type">功能类型</param>
        public async Task SaveAppAuthorize(int objectType, string objectId, string[] moduleIds, int type)
        {
            await authorizeService.SaveAppAuthorize(objectType, objectId, moduleIds,type);
        }
        #endregion
    }
}
