using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：数据源
    /// </summary>
    [Table("lr_base_datasource")]
    public class DataSourceEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string F_Code { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 数据库主键
        /// </summary>
        public string F_DbId { get; set; }
        /// <summary>
        /// sql语句
        /// </summary>
        public string F_Sql { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string F_Description { get; set; }
        /// <summary>
        /// 创建人主键
        /// </summary>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人名字
        /// </summary>
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 修改人主键
        /// </summary>
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改人名字
        /// </summary>
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? F_ModifyDate { get; set; }
        #endregion
    }
}
