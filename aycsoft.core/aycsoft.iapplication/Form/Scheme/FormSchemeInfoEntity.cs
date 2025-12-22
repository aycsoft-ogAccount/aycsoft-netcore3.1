using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.21
    /// 描 述：表单模板信息
    /// </summary>
    [Table("lr_form_schemeinfo")]
    public class FormSchemeInfoEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 表单名字
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 表单分类
        /// </summary>
        public string F_Category { get; set; }
        /// <summary>
        /// 当前模板主键
        /// </summary>
        public string F_SchemeId { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string F_Description { get; set; }
        #endregion

        /// <summary>
        /// 1.正式2.草稿
        /// </summary>
        [NotWrited]
        public int? F_Type { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [NotWrited]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        [NotWrited]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [NotWrited]
        public DateTime? F_CreateDate { get; set; }

    }
}
