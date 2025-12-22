using System.Collections.Generic;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：菜单创建
    /// </summary>
    public class MenuCreate : OperationRequestBase<OperationResultsBase, HttpPostRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN&agentid={0}";
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
        public string agentid { private get; set; }

        /// <summary>
        /// 一级菜单数组，个数应为1~3个
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> button { get; set; }
    }
}
