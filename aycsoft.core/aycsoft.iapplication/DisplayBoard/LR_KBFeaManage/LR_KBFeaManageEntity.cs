using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.14
    /// 描 述：看板发布
    /// </summary>
    [Table("lr_kbfeamanage")]
    public class LR_KBFeaManageEntity
    {
        #region 实体成员
        /// <summary>
        /// 看板管理主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string F_FullName { get; set; }
        /// <summary>
        /// 功能编号
        /// </summary>
        public string F_EnCode { get; set; }
        /// <summary>
        /// 上级功能
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 功能图标
        /// </summary>
        public string F_Icon { get; set; }
        /// <summary>
        /// 看板选择
        /// </summary>
        public string F_KanBanId { get; set; }
        /// <summary>
        /// 模块id
        /// </summary>
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 功能描述
        /// </summary>
        public string F_Description { get; set; }
        #endregion
    }
}

