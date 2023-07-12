using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace WebApi.Services
{
	public class NHibernateHelper
	{
		//Тут мне непонятно, как использовать appsettings для хранения строки подключения
		//private readonly IConfiguration _configuration;

		//public NHibernateHelper(IConfiguration configuration)
		//{
		//	_configuration = configuration;
		//}

		private static ISessionFactory _sessionFactory;

		private static ISessionFactory SessionFactory
		{
			get
			{
				if (_sessionFactory == null)
				{
					using (var confManager = new ConfigurationManager())
					{
						//var connStr = confManager.GetSection("ConnectionStrings").GetConnectionString("ElephantDb");
						_sessionFactory = Fluently.Configure()
							.Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString(c => c
								.Host("trumpet.db.elephantsql.com")
								.Port(5432)
								.Database("ldodqrxb")
								.Username("ldodqrxb")
								.Password("KlCQ62FpgWHfEhEjG7ziyHscyh6pzL1C")))
							.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Startup>())
							.ExposeConfiguration(BuildSchema)
							.BuildSessionFactory();
					}
				}
				return _sessionFactory;
			}
		}

		public static ISession GetSession()
		{
			return SessionFactory.OpenSession();
		}

		private static void BuildSchema(Configuration config)
		{
			new SchemaUpdate(config).Execute(true, true);
		}
	}
}
