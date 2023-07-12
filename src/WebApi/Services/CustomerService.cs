using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
	public class CustomerService : ICustomerService
	{
		private ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public async Task<long?> CreateCustomer(Customer customer)
		{
			return _customerRepository.Create(customer);
		}

		public async Task<Customer> GetCustomer(long id)
		{
			return _customerRepository.Get(id);
		}
	}
}
