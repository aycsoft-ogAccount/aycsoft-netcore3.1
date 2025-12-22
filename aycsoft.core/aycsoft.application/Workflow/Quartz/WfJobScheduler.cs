using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.02.12
    /// 描 述：任务调度器
    /// </summary>
    public class WfJobScheduler
    {
        /// <summary>
        /// 开启任务调度器
        /// </summary>
        public static async Task Start()
        {
            IScheduler scheduler =await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<WorkFlowJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("triggerName", "groupName")
              .WithSimpleSchedule(t =>
                t.WithIntervalInMinutes(60)//WithIntervalInHours
                 .RepeatForever())
                 .Build();

           await scheduler.ScheduleJob(job, trigger);
        }
        /// <summary>
        /// 关闭任务调度器
        /// </summary>
        public static async Task End()
        {
            IScheduler scheduler =await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Shutdown(true);
        }
    }
}
