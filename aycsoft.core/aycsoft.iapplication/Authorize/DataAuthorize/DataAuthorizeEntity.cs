using System;
using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core 力软敏捷开发框架
    /// Copyright (c) 2021-present 力软信息技术（苏州）有限公司
    /// 创建人：young
    /// 日 期：2022.10.25
    /// 描 述：数据权限
    /// </summary>
    [Table("lr_base_dataauth")]
    public class DataAuthorizeEntity
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
        /// 编码
        /// </summary>
        public string F_Code { get; set; }
        /// <summary>
        /// 1.普通权限 2.自定义表单权限
        /// </summary>
        public int? F_Type { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 对象类型1.角色2.用户
        /// </summary>
        public int? F_ObjectType { get; set; }
        /// <summary>
        /// 条件公式
        /// </summary>
        public string F_Formula { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户名字
        /// </summary>
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户名字
        /// </summary>
        public string F_ModifyUserName { get; set; }
        #endregion
    }
}

