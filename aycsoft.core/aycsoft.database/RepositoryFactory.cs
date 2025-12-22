using Autofac;
using ce.autofac.extension;
using aycsoft.util;
using System;

namespace aycsoft.database
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.10
    /// 描 述：定义仓储模型工厂
    /// </summary>
    public class RepositoryFactory
    {
        /// <summary>
        /// 定义仓储（基础库）
        /// </summary>
        /// <returns></returns>
        public IRepository BaseRepository()
        {
            return new Repository(IocManager.Instance.GetService<IDataBase>(ConfigHelper.GetConfig().dbType, new NamedParameter("connString", ConfigHelper.GetConfig().dbConn)));
        }

        /// <summary>
        /// 定义仓储（扩展库）
        /// </summary>
        /// <returns></returns>
        public IRepository BaseRepository(string code)
        {

            if (code == "lrsystemdb")
            {
                return BaseRepository();
            }

            var model = DbCaChe.GetValue(code);
            if (model == null)
            {
                model = BaseRepository().FindEntity<DbModel>(" select F_DBName,F_DbType,F_DbConnection  from lr_base_databaselink where F_DBName = @code ", new { code }).GetAwaiter().GetResult();
                if (model == null)
                {
                    throw (new Exception("此编码找不到对应数据库：" + code));
                }
                DbCaChe.SetValue(code, model);
            }

            return new Repository(IocManager.Instance.GetService<IDataBase>(model.F_DbType, new NamedParameter("connString", model.F_DbConnection)));
        }

        /// <summary>
        /// 定义仓储
        /// </summary>
        /// <param name="conn">链接串</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public IRepository BaseRepository(string conn, string dbType)
        {
            return new Repository(IocManager.Instance.GetService<IDataBase>(dbType, new NamedParameter("connString", conn)));
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns>SqlServer,Oracle,MySql</returns>
        public string DBType(string code)
        {
            if (code == "lrsystemdb")
            {
                return ConfigHelper.GetConfig().dbType;
            }
            else
            {
                var model = DbCaChe.GetValue(code);
                if (model == null)
                {
                    model = BaseRepository().FindEntity<DbModel>(" select F_DBName,F_DbType,F_DbConnection  form lr_base_databaselink where F_DBName = @code ", new { code }).GetAwaiter().GetResult();
                    if (model == null)
                    {
                        throw (new Exception("此编码找不到对应数据库：" + code));
                    }
                    DbCaChe.SetValue(code, model);
                }

                return model.F_DbType;
            }
        }
    }
}
