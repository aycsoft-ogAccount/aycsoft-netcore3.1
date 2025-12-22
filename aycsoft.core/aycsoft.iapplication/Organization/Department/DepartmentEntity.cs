using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.24
    /// 描 述：部门管理
    /// </summary>
    [Table("lr_base_department")]
    public class DepartmentEntity
    {
        #region 实体成员
        /// <summary>
        /// 部门主键
        /// </summary>
        [Key]
        public string F_DepartmentId { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        public string F_CompanyId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 部门代码
        /// </summary>
        public string F_EnCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string F_FullName { get; set; }
        /// <summary>
        /// 部门简称
        /// </summary>
        public string F_ShortName { get; set; }
        /// <summary>
        /// 部门类型
        /// </summary>
        public string F_Nature { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string F_Manager { get; set; }
        /// <summary>
        /// 外线电话
        /// </summary>
        public string F_OuterPhone { get; set; }
        /// <summary>
        /// 内线电话
        /// </summary>
        public string F_InnerPhone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string F_Email { get; set; }
        /// <summary>
        /// 部门传真
        /// </summary>
        public string F_Fax { get; set; }
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
