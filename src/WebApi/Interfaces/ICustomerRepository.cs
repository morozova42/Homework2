using WebApi.Models;

namespace WebApi.Interfaces
{
	/// <summary>
	/// Interface for working with Customers in DB
	/// </summary>
	public interface ICustomerRepository
	{
		/// <summary>
		/// Creates new <see cref="Customer"/> in DB
		/// </summary>
		/// <param name="customer">Customer to create</param>
		/// <returns>Customer Id if success, null otherwise</returns>
		long? Create(Customer customer);

		/// <summary>
		/// Get the <see cref="Customer"/> from DB by Id
		/// </summary>
		/// <param name="id">Customer Id</param>
		/// <returns>Customer with <paramref name="id"/>, null otherwise</returns>
		Customer Get(long id);
	}
}
