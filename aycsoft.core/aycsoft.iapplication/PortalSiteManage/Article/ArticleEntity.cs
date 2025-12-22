using cd.dapper.extension;
using System;
namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：详细信息维护
    /// </summary>
    [Table("lr_ps_article")]
    public class ArticleEntity
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
        /// 缩略图名称
        /// </summary>
        public string F_ImgName { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string F_Img { get; set; }
        /// <summary>
        /// 文章分类
        /// </summary>
        public string F_Category { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string F_Content { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? F_PushDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        public string F_ModifyUserName { get; set; }
        #endregion
    }
}

