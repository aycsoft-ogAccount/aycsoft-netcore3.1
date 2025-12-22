using Autofac;
using aycsoft.database.mysql;
using aycsoft.database.oracle;
using aycsoft.database.sqlserver;

namespace aycsoft.database
{
    /// <summary>
    /// 注册orm
    /// </summary>
    public class DbRegister
    {

        /// <summary>
        /// 注册数据库实现类
        /// </summary>
        /// <param name="builder">服务构建器</param>
        /// <returns>服务构建器</returns>
        public static void Register(ContainerBuilder builder)
        {
            // 注册各个数据库实现
            builder.RegisterType(typeof(SqlserverDataBase)).As(typeof(IDataBase)).Named<IDataBase>("SqlServer");
            builder.RegisterType(typeof(OracleDataBase)).As(typeof(IDataBase)).Named<IDataBase>("Oracle");
            builder.RegisterType(typeof(MySqlDataBase)).As(typeof(IDataBase)).Named<IDataBase>("MySql");
        }
    }
}
