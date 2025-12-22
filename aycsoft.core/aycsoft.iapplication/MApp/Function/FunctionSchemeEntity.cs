using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.07
    /// 描 述：移动端功能管理
    /// </summary>
    [Table("lr_app_fnscheme")]
    public class FunctionSchemeEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 功能模板
        /// </summary>
        /// <returns></returns>
        public string F_Scheme { get; set; }
        #endregion
    }
}
