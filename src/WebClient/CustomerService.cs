using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebClient
{
	internal class CustomerService
	{
		internal async Task<HttpResponseMessage> CreateCustomer(CustomerCreateRequest createRequest)
		{
			using (HttpClient client = new HttpClient())
			{
				JsonContent content = JsonContent.Create(createRequest, typeof(CustomerCreateRequest));
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/customers");
				request.Content = content;
				try
				{
					var response = await client.SendAsync(request);
					return response;
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Произошла ошибка при отправке запроса: {ex.Message}");
					return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError, ReasonPhrase = ex.Message };
				}
			}
		}

		internal async Task<HttpResponseMessage> GetCustomer(long id)
		{
			using (HttpClient client = new HttpClient())
			{
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/customers/" + id);
				try
				{
					var response = await client.SendAsync(request);
					return response;
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Произошла ошибка при отправке запроса: {ex.Message}");
					return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.InternalServerError, ReasonPhrase = ex.Message };
				}
			}
		}
	}
}
