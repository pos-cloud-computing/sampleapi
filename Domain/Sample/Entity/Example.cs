using Domain.Common.Entity;
using Domain.Common.Exception;

namespace Domain.Sample.Entity
{
	public class Example : IEntity<long>
	{
		private readonly long _id;
		private bool _active;
		private string _name;
		public Example(string name)
		{
			ValidateName(name);
			_name = name;
			_active = true;
		}

		public Example(long id, string name, bool active)
		{
			ValidateId(id);
			ValidateName(name);

			_name = name;
			_id = id;
			_active = active;
		}

		public string Name { get { return _name; } }

		public void UpdateName(string name)
		{
			ValidateName(name);
			_name = name;
		}

		public long Id { get { return _id; } }

		public bool Active { get { return _active; } }


		public void ValidateId(long id)
		{
			if (id == 0)
				throw new NotFoundException(Common.Exception.Message.SKY_CORE_EMPTY_ID);
		}
		internal void Inactivate()
		{
			_active = false;
		}

		protected static void ValidateName(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new BusinessException(Message.SKY_EXAMPLE_EMPTY_NULL_NAME);

			if (name.Length > 30)
				throw new BusinessException(Message.SKY_EXAMPLE_MAX_LENGTH_30_NAME);
		}
	}
}
