using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace aycsoft.util
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.23
    /// 描 述：百度编辑器UE配置文件操作文件操作
    /// </summary>
    public static class UeditorConfig
    {
        private static bool noCache = true;
        private static JObject BuildItems()
        {
            string configPath = ConfigHelper.GetValue<string>("baseDir") + "/wwwroot/lib/ueditor/config/config.json";
            var json = File.ReadAllText(configPath);
            return JObject.Parse(json);
        }
        /// <summary>
        /// 
        /// </summary>
        public static JObject Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }
        private static JObject _Items;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}
