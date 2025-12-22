using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.24
    /// 描 述：编号规则种子
    /// </summary>
    [Table("lr_base_coderuleseed")]
    public class CodeRuleSeedEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号规则种子主键
        /// </summary>
        [Key]
        public string F_RuleSeedId { get; set; }
        /// <summary>
        /// 编码规则主键
        /// </summary>
        public string F_RuleId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        public string F_UserId { get; set; }
        /// <summary>
        /// 种子值
        /// </summary>
        public int? F_SeedValue { get; set; }
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
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        public string F_ModifyUserName { get; set; }
        #endregion
    }
}
