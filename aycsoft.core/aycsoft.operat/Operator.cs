using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using aycsoft.util;
using System;

namespace aycsoft.operat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.12
    /// 描 述：当前连接用户信息处理类
    /// </summary>
    public class Operator: IOperator
    {
        //private readonly ICache _cache;
        //public Operator(ICache cache) {
        //    _cache = cache;
        //}
        ///// <summary>
        ///// 判断是否登录
        ///// </summary>
        ///// <param name="account">账号</param>
        ///// <param name="token">token</param>
        ///// <returns></returns>
        //public async Task<bool> IsOnLine(string account,string token)
        //{
        //    try
        //    {
        //        string accountTmp = await _cache.ReadAsync<string>(token);

        //        if (account == accountTmp)
        //        {
        //            return true;
        //        }
        //        else {
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// 添加登录者(保存一天时间)
        ///// </summary>
        ///// <param name="account">账号</param>
        ///// <returns></returns>
        //public async Task<string> AddLoginUser(string account) {
        //    string token = Guid.NewGuid().ToString();
        //    TimeSpan ts = new TimeSpan(1, 0, 0, 0);
        //    var list = await _cache.ReadAsync<List<string>>(account);
        //    if (list == null) {
        //        list = new List<string>();
        //    }
        //    list.Add(token);
        //    await _cache.WriteAsync(account, list, ts);
        //    await _cache.WriteAsync(token, account, ts);
        //    return token;
        //}

        ///// <summary>
        ///// 清除登录者信息
        ///// </summary>
        ///// <param name="account">登录者用户账号</param>
        ///// <returns></returns>
        //public async Task ClearLoginUser(string account)
        //{
        //    var list = await _cache.ReadAsync<List<string>>(account);
        //    if (list != null)
        //    {
        //        foreach (var token in list)
        //        {
        //            await _cache.RemoveAsync(token);
        //        }
        //        await _cache.RemoveAsync(account);
        //    }
        //}



        /// <summary>
        /// 生成jwt令牌
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="userName">用户名称</param>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        public string EncodeToken(string userId, string userName, string account) {
            var token = new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(ConfigHelper.GetConfig().JwtSecret)
            .AddClaim("iat",DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(12).ToUnixTimeSeconds()) // 设置12小时过期
            .AddClaim("UserId", userId).AddClaim("UserName", userName).AddClaim("Account", account)
            .Encode();

            return token;
        }
        /// <summary>
        /// 解密jwt令牌
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns>TokenExpiredException 时间过期,SignatureVerificationException 签证不正确</returns>
        public string DecodeToken(string token)
        {
            try
            {
                var json = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(ConfigHelper.GetConfig().JwtSecret)
                    .MustVerifySignature()
                    .Decode(token);
                return json;
                //Console.WriteLine(json);
            }
            catch (TokenExpiredException)
            {
                return "TokenExpiredException";
                //Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                return "SignatureVerificationException";
                //Console.WriteLine("Token has invalid signature");
            }
        }
    }
}
