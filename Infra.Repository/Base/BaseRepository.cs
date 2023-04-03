using Infra.Repository.Base.Data;
using Microsoft.Extensions.Logging;

namespace Infra.Repository.Base
{
	public class BaseRepository
	{
		private readonly ILogger _logger;
		private readonly OracleBase _oracleBase;

		public BaseRepository(ILogger logger)
		{
			_logger = logger;
			_oracleBase = new OracleBase(logger);
		}
		public OracleBase OracleBase { get { return _oracleBase; } }

		public void LogDebug(string message)
		{
			var messageFormat = string.Format("{0} - {1}", DateTime.Now.ToString("dd-MM-yyy hh:mm:ss"), message);
			_logger.LogDebug(messageFormat);
		}

		public void LogInformation(string message)
		{
			var messageFormat = string.Format("{0} - {1}", DateTime.Now.ToString("dd-MM-yyy hh:mm:ss"), message);
			_logger.LogInformation(messageFormat);
		}

		public void LogWarning(string message)
		{
			var messageFormat = string.Format("{0} - {1}", DateTime.Now.ToString("dd-MM-yyy hh:mm:ss"), message);
			_logger.LogWarning(messageFormat);
		}

		public void LogError(string message, Exception exception)
		{
			var messageFormat = string.Format("{0} - {1}", DateTime.Now.ToString("dd-MM-yyy hh:mm:ss"), message);
			_logger.LogError(messageFormat, exception);
		}
	}
}
