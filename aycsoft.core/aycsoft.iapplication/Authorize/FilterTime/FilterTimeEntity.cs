using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.25
    /// 描 述：时段过滤
    /// </summary>
    [Table("lr_base_filtertime")]
    public class FilterTimeEntity
    {
        #region 实体成员
        /// <summary>
        /// 过滤时段主键
        /// </summary>
        [Key]
        public string F_FilterTimeId { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public string F_ObjectType { get; set; }
        /// <summary>
        /// 访问
        /// </summary>
        public int? F_VisitType { get; set; }
        /// <summary>
        /// 星期一
        /// </summary>
        public string F_WeekDay1 { get; set; }
        /// <summary>
        /// 星期二
        /// </summary>
        public string F_WeekDay2 { get; set; }
        /// <summary>
        /// 星期三
        /// </summary>
        public string F_WeekDay3 { get; set; }
        /// <summary>
        /// 星期四
        /// </summary>
        public string F_WeekDay4 { get; set; }
        /// <summary>
        /// 星期五
        /// </summary>
        public string F_WeekDay5 { get; set; }
        /// <summary>
        /// 星期六
        /// </summary>
        public string F_WeekDay6 { get; set; }
        /// <summary>
        /// 星期日
        /// </summary>
        public string F_WeekDay7 { get; set; }
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
