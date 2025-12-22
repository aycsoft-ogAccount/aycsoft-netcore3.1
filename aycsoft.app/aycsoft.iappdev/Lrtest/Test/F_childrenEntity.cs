using cd.dapper.extension;
using System;

namespace aycsoft.iappdev
{
    /// <summary>
    /// Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期： 2020-06-18 06:35:30
    /// 描 述： f_children表的实体
    /// </summary>
    [Table("f_children")]
    public class F_childrenEntity
    {
        #region 实体成员
        /// <summary>
        /// F_Id
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// F_text
        /// </summary>
        public string F_text { get; set; }
        /// <summary>
        /// F_input
        /// </summary>
        public string F_input { get; set; }
        /// <summary>
        /// F_radio
        /// </summary>
        public string F_radio { get; set; }
        /// <summary>
        /// F_checkbox
        /// </summary>
        public string F_checkbox { get; set; }
        /// <summary>
        /// F_Layer
        /// </summary>
        public string F_Layer { get; set; }
        /// <summary>
        /// F_date
        /// </summary>
        public DateTime? F_date { get; set; }
        /// <summary>
        /// F_parentId
        /// </summary>
        public string F_parentId { get; set; }
        /// <summary>
        /// F_select
        /// </summary>
        public string F_select { get; set; }

        #endregion

        #region 扩展属性

        #endregion
    }
}