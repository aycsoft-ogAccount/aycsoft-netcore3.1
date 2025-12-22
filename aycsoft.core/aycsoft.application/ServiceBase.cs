using aycsoft.database;
using aycsoft.util;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.20
    /// 描 述：服务基类
    /// </summary>
    public class ServiceBase: RepositoryFactory
    {
        /// <summary>
        /// 获取登录者用户名称
        /// </summary>
        /// <returns></returns>
        public string GetUserName() {
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
    }
}
