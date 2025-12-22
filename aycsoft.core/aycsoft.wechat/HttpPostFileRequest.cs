using System.Text;
using System.Threading.Tasks;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：post文件请求
    /// </summary>
    public class HttpPostFileRequest : IHttpSend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<string> Send(string url, string data)
        {
            return new HttpHelper().PostFile(url, data, Encoding.UTF8);
        }
    }
}
