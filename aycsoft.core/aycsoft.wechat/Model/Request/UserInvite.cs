namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：邀请用户关注
    /// </summary>
    public class UserInvite : OperationRequestBase<OperationResultsBase, HttpPostRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/invite/send?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// 员工UserID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string userid { get; set; }

    }
}
