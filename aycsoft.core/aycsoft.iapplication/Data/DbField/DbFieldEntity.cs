using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.23
    /// 描 述：常用字段
    /// </summary>
    [Table("lr_base_dbfield")]
    public class DbFieldEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string F_DataType { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string F_Remark { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        public int? F_Length { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人名字
        /// </summary>
        public string F_CreateUserName { get; set; }

        #endregion
    }
}
