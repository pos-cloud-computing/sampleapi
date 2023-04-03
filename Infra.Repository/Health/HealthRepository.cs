using Domain.Health.Repository;
using Infra.Repository.Base;
using Infra.Repository.Base.Data;
using Microsoft.Extensions.Logging;

namespace Infra.Repository.Health
{
	public class HealthRepository : BaseRepository, IHealthRepository
	{
		public HealthRepository(ILogger<HealthRepository> logger) : base(logger)
		{
		}
		public void IsReady()
		{
			OracleBase.ExecuteSingle<bool>(Connection.SampleDB, "SELECT * FROM SAMPLE where rownum < 2");
		}
	}
}
