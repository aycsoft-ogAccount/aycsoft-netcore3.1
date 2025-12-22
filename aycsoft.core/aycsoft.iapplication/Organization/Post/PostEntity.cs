using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.24
    /// 描 述：岗位管理
    /// </summary>
    [Table("lr_base_post")]
    public class PostEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_PostId { get; set; }
        /// <summary>
        /// 上级主键
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 岗位编号
        /// </summary>
        public string F_EnCode { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        public string F_CompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        public string F_DepartmentId { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int? F_DeleteMark { get; set; }
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
