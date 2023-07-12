using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
	public class CustomerRepository : ICustomerRepository
	{
		public long? Create(Customer customer)
		{
			using (var session = NHibernateHelper.GetSession())
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
			using (var session = NHibernateHelper.GetSession())
			{
				return session.QueryOver<Customer>().Where(c => c.Id == id).SingleOrDefault();
			}
		}
	}
}
