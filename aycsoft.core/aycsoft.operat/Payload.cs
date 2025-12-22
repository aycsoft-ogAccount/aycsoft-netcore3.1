using System;
using System.Collections.Generic;
using System.Text;

namespace aycsoft.operat
{
    /// <summary>
    /// 令牌中的信息
    /// </summary>
    public class Payload
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }
    }
}
