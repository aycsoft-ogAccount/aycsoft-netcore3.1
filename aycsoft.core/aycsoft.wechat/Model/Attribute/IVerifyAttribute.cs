using System;
namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：核实属性接口
    /// </summary>
    public interface IVerifyAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool Verify(Type type, object obj, out string message);
    }
}
