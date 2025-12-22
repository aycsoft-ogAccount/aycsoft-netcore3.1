using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.01
    /// 描 述：任务调度操作类
    /// </summary>
    public class QuartzHelper
    {
        private static readonly ISchedulerFactory schedFact = new StdSchedulerFactory();//构建一个调度工厂

        #region 新增任务
        /// <summary>
        /// 添加只执行一次的任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="starTime">任务开始时间</param>
        /// <param name="endTime">任务结束时间</param>
        /// <param name="keyValue">任务主键</param>
        public static async Task AddRepeatOneJob(string jobName, string starTime, string endTime, string keyValue)
        {
            IScheduler sched = await schedFact.GetScheduler(); //得到一个调度程序

            IJobDetail job = JobBuilder.Create<SchedulingHelper>()//新建任务执行类
                .WithIdentity(jobName, "lrts") // name "myJob", group "group1"
                .UsingJobData("keyValue", keyValue)//传递参数
                .Build();
            //开始时间处理
            DateTime StarTime = DateTime.Now;
            if (!string.IsNullOrEmpty(starTime))
            {
                StarTime = Convert.ToDateTime(starTime);
            }
            DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(StarTime, 1);
            //结束时间处理
            DateTime EndTime = DateTime.Now;
            if (!string.IsNullOrEmpty(endTime))
            {
                EndTime = Convert.ToDateTime(endTime);
            }
            DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(EndTime, 1);
            // 创建一个时间触发器
            SimpleTriggerImpl trigger = new SimpleTriggerImpl();
            trigger.Name = jobName;
            trigger.Group = "lrts";
            trigger.StartTimeUtc = starRunTime;
            trigger.EndTimeUtc = endRunTime;
            trigger.RepeatCount = 0;
            await sched.ScheduleJob(job, trigger);
            // 启动
            if (!sched.IsStarted)
            {
                await sched.Start();
            }
        }
        /// <summary>
        /// 添加简单重复执行任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="keyValue">任务主键</param>
        /// <param name="value">间隔值</param>
        /// <param name="type">间隔类型</param>
        public static async Task AddRepeatJob(string jobName, string starTime, string endTime, string keyValue, int value, string type)
        {
            IScheduler sched = await schedFact.GetScheduler(); //得到一个调度程序

            IJobDetail job = JobBuilder.Create<SchedulingHelper>()//新建任务执行类
                .WithIdentity(jobName, "lrts") // name "myJob", group "group1"
                .UsingJobData("keyValue", keyValue)//传递参数
                .Build();
            //开始时间处理
            DateTime StarTime = DateTime.Now;
            if (!string.IsNullOrEmpty(starTime))
            {
                StarTime = Convert.ToDateTime(starTime);
            }
            DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(StarTime, 1);
            //结束时间处理
            DateTime EndTime = DateTime.MaxValue.AddDays(-1);
            if (!string.IsNullOrEmpty(endTime))
            {
                EndTime = Convert.ToDateTime(endTime);
            }
            DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(EndTime, 1);
            ITrigger trigger = null;

            switch (type)
            {
                case "minute": // 分钟
                    trigger = TriggerBuilder.Create().WithIdentity(jobName, "lrts")
                        .StartAt(starRunTime)
                        .EndAt(endRunTime)
                        .WithSimpleSchedule(t =>
                        t.WithIntervalInMinutes(value)
                        .RepeatForever())
                        .Build();
                    break;
                case "hours": // 小时
                    trigger = TriggerBuilder.Create()
                        .WithIdentity(jobName, "lrts")
                        .StartAt(starRunTime)
                        .EndAt(endRunTime)
                        .WithSimpleSchedule(t =>
                        t.WithIntervalInHours(value)
                        .RepeatForever())
                        .Build();
                    break;
                case "day": // 天
                    trigger = TriggerBuilder.Create()
                         .WithIdentity(jobName, "lrts")
                         .StartAt(starRunTime)
                         .EndAt(endRunTime)
                         .WithSchedule(
                                    CalendarIntervalScheduleBuilder.Create()
                                    .WithIntervalInDays(value)
                                     )
                         .Build();
                    break;
                case "week":// 周
                    trigger = TriggerBuilder.Create()
                     .WithIdentity(jobName, "lrts")
                     .StartAt(starRunTime)
                     .EndAt(endRunTime)
                     .WithSchedule(
                                CalendarIntervalScheduleBuilder.Create()
                                 .WithIntervalInWeeks(value)
                                 )
                     .Build();
                    break;
            }
            //实例化
            await sched.ScheduleJob(job, trigger);
            //启动
            if (!sched.IsStarted)
            {
                await sched.Start();
            }
        }
        /// <summary>
        /// 添加Cron表达式任务（一个表达式）
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="keyValue">主键名称</param>
        /// <param name="corn">Cron表达式</param>
        public static async Task AddCronJob(string jobName, string starTime, string endTime, string keyValue, string corn)
        {
            IScheduler sched = await schedFact.GetScheduler(); //得到一个调度程序

            IJobDetail job = JobBuilder.Create<SchedulingHelper>()//新建任务执行类
                .WithIdentity(jobName, "lrts") // name "myJob", group "group1"
                .UsingJobData("keyValue", keyValue)//传递参数
                .Build();
            //开始时间处理
            DateTime StarTime = DateTime.Now;
            if (!string.IsNullOrEmpty(starTime))
            {
                StarTime = Convert.ToDateTime(starTime);
            }
            DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(StarTime, 1);
            //结束时间处理
            DateTime EndTime = DateTime.MaxValue.AddDays(-1);
            if (!string.IsNullOrEmpty(endTime))
            {
                EndTime = Convert.ToDateTime(endTime);
            }
            DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(EndTime, 1);
            // 创建一个时间触发器
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                             .StartAt(starRunTime)
                                             .EndAt(endRunTime)
                                             .WithIdentity(jobName, "lrts")
                                             .WithCronSchedule(corn)
                                             .Build();
            await sched.ScheduleJob(job, trigger);

            // 启动  
            if (!sched.IsStarted)
            {
                await sched.Start();
            }
        }
        /// <summary>
        /// 添加多触发器任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="keyValue">任务主键</param>
        /// <param name="list">cron数组</param>
        public static async Task AddListCronJob(string jobName, string starTime, string endTime, string keyValue, List<string> list)
        {
            IScheduler sched = await schedFact.GetScheduler(); //得到一个调度程序

            IJobDetail job = JobBuilder.Create<SchedulingHelper>()//新建任务执行类
                .WithIdentity(jobName, "lrts") // name "myJob", group "group1"
                .UsingJobData("keyValue", keyValue)//传递参数
                .StoreDurably()
                .Build();
            //开始时间处理
            DateTime StarTime = DateTime.Now;
            if (!string.IsNullOrEmpty(starTime))
            {
                StarTime = Convert.ToDateTime(starTime);
            }
            DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(StarTime, 1);
            //结束时间处理
            DateTime EndTime = DateTime.MaxValue.AddDays(-1);
            if (!string.IsNullOrEmpty(endTime))
            {
                EndTime = Convert.ToDateTime(endTime);
            }
            DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(EndTime, 1);
            await sched.AddJob(job, true);
            // 创建一个时间触发器
            for (var i = 0; i < list.Count; i++)
            {
                ITrigger trigger = TriggerBuilder.Create()
                                   .WithIdentity("trigger" + Guid.NewGuid().ToString())
                                   .StartAt(starRunTime)
                                   .EndAt(endRunTime)
                                   .ForJob(job)
                                   .WithCronSchedule(list[i])
                                   .Build();
                await sched.ScheduleJob(trigger);
            }
            // 启动  
            if (!sched.IsStarted)
            {
                await sched.Start();
            }
        }
        #endregion

        #region 删除任务
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        public static async Task DeleteJob(string jobName)
        {
            if (!string.IsNullOrEmpty(jobName))
            {
                IScheduler sched = await schedFact.GetScheduler();
                TriggerKey triggerKey = new TriggerKey(jobName, "lrts");
                await sched.PauseTrigger(triggerKey);// 停止触发器  
                await sched.UnscheduleJob(triggerKey);// 移除触发器
                await sched.DeleteJob(JobKey.Create(jobName, "lrts"));// 删除任务  
            }
        }
        #endregion

        #region 任务暂停和启动
        /// <summary>
        /// 暂停一个job任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        public static async Task PauseJob(string jobName)
        {
            if (!string.IsNullOrEmpty(jobName))
            {
                IScheduler sched = await schedFact.GetScheduler(); //得到一个调度程序
                await sched.PauseJob(JobKey.Create(jobName, "lrts"));
            }
        }
        /// <summary>
        /// 重启启动一个job
        /// </summary>
        /// <param name="jobName">任务名称</param>
        public static async Task ResumeJob(string jobName)
        {
            if (!string.IsNullOrEmpty(jobName))
            {
                IScheduler sched = await schedFact.GetScheduler(); //得到一个调度程序
                await sched.ResumeJob(JobKey.Create(jobName, "lrts"));
            }
        }
        #endregion

        #region 扩展应用
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="scheme">任务模板</param>
        public static async Task AddJob(string schemeInfoId, TSSchemeModel scheme)
        {
            string startTime = "";
            string endTime = "";
            if (scheme.startType == 2 && scheme.startTime != null)
            {
                startTime = ((DateTime)scheme.startTime).ToString("yyyy-MM-dd hh:mm:ss");
            }
            if (scheme.endType == 2 && scheme.endTime != null)
            {
                endTime = ((DateTime)scheme.endTime).ToString("yyyy-MM-dd hh:mm:ss");
            }


            switch (scheme.executeType)
            {
                case 1:// 只执行一次
                    await AddRepeatOneJob(schemeInfoId, startTime, endTime, schemeInfoId);//加入只执行一次的任务
                    break;
                case 2:// 简单重复执行
                    await AddRepeatJob(schemeInfoId, startTime, endTime, schemeInfoId, scheme.simpleValue, scheme.simpleType);
                    break;
                case 3:// 明细频率执行
                    List<string> cornlist = new List<string>();
                    foreach (var fre in scheme.frequencyList)
                    {
                        string cron = "0 ";
                        cron += fre.minute + " " + fre.hour + " ";
                        switch (fre.type)
                        {
                            case "day":
                                cron += "* ";
                                break;
                            case "week":
                                cron += "? ";
                                break;
                            case "month":
                                cron += fre.carryDate + " ";
                                break;

                        }
                        cron += fre.carryMounth + " ";
                        if (fre.type == "week")
                        {
                            cron += fre.carryDate + " ";
                        }
                        else
                        {
                            cron += "? ";
                        }
                        cron += "*";
                        cornlist.Add(cron);
                    }
                    await AddListCronJob(schemeInfoId, startTime, endTime, schemeInfoId, cornlist);
                    break;
                case 4:// corn表达式
                    await AddCronJob(schemeInfoId, startTime, endTime, schemeInfoId, scheme.cornValue);
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task InitJob()
        {
            TSSchemeIBLL tSSchemeIBLL = IocManager.Instance.GetService<TSSchemeIBLL>();
            var list = await tSSchemeIBLL.GetList();
            foreach (var item in list)
            {
                await AddJob(item.F_Id, item.F_Scheme.ToObject<TSSchemeModel>());
            }

        }
        #endregion
    }
}
