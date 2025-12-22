using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：门户网站首页配置
    /// </summary>
    [Table("lr_ps_homeconfig")]
    public class HomeConfigEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 名称 
        /// </summary> 
        public string F_Name { get; set; }
        /// <summary> 
        /// 类型1.顶部文字2.底部文字3.底部地址4.logo图片5.微信图片6.顶部菜单7.底部菜单8.轮播图片9.模块 10底部logo 11微信文字
        /// </summary> 
        public string F_Type { get; set; }
        /// <summary> 
        /// 链接类型 
        /// </summary> 
        public int? F_UrlType { get; set; }
        /// <summary> 
        /// 链接地址 
        /// </summary> 
        public string F_Url { get; set; }
        /// <summary> 
        /// 图片 
        /// </summary> 
        public string F_Img { get; set; }
        /// <summary> 
        /// 上级菜单 
        /// </summary> 
        public string F_ParentId { get; set; }
        /// <summary> 
        /// 排序码 
        /// </summary> 
        public int? F_Sort { get; set; }
        /// <summary> 
        /// 模块配置信息 
        /// </summary> 
        public string F_Scheme { get; set; }
        #endregion
    }
}
