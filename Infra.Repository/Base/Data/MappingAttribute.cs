using System;

namespace Infra.Repository.Base
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	[Serializable]
	public sealed class MappingAttribute : System.Attribute
	{
		public string ColumnName { get; set; }

		public string PropertyName { get; set; }

		public bool IsClass { get; set; }
	}
}
