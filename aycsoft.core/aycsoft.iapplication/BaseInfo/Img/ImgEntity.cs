using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.17
    /// 描 述：图片保存
    /// </summary>
    [Table("lr_base_img")]
    public class ImgEntity
    {
        #region 实体成员 
        /// <summary>
        /// 主键 
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string F_ExName { get; set; }
        /// <summary> 
        /// 保存的图片内容 
        /// </summary> 
        public string F_Content { get; set; }
        #endregion
    }
}
