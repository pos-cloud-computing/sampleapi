using Application.DTO.Sample.Adapter;
using Domain.Sample.Repository;
using Domain.Sample.Service;

namespace Application.Facade.Sample
{
	public class ExampleFacade
	{
		private readonly IExampleRepository _exampleRepository;
		private readonly ExampleAdapter _exampleAdapter;
		private readonly ExampleDomainService _exampleDomainService;

		public ExampleFacade(IExampleRepository exampleRepository)
		{
			_exampleRepository = exampleRepository;
			_exampleAdapter = new ExampleAdapter();
			_exampleDomainService = new ExampleDomainService(_exampleRepository);
		}

		public void Create(DTO.Sample.Example example)
		{
			var exampleDomain = _exampleAdapter.Parse(example);
			_exampleDomainService.Create(exampleDomain);
		}

		public DTO.Sample.Example Get(long id)
		{
			var exampleDomain = _exampleRepository.Get(id);
			ExampleDomainService.ValidateNullSample(exampleDomain);

			return _exampleAdapter.Parse(exampleDomain);
		}

		public void Update(DTO.Sample.Example example)
		{
			var exampleDomain = _exampleRepository.Get(example.Id);

			ExampleDomainService.ValidateNullSample(exampleDomain);

			exampleDomain.UpdateName(example.Name);

			_exampleDomainService.Update(exampleDomain);
		}

		public void Patch(long id, DTO.Sample.Example example)
		{
			var exampleDomain = _exampleRepository.Get(id);

			ExampleDomainService.ValidateNullSample(exampleDomain);

			exampleDomain.UpdateName(example.Name);

			_exampleDomainService.Update(exampleDomain);
		}

		public void Inactivate(long id)
		{
			var exampleDomain = _exampleRepository.Get(id);

			ExampleDomainService.ValidateNullSample(exampleDomain);

			_exampleDomainService.Delete(exampleDomain);
		}
	}
}
