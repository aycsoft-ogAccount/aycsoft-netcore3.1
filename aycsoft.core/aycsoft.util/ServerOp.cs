namespace aycsoft.util
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.05
    /// 描 述：服务配置
    /// </summary>
    public class ServerOp
    {
        /// <summary>
        /// 软件名称
        /// </summary>
        public string SoftName { get; set; }
        /// <summary>
        /// 软件版本号
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string dbConn { get; set; }
        /// <summary>
        /// 数据库类型 SqlServer,Oracle,MySql
        /// </summary>
        public string dbType { get; set; }

        /// <summary>
        /// 缓存前缀
        /// </summary>
        public string RedisPrev { get; set; }
        /// <summary>
        /// 缓存地址
        /// </summary>
        public string RedisExchangeHosts { get; set; }
        /// <summary>
        /// 服务目录
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// 皮肤配置
        /// </summary>
        public string UItheme { get; set; }

        /// <summary>
        /// IM地址
        /// </summary>
        public string IMUrl { get; set; }
        /// <summary>
        /// 即时服务是否打开
        /// </summary>
        public bool IMOpen { get; set; }

        /// <summary>
        /// 发出邮箱设置邮箱主机
        /// </summary>
        public string MailHost { get; set; }
        /// <summary>
        /// 发出邮箱的名称
        /// </summary>
        public string MailName { get; set; }
        /// <summary>
        /// 发出邮箱的地址
        /// </summary>
        public string MailUserName { get; set; }
        /// <summary>
        /// 发出邮箱的密码
        /// </summary>
        public string MailPassword { get; set; }

        /// <summary>
        /// 企业号ID
        /// </summary>
        public string CorpId { get; set; }
        /// <summary>
        /// 企业号管理密钥
        /// </summary>
        public string CorpSecret { get; set; }
        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        public string CorpAppId { get; set; }
        /// <summary>
        /// 百度编辑器图片地址
        /// </summary>
        public string UeditorImg { get; set; }

        /// <summary>
        /// JWT认证密钥
        /// </summary>
        public string JwtSecret { get; set; }
        /// <summary>
        /// JWT认证过期时间
        /// </summary>
        public int JwtExp { get; set; }

        /// <summary>
        /// 附件存放地址（主要是考虑到api 和 web 两部分）
        /// </summary>
        public string AnnexesFile { get; set; }

    }
}
