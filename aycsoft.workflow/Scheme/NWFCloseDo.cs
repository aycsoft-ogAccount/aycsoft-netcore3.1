namespace aycsoft.workflow
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：流程撤销作废的时候执行的方法
    /// </summary>
    public class NWFCloseDo
    {
        /// <summary>
        /// 方法执行类型sql，ioc，interface
        /// </summary>
        public string F_CloseDoType { get; set; }
        /// <summary>
        /// 执行sql语句的数据库ID
        /// </summary>
        public string F_CloseDoDbId { get; set; }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        public string F_CloseDoSql { get; set; }
        /// <summary>
        /// 执行的ioc名称
        /// </summary>
        public string F_CloseDoIocName { get; set; }
        /// <summary>
        /// 执行接口
        /// </summary>
        public string F_CloseDoInterface { get; set; }
    }
}
