using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.10
    /// 描 述：授權功能
    /// </summary>
    [Table("lr_base_authorize")]
    public class AuthorizeEntity
    {
        #region 实体成员
        /// <summary>
        /// 授权功能主键
        /// </summary>
        [Key]
        public string F_AuthorizeId { get; set; }
        /// <summary>
        /// 对象分类:1-角色2-用户
        /// </summary>
        public int? F_ObjectType { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 项目类型:1-菜单2-按钮3-视图4-表单5-移动功能
        /// </summary>
        public int? F_ItemType { get; set; }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string F_ItemId { get; set; }
        /// <summary>
        /// 创建时间
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
