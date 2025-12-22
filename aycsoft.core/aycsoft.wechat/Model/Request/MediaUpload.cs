using System.Threading.Tasks;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：媒体上传
    /// </summary>
    public class MediaUpload : OperationRequestBase<MediaUploadResult, HttpPostFileRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token=ACCESS_TOKEN&type={0}";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return string.Format(url, type);
        }

        /// <summary>
        /// 类型
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string type { get; set; }


        /// <summary>
        /// 文件地址
        /// </summary>
        /// <returns></returns>
        public string media { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpSend"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected override Task<string> HttpSend(IHttpSend httpSend, string url)
        {
            return httpSend.Send(url, media);
        }
    }
}
