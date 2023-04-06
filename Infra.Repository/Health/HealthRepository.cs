using Domain.Health.Repository;
using Infra.Repository.Base;
using Infra.Repository.Base.Data;
using Infra.Repository.Sample.POCO;
using Microsoft.Extensions.Logging;

namespace Infra.Repository.Health
{
	public class HealthRepository : MySqlBaseRepository<ExampleOCO>, IHealthRepository
	{
		string _connectionString;
		public HealthRepository(ILogger<HealthRepository> logger) : base(logger)
		{
			_connectionString = Environment.GetEnvironmentVariable("DB_SAMPLE_MSQL");
		}
		public void IsReady()
		{
			var sqlText = "SELECT * FROM SAMPLE LIMIT 1";

			ExecuteQuery<ExampleOCO>(_connectionString, sqlText);
		}
	}
}
