using ce.autofac.extension;
using Microsoft.AspNetCore.Http;

namespace aycsoft.util
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.11
    /// 描 述：上下文上下文帮助类
    /// </summary>
    public static class ContextHelper {
        /// <summary>
        /// 获取上下文值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static object GetItem(string key)
        {
            var accessor = IocManager.Instance.GetService<IHttpContextAccessor>();
            accessor.HttpContext.Items.TryGetValue(key, out object res);
            return res;
        }
        /// <summary>
        /// 设置上下文值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetItem(string key,string value)
        {
            var accessor = IocManager.Instance.GetService<IHttpContextAccessor>();
            if (accessor.HttpContext.Items.ContainsKey(key))
            {
                accessor.HttpContext.Items.Remove(key);
            }
            accessor.HttpContext.Items.Add(key, value);
        }
        /// <summary>
        /// 移除上下文值
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveItem(string key)
        {
            var accessor = IocManager.Instance.GetService<IHttpContextAccessor>();
            if (accessor.HttpContext.Items.ContainsKey(key)) {
                accessor.HttpContext.Items.Remove(key);
            }
            
        }
    }
}
