namespace aycsoft.workflow
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流线段
    /// </summary>
    public class NWFLineInfo
    {
        /// <summary>
        /// 线条Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 线条名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 开始端节点ID
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 结束端节点ID
        /// </summary>
        public string to { get; set; }


        /// <summary>
        /// 通过策略 1.所有情况都通过 2.自定义设置（默认该值为1）
        /// </summary>
        public string strategy { get; set; }
        /// <summary>
        /// 自定义通过策略 agree 同意 disagree 不同意 lrtimeout 超时
        /// </summary>
        public string agreeList { get; set; }
        /// <summary>
        /// 绑定的操作类型sql interface ioc
        /// </summary>
        public string operationType { get; set; }
        /// <summary>
        /// 绑定数据ID
        /// </summary>
        public string dbId { get; set; }
        /// <summary>
        /// 绑定的sql语句
        /// </summary>
        public string strSql { get; set; }
        /// <summary>
        /// 绑定的sql语句(撤销的时候执行)
        /// </summary>
        public string strSqlR { get; set; }
        /// <summary>
        /// 绑定的接口
        /// </summary>
        public string strInterface { get; set; }
        /// <summary>
        /// 绑定的接口(撤销的时候执行)
        /// </summary>
        public string strInterfaceR { get; set; }
        /// <summary>
        /// 绑定的ioc名称
        /// </summary>
        public string iocName { get; set; }
        /// <summary>
        /// 绑定的ioc名称（撤销的时候执行）
        /// </summary>
        public string iocNameR { get; set; }
    }
}
