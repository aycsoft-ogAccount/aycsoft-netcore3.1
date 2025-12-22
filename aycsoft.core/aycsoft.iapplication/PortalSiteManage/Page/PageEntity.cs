using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：门户网站页面配置
    /// </summary>
    [Table("lr_ps_page")]
    public class PageEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 标题 
        /// </summary> 
        public string F_Title { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string F_Img { get; set; }
        /// <summary> 
        /// 类型1.列表2图形列表3详细信息 
        /// </summary> 
        public string F_Type { get; set; }
        /// <summary> 
        /// 创建时间 
        /// </summary> 
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 创建人主键 
        /// </summary> 
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 创建人名称 
        /// </summary> 
        public string F_CreateUserName { get; set; }
        /// <summary> 
        /// 编辑时间 
        /// </summary> 
        public DateTime? F_ModifyDate { get; set; }
        /// <summary> 
        /// 编辑人主键 
        /// </summary> 
        public string F_ModifyUserId { get; set; }
        /// <summary> 
        /// 编辑人名称 
        /// </summary> 
        public string F_ModifyUserName { get; set; }
        /// <summary> 
        /// 页面配置模板 
        /// </summary> 
        public string F_Scheme { get; set; }
        #endregion
    }
}
