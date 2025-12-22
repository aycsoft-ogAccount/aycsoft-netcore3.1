using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.01
    /// 描 述：任务计划模板信息
    /// </summary>
    [Table("lr_ts_schemeinfo")]
    public class TSSchemeInfoEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 任务名称
        /// </summary> 
        public string F_Name { get; set; }

        /// <summary> 
        /// 标志1.启用2.未启用 
        /// </summary> 
        public int? F_IsActive { get; set; }
        /// <summary> 
        /// 开始时间 
        /// </summary> 
        public DateTime? F_BeginTime { get; set; }
        /// <summary> 
        /// 结束类型1.无限期 2.有限期 
        /// </summary> 
        public int? F_EndType { get; set; }
        /// <summary> 
        /// 结束时间 
        /// </summary> 
        public DateTime? F_EndTime { get; set; }
        /// <summary> 
        /// 看板说明
        /// </summary> 
        public string F_Description { get; set; }
        /// <summary> 
        /// 模板 
        /// </summary> 
        public string F_Scheme { get; set; }
        #endregion
    }
}
