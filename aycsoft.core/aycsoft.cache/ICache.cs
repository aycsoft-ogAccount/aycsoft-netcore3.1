using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.cache
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.09
    /// 描 述：定义缓存接口
    /// </summary>
    public interface ICache
    {
        #region Key-Value
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="dbId">指定库ID,默认0</param>
        /// <returns></returns>
        T Read<T>(string cacheKey, int dbId = 0) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="dbId">指定库ID,默认0</param>
        void Write<T>(string cacheKey, T value, int dbId = 0) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="timeSpan">到期时间</param>
        /// <param name="dbId">指定库ID,默认0</param>
        void Write<T>(string cacheKey, T value, TimeSpan timeSpan, int dbId = 0) where T : class;
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="dbId">指定库ID,默认0</param>
        void Remove(string cacheKey, int dbId = 0);
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        void RemoveAll(int dbId = 0);
        #endregion
        #region List

        #region 同步方法

        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="dbId">指定库ID,默认0</param>
        void ListRemove<T>(string cacheKey, T value, int dbId = 0) where T : class;

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="dbId">指定库ID,默认0</param>
        /// <returns></returns>
        List<T> ListRange<T>(string cacheKey, int dbId = 0) where T : class;

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="dbId">指定库ID,默认0</param>
        void ListRightPush<T>(string cacheKey, T value, int dbId = 0) where T : class;

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="dbId">指定库ID,默认0</param>
        /// <returns></returns>
        T ListRightPop<T>(string cacheKey, int dbId = 0) where T : class;


        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="dbId">指定库ID,默认0</param>
        void ListLeftPush<T>(string cacheKey, T value, int dbId = 0) where T : class;

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="dbId">指定库ID,默认0</param>
        /// <returns></returns>
        T ListLeftPop<T>(string cacheKey, int dbId = 0) where T : class;

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="dbId">指定库ID,默认0</param>
        /// <returns></returns>
        long ListLength(string cacheKey, int dbId = 0);

        #endregion 同步方法

        #endregion List


        #region Key-Value 异步
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="dbId">指定库ID,默认0</param>
        /// <returns></returns>
        Task<T> ReadAsync<T>(string cacheKey, int dbId = 0) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="dbId">指定库ID,默认0</param>
        Task WriteAsync<T>(string cacheKey, T value, int dbId = 0) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="timeSpan">到期时间</param>
        /// <param name="dbId">指定库ID,默认0</param>
        Task WriteAsync<T>(string cacheKey, T value, TimeSpan timeSpan, int dbId = 0) where T : class;
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="dbId">指定库ID,默认0</param>
        Task RemoveAsync(string cacheKey, int dbId = 0);
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        /// <param name="dbId">指定库ID,默认0</param>
        Task RemoveAllAsync(int dbId = 0);
        #endregion
    }
}
