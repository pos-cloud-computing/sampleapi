namespace Infra.Repository.Base.Data
{
	public static class Connection
	{
		private static string GetConnectionString(string connectionName)
		{
			var user = Environment.GetEnvironmentVariable(string.Format("{0}_DATABASE_USER", connectionName.ToUpper()));
			var pass = Environment.GetEnvironmentVariable(string.Format("{0}_DATABASE_PASSWORD", connectionName.ToUpper()));
			var cn = Environment.GetEnvironmentVariable(string.Format("{0}_DATABASE_CONNECTION", connectionName.ToUpper()));
			return string.Format(cn, user, pass);
		}

		public static string SampleDB
		{
			get
			{
				return GetConnectionString("SAMPLEDB");
			}
		}

	}
}
