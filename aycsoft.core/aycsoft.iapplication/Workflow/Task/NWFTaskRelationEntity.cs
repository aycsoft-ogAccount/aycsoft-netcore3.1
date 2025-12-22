using cd.dapper.extension;
using System;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流任务执行人对应关系表(新)
    /// </summary>
    [Table("lr_nwf_taskrelation")]
    public class NWFTaskRelationEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Key]
        public string F_Id { get; set; }
        /// <summary> 
        /// 任务主键 
        /// </summary> 
        public string F_TaskId { get; set; }
        /// <summary> 
        /// 任务执行人员主键 
        /// </summary> 
        public string F_UserId { get; set; }
        /// <summary>
        /// 标记0需要处理1暂时不需要处理
        /// </summary>
        public int? F_Mark { get; set; }
        /// <summary>
        /// 处理结果0.未处理1.同意2.不同意3.超时4.其他
        /// </summary>
        public int? F_Result { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? F_Sort { get; set; }
        /// <summary>
        /// 任务执行时间
        /// </summary>
        public DateTime? F_Time { get; set; }

        #endregion
    }
}
