using cd.dapper.extension;
namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.03.13
    /// 描 述：印章管理
    /// </summary>
    [Table("lr_base_stamp")]
    public class StampEntity
    {
        /// <summary>
        /// 印章编号
        /// </summary>
        [Key]
        public string F_StampId { get; set; }
        /// <summary>
        /// 印章名称
        /// </summary>
        public string F_StampName { get; set; }
        /// <summary>
        /// 印章备注
        /// </summary>
        public string F_Description { get; set; }
        /// <summary>
        /// 印章分类
        /// </summary>
        public string F_StampType { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string F_Password { get; set; }
        /// <summary>
        /// 图片文件
        /// </summary>
        public string F_ImgFile { get; set; }
        /// <summary>
        /// 属于人员
        /// </summary>
        public string F_User { get; set; }
        /// <summary>
        /// 印章状态
        /// </summary>
        public int? F_EnabledMark { get; set; }
    }
}
