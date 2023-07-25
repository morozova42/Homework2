using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.DbWorkers
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly NHibernateHelper _nHibernateHelper;

		public CustomerRepository(NHibernateHelper hibernateHelper)
		{
			_nHibernateHelper = hibernateHelper;
		}

		public long? Create(Customer customer)
		{
			using (var session = _nHibernateHelper.GetSession())
			{
				if (session.QueryOver<Customer>().Where(c => c.Id == customer.Id).RowCount() > 0)
				{
					return null;
				}
				return session.Save(customer) as long?;
			}
		}

		public Customer Get(long id)
		{
			using (var session = _nHibernateHelper.GetSession())
			{
				return session.QueryOver<Customer>().Where(c => c.Id == id).SingleOrDefault();
			}
		}
	}
}
