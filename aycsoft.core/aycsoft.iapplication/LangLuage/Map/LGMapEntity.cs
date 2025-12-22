using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.22
    /// 描 述：语言映射
    /// </summary>
    [Table("lr_lg_map")]
    public class LGMapEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 显示内容
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 编码(系统自动产生，作为关联项)
        /// </summary>
        public string F_Code { get; set; }
        /// <summary>
        /// 对应语言显示编码
        /// </summary>
        public string F_TypeCode { get; set; }
        #endregion
    }
}

