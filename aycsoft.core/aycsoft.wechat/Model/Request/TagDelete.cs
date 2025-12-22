namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：标签删除
    /// </summary>
    public class TagDelete : OperationRequestBase<OperationResultsBase, HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/delete?access_token=ACCESS_TOKEN&tagid={0}";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return string.Format(url, tagid);
        }

        /// <summary>
        /// 标签ID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string tagid { get; set; }
    }
}
