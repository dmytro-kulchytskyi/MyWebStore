using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Nhibarnate
{
    public class NhibenateSessionFactoryConfigurator
    {
        private static readonly Lazy<ISessionFactory> sessionFactory = new Lazy<ISessionFactory>(() =>
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(
                    ConfigurationManager.ConnectionStrings["Connection"].ConnectionString).ShowSql())

                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NhibenateSessionFactoryConfigurator>())
                    .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                    .BuildSessionFactory(),
            LazyThreadSafetyMode.ExecutionAndPublication);

        public static ISessionFactory SessionFactory => sessionFactory.Value;
    }
}
