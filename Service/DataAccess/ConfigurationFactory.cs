using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Caches.SysCache2;
using Service.Mappings;

namespace Service.DataAccess
{
    public static class ConfigurationFactory
    {
        private static ISessionFactory sessionFactory;
        private static readonly object lockObject = new object();

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (lockObject)
                    {
                        sessionFactory = ConfigurableSessionFactory();
                        return sessionFactory;
                    }
                }
                return sessionFactory;
            }
        }


        private static ISessionFactory ConfigurableSessionFactory()
        {
            var sqlConfiguration = MsSqlConfiguration.MsSql2008
                .ConnectionString(ConfigurationManager.AppSettings["DBConnectionString"])
                .Cache(c => c.UseQueryCache().UseMinimalPuts().ProviderClass(typeof(SysCacheProvider).AssemblyQualifiedName))
                .ProxyFactoryFactory(typeof(ProxyFactoryFactory).AssemblyQualifiedName).UseReflectionOptimizer();

            var configuration = Fluently.Configure().Database(sqlConfiguration).Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>());

            return configuration.BuildSessionFactory();
        }
    }
}