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
    /// 描 述：角色管理
    /// </summary>
    public class RoleBLL : BLLBase, RoleIBLL,BLL
    {
        #region 属性
        private readonly RoleService roleService = new RoleService();
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取角色数据列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<RoleEntity>> GetList()
        {
            return roleService.GetList();
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<RoleEntity>> GetPageList(Pagination pagination, string keyword)
        {
            return roleService.GetPageList(pagination, keyword);
        }
        /// <summary>
        /// 获取角色数据列表
        /// </summary>
        /// <param name="roleIds">主键串</param>
        /// <returns></returns>
        public Task<IEnumerable<RoleEntity>> GetListByRoleIds(string roleIds)
        {
            return roleService.GetListByRoleIds(roleIds);
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<RoleEntity> GetEntity(string keyValue)
        {
            return roleService.GetEntity(keyValue);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await roleService.Delete(keyValue);
        }
        /// <summary>
        /// 保存角色（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="roleEntity">角色实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, RoleEntity roleEntity)
        {
            await roleService.SaveEntity(keyValue, roleEntity);
        }
        #endregion
    }
}
