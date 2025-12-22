using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.07
    /// 描 述：报表管理
    /// </summary>
    [Table("lr_rpt_scheme")]
    public class RptSchemeEntity
    {
        #region 实体成员
        /// <summary>
        /// 模板主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string F_TempId { get; set; }
        /// <summary>
        /// 报表名称
        /// </summary>
        /// <returns></returns>
        public string F_FullName { get; set; }
        /// <summary>
        /// 报表编号
        /// </summary>
        /// <returns></returns>
        public string F_EnCode { get; set; }
        /// <summary>
        /// 报表分类
        /// </summary>
        /// <returns></returns>
        public string F_TempCategory { get; set; }
        /// <summary>
        /// 报表风格
        /// </summary>
        /// <returns></returns>
        public string F_TempStyle { get; set; }
        /// <summary>
        /// 图表类型
        /// </summary>
        /// <returns></returns>
        public string F_TempType { get; set; }
        /// <summary>
        /// 报表介绍
        /// </summary>
        /// <returns></returns>
        public string F_Description { get; set; }
        /// <summary>
        /// 报表参数Json
        /// </summary>
        /// <returns></returns>
        public string F_ParamJson { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string F_ModifyUserName { get; set; }
        #endregion
    }
}
