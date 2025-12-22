using ce.autofac.extension;
using aycsoft.iapplication;
using Quartz;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.02.12
    /// 描 述：流程定时执行任务
    /// </summary>
    public class WorkFlowJob:IJob
    {
        private readonly NWFProcessIBLL nWFProcessIBLL;

        /// <summary>
        /// 
        /// </summary>
        public WorkFlowJob()
        {
            nWFProcessIBLL = IocManager.Instance.GetService<NWFProcessIBLL>();
        }
        /// <summary>
        /// 执行任务方法
        /// </summary>
        /// <param name="context"></param>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // 流程任务超时处理
                await nWFProcessIBLL.MakeTaskTimeout();
            }
            catch
            {

            }
        }
    }
}
