using System.Collections.Generic;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：发送媒体信息
    /// </summary>
    public class SendMpnews : MessageSend
    {
        /// <summary>
        /// 
        /// </summary>
        public override string msgtype
        {
            get { return "mpnews"; }
        }
        /// <summary>
        /// 
        /// </summary>
        [IsNotNull]
        public SendMpnews.SendItemLoist mpnews { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public class SendItemLoist
        {
            /// <summary>
            /// 
            /// </summary>
            public List<SendMpnews.SendItem> articles { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class SendItem
        {
            /// <summary>
            /// 图文消息缩略图的media_id, 可以在上传多媒体文件接口中获得。此处thumb_media_id即上传接口返回的media_id
            /// </summary>
            /// <returns></returns>
            public string thumb_media_id { get; set; }

            /// <summary>
            /// 图文消息的标题
            /// </summary>
            /// <returns></returns>
            public string title { get; set; }

            /// <summary>
            /// 图文消息的作者
            /// </summary>
            /// <returns></returns>
            public string author { get; set; }

            /// <summary>
            /// 图文消息点击“阅读原文”之后的页面链接
            /// </summary>
            /// <returns></returns>
            public string content_source_url { get; set; }

            /// <summary>
            /// 图文消息的内容，支持html标签
            /// </summary>
            /// <returns></returns>
            public string content { get; set; }

            /// <summary>
            /// 图文消息的描述
            /// </summary>
            /// <returns></returns>
            public string digest { get; set; }

            /// <summary>
            /// 是否显示封面，1为显示，0为不显示
            /// </summary>
            /// <returns></returns>
            public string show_cover_pic { get; set; }
        }
    }
}
