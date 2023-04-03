using Application.DTO.Base;
using Infra.Repository.Sample.POCO;

namespace Infra.Repository.Sample.Adapter
{
	internal class ExampleAdpter : IParser<ExampleOCO, Domain.Sample.Entity.Example>
	{
		public Domain.Sample.Entity.Example Parse(ExampleOCO origin)
		{
			if (origin == null
				|| (origin.Id == 0 && string.IsNullOrEmpty(origin.Name)))
				return null;

			var exampleDomain = new Domain.Sample.Entity.Example(origin.Id, origin.Name, origin.Active);

			return exampleDomain;
		}

		public List<Domain.Sample.Entity.Example> Parse(List<ExampleOCO> origin)
		{
			var messagesDomain = new List<Domain.Sample.Entity.Example>();
			foreach (var item in origin)
				messagesDomain.Add(Parse(item));

			return messagesDomain;

		}

	}
}