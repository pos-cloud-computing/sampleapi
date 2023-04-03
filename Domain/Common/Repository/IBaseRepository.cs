using Domain.Common.Entity;

namespace Domain.Common.Repository
{
	public interface IBaseRepository
	{
		public void LogDebug(string message);

		public void LogInformation(string message);

		public void LogWarning(string message);

		public void LogError(string message, System.Exception exception);
	}
}
