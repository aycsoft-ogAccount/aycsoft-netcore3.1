namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：发送文本消息
    /// </summary>
    public class SendText : MessageSend
    {
        /// <summary>
        /// 
        /// </summary>
        public override string msgtype
        {
            get { return "text"; }
        }
        /// <summary>
        /// 
        /// </summary>
        [IsNotNull]
        public SendText.SendItem text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public class SendItem
        {
            /// <summary>
            /// 消息内容
            /// </summary>
            /// <returns></returns>
            [IsNotNull]
            public string content { get; set; }
        }
    }
}
