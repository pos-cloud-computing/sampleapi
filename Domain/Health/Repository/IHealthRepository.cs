using Domain.Common.Repository;

namespace Domain.Health.Repository
{
	public interface IHealthRepository : IBaseRepository
	{
		void IsReady();
	}
}
