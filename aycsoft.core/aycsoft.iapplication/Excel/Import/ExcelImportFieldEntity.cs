using cd.dapper.extension;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.19
    /// 描 述：Excel数据导入设置字段
    /// </summary>
    [Table("lr_excel_importfileds")]
    public class ExcelImportFieldEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string F_Id { get; set; }
        /// <summary>
        /// 导入模板Id
        /// </summary>
        public string F_ImportId { get; set; }
        /// <summary>
        /// 字典名字
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// excel名字
        /// </summary>
        public string F_ColName { get; set; }
        /// <summary>
        /// 唯一性验证:0要,1需要
        /// </summary>
        public int? F_OnlyOne { get; set; }
        /// <summary>
        /// 关联类型0:无关联,1:GUID,2:数据字典3:数据表;4:固定数值;5:操作人ID;6:操作人名字;7:操作时间;
        /// </summary>
        public int? F_RelationType { get; set; }
        /// <summary>
        /// 数据字典编号
        /// </summary>
        public string F_DataItemCode { get; set; }
        /// <summary>
        /// 固定数据
        /// </summary>
        public string F_Value { get; set; }
        /// <summary>
        /// 关联库id
        /// </summary>
        public string F_DataSourceId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? F_SortCode { get; set; }
        #endregion
    }
}
