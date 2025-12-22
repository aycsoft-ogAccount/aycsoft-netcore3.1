namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：标签更新
    /// </summary>
    public class TagUpdate : OperationRequestBase<OperationResultsBase, HttpPostRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token=ACCESS_TOKEN";
        }
        /// <summary>
        /// 
        /// </summary>

        [IsNotNull]
        public string tagid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [IsNotNull]
        [Length(1, 64)]
        public string tagname { get; set; }
    }
}
