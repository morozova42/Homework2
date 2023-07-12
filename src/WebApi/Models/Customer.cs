using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace WebApi.Models
{
	public class Customer
	{
		public virtual long Id { get; init; }

		[Required]
		public virtual string Firstname { get; init; }

		[Required]
		public virtual string Lastname { get; init; }

		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}