using System;
namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：OpenToken获取
    /// </summary>
    public class OpenTokenGet : OperationRequestBase<OpenTokenGetResult, HttpGetRequest>
    {
        private string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return string.Format(url, appid, secret, code);
        }

        /// <summary>
        /// 应用唯一标识，在微信开放平台提交应用审核通过后获得
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string appid { get; set; }
        /// <summary>
        /// 应用密钥AppSecret，在微信开放平台提交应用审核通过后获得
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string secret { get; set; }
        /// <summary>
        /// 填写第一步获取的code参数
        /// </summary>
        [IsNotNull]
        public string code { get; set; }
    }
}
