using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.07
    /// 描 述：App首页图片管理 
    /// </summary> 
    [Table("lr_app_dtimg")]
    public class DTImgEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 图片说明 
        /// </summary> 
        /// <returns></returns> 
        public string F_Des { get; set; }
        /// <summary> 
        /// 图片文件名 
        /// </summary> 
        /// <returns></returns> 
        public string F_FileName { get; set; }
        /// <summary> 
        /// 有效标志0否1是 
        /// </summary> 
        /// <returns></returns> 
        public int? F_EnabledMark { get; set; }
        /// <summary> 
        /// 排序码 
        /// </summary> 
        /// <returns></returns> 
        public int? F_SortCode { get; set; }
        #endregion
    }
}
