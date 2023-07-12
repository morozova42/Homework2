using FluentNHibernate.Mapping;
using WebApi.Models;

namespace WebApi.Mappings
{
	public class CustomerMap : ClassMap<Customer>
	{
		public CustomerMap()
		{
			Table("customers");
			Id(c => c.Id);
			Map(c => c.Firstname);
			Map(c => c.Lastname);
		}
	}
}
