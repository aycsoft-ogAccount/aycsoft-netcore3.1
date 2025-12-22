namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：发送视频信息
    /// </summary>
    public class SendVideo : MessageSend
    {
        /// <summary>
        /// 
        /// </summary>
        public override string msgtype
        {
            get { return "video"; }
        }
        /// <summary>
        /// 
        /// </summary>
        [IsNotNull]
        public SendVideo.SendItem video { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public class SendItem
        {
            /// <summary>
            /// 媒体资源文件ID
            /// </summary>
            /// <returns></returns>
            public string media_id { get; set; }

            /// <summary>
            /// 视频消息的标题
            /// </summary>
            /// <returns></returns>
            public string title { get; set; }

            /// <summary>
            /// 视频消息的描述
            /// </summary>
            /// <returns></returns>
            public string description { get; set; }
        }
    }
}
