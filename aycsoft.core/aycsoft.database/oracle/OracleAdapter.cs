using cd.dapper.extension;
using System.Text;

namespace aycsoft.database.oracle
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.09
    /// 描 述：oracle sql 适配器
    /// </summary>
    public class OracleAdapter : ISqlAdapter
    {
        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="sb">字串构造器</param>
        /// <param name="columnName">列名</param>
        public void AppendColumnName(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("[{0}]", columnName);
        }
        /// <summary>
        /// 添加列等一个参数
        /// </summary>
        /// <param name="sb">字串构造器</param>
        /// <param name="columnName">列名</param>
        public void AppendColumnNameEqualsValue(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("[{0}] = :{1}", columnName, columnName);
        }
    }
}
