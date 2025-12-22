namespace aycsoft.workflow
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流流转字段条件
    /// </summary>
    public class NWFCondition
    {
        /// <summary>
        /// 数据库主键
        /// </summary>
        public string dbId { get; set; }
        /// <summary>
        /// 表格
        /// </summary>
        public string table { get; set; }
        /// <summary>
        /// 关联字段
        /// </summary>
        public string field1 { get; set; }
        /// <summary>
        /// 比较字段
        /// </summary>
        public string field2 { get; set; }
        /// <summary>
        /// 比较类型1.等于2.不等于3.大于4.大于等于5.小于6.小于等于7.包含8.不包含9.包含于10.不包含于
        /// </summary>
        public int compareType { get; set; }
        /// <summary>
        /// 数据值
        /// </summary>
        public string value { get; set; }
    }
}
