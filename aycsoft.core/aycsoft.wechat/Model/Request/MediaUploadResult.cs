namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：媒体上传结果
    /// </summary>
    public class MediaUploadResult : OperationResultsBase
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）,普通文件(file)
        /// </summary>
        /// <returns></returns>
        public string type { get; set; }

        /// <summary>
        /// 媒体文件上传后获取的唯一标识
        /// </summary>
        /// <returns></returns>
        public string media_id { get; set; }

        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        /// <returns></returns>
        public string created_at { get; set; }
    }
}
