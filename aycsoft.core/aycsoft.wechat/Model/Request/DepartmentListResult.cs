using System.Collections.Generic;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：部门列表返回结果
    /// </summary>
    public class DepartmentListResult : OperationResultsBase
    {
        /// <summary>
        /// 部门列表数据。以部门的order字段从小到大排列
        /// </summary>
        /// <returns></returns>
        public List<DepartmentItem> department { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public class DepartmentItem
        {
            /// <summary>
            /// 部门id
            /// </summary>
            /// <returns></returns>
            public string id { get; set; }

            /// <summary>
            /// 部门名称
            /// </summary>
            /// <returns></returns>
            public string name { get; set; }

            /// <summary>
            /// 父亲部门id。根部门为1
            /// </summary>
            /// <returns></returns>
            public string parentid { get; set; }
        }
    }
}
