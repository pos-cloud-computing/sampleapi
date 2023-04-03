using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Infra.Repository.Base.Data
{
	public class OracleBase
	{
		private readonly ILogger _logger;
		public OracleBase(ILogger logger)
		{
			_logger = logger;
		}

		internal T NextValueSequence<T>(string connectionString, string sequenceName) where T : struct
		{
			var sql = string.Format("select {0}.nextval from dual", sequenceName);

			return this.ExecuteScalar<T>(connectionString, sql);
		}

		internal T ExecuteScalar<T>(string connectionString, string sql, Dictionary<string, object> parameters = null, CommandType type = CommandType.Text) where T : struct
		{
			_logger.LogDebug(string.Format("ExecuteScalar for {0} sql '{1}'.", typeof(T), sql));
			object obj;

			using (OracleConnection conn = new OracleConnection(connectionString))
			{
				conn.Open();
				obj = ExecScalar(sql, parameters, type, conn);
				conn.Close();
			}

			_logger.LogDebug(string.Format("ExecuteScalar for {0} sql '{1}'.", typeof(T), sql), obj);

			return obj != DBNull.Value && obj != null ? (T)Convert.ChangeType(obj, typeof(T)) : default(T);
		}

		internal List<T> ExecuteCommand<T>(string connectionString, string sql, Dictionary<string, object> parameters = null, CommandType type = CommandType.Text) where T : new()
		{
			_logger.LogDebug(string.Format("ExecuteSingleExecuteSingle for {0} sql '{1}'.", typeof(T), sql));
			return ExecuteDataSet(sql, parameters, type, connectionString).Binding<T>();
		}

		internal T ExecuteSingle<T>(string connectionString, string sql, Dictionary<string, object> parameters = null, CommandType type = CommandType.Text) where T : new()
		{

			DataSet dataSet = ExecuteDataSet(sql, parameters, type, connectionString);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				_logger.LogDebug(string.Format("ExecuteSingleExecuteSingle for {0} sql '{1}'.", typeof(T), sql));
				var objetPoco = dataSet.Tables[0].Rows[0].Binding<T>();

				return objetPoco;
			}

			return new T();
		}

		internal int ExecuteNoQuery(string sql, Dictionary<string, object> parameters = null, CommandType type = CommandType.Text, string connectionString = null)
		{
			int num;

			using (OracleConnection conn = new OracleConnection(connectionString))
			{
				conn.Open();
				num = ExecNoQuery(sql, parameters, type, conn);
				conn.Close();
			}

			_logger.LogDebug(string.Format("ExecuteNoQuery  num '{0}'.", num));
			return num;
		}

		private object ExecScalar(string sql, Dictionary<string, object> parameters, CommandType type, OracleConnection conn)
		{
			OracleCommand oracleCommand = new OracleCommand(sql, conn);
			oracleCommand.CommandType = type;
			using (OracleCommand command = oracleCommand)
			{
				command.BindByName = true;
				if (parameters != null)
					BindingParameters(command, parameters);
				object obj = command.ExecuteScalar();
				return obj;
			}
		}

		private int ExecNoQuery(string sql, Dictionary<string, object> parameters, CommandType type, OracleConnection conn)
		{

			OracleCommand oracleCommand = new OracleCommand(sql, conn);
			oracleCommand.CommandType = type;
			using (OracleCommand command = oracleCommand)
			{
				command.BindByName = true;
				if (parameters != null)
					BindingParameters(command, parameters);
				int num = command.ExecuteNonQuery();
				return num;
			}
		}

		private DataSet ExecuteDataSet(string sql, Dictionary<string, object> parameters = null, CommandType type = CommandType.Text, string connection = null)
		{
			using (OracleConnection conn = new OracleConnection(connection))
			{
				conn.Open();
				using (DataSet dataSet = ExecDataSet(sql, parameters, type, conn))
				{
					conn.Close();
					return dataSet;
				}
			}
		}

		private DataSet ExecDataSet(string sql, Dictionary<string, object> parameters, CommandType type, OracleConnection conn)
		{
			var oracleCommand = new OracleCommand(sql, conn);
			oracleCommand.CommandType = type;
			using (OracleCommand oracleCommand2 = oracleCommand)
			{
				oracleCommand2.BindByName = true;
				if (parameters != null)
					BindingParameters(oracleCommand2, parameters);
				else
					_logger.LogDebug(string.Format("Without BindingParameters - ", oracleCommand2.CommandText));

				using (OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand2))
				{
					using (DataSet dataSet = new DataSet())
					{
						oracleDataAdapter.Fill(dataSet);
						return dataSet;
					}
				}
			}
		}

		private void BindingParameters(OracleCommand command, Dictionary<string, object> parameters)
		{
			var commandText = command.CommandText;
			foreach (KeyValuePair<string, object> parameter in parameters)
			{
				string[] strArray = parameter.Key.Split(' ');
				if (strArray.Length > 1)
				{
					if (strArray[0].Equals("out"))
					{
						command.Parameters.Add(parameter.Key, OracleDbType.Int64, parameter.Value, ParameterDirection.Output);
					}
				}
				else
				{
					command.Parameters.Add(parameter.Key, parameter.Value);
				}
			}
			_logger.LogDebug(string.Format("BindingParameters - {0}", commandText));
		}
	}
}
