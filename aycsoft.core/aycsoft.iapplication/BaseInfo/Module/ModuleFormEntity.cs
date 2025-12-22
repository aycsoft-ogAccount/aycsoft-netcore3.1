using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.10
    /// 描 述：功能模块视图列
    /// </summary>
    [Table("lr_base_moduleform")]
    public class ModuleFormEntity
    {
        #region 实体成员
        /// <summary>
        /// 列主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string F_ModuleFormId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>
        /// <returns></returns>
        public string F_ModuleId { get; set; }
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
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? F_SortCode { get; set; }
        #endregion
    }
}
