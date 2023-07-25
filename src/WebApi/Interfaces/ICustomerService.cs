using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces
{
	/// <summary>
	/// Adapter service to <see cref="ICustomerRepository"/>
	/// </summary>
	public interface ICustomerService
	{
		/// <summary>
		/// Creates new <see cref="Customer"/> in <see cref="ICustomerRepository"/>
		/// </summary>
		/// <param name="customer">Customer to create</param>
		Task<long?> CreateCustomer(Customer customer);

		/// <summary>
		/// Gets the <see cref="Customer"/> from <see cref="ICustomerRepository"/> by Id
		/// </summary>
		/// <param name="id">Customer Id</param>
		Task<Customer> GetCustomer(long id);
	}
}
