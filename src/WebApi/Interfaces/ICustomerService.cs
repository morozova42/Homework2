using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces
{
	public interface ICustomerService
	{
		Task<long?> CreateCustomer(Customer customer);
		Task<Customer> GetCustomer(long id);
	}
}
