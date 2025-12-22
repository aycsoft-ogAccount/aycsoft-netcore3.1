using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.24
    /// 描 述：公司管理
    /// </summary>
    [Table("lr_base_company")]
    public class CompanyEntity
    {
        #region 实体成员
        /// <summary>
        /// 公司主键
        /// </summary>
        [Key]
        public string F_CompanyId { get; set; }
        /// <summary>
        /// 公司分类
        /// </summary>
        public int? F_Category { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string F_EnCode { get; set; }
        /// <summary>
        /// 公司简称
        /// </summary>
        public string F_ShortName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string F_FullName { get; set; }
        /// <summary>
        /// 公司性质
        /// </summary>
        public string F_Nature { get; set; }
        /// <summary>
        /// 外线电话
        /// </summary>
        public string F_OuterPhone { get; set; }
        /// <summary>
        /// 内线电话
        /// </summary>
        public string F_InnerPhone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string F_Fax { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string F_Postalcode { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string F_Email { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string F_Manager { get; set; }
        /// <summary>
        /// 省主键
        /// </summary>
        public string F_ProvinceId { get; set; }
        /// <summary>
        /// 市主键
        /// </summary>
        public string F_CityId { get; set; }
        /// <summary>
        /// 县/区主键
        /// </summary>
        public string F_CountyId { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string F_Address { get; set; }
        /// <summary>
        /// 公司主页
        /// </summary>
        public string F_WebAddress { get; set; }
        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime? F_FoundedTime { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string F_BusinessScope { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string F_Description { get; set; }
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
