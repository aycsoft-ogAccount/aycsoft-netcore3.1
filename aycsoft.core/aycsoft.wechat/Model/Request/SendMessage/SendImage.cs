namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：发送图片
    /// </summary>
    public class SendImage : MessageSend
    {
        /// <summary>
        /// 
        /// </summary>
        public override string msgtype
        {
            get { return "image"; }
        }
        /// <summary>
        /// 
        /// </summary>
        [IsNotNull]
        public SendImage.SendItem image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public class SendItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string media_id { get; set; }
        }
    }
}
