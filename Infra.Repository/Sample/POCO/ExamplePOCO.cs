using Infra.Repository.Base;

namespace Infra.Repository.Sample.POCO
{
	public class ExampleOCO
	{
		[Mapping(ColumnName = "Id")]
		public long Id { get; set; }

		[Mapping(ColumnName = "Name")]
		public string Name { get; set; }

		[Mapping(ColumnName = "Active")]
		public bool Active { get; set; }
	}
}
