using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace WebApi.DbWorkers
{
	public class NHibernateHelper
	{
		private readonly IConfiguration _configuration;

		public NHibernateHelper(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		private ISessionFactory _sessionFactory;

		private ISessionFactory SessionFactory
		{
			get
			{
				if (_sessionFactory == null)
				{
					using (var confManager = new ConfigurationManager())
					{
						var connStr = _configuration["ConnectionStrings:ElephantDb"];
						_sessionFactory = Fluently.Configure()
							.Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString(c => c.Is(connStr)))
							.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Startup>())
							.ExposeConfiguration(BuildSchema)
							.BuildSessionFactory();
					}
				}
				return _sessionFactory;
			}
		}

		public ISession GetSession()
		{
			return SessionFactory.OpenSession();
		}

		private void BuildSchema(Configuration config)
		{
			new SchemaUpdate(config).Execute(true, true);
		}
	}
}
