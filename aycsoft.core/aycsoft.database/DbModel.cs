using System;
using System.Collections.Generic;
using System.Text;

namespace aycsoft.database
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：数据库连接
    /// </summary>
    public class DbModel
    {
        /// <summary>
        /// 数据库编码
        /// </summary>
        /// <returns></returns>
        public string F_DBName { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        /// <returns></returns>
        public string F_DbType { get; set; }
        /// <summary>
        /// 连接地址
        /// </summary>
        /// <returns></returns>
        public string F_DbConnection { get; set; }
    }
}
