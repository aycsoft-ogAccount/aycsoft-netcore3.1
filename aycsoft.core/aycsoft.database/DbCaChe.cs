using System.Collections.Concurrent;

namespace aycsoft.database
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.10
    /// 描 述：扩展数据库信息缓存
    /// </summary>
    public class DbCaChe
    {
        /// <summary>
        /// 扩展数据库缓存
        /// </summary>
        private static readonly ConcurrentDictionary<string, DbModel> _dblist =
           new ConcurrentDictionary<string, DbModel>();


        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetValue(string key, DbModel value)
        {
            _dblist.GetOrAdd(key, value);
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static DbModel GetValue(string key)
        {
            _dblist.TryGetValue(key, out DbModel result);
            return result;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static DbModel RemoveValue(string key)
        {
            _dblist.TryGetValue(key, out DbModel result);
            return result;
        }

    }
}
