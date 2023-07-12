using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
	[Route("customers")]
	public class CustomerController : Controller
	{
		private ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet("{id:long}")]
		public async Task<Response<Customer>> GetCustomerAsync([FromRoute] long id)
		{
			Console.WriteLine($"Got GET request with id = {id}");
			try
			{
				var customer = await _customerService.GetCustomer(id);
				if (customer == null)
				{
					return new Response<Customer> { Result = 404, Error = "Не найден пользователь с таким id" };
				}

				return new Response<Customer> { Result = 200, Data = customer };
			}
			catch (Exception ex)
			{
				return new Response<Customer> { Result = 500, Error = ex.Message};
			}
		}

		[HttpPost]
		public async Task<Response<long?>> CreateCustomerAsync([FromBody] Customer customer)
		{
			Console.WriteLine($"Got POST request with body = {customer}");
			try
			{
				var id = await _customerService.CreateCustomer(customer);
				if (id == null)
				{
					return new Response<long?> { Result = 409, Error = "Пользователь с таким id уже есть в БД" };
				}
				return new Response<long?> { Result = 201, Data = id };
			}
			catch (Exception ex)
			{
				return new Response<long?> { Result = 500, Error = ex.Message };
			}
		}
	}
}