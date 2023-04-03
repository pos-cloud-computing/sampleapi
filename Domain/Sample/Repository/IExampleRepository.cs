using Domain.Common.Repository;

namespace Domain.Sample.Repository
{
	public interface IExampleRepository : IBaseRepository
	{
		public void Save(Entity.Example example);

		public Entity.Example Get(long id);

		public Entity.Example GetByName(string name);
	}
}
