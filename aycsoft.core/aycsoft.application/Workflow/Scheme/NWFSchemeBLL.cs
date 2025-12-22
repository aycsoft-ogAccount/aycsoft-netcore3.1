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
    /// 日 期：2020.02.11
    /// 描 述：工作流模板(新)
    /// </summary>
    public class NWFSchemeBLL:BLLBase, NWFSchemeIBLL,BLL
    {
        private NWFSchemeService nWFSchemeService = new NWFSchemeService();

        #region 获取数据
        /// <summary>
        /// 获取流程分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFSchemeInfoEntity>> GetInfoPageList(Pagination pagination, string queryJson)
        {
            return nWFSchemeService.GetInfoPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取自定义流程列表
        /// </summary>
        /// <param name="userId">用户</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFSchemeInfoEntity>> GetInfoList(string userId)
        {
            var userInfo = await this.CurrentUser(userId);
            var postIds = await this.CurrentUserPostIds(userInfo.F_UserId);
            var roleIds = await this.CurrentUserRoleIds(userInfo.F_UserId);
            return await nWFSchemeService.GetInfoList(userInfo.F_UserId, postIds, roleIds);
        }
        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<NWFSchemeInfoEntity>> GetALLInfoList()
        {
            return nWFSchemeService.GetALLInfoList();
        }
        /// <summary>
        /// 获取流程模板分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public async Task<IEnumerable<NWFSchemeInfoEntity>> GetAppInfoPageList(Pagination pagination, string queryJson)
        {
            var userInfo = await this.CurrentUser();
            var postIds = await this.CurrentUserPostIds();
            var roleIds = await this.CurrentUserRoleIds();
            return await nWFSchemeService.GetAppInfoPageList(pagination, queryJson, userInfo.F_UserId, postIds, roleIds);
        }

        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<NWFSchemeInfoEntity> GetInfoEntity(string keyValue)
        {
            return nWFSchemeService.GetInfoEntity(keyValue);
        }
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        public Task<NWFSchemeInfoEntity> GetInfoEntityByCode(string code)
        {
            return nWFSchemeService.GetInfoEntityByCode(code);
        }

        /// <summary>
        /// 获取流程模板权限列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFSchemeAuthEntity>> GetAuthList(string schemeInfoId)
        {
            return nWFSchemeService.GetAuthList(schemeInfoId);
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">流程信息主键</param>
        /// <returns></returns>
        public Task<IEnumerable<NWFSchemeEntity>> GetSchemePageList(Pagination pagination, string schemeInfoId)
        {
            return nWFSchemeService.GetSchemePageList(pagination, schemeInfoId);
        }
        /// <summary>
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<NWFSchemeEntity> GetSchemeEntity(string keyValue)
        {
            return nWFSchemeService.GetSchemeEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            await nWFSchemeService.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="infoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        /// <param name="authList">模板权限信息</param>
        public async Task SaveEntity(string keyValue, NWFSchemeInfoEntity infoEntity, NWFSchemeEntity schemeEntity, IEnumerable<NWFSchemeAuthEntity> authList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                NWFSchemeEntity oldNWFSchemeEntity =await GetSchemeEntity(infoEntity.F_SchemeId);
                if (oldNWFSchemeEntity.F_Content == schemeEntity.F_Content && oldNWFSchemeEntity.F_Type == schemeEntity.F_Type)
                {
                    schemeEntity = null;
                }
            }
            await nWFSchemeService.SaveEntity(keyValue, infoEntity, schemeEntity, authList);
        }
        /// <summary>
        /// 更新流程模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        public async Task UpdateScheme(string schemeInfoId, string schemeId)
        {
            await nWFSchemeService.UpdateScheme(schemeInfoId, schemeId);
        }
        /// <summary>
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public async Task UpdateState(string schemeInfoId, int state)
        {
            await nWFSchemeService.UpdateState(schemeInfoId, state);
        }
        #endregion
    }
}
