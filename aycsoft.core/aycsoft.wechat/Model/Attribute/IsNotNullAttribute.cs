using System;
using System.Collections;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：不能为空属性
    /// </summary>
    public class IsNotNullAttribute : Attribute, IVerifyAttribute
    {
        bool IsNotNull { get; set; }

        string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IsNotNullAttribute()
        {
            IsNotNull = true;
            Message = "不能为空";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isNotNull"></param>
        public IsNotNullAttribute(bool isNotNull)
        {
            IsNotNull = isNotNull;
            Message = "不能为空";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isNull"></param>
        /// <param name="message"></param>
        public IsNotNullAttribute(bool isNull, string message)
        {
            IsNotNull = isNull;
            Message = message;
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

            if (IsNotNull == false)
            {
                return true;
            }

            if (obj == null)
            {
                message = Message;
                return false;
            }

            if (obj is IList)
            {

                if ((obj as IList).Count <= 0)
                {
                    message = Message;
                    return false;
                }
            }

            return true;
        }
    }
}
