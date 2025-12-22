using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.31
    /// 描 述：桌面图表配置
    /// </summary>
    [Table("lr_dt_chart")]
    public class DTChartEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string F_Icon { get; set; }
        /// <summary>
        /// 数据库ID
        /// </summary>
        public string F_DataSourceId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 图表类型0饼图1折线图2柱状图
        /// </summary>
        public int? F_Type { get; set; }
        /// <summary>
        /// sql语句
        /// </summary>
        public string F_Sql { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? F_Sort { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string F_Description { get; set; }
        /// <summary>
        /// 皮肤一比例
        /// </summary>
        public int? F_Proportion1 { get; set; }
        /// <summary>
        /// 皮肤二比例
        /// </summary>
        public int? F_Proportion2 { get; set; }
        /// <summary>
        /// 皮肤三比例
        /// </summary>
        public int? F_Proportion3 { get; set; }
        /// <summary>
        /// 皮肤四比例
        /// </summary>
        public int? F_Proportion4 { get; set; }
        #endregion
    }
}
