using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.10
    /// 描 述：功能模块按钮
    /// </summary>
    [Table("lr_base_modulebutton")]
    public class ModuleButtonEntity
    {
        #region 实体成员
        /// <summary>
        /// 按钮主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string F_ModuleButtonId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>
        /// <returns></returns>
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        public string F_Icon { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        public string F_EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string F_FullName { get; set; }
        /// <summary>
        /// Action地址
        /// </summary>
        /// <returns></returns>
        public string F_ActionAddress { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? F_SortCode { get; set; }
        #endregion
    }
}
