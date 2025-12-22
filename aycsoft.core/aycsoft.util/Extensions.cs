using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aycsoft.util
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.09
    /// 描 述：扩展.json序列反序列化
    /// </summary>
    public static class ExtensionsJson
    {
        /// <summary>
        /// 转成json对象
        /// </summary>
        /// <param name="Json">json字串</param>
        /// <returns></returns>
        public static object ToJson(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject(Json);
        }
        /// <summary>
        /// 转成json字串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        /// <summary>
        /// 转成json字串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="datetimeformats">时间格式化</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string datetimeformats)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        /// <summary>
        /// 字串反序列化成指定对象实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="Json">字串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string Json)
        {
            return Json == null ? default(T) : JsonConvert.DeserializeObject<T>(Json);
        }
        /// <summary>
        /// 字串反序列化成指定对象实体(列表)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="Json">字串</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<List<T>>(Json);
        }
        /// <summary>
        /// 字串反序列化成DataTable
        /// </summary>
        /// <param name="Json">字串</param>
        /// <returns></returns>
        public static DataTable ToTable(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<DataTable>(Json);
        }
        /// <summary>
        /// 字串反序列化成linq对象
        /// </summary>
        /// <param name="Json">字串</param>
        /// <returns></returns>
        public static JObject ToJObject(this string Json)
        {
            return Json == null ? JObject.Parse("{}") : JObject.Parse(Json.Replace("&nbsp;", ""));
        }
    }
    /// <summary>
    /// 检测扩展
    /// </summary>
    public static class ExtensionsValid
    {
        /// <summary>
        /// 检测空值,为null则抛出ArgumentNullException异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNull(this object obj, string parameterName)
        {
            if (obj == null)
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid? value)
        {
            if (value == null)
                return true;
            return IsEmpty(value.Value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid value)
        {
            if (value == Guid.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this object value)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }
        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="obj">字串</param>
        /// <param name="value">包含字串</param>
        /// <returns></returns>
        public static bool ContainsEx(this string obj, string value)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return false;
            }
            else
            {
                return obj.Contains(value);
            }
        }
        /// <summary>
        /// 字串是否在指定字串中存在
        /// </summary>
        /// <param name="obj">字串</param>
        /// <param name="value">被包含字串</param>
        /// <returns></returns>
        public static bool Like(this string obj, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(obj))
            {
                return false;
            }
            else
            {
                if (value.IndexOf(obj) != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 数据字段类型转化
        /// </summary>
        /// <param name="str">字串</param>
        /// <returns></returns>
        public static DbType ToDbType(this string str) {
            DbType res = DbType.String;
            switch (str)
            {
                case "int?":
                    res = DbType.Int32;
                    break;
                case "byte?":
                    res = DbType.Byte;
                    break;
                case "float?":
                    res = DbType.Single;
                    break;
                case "decimal?":
                    res = DbType.Decimal;
                    break;
                case "string":
                    res = DbType.String;
                    break;
                case "bool?":
                    res = DbType.Boolean;
                    break;
                case "DateTime?":
                    res = DbType.DateTime;
                    break;
            }

            return res;
        }

    }
    /// <summary>
    /// 字串扩展
    /// </summary>
    public static class ExtensionsStr
    {
        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToDateTimeString(this DateTime dateTime, bool isRemoveSecond = false)
        {
            if (isRemoveSecond)
                return dateTime.ToString("yyyy-MM-dd HH:mm");
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false)
        {
            if (dateTime == null)
                return string.Empty;
            return ToDateTimeString(dateTime.Value, isRemoveSecond);
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToDateString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ToDateString(dateTime.Value);
        }

        /// <summary>
        /// 获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ToTimeString(dateTime.Value);
        }

        /// <summary>
        /// 获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToMillisecondString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToMillisecondString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ToMillisecondString(dateTime.Value);
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToChineseDateString(this DateTime dateTime)
        {
            return string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToChineseDateString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ToChineseDateString(dateTime.SafeValue());
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToChineseDateTimeString(this DateTime dateTime, bool isRemoveSecond = false)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
            result.AppendFormat(" {0}时{1}分", dateTime.Hour, dateTime.Minute);
            if (isRemoveSecond == false)
                result.AppendFormat("{0}秒", dateTime.Second);
            return result.ToString();
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToChineseDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false)
        {
            if (dateTime == null)
                return string.Empty;
            return ToChineseDateTimeString(dateTime.Value);
        }
    }
    /// <summary>
    /// 转化扩展
    /// </summary>
    public static class ExtensionsConvert
    {
        #region 数值转换
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int ToInt(this object data)
        {
            if (data == null)
                return 0;
            int result;
            var success = int.TryParse(data.ToString(), out result);
            if (success)
                return result;
            try
            {
                return Convert.ToInt32(ToDouble(data, 0));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int? ToIntOrNull(this object data)
        {
            if (data == null)
                return null;
            int result;
            bool isValid = int.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double ToDouble(this object data)
        {
            if (data == null)
                return 0;
            double result;
            return double.TryParse(data.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为双精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object data, int digits)
        {
            return Math.Round(ToDouble(data), digits);
        }

        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double? ToDoubleOrNull(this object data)
        {
            if (data == null)
                return null;
            double result;
            bool isValid = double.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal ToDecimal(this object data)
        {
            if (data == null)
                return 0;
            decimal result;
            return decimal.TryParse(data.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object data, int digits)
        {
            return Math.Round(ToDecimal(data), digits);
        }

        /// <summary>
        /// 转换为可空高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal? ToDecimalOrNull(this object data)
        {
            if (data == null)
                return null;
            decimal result;
            bool isValid = decimal.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为可空高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object data, int digits)
        {
            var result = ToDecimalOrNull(data);
            if (result == null)
                return null;
            return Math.Round(result.Value, digits);
        }

        #endregion

        #region 日期转换
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime ToDate(this object data)
        {
            if (data == null)
                return DateTime.MinValue;
            DateTime result;
            return DateTime.TryParse(data.ToString(), out result) ? result : DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateOrNull(this object data)
        {
            if (data == null)
                return null;
            DateTime result;
            bool isValid = DateTime.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        #endregion

        #region 布尔转换
        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool(this object data)
        {
            if (data == null)
                return false;
            bool? value = GetBool(data);
            if (value != null)
                return value.Value;
            bool result;
            return bool.TryParse(data.ToString(), out result) && result;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        private static bool? GetBool(this object data)
        {
            switch (data.ToString().Trim().ToLower())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool? ToBoolOrNull(this object data)
        {
            if (data == null)
                return null;
            bool? value = GetBool(data);
            if (value != null)
                return value.Value;
            bool result;
            bool isValid = bool.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        #endregion

        #region 字符串转换
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="data">数据</param>
        public static string ToString(this object data)
        {
            return data == null ? string.Empty : data.ToString().Trim();
        }
        #endregion
    }
}
