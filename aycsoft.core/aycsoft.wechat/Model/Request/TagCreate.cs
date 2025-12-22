namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：标签创建
    /// </summary>
    public class TagCreate : OperationRequestBase<TagCreateResult, HttpPostRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/tag/create?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// 标签名称。长度为1~64个字符，标签不可与其他同组的标签重名，也不可与全局标签重名
        /// </summary>
        /// <returns></returns>
        [Length(1, 64)]
        [IsNotNull]
        public string tagname { get; set; }
    }
}
