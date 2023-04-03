using Domain.Common.Exception;
using Domain.Sample.Entity;
using Domain.Sample.Repository;

namespace Domain.Sample.Service
{
	public class ExampleDomainService
	{
		private readonly IExampleRepository _sampleRepository;
		public ExampleDomainService(IExampleRepository sampleRepository)
		{
			_sampleRepository = sampleRepository;
		}
		public void Create(Entity.Example example)
		{
			ValidateNullSample(example);
			ValidateDuplicitySample(example);
			_sampleRepository.Save(example);
		}

		public void Update(Entity.Example example)
		{
			ValidateFoundSample(example);
			_sampleRepository.Save(example);
		}

		public void Delete(Entity.Example example)
		{

			ValidateFoundSample(example);

			example.Inactivate();

			_sampleRepository.Save(example);
		}

		private void ValidateDuplicitySample(Entity.Example example)
		{
			var exampleDomain = _sampleRepository.GetByName(example.Name);

			if (exampleDomain != null)
				throw new BusinessException(Message.SKY_EXAMPLE_EXISTING);
		}

		public static void ValidateNullSample(Entity.Example example)
		{
			if (example == null)
				throw new NotFoundException(Message.SKY_EXAMPLE_NOT_FOUND);
		}

		public void ValidateFoundSample(Entity.Example example)
		{
			if (example == null || example.Id == 0)
				throw new BusinessException(Message.SKY_EXAMPLE_NOT_FOUND_SAMPLE);
		}
	}
}
