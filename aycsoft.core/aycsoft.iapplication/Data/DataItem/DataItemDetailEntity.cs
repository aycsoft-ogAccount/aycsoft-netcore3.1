using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：数据字典明细
    /// </summary>
    [Table("lr_base_dataitemdetail")]
    public class DataItemDetailEntity
    {
        #region 实体成员
        /// <summary>
        /// 明细主键
        /// </summary>
        [Key]
        public string F_ItemDetailId { get; set; }
        /// <summary>
        /// 分类主键
        /// </summary>
        public string F_ItemId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string F_ItemCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string F_ItemName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string F_ItemValue { get; set; }
        /// <summary>
        /// 快速查询
        /// </summary>
        public string F_QuickQuery { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        public string F_SimpleSpelling { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public int? F_IsDefault { get; set; }
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
