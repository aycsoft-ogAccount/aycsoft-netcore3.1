using cd.dapper.extension;
using System;

namespace aycsoft.iappdev
{
    /// <summary>
    /// Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期： 2020-06-18 06:35:30
    /// 描 述： f_parent表的实体
    /// </summary>
    [Table("f_parent")]
    public class F_parentEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        public string F_text { get; set; }
        /// <summary>
        /// 文本区域
        /// </summary>
        public string F_textarea { get; set; }
        /// <summary>
        /// 编辑框
        /// </summary>
        public string F_edit { get; set; }
        /// <summary>
        /// 单选
        /// </summary>
        public string F_radio { get; set; }
        /// <summary>
        /// 多选
        /// </summary>
        public string F_checkbox { get; set; }
        /// <summary>
        /// 选择框
        /// </summary>
        public string F_select { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? F_date { get; set; }
        /// <summary>
        /// 日期差
        /// </summary>
        public string F_datev { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string F_code { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string F_company { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string F_department { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string F_user { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string F_file { get; set; }
        /// <summary>
        /// 时间2
        /// </summary>
        public DateTime? F_date2 { get; set; }
        /// <summary>
        /// 当前公司
        /// </summary>
        public string F_ccompany { get; set; }
        /// <summary>
        /// 当前部门
        /// </summary>
        public string F_cdepartment { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public string F_cuser { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime? F_cdate { get; set; }

        #endregion

        #region 扩展属性

        #endregion
    }
}