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

		/// <summary>
		/// Get customer
		/// </summary>
		/// <param name="id">id of customer</param>
		/// <returns>Customer with id</returns>
		/// <response code="200">Successfully got</response>
		/// <response code="404">No such customer found</response>
		/// <response code="500">Server error</response>
		[HttpGet("{id:long}")]
		[ProducesResponseType(typeof(Customer), 200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<ActionResult> GetCustomerAsync([FromRoute] long id)
		{
			Console.WriteLine($"Got GET request with id = {id}");
			try
			{
				var customer = await _customerService.GetCustomer(id);
				if (customer == null)
				{
					return NotFound("Не найден пользователь с таким id");
				}

				return Ok(customer);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		/// <summary>
		/// Create new customer
		/// </summary>
		/// <param name="customer">Customer to create</param>
		/// <returns>id of customer entity in db</returns>
		/// <response code="201">Successfully created</response>
		/// <response code="409">Customer with this id already exists</response>
		/// <response code="500">Server error</response>
		[HttpPost]
		[ProducesResponseType(typeof(long), 201)]
		[ProducesResponseType(409)]
		[ProducesResponseType(500)]
		public async Task<ActionResult> CreateCustomerAsync([FromBody] Customer customer)
		{
			Console.WriteLine($"Got POST request with body = {customer}");
			try
			{
				var id = await _customerService.CreateCustomer(customer);
				if (id == null)
				{
					return Conflict("Пользователь с таким id уже есть в БД");
				}

				return StatusCode(201, id);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}