using System;
using System.Text;
using System.Threading.Tasks;
using aycsoft.util;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：Token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 
        /// </summary>
        public static Token _Token;
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int expires_in { get; set; }

        private DateTime createTokenTime = DateTime.Now;

        /// <summary>
        /// 到期时间(防止时间差，提前1分钟到期)
        /// </summary>
        /// <returns></returns>
        public DateTime TookenOverdueTime
        {
            get { return createTokenTime.AddSeconds(expires_in - 60); }
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        public static async Task Renovate()
        {
            if (_Token == null)
            {
                await GetNewToken();
            }

            Token._Token.createTokenTime = DateTime.Now;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> IsTimeOut()
        {
            if (_Token == null)
            {
                await GetNewToken();
            }

            return DateTime.Now >= Token._Token.TookenOverdueTime;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<Token> GetNewToken()
        {
            string strulr = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";

            string corpID = ConfigHelper.GetConfig().CorpId; //企业ID
            string Secret = ConfigHelper.GetConfig().CorpSecret;//管理员组ID

            HttpHelper http = new HttpHelper();

            string respone = await http.Get(string.Format(strulr, corpID, Secret), Encoding.UTF8);

            var token = respone.ToObject<Token>();

            Token._Token = token;

            return token;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetToken()
        {
            if (_Token == null)
            {
                await GetNewToken();
            }
            return _Token.access_token;
        }
    }
}
