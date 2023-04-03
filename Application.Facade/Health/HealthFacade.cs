using Domain.Health.Repository;

namespace Application.Facade.Health
{
	public class HealthFacade
	{
		private readonly IHealthRepository _healthRepository;
		public HealthFacade(IHealthRepository healthRepository)
		{
			_healthRepository = healthRepository;
			_healthRepository.LogDebug("HealthFacade new");
		}

		public void IsReady()
		{
			_healthRepository.LogDebug("IsReady Start");
			_healthRepository.IsReady();
			_healthRepository.LogDebug("IsReady End");
		}
	}
}
