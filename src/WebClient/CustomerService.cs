using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebClient
{
	internal class CustomerService
	{
		internal async Task<Response<long?>> CreateCustomer(CustomerCreateRequest createRequest)
		{
			using (HttpClient client = new HttpClient())
			{
				JsonContent content = JsonContent.Create(createRequest, typeof(CustomerCreateRequest));
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/customers");
				request.Content = content;
				try
				{
					using var response = await client.SendAsync(request);
					var idResponse = await response.Content.ReadFromJsonAsync<Response<long?>>();
					return idResponse;
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Произошла ошибка при отправке запроса: {ex.Message}");
				}
				return new Response<long?>();
			}
		}

		internal async Task<Response<Customer>> GetCustomer(long id)
		{
			using (HttpClient client = new HttpClient())
			{
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/customers/" + id);
				try
				{
					using var response = await client.SendAsync(request);
					var customerResponse = await response.Content.ReadFromJsonAsync<Response<Customer>>();
					return customerResponse;
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Произошла ошибка при отправке запроса: {ex.Message}");
				}
				return new Response<Customer>();
			}
		}
	}
}
