using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.25
    /// 描 述：附件管理
    /// </summary>
    [Table("lr_base_annexesfile")]
    public class AnnexesFileEntity
    {
        #region 实体成员
        /// <summary>
        /// 文件主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 附件夹主键
        /// </summary>
        public string F_FolderId { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string F_FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string F_FilePath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string F_FileSize { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string F_FileExtensions { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string F_FileType { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int? F_DownloadCount { get; set; }
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
        #endregion
    }
}
