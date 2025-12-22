using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流模板权限(新)
    /// </summary>
    [Table("lr_nwf_schemeauth")]
    public class NWFSchemeAuthEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程模板信息主键 
        /// </summary> 
        public string F_SchemeInfoId { get; set; }
        /// <summary> 
        /// 对象名称 
        /// </summary> 
        public string F_ObjName { get; set; }
        /// <summary> 
        /// 对应对象主键 
        /// </summary> 
        public string F_ObjId { get; set; }
        /// <summary> 
        /// 对应对象类型1岗位2角色3用户4所用人可看 
        /// </summary> 
        public int? F_ObjType { get; set; }
        #endregion
    }
}
