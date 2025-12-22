using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.14
    /// 描 述：看板配置信息
    /// </summary>
    [Table("lr_kbconfiginfo")]
    public class LR_KBConfigInfoEntity
    {
        #region 实体成员
        /// <summary>
        /// 模块主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 看板id
        /// </summary>
        public string F_KanBanId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string F_ModeName { get; set; }
        /// <summary>
        /// 类型statistics统计;2表格3图表
        /// </summary>
        public string F_Type { get; set; }
        /// <summary>
        /// 上边距
        /// </summary>
        public string F_TopValue { get; set; }
        /// <summary>
        /// 左边距
        /// </summary>
        public string F_LeftValue { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string F_WidthValue { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public string F_HightValue { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 刷新时间（分钟）
        /// </summary>
        public int? F_RefreshTime { get; set; }
        /// <summary>
        /// 配置信息
        /// </summary>
        public string F_Configuration { get; set; }
        #endregion
    }
}

