using Application.DTO.Base;

namespace Application.DTO.Sample.Adapter
{
	public class ExampleAdapter : IParser<Example, Domain.Sample.Entity.Example>, IParser<Domain.Sample.Entity.Example, Example>
	{
		public Domain.Sample.Entity.Example Parse(Example origin)
		{
			if (origin == null) return null;

			var exampleDomain = new Domain.Sample.Entity.Example(origin.Name);

			return exampleDomain;
		}

		public List<Domain.Sample.Entity.Example> Parse(List<Example> origin)
		{
			if (origin == null) return null;

			var examplesDomain = new List<Domain.Sample.Entity.Example>();

			if (origin == null) return null;

			foreach (var item in origin)
				examplesDomain.Add(Parse(item));

			return examplesDomain;
		}

		public Example Parse(Domain.Sample.Entity.Example origin)
		{
			var exampleDTO = new Example();

			if (origin == null) return null;

			exampleDTO.Id = origin.Id;
			exampleDTO.Name = origin.Name;
			exampleDTO.Active = origin.Active;
			return exampleDTO;
		}

		public List<Example> Parse(List<Domain.Sample.Entity.Example> origin)
		{
			if (origin == null) return null;

			var examplesDTO = new List<Example>();

			foreach (var item in origin)
				examplesDTO.Add(Parse(item));

			return examplesDTO;
		}
	}
}
