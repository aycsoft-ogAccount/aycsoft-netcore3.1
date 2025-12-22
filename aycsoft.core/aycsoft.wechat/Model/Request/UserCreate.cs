using System.Collections.Generic;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：用户创建
    /// </summary>
    public class UserCreate : OperationRequestBase<OperationResultsBase, HttpPostRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// 员工UserID。对应管理端的帐号，企业内必须唯一。长度为1~64个字符
        /// </summary>
        /// <returns></returns>
        [Length(1, 64)]
        [IsNotNull]
        public string userid { get; set; }

        /// <summary>
        /// 成员名称。长度为1~64个字符
        /// </summary>
        /// <returns></returns>
        [Length(1, 64)]
        [IsNotNull]
        public string name { get; set; }

        /// <summary>
        /// 成员所属部门id列表。注意，每个部门的直属员工上限为1000个
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public List<string> department { get; set; }

        /// <summary>
        /// 职位信息。长度为0~64个字符
        /// </summary>
        /// <returns></returns>
        [Length(0, 64)]
        public string position { get; set; }

        /// <summary>
        /// 手机号码。企业内必须唯一，mobile/weixinid/email三者不能同时为空
        /// </summary>
        /// <returns></returns>
        public string mobile { get; set; }

        /// <summary>
        /// 性别。gender=0表示男，=1表示女。默认gender=0
        /// </summary>
        /// <returns></returns>
        public string gender { get; set; }

        /// <summary>
        /// 办公电话。长度为0~64个字符
        /// </summary>
        /// <returns></returns>
        [Length(0, 64)]
        public string tel { get; set; }

        /// <summary>
        /// 邮箱。长度为0~64个字符。企业内必须唯一
        /// </summary>
        /// <returns></returns>
        [Length(0, 64)]
        public string email { get; set; }

        /// <summary>
        /// 微信号。企业内必须唯一
        /// </summary>
        /// <returns></returns>
        public string weixinid { get; set; }
    }
}
