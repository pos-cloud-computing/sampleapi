using System.Data;
using System.Reflection;

namespace Infra.Repository.Base
{
	public static class DataExtension
	{
		public static List<TEntity> Binding<TEntity>(this DataSet ds)
			where TEntity : new() => ds != null
					&& ds.Tables.Count > 0 &&
					ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows.Cast<DataRow>().Select<DataRow, TEntity>((Func<DataRow, TEntity>)(row => row.Binding<TEntity>())).ToList<TEntity>() : new List<TEntity>(0);

		public static TEntity Binding<TEntity>(this DataRow row) where TEntity : new()
		{
			TEntity entity = new TEntity();
			using (IEnumerator<DataColumn> enumerator = row.Table.Columns.Cast<DataColumn>().Where<DataColumn>((Func<DataColumn, bool>)(column => row[column.ColumnName] != DBNull.Value)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataColumn column = enumerator.Current;
					foreach (PropertyInfo property1 in entity.GetType().GetProperties())
					{
						if (property1.CanWrite)
						{
							MappingAttribute mappingAttribute = ((IEnumerable<MappingAttribute>)property1.GetCustomAttributes(typeof(MappingAttribute), false)).FirstOrDefault((Func<MappingAttribute, bool>)(attr => attr.ColumnName.ToLowerInvariant().Equals(column.ColumnName.ToLowerInvariant())));
							if (mappingAttribute != null)
							{
								if (mappingAttribute.IsClass)
								{
									string propertyName = mappingAttribute.PropertyName;
									if (property1.GetValue((object)entity, (object[])null) == null)
										property1.SetValue((object)entity, Activator.CreateInstance(Type.GetType(property1.PropertyType.AssemblyQualifiedName)), (object[])null);
									object entity2 = property1.GetValue((object)entity, (object[])null);
									PropertyInfo property2 = entity2.GetType().GetProperty(propertyName);
									if (DataExtension.SetProperty(row, column, property2, entity2))
										break;
								}
								else if (DataExtension.SetProperty(row, column, property1, (object)entity))
									break;
							}
						}
					}
				}
			}
			return entity;
		}
		public static T GetValue<T>(this DataRow row, string column) where T : struct => row.Table.Columns.Contains(column) && row[column] != DBNull.Value ? (T)Convert.ChangeType(row[column], typeof(T)) : default(T);

		public static string GetString(this DataRow row, string column) => row.Table.Columns.Contains(column) && row[column] != DBNull.Value ? row[column].ToString() : string.Empty;

		private static bool SetProperty(DataRow row, DataColumn column, PropertyInfo propertyInfo, object entity)
		{
			if (propertyInfo == (PropertyInfo)null)
				return false;
			object obj = !propertyInfo.PropertyType.IsEnum ? (!(Nullable.GetUnderlyingType(propertyInfo.PropertyType) != (Type)null) ? Convert.ChangeType(row[column.ColumnName], Type.GetType(propertyInfo.PropertyType.AssemblyQualifiedName)) : (Nullable.GetUnderlyingType(propertyInfo.PropertyType).IsEnum ? Enum.Parse(Nullable.GetUnderlyingType(propertyInfo.PropertyType), row[column.ColumnName].ToString()) : Convert.ChangeType(row[column.ColumnName], Nullable.GetUnderlyingType(propertyInfo.PropertyType)))) : Enum.Parse(Type.GetType(propertyInfo.PropertyType.AssemblyQualifiedName), row[column.ColumnName].ToString());
			propertyInfo.SetValue(entity, obj, (object[])null);
			return true;
		}
	}
}
