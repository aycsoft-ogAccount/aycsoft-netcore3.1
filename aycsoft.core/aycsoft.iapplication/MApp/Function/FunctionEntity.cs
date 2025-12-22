using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.07
    /// 描 述：移动端功能管理
    /// </summary>
    [Table("lr_app_function")]
    public class FunctionEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        /// <returns></returns>
        public string F_Type { get; set; }
        /// <summary>
        /// 自定义表单ID
        /// </summary>
        /// <returns></returns>
        public string F_FormId { get; set; }
        /// <summary>
        /// 代码ID
        /// </summary>
        /// <returns></returns>
        public string F_CodeId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        /// <returns></returns>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        /// <returns></returns>
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改人名字
        /// </summary>
        /// <returns></returns>
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        public string F_Icon { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public string F_SchemeId { get; set; }
        /// <summary>
        ///  1 启用 0 停用
        /// </summary>
        public int? F_EnabledMark { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int? F_SortCode { get; set; }


        /// <summary>
        /// 是否是代码开发的1是2不是（自定义表单功能）
        /// </summary>
        /// <returns></returns>
        public int? F_IsSystem { get; set; }

        /// <summary>
        /// 功能地址
        /// </summary>
        /// <returns></returns>
        public string F_Url { get; set; }

        #endregion

        

        #region 扩展属性
        /// <summary>
        /// 模板数据
        /// </summary>
        [NotWrited]
        public string F_Scheme { get; set; }
        #endregion
    }
}