using System.Collections.Generic;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：获取部门成员接口返回结果
    /// </summary>
    public class UserSimplelistResult : OperationResultsBase
    {
        /// <summary>
        /// 成员列表
        /// </summary>
        /// <returns></returns>
        public List<UserSimplelistItem> userlist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public class UserSimplelistItem
        {
            /// <summary>
            /// 员工UserID。对应管理端的帐号
            /// </summary>
            /// <returns></returns>
            public string userid { get; set; }

            /// <summary>
            /// 成员名称
            /// </summary>
            /// <returns></returns>
            public string name { get; set; }
        }
    }
}
