using WebApi.Models;

namespace WebApi.Interfaces
{
	public interface ICustomerRepository
	{
		long? Create(Customer customer);
		Customer Get(long id);
	}
}
