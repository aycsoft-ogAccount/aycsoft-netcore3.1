using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：Excel数据导出设置
    /// </summary>
    [Table("lr_excel_export")]
    public class ExcelExportEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 绑定的JQgirdId
        /// </summary>
        public string F_GridId { get; set; }
        /// <summary>
        /// 功能模块Id
        /// </summary>
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 按钮Id
        /// </summary>
        public string F_ModuleBtnId { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string F_BtnName { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string F_ModifyUserName { get; set; }
        #endregion
    }
}
