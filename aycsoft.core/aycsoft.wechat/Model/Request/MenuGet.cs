namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：菜单获取
    /// </summary>
    public class MenuGet : OperationRequestBase<MenuGetResult, HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/menu/get?access_token=ACCESS_TOKEN&agentid={0}";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return string.Format(url, agentid);
        }


        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string agentid { get; set; }
    }
}
