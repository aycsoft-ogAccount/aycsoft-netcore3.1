using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace aycsoft.util
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：发送消息给SignalR集结器
    /// </summary>
    public static class SendHubs
    {
        /// <summary>
        /// 调用hub方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args">参数</param>
        public static async Task callMethod(string methodName, params object[] args)
        {
            if (ConfigHelper.GetConfig().IMOpen) {
                var connection = new HubConnectionBuilder()
                    .WithUrl(ConfigHelper.GetConfig().IMUrl + "/ChatsHub")
                    .Build();
                await connection.StartAsync();
                await connection.InvokeAsync(methodName, args);
                await connection.StopAsync();
            }
            
        }
    }
}
