using System;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
	static class Program
	{
		const int StatusOk = 200;
		const int StatusCreated = 201;
		const int ExistingId = 5;

		static Task Main(string[] args)
		{
			Console.WriteLine("Enter '123' to get customer with identificator 123");
			Console.WriteLine($"Enter 'random' to create new customer and get his information");
			Console.WriteLine($"Enter 'random+' to trying create customer with existing id");
			Console.WriteLine(@"Enter 'q' to exit");
			CustomerService customerService = new CustomerService();

			string command;
			do
			{
				command = Console.ReadLine();
				if (long.TryParse(command, out long id))
				{
					using var customerResponse = customerService.GetCustomer(id).Result;
					if (customerResponse.StatusCode != System.Net.HttpStatusCode.OK)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"{customerResponse.ReasonPhrase}: {customerResponse.Content.ReadAsStringAsync().Result}");
					}
					else
					{
						Console.WriteLine(customerResponse.Content.ReadFromJsonAsync<Customer>().Result);
					}
					Console.ResetColor();
				}
				else if (command == "random")
				{
					CustomerCreateRequest newCustomer = RandomCustomer();
					using var idResponse = customerService.CreateCustomer(newCustomer).Result;
					if (idResponse.StatusCode != System.Net.HttpStatusCode.Created)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"{idResponse.ReasonPhrase}: {idResponse.Content.ReadAsStringAsync().Result}");
					}
					else
					{
						var newId = idResponse.Content.ReadAsStringAsync().Result;
						Console.WriteLine(newId);
						var customerResponse = customerService.GetCustomer(long.Parse(newId)).Result;
						Console.WriteLine(customerResponse.Content.ReadFromJsonAsync<Customer>().Result);
					}
					Console.ResetColor();
				}
				else if (command == "random+")
				{
					CustomerCreateRequest newCustomer = RandomCustomer();
					newCustomer.Id = ExistingId;
					using var idResponse = customerService.CreateCustomer(newCustomer).Result;
					if (idResponse.StatusCode != System.Net.HttpStatusCode.Created)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"{idResponse.ReasonPhrase}: {idResponse.Content.ReadAsStringAsync().Result}");
					}
					else
					{
						var newId = idResponse.Content.ReadAsStringAsync().Result;
						Console.WriteLine(newId);
						var customerResponse = customerService.GetCustomer(long.Parse(newId)).Result;
						Console.WriteLine(customerResponse.Content.ReadFromJsonAsync<Customer>().Result);
					}
					Console.ResetColor();
				}
			}
			while (command.ToLower() != "q");

			return null;
		}

		private static CustomerCreateRequest RandomCustomer()
		{
			Random rnd = new Random();
			return new CustomerCreateRequest
			{
				Firstname = GetRandomString(rnd.Next(3, 8)),
				Lastname = GetRandomString(rnd.Next(5, 12))
			};
		}

		private static string GetRandomString(int length)
		{
			StringBuilder str_build = new StringBuilder();
			Random random = new Random();

			char letter;

			for (int i = 0; i < length; i++)
			{
				double flt = random.NextDouble();
				int shift = Convert.ToInt32(Math.Floor(25 * flt));
				letter = Convert.ToChar(shift + 65);
				str_build.Append(letter);
			}
			return str_build.ToString();
		}
	}
}