using Infra.Repository.Base;

namespace Infra.Repository.Sample.POCO
{
	public class ExampleOCO
	{
		[Mapping(ColumnName = "ID_SAMPLE_API")]
		public long Id { get; set; }

		[Mapping(ColumnName = "NM_SAMPLE")]
		public string Name { get; set; }

		[Mapping(ColumnName = "FL_ATIVO")]
		public bool Active { get; set; }
	}
}
