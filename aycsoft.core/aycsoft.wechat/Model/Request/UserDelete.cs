namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：用户删除
    /// </summary>
    public class UserDelete : OperationRequestBase<OperationResultsBase, HttpPostRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token=ACCESS_TOKEN&userid={0}";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return string.Format(url, userid);
        }

        /// <summary>
        /// 员工UserID。对应管理端的帐号
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string userid { get; set; }
    }
}
