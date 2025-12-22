using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.01
    /// 描 述：任务计划模板信息
    /// </summary>
    public class SchedulingHelper : IJob
    {
        private readonly TSSchemeIBLL tSSchemeIBLL;
        private readonly TSLogIBLL tSLogIBLL;
        /// <summary>
        /// 
        /// </summary>
        public SchedulingHelper()
        {
            tSSchemeIBLL = IocManager.Instance.GetService<TSSchemeIBLL>();
            tSLogIBLL = IocManager.Instance.GetService<TSLogIBLL>();
        }

        /// <summary>
        /// 任务执行方法
        /// </summary>
        /// <param name="keyValue">任务进程主键</param>
        /// <returns></returns>
        private async Task<bool> _Execute(string keyValue)
        {
            bool isOk = true;
            string msg = "";

            TSSchemeInfoEntity tSSchemeInfoEntity = null;

            // 获取一个任务进程
            try
            {
                tSSchemeInfoEntity = await tSSchemeIBLL.GetSchemeInfoEntity(keyValue);
            }
            catch (Exception ex)
            {
                isOk = false;
                msg = "获取任务异常：" + ex.Message;
            }

            if (tSSchemeInfoEntity == null || tSSchemeInfoEntity.F_IsActive == 2)
            {
                return true;
            }

            string name = tSSchemeInfoEntity.F_Name;

            bool flag = false;
            // 执行任务
            if (isOk)
            {
                try
                {
                    TSSchemeModel tSSchemeModel = tSSchemeInfoEntity.F_Scheme.ToObject<TSSchemeModel>();
                    switch (tSSchemeModel.methodType)
                    {
                        case 1:// sql
                            await tSSchemeIBLL.ExecuteBySql(tSSchemeModel.dbId, tSSchemeModel.strSql);
                            msg = "执行sql语句【" + tSSchemeModel.dbId + "】" + tSSchemeModel.strSql;
                            break;
                        case 2:// 存储过程
                            await tSSchemeIBLL.ExecuteProc(tSSchemeModel.dbId, tSSchemeModel.procName);
                            msg = "执行存储过程【" + tSSchemeModel.dbId + "】" + tSSchemeModel.strSql;
                            break;
                        case 3:// 接口
                            msg = "调用接口";
                            if (tSSchemeModel.urlType == "1")
                            {
                                msg += "[GET]";
                                await HttpMethods.Get(tSSchemeModel.url);
                            }
                            else
                            {
                                msg += "[POST]";
                                await HttpMethods.Post(tSSchemeModel.url);
                            }
                            msg += tSSchemeModel.url;
                            break;
                        case 4:// 依赖注入
                            msg = "依赖注入" + tSSchemeModel.iocName;
                            if (!string.IsNullOrEmpty(tSSchemeModel.iocName) && IocManager.Instance.IsRegistered<ITsMethod>(tSSchemeModel.iocName))
                            {

                                ITsMethod iTsMethod = IocManager.Instance.GetService<ITsMethod>(tSSchemeModel.iocName);
                                iTsMethod.Execute();
                            }
                            break;
                    }

                    flag |= tSSchemeModel.executeType == 1;

                }
                catch (Exception ex)
                {
                    isOk = false;
                    msg += "执行方法出错：" + ex.Message;
                }
            }

            try
            {
                // 新增一条任务日志
                TSLogEntity logEntity = new TSLogEntity()
                {
                    F_ExecuteResult = isOk ? 1 : 2,
                    F_Des = msg,
                    F_Name = name
                };
                await tSLogIBLL.SaveEntity("", logEntity);
            }
            catch (Exception)
            {
            }


            return isOk;
        }

        /// <summary>
        /// 任务执行方法
        /// </summary>
        /// <param name="context"></param>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                string keyValue = dataMap.GetString("keyValue");

                TSSchemeInfoEntity tSSchemeInfoEntity = null;
                if (!await _Execute(keyValue))
                { // 如果异常，需要重新执行一次
                    tSSchemeInfoEntity = await tSSchemeIBLL.GetSchemeInfoEntity(keyValue);
                    if (tSSchemeInfoEntity != null)
                    {
                        TSSchemeModel tSSchemeModel = tSSchemeInfoEntity.F_Scheme.ToObject<TSSchemeModel>();
                        if (tSSchemeModel.isRestart == 1)
                        {
                            for (int i = 0; i < tSSchemeModel.restartNum; i++)
                            {
                                Thread.Sleep(60 * 1000 * tSSchemeModel.restartMinute);  // 停顿1000毫秒
                                if (await _Execute(keyValue))
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
