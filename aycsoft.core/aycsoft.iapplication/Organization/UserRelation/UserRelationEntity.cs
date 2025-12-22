using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.25
    /// 描 述：用户关联对象
    /// </summary>
    [Table("lr_base_userrelation")]
    public class UserRelationEntity
    {
        #region 实体成员
        /// <summary>
        /// 用户关系主键
        /// </summary>
        [Key]
        public string F_UserRelationId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        public string F_UserId { get; set; }
        /// <summary>
        /// 分类:1-角色2-岗位
        /// </summary>
        public int? F_Category { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        #endregion
    }
}
