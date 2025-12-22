using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：业务基类
    /// </summary>
    public class BLLBase
    {

        #region 基础信息
        private UserEntity userInfo;
        private string roleIds;
        private string postIds;

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public async Task<UserEntity> CurrentUser(string userId = null)
        {
            var userBLL = IocManager.Instance.GetService<UserIBLL>();
            if (string.IsNullOrEmpty(userId))
            {
                if (userInfo == null) {
                    userInfo = await userBLL.GetEntity();
                }
                return userInfo;
            }
            else
            {
                return await userBLL.GetEntity(userId);
            }

        }

        /// <summary>
        /// 获取当前登录者的角色id集合
        /// </summary>
        /// <returns></returns>
        public async Task<string> CurrentUserRoleIds()
        {
            if (roleIds == null)
            {
                var _userInfo = await CurrentUser();
                var userRelationIBLL = IocManager.Instance.GetService<UserRelationIBLL>();
                roleIds = await userRelationIBLL.GetObjectIds(_userInfo.F_UserId, 1);
            }

            return roleIds;
        }
        /// <summary>
        /// 获取当前登录者的角色id集合
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        public Task<string> CurrentUserRoleIds(string userId)
        {
            var userRelationIBLL = IocManager.Instance.GetService<UserRelationIBLL>();
            return userRelationIBLL.GetObjectIds(userId, 1);
        }


        /// <summary>
        /// 获取当前登录者的岗位id集合
        /// </summary>
        /// <returns></returns>
        public async Task<string> CurrentUserPostIds()
        {
            if (postIds == null)
            {
                var _userInfo = await CurrentUser();
                var userRelationIBLL = IocManager.Instance.GetService<UserRelationIBLL>();
                postIds = await userRelationIBLL.GetObjectIds(_userInfo.F_UserId, 2);
            }

            return postIds;
        }

        /// <summary>
        ///  获取当前登录者的岗位id集合
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        public Task<string> CurrentUserPostIds(string userId)
        {
            var userRelationIBLL = IocManager.Instance.GetService<UserRelationIBLL>();
            return userRelationIBLL.GetObjectIds(userId, 2);
        }


        /// <summary>
        /// 获取登录者用户名称
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            return ContextHelper.GetItem("userName") as string;
        }
        /// <summary>
        /// 获取登录者用户Id
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            return ContextHelper.GetItem("userId") as string;
        }
        /// <summary>
        /// 获取登录者用户账号
        /// </summary>
        /// <returns></returns>
        public string GetUserAccount()
        {
            return ContextHelper.GetItem("account") as string;
        }
        #endregion

        #region 数据权限
        /// <summary>
        /// 获取数据权限设置sql查询语句
        /// </summary>
        /// <param name="code">数据权限编码</param>
        /// <returns></returns>
        public async Task<string> GetDataAuthoritySql(string code)
        {
            var dataAuthorizeIBLL = IocManager.Instance.GetService<DataAuthorizeIBLL>();
            return await dataAuthorizeIBLL.GetWhereSql(code);
        }
        #endregion

        #region 单据编码
        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="code">编码规则编码</param>
        /// <returns></returns>
        public Task<string> GetRuleCode(string code)
        {
            var codeRuleIBLL = IocManager.Instance.GetService<CodeRuleIBLL>();
            return codeRuleIBLL.GetBillCode(code);
        }
        /// <summary>
        /// 占用编码
        /// </summary>
        /// <param name="code">编码规则编码</param>
        /// <returns></returns>
        public async Task UseRuleSeed(string code)
        {
            var codeRuleIBLL = IocManager.Instance.GetService<CodeRuleIBLL>();
            await codeRuleIBLL.UseRuleSeed(code);
        }
        #endregion

    }
}
