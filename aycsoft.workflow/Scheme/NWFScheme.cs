using System.Collections.Generic;

namespace aycsoft.workflow
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：工作流模板模型
    /// </summary>
    public class NWFScheme
    {
        /// <summary>
        /// 节点数据
        /// </summary>
        public List<NWFNodeInfo> nodes { get; set; }
        /// <summary>
        /// 线条数据
        /// </summary>
        public List<NWFLineInfo> lines { get; set; }
        /// <summary>
        /// 流程撤销作废的时候执行的方法
        /// </summary>
        public NWFCloseDo closeDo { get; set; }
    }
}
