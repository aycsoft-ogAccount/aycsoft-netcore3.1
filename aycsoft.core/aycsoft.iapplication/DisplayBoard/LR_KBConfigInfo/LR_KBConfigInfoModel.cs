namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.14
    /// 描 述：看板配置信息数据模型
    /// </summary>
    public class ConfigInfoModel
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 模块类型
        /// </summary>
        public string modelType { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string dataType { get; set; }
        /// <summary>
        /// 类型 1 sql语句 2 接口
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 数据库ID
        /// </summary>
        public string dbId { get; set; }
        /// <summary>
        /// sql 语句
        /// </summary>
        public string sql { get; set; }
        /// <summary>
        /// 接口url地址
        /// </summary>
        public string url { get; set; }
    }
    /// <summary>
    /// 看板配置信息
    /// </summary>
    public class ConfigInfoDataModel
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 类型 1 sql语句 2 接口
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 模块类型
        /// </summary>
        public string modelType { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string dataType { get; set; }
        /// <summary>
        /// 请求数据
        /// </summary>
        public object data { get; set; }
    }
}
