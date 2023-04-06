using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace Infra.Repository.Base.Data
{
	public abstract class MySqlBaseRepository<T> where T : class
	{
		private readonly ILogger _logger;
		private readonly OracleBase _oracleBase;

		public MySqlBaseRepository(ILogger logger)
		{
			_oracleBase = new OracleBase(logger);
		}
		protected async Task<IEnumerable<T>> ExecuteQueryAsync(string connectionString, string query, object parameters = null)
		{
			using MySqlConnection connection = new MySqlConnection(connectionString);
			using MySqlCommand command = new MySqlCommand(query, connection);
			await connection.OpenAsync().ConfigureAwait(false);

			if (parameters != null)
			{
				foreach (var prop in parameters.GetType().GetProperties())
				{
					command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters, null));
				}
			}

			using var reader = await command.ExecuteReaderAsync().ConfigureAwait(false);
			var results = new List<T>();

			while (await reader.ReadAsync().ConfigureAwait(false))
			{
				var result = Activator.CreateInstance<T>();

				foreach (var prop in result.GetType().GetProperties())
				{
					if (prop.PropertyType == typeof(int))
					{
						prop.SetValue(result, Convert.ToInt32(reader[prop.Name]));
					}
					else if (prop.PropertyType == typeof(decimal))
					{
						prop.SetValue(result, Convert.ToDecimal(reader[prop.Name]));
					}
					else
					{
						prop.SetValue(result, reader[prop.Name]);
					}
				}

				results.Add(result);
			}

			return results;
		}

		protected List<T> ExecuteQuery<T>(string connectionString, string query, object parameters = null)
		{
			List<T> results = new List<T>();

			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();

				using (var command = new MySqlCommand(query, connection))
				{
					if (parameters != null)
					{

						foreach (var prop in parameters.GetType().GetProperties())
						{
							command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters, null));
						}
					}

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							T item = Activator.CreateInstance<T>();
							foreach (var prop in typeof(T).GetProperties())
							{
								if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
								{
									prop.SetValue(item, reader.GetValue(reader.GetOrdinal(prop.Name)), null);
								}
							}
							results.Add(item);
						}
					}
				}
			}

			return results;
		}

		protected int ExecuteUpdateQuery(string connectionString, string query, object parameters = null)
		{
			int rowsAffected = 0;

			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();

				using (var command = new MySqlCommand(query, connection))
				{
					if (parameters != null)
					{
						foreach (var prop in parameters.GetType().GetProperties())
						{
							command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters));
						}
					}

					rowsAffected = command.ExecuteNonQuery();
				}
			}

			return rowsAffected;
		}

		protected int ExecuteInsertQuery(string connectionString, string query, object parameters = null)
		{
			int lastInsertedId = 0;

			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();

				using (var command = new MySqlCommand(query, connection))
				{
					if (parameters != null)
					{
						foreach (var prop in parameters.GetType().GetProperties())
						{
							command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters));
						}
					}

					command.ExecuteNonQuery();

					// Recupera o ID da última linha inserida
					lastInsertedId = (int)command.LastInsertedId;
				}
			}

			return lastInsertedId;
		}

		protected async Task<int> ExecuteNonQueryAsync(string connectionString, string query, object parameters = null)
		{
			using MySqlConnection connection = new MySqlConnection(connectionString);
			using MySqlCommand command = new MySqlCommand(query, connection);
			await connection.OpenAsync().ConfigureAwait(false);

			if (parameters != null)
			{
				foreach (var prop in parameters.GetType().GetProperties())
				{
					command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters, null));
				}
			}

			return await command.ExecuteNonQueryAsync().ConfigureAwait(false);
		}

		public void LogDebug(string message)
		{
			var messageFormat = string.Format("{0} - {1}", DateTime.Now.ToString("dd-MM-yyy hh:mm:ss"), message);
			//_logger.LogDebug(messageFormat);
		}

		public void LogInformation(string message)
		{
			var messageFormat = string.Format("{0} - {1}", DateTime.Now.ToString("dd-MM-yyy hh:mm:ss"), message);
			//_logger.LogInformation(messageFormat);
		}

		public void LogWarning(string message)
		{
			var messageFormat = string.Format("{0} - {1}", DateTime.Now.ToString("dd-MM-yyy hh:mm:ss"), message);
			//_logger.LogWarning(messageFormat);
		}

		public void LogError(string message, Exception exception)
		{
			var messageFormat = string.Format("{0} - {1}", DateTime.Now.ToString("dd-MM-yyy hh:mm:ss"), message);
			//_logger.LogError(messageFormat, exception);
		}
	}
}
