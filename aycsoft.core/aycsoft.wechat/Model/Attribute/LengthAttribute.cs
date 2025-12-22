using System;
namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：字段长度属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class LengthAttribute : Attribute, IVerifyAttribute
    {
        int MinLength { get; set; }

        int MaxLength { get; set; }

        string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        public LengthAttribute(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            Message = string.Format("字符串长度应在{0}到{1}之间", minLength, maxLength);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="message"></param>
        public LengthAttribute(int minLength, int maxLength, string message)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            Message = string.Format(message, minLength, maxLength);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Verify(Type type, object obj, out string message)
        {
            message = "";

            if (type == typeof(string) && obj != null)
            {
                if ((obj as string).Length > MaxLength || (obj as string).Length < MinLength)
                {
                    message = Message;
                    return false;
                }
            }

            return true;
        }
    }
}
