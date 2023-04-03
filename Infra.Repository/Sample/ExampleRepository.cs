using Domain.Sample.Repository;
using Infra.Repository.Base;
using Infra.Repository.Base.Data;
using Infra.Repository.Sample.Adapter;
using Infra.Repository.Sample.POCO;
using Microsoft.Extensions.Logging;
using Example = Domain.Sample.Entity.Example;

namespace Infra.Repository.Sample
{
	public class ExampleRepository : BaseRepository, IExampleRepository
	{
		private readonly ExampleAdpter _exampleAdpter;
		public ExampleRepository(ILogger<ExampleRepository> logger) : base(logger)
		{
			_exampleAdpter = new ExampleAdpter();
		}

		public Example Get(long id)
		{
			var parameters = new Dictionary<string, object>();
			var sqlText = @"SELECT ID_SAMPLE_API,NM_SAMPLE, FL_ATIVO FROM SAMPLE where ID_SAMPLE_API =:ID_SAMPLE_API";

			parameters.Add(":ID_SAMPLE_API", id);

			var samplesPOCO = OracleBase.ExecuteSingle<ExampleOCO>(Connection.SampleDB, sqlText, parameters);

			return _exampleAdpter.Parse(samplesPOCO);
		}

		public Example GetByName(string name)
		{
			var parameters = new Dictionary<string, object>();
			var sqlText = @"SELECT ID_SAMPLE_API,NM_SAMPLE FROM SAMPLE where NM_SAMPLE =:NM_SAMPLE";

			parameters.Add(":NM_SAMPLE", name);

			var samplesPOCO = OracleBase.ExecuteSingle<ExampleOCO>(Connection.SampleDB, sqlText, parameters);

			return _exampleAdpter.Parse(samplesPOCO);
		}

		public void Save(Example example)
		{
			var parameters = new Dictionary<string, object>();
			var sql = "INSERT INTO  SAMPLE (ID_SAMPLE_API,NM_SAMPLE, FL_ATIVO) values (SAMPLE_API_SEQUENCE.nextval, :NM_SAMPLE, :FL_ATIVO)";

			parameters.Add(":NM_SAMPLE", example.Name);

			if (example.Id != 0)
			{
				sql = "UPDATE SAMPLE SET NM_SAMPLE = :NM_SAMPLE, FL_ATIVO = :FL_ATIVO WHERE ID_SAMPLE_API = :ID_SAMPLE_API ";
				parameters.Add(":ID_SAMPLE_API", example.Id);
			}

			parameters.Add(":FL_ATIVO", Convert.ToInt16(example.Active));

			OracleBase.ExecuteNoQuery(sql, parameters, System.Data.CommandType.Text, Connection.SampleDB);
		}
	}
}
