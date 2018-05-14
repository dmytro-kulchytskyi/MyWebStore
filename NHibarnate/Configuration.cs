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

namespace DimasikStore.Nhibarnate
{
    public class Configuration
    {
        private static readonly Lazy<ISessionFactory> sessionFactory = new Lazy<ISessionFactory>(() =>
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConfigurationManager.ConnectionStrings["DbConnection"].ToString()).ShowSql())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Configuration>())
                    .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                    .BuildSessionFactory(),
            LazyThreadSafetyMode.PublicationOnly);

        public static ISessionFactory SessionFactory => sessionFactory.Value;
    }
}
