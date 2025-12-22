using System;
using System.Threading.Tasks;
using aycsoft.util;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：操作请求基础类
    /// </summary>
    public abstract class OperationRequestBase<T, THttp> : ISend<T>
            where T : OperationResultsBase, new()
            where THttp : IHttpSend, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract string Url();

        /// <summary>
        /// 视同attribute进行简单校验
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool Verify(out string message)
        {
            message = "";
            foreach (var pro in this.GetType().GetProperties())
            {
                var v = pro.GetCustomAttributes(typeof(IVerifyAttribute), true);


                foreach (IVerifyAttribute verify in pro.GetCustomAttributes(typeof(IVerifyAttribute), true))
                {
                    if (!verify.Verify(pro.PropertyType, pro.GetValue(this), out message))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 格式化URL，替换Token
        /// </summary>
        /// <returns></returns>
        protected async Task<string> GetUrl()
        {
            if (await Token.IsTimeOut())
            {
                await Token.GetNewToken();
            }

            string url = Url();

            if (url.Contains("=ACCESS_TOKEN"))
            {
                url = url.Replace("=ACCESS_TOKEN", "=" + await Token.GetToken());
            }

            return url;
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public async Task<T> Send()
        {
            string message;
            if (!Verify(out message))
            {
                throw new Exception(message);
            }

            //string result = new HttpHelper().Post(url, JsonConvert.SerializeObject(this), Encoding.UTF8, Encoding.UTF8);

            IHttpSend httpSend = new THttp();

            string result = await HttpSend(httpSend, await GetUrl());

            return GetDeserializeObject(result);
        }
        /// <summary>
        /// 开放平台发送
        /// </summary>
        /// <returns></returns>
        public async Task<T> OpenSend()
        {
            string message;
            if (!Verify(out message))
            {
                throw new Exception(message);
            }

            //string result = new HttpHelper().Post(url, JsonConvert.SerializeObject(this), Encoding.UTF8, Encoding.UTF8);

            IHttpSend httpSend = new THttp();

            string result = await HttpSend(httpSend, Url());

            return GetDeserializeObject(result);
        }

        /// <summary>
        /// 处理返回结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual T GetDeserializeObject(string result)
        {
            return result.ToObject<T>();
        }

        /// <summary>
        /// 处理发送请求
        /// </summary>
        /// <param name="httpSend"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected virtual Task<string> HttpSend(IHttpSend httpSend, string url)
        {
            return httpSend.Send(url, this.ToJson());
        }

    }
}
