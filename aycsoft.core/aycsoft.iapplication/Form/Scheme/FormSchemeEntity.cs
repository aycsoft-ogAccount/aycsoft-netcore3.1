using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.21
    /// 描 述：表单模板
    /// </summary>
    [Table("lr_form_scheme")]
    public class FormSchemeEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 模板信息主键
        /// </summary>
        public string F_SchemeInfoId { get; set; }
        /// <summary>
        /// 1.正式2.草稿
        /// </summary>
        public int? F_Type { get; set; }
        /// <summary>
        /// 模板
        /// </summary>
        public string F_Scheme { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string F_CreateUserName { get; set; }
        #endregion
    }
}
