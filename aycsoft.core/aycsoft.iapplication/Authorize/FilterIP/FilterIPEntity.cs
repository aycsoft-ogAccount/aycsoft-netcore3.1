using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.25
    /// 描 述：IP过滤
    /// </summary>
    [Table("lr_base_filterip")]
    public class FilterIPEntity
    {
        #region 实体成员
        /// <summary>
        /// 过滤IP主键
        /// </summary>
        [Key]
        public string F_FilterIPId { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public string F_ObjectType { get; set; }
        /// <summary>
        /// 对象Id
        /// </summary>
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 访问
        /// </summary>
        public int? F_VisitType { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int? F_Type { get; set; }
        /// <summary>
        /// IP访问
        /// </summary>
        public string F_IPLimit { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string F_Description { get; set; }
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
