using Domain.Sample.Repository;
using Infra.Repository.Base;
using Infra.Repository.Base.Data;
using Infra.Repository.Sample.Adapter;
using Infra.Repository.Sample.POCO;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Xml.Linq;
using Example = Domain.Sample.Entity.Example;

namespace Infra.Repository.Sample
{
	public class ExampleRepository : MySqlBaseRepository<ExampleOCO>, IExampleRepository
	{
		private readonly ExampleAdpter _exampleAdpter;
		private string _connectionString ;
		public ExampleRepository(ILogger<ExampleRepository> logger) : base(logger)
		{
			_exampleAdpter = new ExampleAdpter();
			_connectionString = Environment.GetEnvironmentVariable("DB_SAMPLE_MSQL"); 
		}

		public Example Get(long id)
		{
			var sqlText = "SELECT Id,Name, Active FROM SAMPLE where id = @Id";

			var parameters = new { Id = id };

			var samplesPOCO = ExecuteQuery<ExampleOCO>(_connectionString, sqlText, parameters);

			return _exampleAdpter.Parse(samplesPOCO.FirstOrDefault());
		}

		public Example GetByName(string name)
		{
			var sqlText = @"SELECT Id,Name, Active FROM SAMPLE where Name = @Name";

			var parameters = new { Name = name };

			var samplesPOCO = ExecuteQuery<ExampleOCO>(_connectionString, sqlText, parameters);

			return _exampleAdpter.Parse(samplesPOCO.FirstOrDefault());
		}

		public void Save(Example example)
		{
			var sql = "INSERT INTO  SAMPLE (Name, Active) values ( @Name, @Active)";

			if (example.Id != 0)
			{
				sql = "UPDATE SAMPLE SET Name = @Name, Active = @Active WHERE Id = @Id ";
				var parametersUpdate = new { Name = example.Name, Active = example.Active, Id = example.Id };
				ExecuteUpdateQuery(_connectionString, sql, parametersUpdate);
			}
			else
			{
				var parameters = new { Name = example.Name, Active = example.Active };

				ExecuteUpdateQuery(_connectionString, sql, parameters);
			}

		}
	}
}
