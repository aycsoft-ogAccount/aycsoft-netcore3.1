using System;
namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：用户信息获取
    /// </summary>
    public class OpenUserGet : OperationRequestBase<OpenUserGetResult, HttpGetRequest>
    {
        private string url = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}lang=zh_CN";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return string.Format(url, access_token, openid);
        }

        /// <summary>
        /// 普通用户标识，对该公众帐号唯一
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string openid { get; set; }
        /// <summary>
        /// token
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string access_token { get; set; }
    }
}
