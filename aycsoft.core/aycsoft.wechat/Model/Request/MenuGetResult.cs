using System.Collections.Generic;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：菜单结果
    /// </summary>
    public class MenuGetResult : OperationResultsBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Menu menu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public class Menu
        {

            /// <summary>
            /// 一级菜单数组，个数应为1~3个
            /// </summary>
            /// <returns></returns>
            public List<MenuItem> button { get; set; }
        }
    }
}
