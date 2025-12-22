using System.Threading.Tasks;

namespace aycsoft.operat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.12
    /// 描 述：当前连接用户信息处理类
    /// </summary>
    public interface IOperator
    {
        ///// <summary>
        ///// 判断是否登录
        ///// </summary>
        ///// <param name="account">账号</param>
        ///// <param name="token">token</param>
        ///// <returns></returns>
        //Task<bool> IsOnLine(string account, string token);

        ///// <summary>
        ///// 添加登录者
        ///// </summary>
        ///// <param name="account">账号</param>
        ///// <returns></returns>
        //Task<string> AddLoginUser(string account);

        ///// <summary>
        ///// 清除登录者信息
        ///// </summary>
        ///// <param name="account">登录者用户账号</param>
        ///// <returns></returns>
        //Task ClearLoginUser(string account);

        /// <summary>
        /// 生成jwt令牌
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="userName">用户名称</param>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        string EncodeToken(string userId, string userName, string account);
        /// <summary>
        /// 解密jwt令牌
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns>TokenExpiredException 时间过期,SignatureVerificationException 签证不正确</returns>
        string DecodeToken(string token);
    }
}
