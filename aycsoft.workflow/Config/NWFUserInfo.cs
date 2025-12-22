namespace aycsoft.workflow
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：流程人员信息
    /// </summary>
    public class NWFUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 标记 0需要审核1暂时不需要审核
        /// </summary>
        public int Mark { get; set; }
        /// <summary>
        /// 是否有审核人
        /// </summary>
        public bool noPeople { get; set; }
    }
}
