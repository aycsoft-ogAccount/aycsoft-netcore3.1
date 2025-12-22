using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.22
    /// 描 述：语言类型
    /// </summary>
    [Table("lr_lg_type")]
    public class LGTypeEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 语言名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 语言编码（不予许重复）
        /// </summary>
        public string F_Code { get; set; }
        /// <summary>
        /// 是否是主语言0不是1是
        /// </summary>
        public int? F_IsMain { get; set; }
        #endregion
    }
}

