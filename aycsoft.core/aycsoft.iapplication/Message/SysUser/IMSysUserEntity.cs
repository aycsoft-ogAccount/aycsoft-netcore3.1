using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：即时通讯用户注册
    /// </summary>
    [Table("lr_im_sysuser")]
    public class IMSysUserEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 系统用户名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 系统用户编码
        /// </summary>
        public string F_Code { get; set; }
        /// <summary>
        /// 系统用户图标
        /// </summary>
        public string F_Icon { get; set; }
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
        /// <summary>
        /// F_Description
        /// </summary>
        public string F_Description { get; set; }
        #endregion
    }
}
