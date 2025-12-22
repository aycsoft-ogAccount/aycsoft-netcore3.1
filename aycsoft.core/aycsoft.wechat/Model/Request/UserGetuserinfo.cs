namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：获取用户信息
    /// </summary>
    public class UserGetuserinfo : OperationRequestBase<UserGetuserinfoResult, HttpGetRequest>
    {
        private string url =
            "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=ACCESS_TOKEN&code={0}&agentid={1}";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return string.Format(url, code, agentid);
        }

        /// <summary>
        /// 通过员工授权获取到的code，每次员工授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string code { get; set; }

        /// <summary>
        /// 跳转链接时所在的企业应用ID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string agentid { get; set; }
    }
}
